using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using JCPCars.Models;
using System.IO;
using JCPCars.DAL;
using System.Data.Entity;
using JCPCars.Infrastructure;
using System.Net;

namespace JCPCars.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        StoreContext db = new StoreContext();


        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
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

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";

            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                //HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };
            return View(model);
        }


        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }


        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }


        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }


        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            Error
        }


        [Authorize(Roles = "Admin")]
        public ActionResult AddProduct(int? carId, bool? confirmSuccess)
        {
            if (carId.HasValue)
                ViewBag.EditMode = true;
            else
                ViewBag.EditMode = false;

            var result = new EditProductViewModel();
            var series = db.Series.ToArray();
            result.Series = series;
            result.ConfirmSuccess = confirmSuccess;

            Car a;

            if (!carId.HasValue)
            {
                a = new Car();
            }
            else
            {
                a = db.Cars.Find(carId);
            }

            result.Car = a;

            return View(result);
        }

        [HttpPost]
        public ActionResult AddProduct(HttpPostedFileBase file, EditProductViewModel model)
        {
            if (model.Car.CarId > 0)
            {
                // Saving existing entry

                db.Entry(model.Car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AddProduct", new { confirmSuccess = true });
            }
            else
            {
                // Creating new entry

                var f = Request.Form;
                // Verify that the user selected a file
                if (file != null && file.ContentLength > 0 && ModelState.IsValid)
                {
                    // Generate filename

                    var fileExt = Path.GetExtension(file.FileName);
                    var filename = Guid.NewGuid() + fileExt;

                    var path = Path.Combine(Server.MapPath(AppConfig.CarFolderRelative), filename);
                    file.SaveAs(path);

                    // Save info to DB
                    model.Car.PictureFileName = filename;
                    model.Car.DateAdded = DateTime.Now;

                    db.Entry(model.Car).State = EntityState.Added;
                    db.SaveChanges();

                    return RedirectToAction("AddProduct", new { confirmSuccess = true });
                }
                else
                {
                    ModelState.AddModelError("", "Nie wskazano pliku.");
                    var series = db.Series.ToArray();
                    model.Series = series;
                    return View(model);
                }
            }

        }

        // GET: PersonalDetails/Delete
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? carId)
        {
            if (carId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(carId);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int carId)
        {
            Car car = db.Cars.Find(carId);
            db.Cars.Remove(car);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion
    }
}