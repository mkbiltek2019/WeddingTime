using AIT.WebUIComponent.Models.Account;
using AIT.WebUIComponent.Models.Emails;
using AIT.WebUIComponent.Services.Emails;
using AIT.WebUIComponent.Services.Emails.Enum;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AIT.WebUIComponent.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationSignInManager _signInManager;
        private readonly ApplicationUserManager _userManager;        
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IEmailService _emailService;

        public AccountController(ApplicationSignInManager signInManager,
            ApplicationUserManager userManager,            
            IAuthenticationManager authenticationManager,
            IEmailService emailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _authenticationManager = authenticationManager;
            _emailService = emailService;
        }
        
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AutoLogin(AutoLoginModel model)
        {
            if (!ModelState.IsValid || !model.AutoLoginEnabled)
            {
                return View("Error");
            }

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user != null && !user.EmailConfirmed)
            {
                return View("Error");
            }
            
            await _signInManager.SignInAsync(user, false, false);
            await UpdateLastLoginDate(user);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Login(SignInModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { IsValid = false, ErrorMsg = "Nieprawidłowa nazwa użytkownika lub hasło." });
            }

            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null)
            {
                if (!user.EmailConfirmed)
                {
                    var callbackUrl = await ConfirmEmailCallbackUrl(user);
                    await SendEmailAsync(EmailType.ConfirmAccount, user, callbackUrl);
                    return Json(new { IsValid = false, ErrorMsg = "Musisz potwierdzić e-mail, żeby móc zalogować się do serwisu. Link aktywacyjny został ponownie wysłany." });
                }
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    await UpdateLastLoginDate(user);
                    string redirectUrl = Url.Action("Index", string.IsNullOrEmpty(returnUrl) ? "Home" : returnUrl.Remove(0, 1), null, Request.Url.Scheme);
                    return Json(new { IsValid = true, Url = redirectUrl });               
                case SignInStatus.Failure:
                default:
                    return Json(new { IsValid = false, ErrorMsg = "Nieprawidłowa nazwa użytkownika lub hasło." });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { IsValid = false, ErrorMsgs = new[] { "Wystąpił błąd podczas rejestracji." } });
            }

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                CreationDate = DateTime.Today,
                AccountState = AccountState.RequiresConfirmation,
                AutoLoginEnabled = true
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {                
                var callbackUrl = await ConfirmEmailCallbackUrl(user);
                await SendEmailAsync(EmailType.ConfirmAccount, user, callbackUrl);

                string redirectUrl = Url.Action("Confirm", "Account", null, Request.Url.Scheme);
                return Json(new { IsValid = true, Url = redirectUrl });
            }
            
            return Json(new { IsValid = false, ErrorMsgs = result.Errors });
        }

        [AllowAnonymous]
        public ActionResult ConfirmNewEmail()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Confirm()
        {           
            return View();
        }

        [AllowAnonymous]
        public ActionResult EmailAlreadyConfirmed()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user != null && user.EmailConfirmed)
            {
                return View("EmailAlreadyConfirmed");
            }
            var result = await _userManager.ConfirmEmailAsync(userId, code);

            var autoLoginModel = new AutoLoginModel();
            ViewBag.StatusMessage = user.AccountState == AccountState.RequiresConfirmation ? "Witamy wsród zamiłowanych użytkowników!" :
                user.AccountState == AccountState.EmailChanged ? "Zmiana adresu e-mail została właśnie potwierdzona." : string.Empty;

            if (user.AutoLoginEnabled && user.AccountState != AccountState.Confirmed)
            {
                autoLoginModel.AutoLoginEnabled = true;
                autoLoginModel.UserId = userId;
            }
            
            user.AccountState = AccountState.Confirmed;
            user.AutoLoginEnabled = false;
            await _userManager.UpdateAsync(user);

            return result.Succeeded ? View("EmailConfirmation", autoLoginModel) : View("Error");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await _authenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login", new { returnUrl = returnUrl });
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await _signInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    var user = await _userManager.FindByEmailAsync(loginInfo.Email);
                    await UpdateLastLoginDate(user);
                    return RedirectToLocal(returnUrl);               
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Username = loginInfo.Email, LoginProvider = loginInfo.Login.LoginProvider });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await _authenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Username, Email = info.Email, EmailConfirmed = true };
                var result = await _userManager.CreateAsync(user);

                user.CreationDate = DateTime.Today;
                await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        await UpdateLastLoginDate(user);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !user.EmailConfirmed)
                {
                    return View("ForgotPasswordConfirmation");                              // don't reveal that the user does not exist or is not confirmed
                }

                string code = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, Request.Url.Scheme);
                
                await SendEmailAsync(EmailType.ResetPassword, user, callbackUrl);
                
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            return View(model);                                                             // if we got this far, something failed, redisplay form
        }

        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string userId, string code)  
        {
            return userId == null || code == null ? View("Error") : View();            
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await _userManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        #region Helpers

        private async Task<string> ConfirmEmailCallbackUrl(ApplicationUser user)
        {
            string code = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);
            return Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, Request.Url.Scheme);
        }

        private Task SendEmailAsync(EmailType type, ApplicationUser user, string callbackUrl)
        {
            var emailModel = new SystemMessage { Name = user.UserName, CallbackUrl = callbackUrl };
            return _emailService.SendAsync(type, emailModel, user.Email);
        }

        private const string XsrfKey = "XsrfId";

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        
        private async Task UpdateLastLoginDate(ApplicationUser user)
        {
            user.LastLoginDate = DateTime.Today;
            await _userManager.UpdateAsync(user);
        }

        #endregion
    }
}
