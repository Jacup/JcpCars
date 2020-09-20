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
        //StoreContext db = new StoreContext();

        //public MessageController()
        //{
        //}

        //public MessageController(StoreContext context)
        //{
        //    this.db = context;
        //}

        //public MessageController(ApplicationUserManager userManager)
        //{
        //    UserManager = userManager;
        //}

        //public enum ManageMessageId
        //{
        //    ChangePasswordSuccess,
        //    SetPasswordSuccess,
        //    RemoveLoginSuccess,
        //    LinkSuccess,
        //    Error
        //}

        //private ApplicationUserManager _userManager;
        //public ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //    private set
        //    {
        //        _userManager = value;
        //    }
        //}

        //private IAuthenticationManager AuthenticationManager
        //{
        //    get
        //    {
        //        return HttpContext.GetOwinContext().Authentication;
        //    }
        //}




        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Message message)
        {

            int messageId = message.MessageId;
            //string toUserId = message.ToUserId;
            //string fromUserId = message.FromUserId;
            //string content = message.Content;

            return View();
        }
















        //[Authorize]
        //public ActionResult AddProduct(int? carId, bool? confirmSuccess)
        //{
        //    if (carId.HasValue)
        //        ViewBag.EditMode = true;
        //    else
        //        ViewBag.EditMode = false;

        //    var result = new EditProductViewModel();
        //    var series = db.Series.ToArray();
        //    result.Series = series;
        //    result.ConfirmSuccess = confirmSuccess;

        //    var user = UserManager.FindById(User.Identity.GetUserId());

        //    Car a;

        //    if (!carId.HasValue)
        //    {
        //        a = new Car();
        //    }
        //    else
        //    {
        //        a = db.Cars.Find(carId);
        //    }

        //    result.Car = a;

        //    return View(result);
        //}

        //[HttpPost]
        //public ActionResult AddProduct(HttpPostedFileBase file, EditProductViewModel model)
        //{
        //    if (model.Car.CarId > 0)
        //    {

        //        db.Entry(model.Car).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("AddProduct", new { confirmSuccess = true });
        //    }
        //    else
        //    {
        //        // Creating new entry

        //        var f = Request.Form;
        //        // Verify that the user selected a file
        //        if (file != null && file.ContentLength > 0 && ModelState.IsValid)
        //        {
        //            // Generate filename

        //            var fileExt = Path.GetExtension(file.FileName);
        //            var filename = Guid.NewGuid() + fileExt;

        //            var path = Path.Combine(Server.MapPath(AppConfig.CarFolderRelative), filename);
        //            file.SaveAs(path);

        //            // Save info to DB
        //            model.Car.PictureFileName = filename;
        //            model.Car.DateAdded = DateTime.Now;

        //            var userId = User.Identity.GetUserId();
        //            model.Car.UserId = userId;

        //            db.Entry(model.Car).State = EntityState.Added;
        //            db.SaveChanges();

        //            return RedirectToAction("AddProduct", new { confirmSuccess = true });
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Sprawdź poprawność formularza.");
        //            var series = db.Series.ToArray();
        //            model.Series = series;
        //            return View(model);
        //        }
        //    }

        //}

    }
}