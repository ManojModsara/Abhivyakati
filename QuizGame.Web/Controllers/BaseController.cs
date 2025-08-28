using QuizGame.Dto;
using QuizGame.Web.LIBS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using QuizGame.Web.Code.LIBS;

using DataTables.AspNet.Mvc5;
using DataTables.AspNet.Core;
using static QuizGame.Core.Enums;
using SiteKey = QuizGame.Web.LIBS.SiteKey;
using QuizGame.Core;
using QuizGame.Data;
using QuizGame.Models.Secuirity;
using QuizGame.Web.Code.Serialization;
using QuizGame.Service;
using System.IO;
using QuizGame.LIBS;

namespace QuizGame.Web.Controllers
{
    public class BaseController : Controller
    {
        private IActivityLogService activityLogService;
        private IRoleService roleService;
        public BaseController(IActivityLogService _activityLogService, IRoleService _roleService)
        {
            this.activityLogService = _activityLogService;
            this.roleService = _roleService;
        }

        #region "Notificatons"

        private void ShowMessages(string title, string message, MessageType messageType, bool isCurrentView)
        {
            Notification model = new Notification
            {
                Heading = title,
                Message = message,
                Type = messageType
            };
            if (isCurrentView)
                this.ViewData.AddOrReplace("NotificationModel", model);
            else
                this.TempData.AddOrReplace("NotificationModel", model);
        }

        protected void ShowErrorMessage(string title, string message, bool isCurrentView = true)
        {
            ShowMessages(title, message, MessageType.Danger, isCurrentView);
        }

        protected void ShowSuccessMessage(string title, string message, bool isCurrentView = true)
        {
            ShowMessages(title, message, MessageType.Success, isCurrentView);
        }

        protected void ShowWarningMessage(string title, string message, bool isCurrentView = true)
        {
            ShowMessages(title, message, MessageType.Warning, isCurrentView);
        }

        protected void ShowInfoMessage(string title, string message, bool isCurrentView = true)
        {
            ShowMessages(title, message, MessageType.Info, isCurrentView);
        }


        #endregion

        #region "Authentication"
        public CustomPrincipal CurrentUser
        {
            get { return HttpContext.User as CustomPrincipal; }
        }

        public void CreateAuthenticationTicket(dynamic user, bool isPersist)
        {
            if (user != null)
            {
                CustomPrincipal principal = new CustomPrincipal(user, (byte)user.RoleId);
                principal.UserID = (int)user.Id;
                principal.UserName = user.Username;
                principal.RoleId = (int)user.RoleId;
                var authTicket = new FormsAuthenticationTicket(1,
                    user.Username,
                    DateTime.Now,
                    DateTime.Now.AddDays(1),
                    isPersist,
                    JsonConvert.SerializeObject(principal));

                string encTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                Response.Cookies.Add(faCookie);
            }
        }

        public void RemoveAuthentication()
        {
            FormsAuthentication.SignOut();
        }
        public ActionAllowedDto ActionAllowed(string menuid, int roleid, int aId = 1)
        {
            var menu = roleService.GetMenusByRoleId(roleid).Where(x => x.MenuId == menuid).FirstOrDefault();
            ActionAllowedDto action = new ActionAllowedDto();
            action.AllowView = menu != null ? true : false;
            int mid = menu != null ? menu.Id : 0;
            var RolePermission = roleService.GetRolePermission(roleid);
            var acts = RolePermission.Where(m => m.MenuId == mid).FirstOrDefault();
            action.RoleId = roleid;
            action.AllowCreate = acts != null ? acts.AllowCreate : false;
            action.AllowEdit = acts != null ? acts.AllowUpdate : false;
            action.AllowDelete = acts != null ? acts.AllowDelete : false;
            if (!action.AllowView || (aId == 2 && !action.AllowCreate) || (aId == 3 && !action.AllowEdit) || (aId == 4 && !action.AllowDelete))
                throw new Exception("Access Denied!");
            return action;
        }
        public ActionAllowedDto ActionAllowed(string menuid, int roleid)
        {
            var RolePermission = roleService.GetRolePermission(roleid);

            var acts = RolePermission.Where(m => m.MenuId ==Convert.ToInt32(1)).FirstOrDefault();

            ActionAllowedDto actionAllowed = new ActionAllowedDto();
            actionAllowed.RoleId = roleid;
            actionAllowed.AllowCreate = acts.AllowCreate;
            actionAllowed.AllowEdit = acts.AllowUpdate;
            actionAllowed.AllowDelete = acts.AllowDelete;

            return actionAllowed;
        }
        #endregion
        public PartialViewResult CreateModelStateErrors()
        {
            return PartialView("_ValidationSummary", ModelState.Values.SelectMany(x => x.Errors));
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.Result = new ViewResult
            {

                ViewName = "~/Views/Account/Index.cshtml"
            };

            string message = "Something went wrong!";

            if (filterContext.Exception.Message.Contains("Access Denied"))
                message = "Access Denied!";

            ShowErrorMessage("Error!", message, false);

            base.OnException(filterContext);

            LogException(filterContext.Exception, "Access Denied!");

        }
        public void LogException(Exception ex, string comment = "")
        {
            try
            {
                string filepath = System.Web.HttpContext.Current.Server.MapPath("~/ExceptionLog/");  //Text File Path

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);

                }
                filepath = filepath + DateTime.Today.ToString("dd-MM-yy") + ".txt";   //Text File Name
                if (!System.IO.File.Exists(filepath))
                {


                    System.IO.File.Create(filepath).Dispose();

                }
                using (StreamWriter sw = System.IO.File.AppendText(filepath))
                {
                    sw.WriteLine("================= ***EXCEPTION DETAILS" + " " + DateTime.Now.ToString() + "*** =============");
                    sw.WriteLine("COMMENT:");
                    sw.WriteLine(comment);
                    sw.WriteLine();
                    sw.WriteLine("Error Occured:");
                    sw.WriteLine((SiteKey.DomainName.Contains("localhost") || SiteKey.DomainName.Contains("www.dev.") ? "Test" : "Live"));
                    sw.WriteLine();

                    sw.WriteLine("Date Time:");
                    sw.WriteLine(DateTime.Now.ToString());
                    sw.WriteLine();

                    sw.WriteLine("Error Code:");
                    sw.WriteLine(ex.GetHashCode().ToString());
                    sw.WriteLine();

                    sw.WriteLine("Base Exception:");
                    sw.WriteLine(ex.GetBaseException().ToString());
                    sw.WriteLine();

                    sw.WriteLine("Exception Type:");
                    sw.WriteLine(ex.GetType().ToString());
                    sw.WriteLine();

                    sw.WriteLine("Inner Exception:");
                    sw.WriteLine(ex.InnerException.ToString());
                    sw.WriteLine();

                    sw.WriteLine("Exception Message: ");
                    sw.WriteLine(ex.Message);
                    sw.WriteLine();

                    sw.WriteLine("Exception Source:  ");
                    sw.WriteLine(ex.Source);
                    sw.WriteLine();

                    sw.WriteLine("Stack Trace: ");
                    sw.WriteLine(ex.StackTrace.ToString());
                    sw.WriteLine();

                    sw.WriteLine("Generic Info: ");
                    sw.WriteLine(ex.ToString());
                    sw.WriteLine();


                    sw.WriteLine("=================================== ***End*** =============================================");
                    sw.WriteLine();
                    sw.Flush();
                    sw.Close();

                }
            }
            catch (Exception)
            {

            }
        }
        public DataTablesJsonResult DataTablesJsonResult(int total, IDataTablesRequest request, IEnumerable<object> data, IDictionary<string, object> additionalParameter = null)
        {
            var response = DataTablesResponse.Create(request, total, total, data, additionalParameter);
            return new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
        }
        
        #region "DataTables Response"

        public DataTablesJsonResult DataTablesJsonResult(int total, IDataTablesRequest request, IEnumerable<object> data)
        {
            var response = DataTablesResponse.Create(request, total, total, data);
            return new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Serialization"

        public ActionResult NewtonSoftJsonResult(object data)
        {
            return new JsonNetResult
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public void LogException(Exception ex)
        {
            try
            {


                string filepath = System.Web.HttpContext.Current.Server.MapPath("~/ExceptionLog/");  //Text File Path

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);

                }
                filepath = filepath + DateTime.Today.ToString("dd-MM-yy") + ".txt";   //Text File Name
                if (!System.IO.File.Exists(filepath))
                {


                    System.IO.File.Create(filepath).Dispose();

                }
                using (StreamWriter sw = System.IO.File.AppendText(filepath))
                {
                    sw.WriteLine("================= ***EXCEPTION DETAILS" + " " + DateTime.Now.ToString() + "*** =============");
                    sw.WriteLine("Error Occured:");
                    sw.WriteLine((SiteKey.DomainName.Contains("localhost") || SiteKey.DomainName.Contains("www.dev.") ? "Test" : "Live"));
                    sw.WriteLine();

                    sw.WriteLine("Date Time:");
                    sw.WriteLine(DateTime.Now.ToString());
                    sw.WriteLine();

                    sw.WriteLine("Error Code:");
                    sw.WriteLine(ex.GetHashCode().ToString());
                    sw.WriteLine();

                    sw.WriteLine("Base Exception:");
                    sw.WriteLine(ex.GetBaseException().ToString());
                    sw.WriteLine();

                    sw.WriteLine("Exception Type:");
                    sw.WriteLine(ex.GetType().ToString());
                    sw.WriteLine();

                    sw.WriteLine("Inner Exception:");
                    sw.WriteLine(ex.InnerException.ToString());
                    sw.WriteLine();

                    sw.WriteLine("Exception Message: ");
                    sw.WriteLine(ex.Message);
                    sw.WriteLine();

                    sw.WriteLine("Exception Source:  ");
                    sw.WriteLine(ex.Source);
                    sw.WriteLine();

                    sw.WriteLine("Stack Trace: ");
                    sw.WriteLine(ex.StackTrace.ToString());
                    sw.WriteLine();

                    sw.WriteLine("Generic Info: ");
                    sw.WriteLine(ex.ToString());
                    sw.WriteLine();

                    sw.WriteLine("=================================== ***End*** =============================================");
                    sw.WriteLine();
                    sw.Flush();
                    sw.Close();

                }

            }
            catch (Exception e)
            {
                LogException(e);
            }
        }
        public void LogActivity(ActivityLogDto activityLogModel)
        {
            try
            {
                ActivityLog activityLog = new ActivityLog();
                activityLog.ActivityName = activityLogModel.ActivityName;
                activityLog.ActivityPage = activityLogModel.ActivityPage;
                activityLog.IPAddress = GeneralMethods.Fetch_UserIP();
                activityLog.Remark = activityLogModel.Remark;
                if (activityLogModel.UserId != null && activityLogModel.UserId > 0)
                {
                    activityLog.UserId = activityLogModel?.UserId ?? 0;
                }
                activityLogService.Save(activityLog);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }
        public void LogActivity(string msgText)
        {
            try
            {
                string filepath = System.Web.HttpContext.Current.Server.MapPath("~/ActivityLog/");  //Text File Path
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                filepath = filepath + DateTime.Today.ToString("dd-MM-yy") + ".txt";   //Text File Name
                if (!System.IO.File.Exists(filepath))
                {
                    System.IO.File.Create(filepath).Dispose();
                }
                using (StreamWriter sw = System.IO.File.AppendText(filepath))
                {
                    sw.WriteLine("---------LOG DETAILS" + " " + DateTime.Now.ToString() + "--------------");
                    sw.WriteLine("Error Occured:");
                    sw.WriteLine((SiteKey.DomainName.Contains("localhost") || SiteKey.DomainName.Contains("www.dev.") ? "Test" : "Live"));
                    sw.WriteLine();
                    sw.WriteLine(msgText);
                    sw.WriteLine();
                    sw.WriteLine("----------------------------------------------------------------");
                    sw.WriteLine();
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                LogException(e);
            }
        }
        #endregion
    }
}