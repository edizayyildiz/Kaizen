using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Kaizen.Web.Models;
using Kaizen.Service;
using System.IO;
using AutoMapper;
using Kaizen.Model;

namespace Kaizen.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class ManageController : ControllerBase
    {
        private ApplicationSignInManager _signInManager;
        private readonly ISuggestionService suggestionService;
        private readonly IDepartmentService departmentService;

        public ManageController(ApplicationUserManager userManager, IEmployeeService employeeService, ISuggestionService suggestionService, IDepartmentService departmentService, ApplicationRoleManager roleManager) : base(userManager, employeeService, roleManager)
        {
            this.suggestionService = suggestionService;
            this.departmentService = departmentService;
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IEmployeeService employeeService, ISuggestionService suggestionService, IDepartmentService departmentService, ApplicationRoleManager roleManager) : base(userManager, employeeService, roleManager)
        {
            this.userManager = userManager;
            SignInManager = signInManager;
            this.employeeService = employeeService;
            this.suggestionService = suggestionService;
            this.departmentService = departmentService;
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

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            var userName = User.Identity.Name;
            var currentUser = userManager.FindByNameAsync(userName).Result; // result async i senkrona çeviriyor
            var employee = employeeService.GetAll().FirstOrDefault(f => f.Email == currentUser.Email);
            var suggestions = suggestionService.GetAll(f => f.EmployeeId == employee.Id).OrderByDescending(w => w.UpdatedAt);
            var departments = employee.Departments.ToList().Select(s => s.Name);
            ViewBag.Suggestions = suggestions;
            ViewBag.Position = employee.Position;
            ViewBag.Departments = departments;
            ViewBag.Company = employee.Company.Name;


            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await userManager.GetPhoneNumberAsync(userId),
                TwoFactor = await userManager.GetTwoFactorEnabledAsync(userId),
                Logins = await userManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult UploadPhoto(HttpPostedFileBase file)
        {
            var userName = User.Identity.Name;
            var user = userManager.FindByNameAsync(userName).Result;
            var employee = employeeService.GetAll().FirstOrDefault(f => f.Email == user.Email);
            if (file != null && file.ContentLength > 0) // byte üzerinden kontrol et
            {
                if (file.ContentLength <= 3145728) // Dosya boyutu 3 MB dan büyük olamaz
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string extension = Path.GetExtension(fileName).ToLower();
                    if (extension == ".jpeg" || extension == ".jpg" || extension == ".png" || extension == ".gif")
                    {
                        string path = Path.Combine(Server.MapPath("~/Uploads/ProfilePhotos"), fileName);
                        if (employee.Photo != null) // Eğer profil fotoğrafı var ise siler
                        {
                            string oldPhotoPath = Path.Combine(Server.MapPath("~/Uploads/ProfilePhotos"), employee.Photo);
                            System.IO.File.Delete(oldPhotoPath);
                        }
                        file.SaveAs(path); // Yeni fotoğrafı kaydeder
                        employee.Photo = fileName;
                        employeeService.Update(employee);
                        return Json("/Uploads/ProfilePhotos/" + fileName);
                    }
                    else
                    {
                        return Json(new { message = "Dosya uzantısı .jpeg, .jpg, .png veya .gif olmalıdır." });
                    }
                }
                else
                {
                    return Json(new { message = "Dosya boyutu 3 MB'dan büyük olamaz." });
                }
            }
            else
            {
                return Json(new { message = "Lütfen bir fotoğraf seçiniz." });
            }
        }
        [HttpPost]
        public ActionResult UpdatePersonal(string FirstName, string LastName, string Position, string UserName)
        {
            if (FirstName != "" && LastName != "" && Position != "" && UserName != "")
            {
                var currentUserMail = User.Identity.Name;
                var user = userManager.FindByName(currentUserMail);
                var employee = employeeService.GetAll().FirstOrDefault(f => f.Email == currentUserMail);
                var employeeModel = Mapper.Map<EmployeeViewModel>(employee);

                employeeModel.FirstName = FirstName;
                employeeModel.LastName = LastName;
                user.FirstName = FirstName;
                user.LastName = LastName;
                employeeModel.UserName = UserName;
                employeeModel.Position = Position;

                var employeeToUpdate = Mapper.Map<Employee>(employeeModel);
                this.employeeService.Update(employeeToUpdate);
                return Json(true);
            }
            else
            {
                return Json(new { message = "Lütfen eksik alanları doldurunuz." });
            }
        }
        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await userManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generate the token and send it
            var code = await userManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (userManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };
                await userManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await userManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await userManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await userManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await userManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }

        //
        // POST: /Manage/RemovePhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await userManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await userManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
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
                var result = await userManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
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
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await userManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new Kaizen.Web.Controllers.AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await userManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && userManager != null)
            {
                userManager.Dispose();
                userManager = null;
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

        private bool HasPassword()
        {
            var user = userManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = userManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion
    }
}