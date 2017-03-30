using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using DATNQLBH.Models;
using DATNQLBH.Manager;
using DATNQLBH.Models.CSDL;
using System.Threading;
using System.Data.Entity;
using System.Transactions;
using DATNQLBH.Migrations;

namespace DATNQLBH.Controllers
{
    [Authorize(Roles ="SuperAdmin,Admin")]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private KiemTra kiemtra = new KiemTra();
        ShopEntities db= new ShopEntities();
        
        public AccountController()
        {
         
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
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
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
           
        
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
         
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    {
                        if (User.IsInRole("Staff"))
                        {
                            KiemTra kiemtra = new KiemTra();
                            var user = kiemtra.getUser(User.Identity.Name);
                            if (user.Active == false)
                                return View(model);
                        }
                        if (User.IsInRole("Admin"))
                        {
                            KiemTra kiemtra = new KiemTra();
                            var user = kiemtra.getUser(User.Identity.Name);
                            if (user.Active == false && user.MaCN.Split('-')[0].Equals("ChiNhanh"))
                                return View(model);
                            if(ShopEntities.CreateEntitiesForSpecificDatabaseName(user.MaCN).Database.CreateIfNotExists())
                            {
                                var chinhanh = db.ChiNhanhs.FirstOrDefault(x => x.MaCN.Equals(user.MaCN));
                                var cn = new ChiNhanh { Date = chinhanh.Date, DiaChi = chinhanh.DiaChi, Email = chinhanh.Email, FaceBook = chinhanh.FaceBook, Fax = chinhanh.Fax, Logo = chinhanh.Logo, MaCN = chinhanh.MaCN, Name = chinhanh.Name, SDT = chinhanh.SDT, SoTaiKhoan = chinhanh.SoTaiKhoan, WebSite = chinhanh.WebSite };
                                Configuration.ThemCuaHang(user, cn);
                            }
                        }
                        if (returnUrl != "")
                        {
                            return RedirectToLocal(returnUrl);
                        }
                        else { return RedirectToAction("Index", "Home"); }
                    }
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> KiemtraDangNhapWinform(string UserName, string Password)
        {
            var result = await SignInManager.PasswordSignInAsync(UserName, Password, false, shouldLockout: false);
            if(result==SignInStatus.Success)
            {
                var user = kiemtra.getUser(UserName);
                var cn = user.ChiNhanh;
                return Json(new { result=1, UserName= user.UserName,QuyenSuDung="",TenCN=cn.Name,DiaChi=cn.DiaChi,SDT=cn.SDT, Email=cn.Email});
            }
            return Json(new { result = 0 });
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register(int? madangky)
        {
           
            ViewBag.madangky = madangky.HasValue? madangky.Value:1;
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                int madangky = model.madangky.HasValue ? model.madangky.Value : 1;
                //Chi nhánh
                var chinhanh = new ChiNhanh { MaCN = "GianHang-" + model.UserName, Name = "GianHang-" + model.UserName, DiaChi = model.Address, Email = model.Email, SDT = model.SDT, Date = DateTime.Now };
                db.ChiNhanhs.Add(chinhanh);
               
                //Hợp đồng
                
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, Address = model.Address, Birthday = model.Birthday, FullName = model.FullName, PhoneNumber = model.SDT,  Active = true, Experience = 0, NgayDangKy = DateTime.Now };


                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    db.SaveChanges();
                    user.MaCN = chinhanh.MaCN;
                   
                    UserManager.AddToRole(user.Id, "Admin");
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    var cn = new ChiNhanh { MaCN = "GianHang-" + model.UserName, Name = "GianHang-" + model.UserName, DiaChi = model.Address, Email = model.Email, SDT = model.SDT, Date = DateTime.Now };
                    var user1 = new ApplicationUser { UserName = model.UserName, Email = model.Email, Address = model.Address, Birthday = model.Birthday, FullName = model.FullName, PhoneNumber = model.SDT, MaCN = "GianHang-" + model.UserName, Active = true, Experience = 0, NgayDangKy = DateTime.Now };
                    Configuration.ThemCuaHang(user1, cn);

                    var hopdong = new HopDongChiNhanh { MaCN = chinhanh.MaCN, MaLoaiGianHang = 1, BeginDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7) };
                    db.HopDongChiNhanhs.Add(hopdong);
                    db.SaveChanges();

                   
                 
                    if (madangky != 1)
                        DangKyBaoKim(madangky);
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);

                
                db.ChiNhanhs.Remove(chinhanh);
                db.SaveChanges();

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        public ActionResult DangKyBaoKimThanhCong(int madangky, string username)
        {
            var user = kiemtra.getUser(User.Identity.Name);
            if (username.Equals(user.UserName))
            {
                int LoaiGianHangId = madangky;
                var loai = db.LoaiGianHangs.Find(LoaiGianHangId);
                var hopdong = new HopDongChiNhanh { MaCN = "GianHang-" + username, MaLoaiGianHang = 1, BeginDate = DateTime.Now, EndDate = DateTime.Now.AddDays(30) };
                db.HopDongChiNhanhs.Add(hopdong);             
                db.SaveChanges();

            }
            return RedirectToAction("Index", "Home");
        }
        private void DangKyBaoKim(int madangky)
        {
            var user = kiemtra.getUser(User.Identity.Name);
            int LoaiGianHangId = madangky;
            var loai = db.LoaiGianHangs.Find(LoaiGianHangId);
            BaoKimPayment bk = new BaoKimPayment();
            string description = "Gói dịch vụ đăng ký, số tiền " + loai.Price.ToString("#,##0").Replace(",",".") + " đồng của chúng tôi";
            string url_success = "http://qlbh.azurewebsites.net/Account/DangKyBaoKimThanhCong?madangky="+ madangky+"&username="+user.UserName;
            string url_error = "http://qlbh.azurewebsites.net/Account/Error";
            var url = bk.createRequestUrl(user.UserName, "huy751574@ymail.com", loai.Price + "", description, url_success, url_error);
            Redirect(url);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
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
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "GiaoDien");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
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

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
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
        #endregion
    }
}