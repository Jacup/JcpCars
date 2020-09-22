using JCPCars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Data.Entity;
using JCPCars.DAL;
using System.IO;
using JCPCars.Infrastructure;
using JCPCars.ViewModels;
using System.Net;
using Hangfire;

namespace JCPCars.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        StoreContext db = new StoreContext();

        public MessageController()
        {
        }

        public MessageController(StoreContext context)
        {
            this.db = context;
        }

        public MessageController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        private ApplicationUserManager _userManager;
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

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }



        public ActionResult List()
        {
            bool isAdmin = User.IsInRole("Admin");
            ViewBag.UserIsAdmin = isAdmin;

            IEnumerable<Message> userMessages;

            // For admin users - return all orders
            if (isAdmin)
            {
                userMessages = db.Messages;
            }
            else
            {
                var userId = User.Identity.GetUserId();
                userMessages = db.Messages.Where(o => o.ToUserId == userId);
            }

            return View(userMessages);
        }



        [Authorize]
        public ActionResult Index(int? id, bool? confirmSuccess)
        {
            if (id.HasValue)
                ViewBag.EditMode = true;
            else
                ViewBag.EditMode = false;

            Car car = db.Cars.Find(id);

            if (car == null)
            {
                return HttpNotFound();
            }

            var result = new MessageViewModel();
            var cars = db.Cars.ToArray();
            result.Cars = cars;
            result.ConfirmSuccess = confirmSuccess;

            Message a;

            a = new Message();


            result.Message = a;

            return View(result);
        }

        [HttpPost]
        public ActionResult Index(int id, MessageViewModel model)
        {
            if (model.Message.MessageId > 0)
            {

                db.Entry(model.Message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { confirmSuccess = true });
            }
            else
            {
                // Creating new entry
                Car car = db.Cars.Find(id);

                var f = Request.Form;

                if (ModelState.IsValid)
                {
                    // Save info to DB
                    model.Message.Created = DateTime.Now;
                    model.Message.CarId = id;

                    var userId = User.Identity.GetUserId();
                    model.Message.UserId = userId;

                    model.Message.ToUserId = car.UserId;


                    db.Entry(model.Message).State = EntityState.Added;
                    db.SaveChanges();

                    return RedirectToAction("Index", new { confirmSuccess = true });
                }
                else
                {
                    ModelState.AddModelError("", "Sprawdź poprawność formularza.");
                    var cars = db.Cars.ToArray();
                    model.Cars = cars;
                    return View(model);
                }
            }

        }


        public ActionResult Delete(int? messageId)
        {
            if (messageId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(messageId);
            if (message == null)
            {
                return HttpNotFound();
            }

            return View(message);

        }

        // POST: Messages/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int messageId)
        {
            Message message = db.Messages.Find(messageId);
            db.Messages.Remove(message);
            db.SaveChanges();
            return RedirectToAction("List");
        }


    }
}