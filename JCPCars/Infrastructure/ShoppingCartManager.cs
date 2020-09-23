using JCPCars.DAL;
using JCPCars.Infrastructure;
using JCPCars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JCPCars.Infrastructure
{
    public class ShoppingCartManager
    {
        public const string CartSessionKey = "CartData";

        private StoreContext db;

        private ISessionManager session;

        public ShoppingCartManager(ISessionManager session, StoreContext db)
        {
            this.session = session;
            this.db = db;
        }

        public void AddToCart(int carid)
        {
            var cart = this.GetCart();

            var cartItem = cart.Find(c => c.Car.CarId == carid);

            if (cartItem != null)
                cartItem.Quantity++;
            else
            {
                // Find car and add it to cart
                var carToAdd = db.Cars.Where(a => a.CarId == carid).SingleOrDefault();
                if (carToAdd != null)
                {
                    var newCartItem = new CartItem()
                    {
                        Car = carToAdd,
                        Quantity = 1,
                        TotalPrice = carToAdd.PricePer1h
                    };

                    cart.Add(newCartItem);
                }
            }

            session.Set(CartSessionKey, cart);
        }

        public int RemoveFromCart(int carid)
        {
            var cart = this.GetCart();

            var cartItem = cart.Find(c => c.Car.CarId == carid);

            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                    return cartItem.Quantity;
                }
                else
                    cart.Remove(cartItem);
            }

            // Return count of removed item currently inside cart
            return 0;
        }


        public List<CartItem> GetCart()
        {
            List<CartItem> cart;

            if (session.Get<List<CartItem>>(CartSessionKey) == null)
            {
                cart = new List<CartItem>();
            }
            else
            {
                cart = session.Get<List<CartItem>>(CartSessionKey) as List<CartItem>;
            }

            return cart;
        }

        public decimal GetCartTotalPrice()
        {
            var cart = this.GetCart();
            return cart.Sum(c => (c.Quantity * c.Car.PricePer1h));
        }

        public int GetCartItemsCount()
        {
            var cart = this.GetCart();
            int count = cart.Sum(c => c.Quantity);

            return count;
        }

        public Order CreateOrder(Order newOrder, string userId)
        {
            var cart = this.GetCart();

            newOrder.DateCreated = DateTime.Now;
            newOrder.UserId = userId;

            this.db.Orders.Add(newOrder);

            if (newOrder.OrderItems == null)
                newOrder.OrderItems = new List<OrderItem>();

            decimal cartTotal = 0;

            foreach (var cartItem in cart)
            {
                var newOrderItem = new OrderItem()
                {
                    CarId = cartItem.Car.CarId,
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.Car.PricePer1h
                };

                cartTotal += (cartItem.Quantity * cartItem.Car.PricePer1h);

                newOrder.OrderItems.Add(newOrderItem);
            }

            newOrder.TotalPrice = cartTotal;

            this.db.SaveChanges();

            return newOrder;
        }

        public void EmptyCart()
        {
            session.Set<List<CartItem>>(CartSessionKey, null);
        }

    }
}