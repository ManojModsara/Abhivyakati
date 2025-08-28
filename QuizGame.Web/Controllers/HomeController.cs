using QuizGame.Service;
using QuizGame.Web.Code.Attributes;
using QuizGame.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizGame.Controllers
{
    [CustomAuthorization()]
    public class HomeController : BaseController
    {
        public HomeController(IActivityLogService _activityLogService, IRoleService roleService) : base(_activityLogService, roleService)
        {

        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}