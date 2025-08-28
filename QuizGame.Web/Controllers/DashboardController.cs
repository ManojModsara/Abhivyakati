using QuizGame.Data;
using QuizGame.Dto;
using QuizGame.Service;
using QuizGame.Web.Code.Attributes;
using QuizGame.Web.LIBS;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace QuizGame.Web.Controllers
{
    [CustomAuthorization()]
    public class DashboardController : BaseController
    {
        // GET: Dashboard

        readonly ActivityLogDto activityLogModel;
        public DashboardController(IActivityLogService _activityLogService, IRoleService _roleService) : base(_activityLogService, _roleService)
        {
            activityLogModel = new ActivityLogDto(); ;
        }
        public ActionResult Index()
        {
            try
            {
                activityLogModel.ActivityName = "Dashboard VISIT";
                activityLogModel.ActivityPage = "Get:ActivityLog/Index";
                activityLogModel.Remark = "";
                activityLogModel.UserId = CurrentUser?.UserID ?? 0;
                LogActivity(activityLogModel);
            }
            catch (Exception e)
            {
                LogException(e);
            }
            return View();
        }

        [CustomAuthorization()]
        [HttpGet]
        public ActionResult Signout()
        {
            RemoveAuthentication();
            SiteSession.SessionUser = null; // for webforms
            Response.Cookies["UserSessionCookies"].Expires = System.DateTime.Now.AddSeconds(1); // Clear cookies of SiteSession.SessionUser
            return RedirectToAction("Index", "Login");
        }
    }
}