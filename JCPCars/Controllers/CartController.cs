using JCPCars.DAL;
using JCPCars.Models;
using JCPCars.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using JCPCars.Infrastructure;
using System.Web.Hosting;
using System.Net;
using Hangfire;


namespace JCPCars.Controllers
{
    public class CartController : Controller
    {

        private StoreContext db = new StoreContext();

        private ISessionManager sessionManager { get; set; }

        private ApplicationUserManager _userManager;

        public CartController(ISessionManager sessionManager)
        {
            this.sessionManager = sessionManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {
            ShoppingCartManager shoppingCartManager = new ShoppingCartManager(this.sessionManager, this.db);

            var cartItems = shoppingCartManager.GetCart();
            var cartTotalPrice = shoppingCartManager.GetCartTotalPrice();

            CartViewModel cartVM = new CartViewModel() { CartItems = cartItems, TotalPrice = cartTotalPrice };

            return View(cartVM);
        }

        public ActionResult AddToCart(int id)
        {
            ShoppingCartManager shoppingCart = new ShoppingCartManager(this.sessionManager, this.db);
            shoppingCart.AddToCart(id);



            return RedirectToAction("Index", "Cart");
        }


        public int GetCartItemsCount()
        {
            ShoppingCartManager shoppingCartManager = new ShoppingCartManager(this.sessionManager, this.db);
            return shoppingCartManager.GetCartItemsCount();
        }

        public ActionResult RemoveFromCart(int carID)
        {
            ShoppingCartManager shoppingCartManager = new ShoppingCartManager(this.sessionManager, this.db);

            int itemCount = shoppingCartManager.RemoveFromCart(carID);
            int cartItemsCount = shoppingCartManager.GetCartItemsCount();
            decimal cartTotal = shoppingCartManager.GetCartTotalPrice();

            // Return JSON to process it in JavaScript
            var result = new CartRemoveViewModel
            {
                RemoveItemId = carID,
                RemovedItemCount = itemCount,
                CartTotal = cartTotal,
                CartItemsCount = cartItemsCount
            };

            return Json(result);
        }

        public async Task<ActionResult> Checkout()
        {
            if (Request.IsAuthenticated)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                var order = new Order
                {
                    FirstName = user.UserData.FirstName,
                    LastName = user.UserData.LastName,
                    Address = user.UserData.Address,
                    Email = user.UserData.Email,
                    PhoneNumber = user.UserData.PhoneNumber
                };

                return View(order);
            }
            else
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Checkout", "Cart") });
        }

        [HttpPost]
        public async Task<ActionResult> Checkout(Order orderdetails)
        {
            if (ModelState.IsValid)
            {

                // Get user
                var userId = User.Identity.GetUserId();

                // Save Order
                ShoppingCartManager shoppingCartManager = new ShoppingCartManager(this.sessionManager, this.db);
                var newOrder = shoppingCartManager.CreateOrder(orderdetails, userId);

                // Update profile information
                var user = await UserManager.FindByIdAsync(userId);
                TryUpdateModel(user.UserData);
                await UserManager.UpdateAsync(user);

                // Empty cart
                shoppingCartManager.EmptyCart();

                var order = db.Orders.Include("OrderItems").Include("OrderItems.Car").SingleOrDefault(o => o.OrderId == newOrder.OrderId);

                return RedirectToAction("OrderConfirmation");
            }
            else
                return View(orderdetails);
        }

        public ActionResult OrderConfirmation()
        {
            return View();
        }


    }
};