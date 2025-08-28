using QuizGame.Core;
using QuizGame.Data;
using QuizGame.Dto;
using QuizGame.Service;
using QuizGame.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizGame.Controllers
{
    public class ActivityLogController : BaseController
    {
        #region "Fields"

        private IActivityLogService activityLogService;
        private IRoleService roleService;
        ActivityLogDto activityLogModel;
        public ActionAllowedDto actionAllowedDto;
        #endregion

        #region "Constructor"
        public ActivityLogController(IRoleService _userroleService, IActivityLogService _activityLogService) : base(_activityLogService, _userroleService)
        {
            this.activityLogService = _activityLogService;
            this.roleService = _userroleService;
            this.activityLogModel = new ActivityLogDto();

            this.actionAllowedDto = new ActionAllowedDto();

        }
        #endregion
        // GET: ActivityLog
        public ActionResult Index()
        {
            try
            {
                activityLogModel.ActivityName = "Activity Log VISIT";
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

        [HttpPost]
        public ActionResult GetActivityLogReport(DataTableServerSide model)
        {
            ViewBag.actionAllowed = actionAllowedDto = ActionAllowed("ActivityLog", CurrentUser.Roles.FirstOrDefault());

            KeyValuePair<int, List<ActivityLog>> requestResponses = activityLogService.GetActivityLogs(model);

            return Json(new
            {
                draw = model.draw,
                recordsTotal = requestResponses.Key,
                recordsFiltered = requestResponses.Key,
                data = requestResponses.Value.Select(c => new List<object> {
                    c.Id,
                    c.User?.UserProfile?.ORGName ?? c.User?.UserProfile?.FullName,
                    c.ActivityName,
                    (c.ActivityDate).ToString(),
                    c.IPAddress,
                    c.ActivityPage,
                    c.Remark
                    })
            }, JsonRequestBehavior.AllowGet);

        }

    }
}