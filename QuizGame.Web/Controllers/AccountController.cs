using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using QuizGame.Data;
using QuizGame.Dto;
using QuizGame.Service;
using QuizGame.Web.Code.LIBS;


namespace QuizGame.Web.Controllers
{
    [AllowAnonymous()]
    public class AccountController : BaseController
    {
        #region "Fields"
        private ILoginService loginService;
        private IRoleService roleService;
        ActivityLogDto activityLogModel;
        #endregion

        #region "Constructor"
        public AccountController(ILoginService _userloginService, IRoleService _userroleService, IActivityLogService _activityLogService) : base(_activityLogService, _userroleService)
        {
            this.loginService = _userloginService;
            this.roleService = _userroleService;
            this.activityLogModel = new ActivityLogDto();
        }
        #endregion
        
        #region "Action"
        [HttpGet]
        public ActionResult Index()
        {
            string pass = EncryptDecrypt.Decrypt("/m9EWAcLYNI=");
            try
            {
                activityLogModel.ActivityName = "LOGIN VISIT";
                activityLogModel.ActivityPage = "Get:Account/Index";
                activityLogModel.Remark = "";
                activityLogModel.UserId = CurrentUser?.UserID ?? 0;
                LogActivity(activityLogModel);
            }
            catch (Exception e)
            {
                LogException(e);
            }
            return View(new LoginDto());
        }
        [HttpPost]
        public ActionResult Index(LoginDto model)
        {
            try
            {
                activityLogModel.ActivityName = "LOGIN REQUEST";
                activityLogModel.ActivityPage = "Post:Account/Index";
                activityLogModel.Remark = "";
                activityLogModel.UserId = CurrentUser?.UserID ?? 0;
                LogActivity(activityLogModel);
            }
            catch (Exception e)
            {
                LogException(e);
            }
            if (ModelState.IsValid)
            {
                User objLogin = loginService.GetUserDeatils(model.Email, EncryptDecrypt.Encrypt(model.Password));
                if (objLogin != null)
                {
                    if (model.RememberMe)
                    {
                        Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
                        Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
                    }
                    else
                    {
                        Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);
                    }
                    Response.Cookies["UserName"].Value = model.Email.Trim();
                    Response.Cookies["Password"].Value = model.Password.Trim();

                    User userInfo = loginService.GetUserDeatilByEmail(model.Email);
            

                    CreateAuthenticationTicket(userInfo, model.RememberMe);

                    HttpCookie userSessionCookies = new HttpCookie("UserSessionCookies");
                  
                    userSessionCookies.Expires = DateTime.Now.AddHours(10);
                    Response.Cookies.Add(userSessionCookies);
                    ShowSuccessMessage("Success!", "Hi" + userInfo.Id ?? "" + "User logged in successfully");
                    try
                    {
                        activityLogModel.ActivityName = "LOGIN SUCCESS";
                        activityLogModel.ActivityPage = "Post:Account/Index";
                        activityLogModel.Remark = "";
                        activityLogModel.UserId = CurrentUser?.UserID ?? 0;
                        LogActivity(activityLogModel);
                    }
                    catch (Exception e)
                    {
                        LogException(e);
                    }
                    return RedirectToAction("Index", "Home", new { id = userInfo.Id });
                }
                else
                {
                    ShowErrorMessage("Failed!", "Invalid User Details.");
                    try
                    {
                        activityLogModel.ActivityName = "LOGIN FAILED";
                        activityLogModel.ActivityPage = "Post:Account/Index";
                        activityLogModel.Remark = "Invalid Credentials, Email: " + model.Email + ", Password: " + model.Password;
                        activityLogModel.UserId = CurrentUser?.UserID ?? 0;
                        LogActivity(activityLogModel);
                    }
                    catch (Exception ex)
                    {
                        LogException(ex);
                    }
                }
            }
            else
            {
                ShowErrorMessage("Failed!", "Invalid UserName or Password.");
                try
                {
                    activityLogModel.ActivityName = "LOGIN FAILED";
                    activityLogModel.ActivityPage = "Post:Account/Index";
                    activityLogModel.Remark = "Invalid Credentials, Email: " + model.Email + ", Password: " + model.Password;
                    activityLogModel.UserId = CurrentUser?.UserID ?? 0;
                    LogActivity(activityLogModel);
                }
                catch (Exception ex)
                {
                    LogException(ex);
                }
            }
            return View();
        }
        public ActionResult ForgotPassword()
        {
            return View(new ForgotPasswordDto());
        }
        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordDto model)
        {
            try
            {
                activityLogModel.ActivityName = "FORGOTPASSWORD REQUEST";
                activityLogModel.ActivityPage = "Post:Account/ForgotPassword";
                activityLogModel.Remark = "";
                activityLogModel.UserId = CurrentUser?.UserID ?? 0;
                LogActivity(activityLogModel);
            }
            catch (Exception e)
            {
                LogException(e);
            }
            if (model != null && model.Email != null && model.Email != "")
            {
                try
                {
                    var objforgotmail = loginService.GetUserDeatilByEmail(model.Email);
                    if (objforgotmail != null)
                    {
                        objforgotmail = loginService.Update(objforgotmail);

                        #region "Send Reset Link"
                        FlexiMail objMail = new FlexiMail
                        {
                            From = SiteKey.From,
                            //To = objforgotmail.UserProfile.Email,//"parmeshwar.sharma@ezytm.com", 
                            CC = "",
                            BCC = "",
                            Subject = "Forgot Passoword",
                            MailBodyManualSupply = true,
                            ValueArray = new string[]  {
                                                    //objforgotmail.UserProfile.FullName,
                                                    SiteKey.DomainName,
                                                    //objforgotmail.ResetCode

                                                    }
                        };


                        objMail.MailBody = objMail.GetHtml("ForgotPassword.html");
                        objMail.Send();

                        ViewBag.Invalid = "Password has been sent your email. ";
                        return View();
                        #endregion
                    }

                    else
                    {
                        ViewBag.Invalid = "Invalid email";
                        try
                        {
                            activityLogModel.ActivityName = "FORGOTPASSWORD FAILED";
                            activityLogModel.ActivityPage = "Post:Account/ForgotPassword";
                            activityLogModel.Remark = "Email not valid, email: " + model.Email;
                            activityLogModel.UserId = CurrentUser?.UserID ?? 0;
                            LogActivity(activityLogModel);
                        }
                        catch (Exception e)
                        {
                            LogException(e);
                        }



                        model.Email = "";
                        return View();
                    }

                }
                catch (Exception ex)
                {
                    ShowErrorMessage("Error", "Internal error occured.", true);



                    try
                    {
                        activityLogModel.ActivityName = "FORGOTPASSWORD FAILED";
                        activityLogModel.ActivityPage = "Post:Account/ForgotPassword";
                        activityLogModel.Remark = "Exception: " + ex.Message;
                        activityLogModel.UserId = CurrentUser?.UserID ?? 0;
                        LogActivity(activityLogModel);
                    }
                    catch (Exception e)
                    {
                        LogException(e);
                    }
                    return View(model);
                }


            }
            return View(model);
        }
        public ActionResult RecoverPassword(Guid id)
        {
            if (id != Guid.Empty)
            {
                ResetPasswordDto model = new ResetPasswordDto();

                User user = loginService.GetUserDeatilByGuid(id);
                if (user != null)
                {
                    model.ResetToken = id;
                    model.UserId = (int)user.Id;

                }
                return View(model);
            }
            return View(new ResetPasswordDto());
        }
        [HttpPost]
        public ActionResult RecoverPassword(ResetPasswordDto model)
        {
            if (ModelState.IsValid)
            {
                User user = loginService.GetUserDeatilByGuid(model.ResetToken);
                if (user != null)
                {
                    //user.ResetCode = null;
                    user.Password = EncryptDecrypt.Encrypt(model.Password);//AESSecurity.EncryptStringToBytes(model.Password);
                    loginService.Update(user);
                    ShowSuccessMessage("Success", "Password have been saved successfullly", false);
                    return RedirectToAction("index", "account");
                }
                else
                {
                    ShowErrorMessage("Error", "We are sorry, but the link you are trying to access has expired. You can either login or go to home page.", true);
                }

            }
            return View();
        }
        public ActionResult ChangePassword()
        {


            if (CurrentUser != null && CurrentUser.UserID > 0)
            {
                ChangePasswordDto model = new ChangePasswordDto();

                User user = loginService.GetUserDeatilById(CurrentUser.UserID);
                if (user != null)
                {
                    // model.ResetToken = userId;
                    model.UserId = (int)user.Id;
                    model.CurrentPassword = user.Password;

                }
                return View(model);
            }
            return View(new ChangePasswordDto());
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordDto model)
        {
            if (ModelState.IsValid)
            {
                User user = loginService.GetUserDeatilById(CurrentUser.UserID);
                if (user != null)
                {
                    if (user.Password == model.CurrentPassword)
                    {
                        //user.ResetCode = null;
                        user.Password =EncryptDecrypt.Encrypt(model.NewPassword);//AESSecurity.EncryptStringToBytes(model.Password);
                        loginService.Update(user);
                        ShowSuccessMessage("Success", "Password has been changed successfully.", true);
                    }
                    else
                    {
                        ShowErrorMessage("Error", "Current password doesn't match.", true);

                    }

                }
                else
                {
                    ShowErrorMessage("Error", "We are sorry, but the link you are trying to access has expired. You can either login or go to home page.", false);
                }

            }
            return View();
        }
        public ActionResult AdminMenu()
        {

            int roleID = CurrentUser.Roles.FirstOrDefault();
            var menus = roleService.GetMenusByRoleId(roleID).OrderBy(x => x.Id).ToList();
            return PartialView("_MenuBar", menus);
        }
        [HttpGet]
        public ActionResult Logout()
        {
            var c = CurrentUser.UserID;
            RemoveAuthentication();
            return RedirectToAction("index", "account");
        }
        public ActionResult Success()
        {
            RemoveAuthentication();
            return RedirectToAction("success");
        }
        #endregion
       
    }
}