//using JCPCars.Models;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.Owin;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace JCPCars.Controllers
//{
//    public class MessageController : Controller
//    {
//        public MessageController(ApplicationUserManager userManager)
//        {
//            UserManager = userManager;
//        }
//        private ApplicationUserManager _userManager;
//        public ApplicationUserManager UserManager
//        {
//            get
//            {
//                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
//            }
//            private set
//            {
//                _userManager = value;
//            }
//        }

//        public ActionResult Index()
//        {
//            return View();
//        }


//        [Authorize]
//        public ActionResult SendMessage()

//        {
//            var user = UserManager.FindById(User.Identity.GetUserId());
            
//            Message b;
//            b = new Message();
//            result.Message = b;

//            return View(result);
//        }


//        [HttpPost]
//        public ActionResult AddProduct(HttpPostedFileBase file, EditProductViewModel model)
//        {
//            if (model.Messsage.M > 0)
//            {
//                // Saving existing entry

//                db.Entry(model.Car).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("AddProduct", new { confirmSuccess = true });
//            }
//            else
//            {
//                // Creating new entry

//                var f = Request.Form;
//                // Verify that the user selected a file
//                if (file != null && file.ContentLength > 0 && ModelState.IsValid)
//                {
//                    // Generate filename

//                    var fileExt = Path.GetExtension(file.FileName);
//                    var filename = Guid.NewGuid() + fileExt;

//                    var path = Path.Combine(Server.MapPath(AppConfig.CarFolderRelative), filename);
//                    file.SaveAs(path);

//                    // Save info to DB
//                    model.Car.PictureFileName = filename;
//                    model.Car.DateAdded = DateTime.Now;

//                    var userId = User.Identity.GetUserId();
//                    model.Car.UserId = userId;

//                    db.Entry(model.Car).State = EntityState.Added;
//                    db.SaveChanges();

//                    return RedirectToAction("AddProduct", new { confirmSuccess = true });
//                }
//                else
//                {
//                    ModelState.AddModelError("", "Sprawdź poprawność formularza.");
//                    var series = db.Series.ToArray();
//                    model.Series = series;
//                    return View(model);
//                }
//            }

//        }


//    }
//}