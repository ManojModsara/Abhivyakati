using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Globalization;
using System.Configuration;
using System.Threading.Tasks;


namespace QuizGame.API.Controllers
{
    public class UserController : ApiController
    {

        #region Reference Variables
        //private readonly IApiKeyService apiKey;
        //private IUserLoginService userLoginService;
        //private IDepartmentService departmentService;
        //private ITechnologyService technologyService;
        //private IProjectService projectService;
        //private ITimesheetService timesheetService;
        //private IVirtualDeveloperService virtualDeveloperService;
        #endregion

        #region Constructor
        //public UserController(IApiKeyService _apiKey, IUserLoginService _userLoginService, IDepartmentService _departmentService,  ITechnologyService _technologyService, IProjectService _projectService,ITimesheetService _timesheetService, IVirtualDeveloperService _virtualDeveloperService)
        //{
        //    this.apiKey = _apiKey;
        //    this.userLoginService = _userLoginService;
        //    this.departmentService = _departmentService;
        //    this.technologyService = _technologyService;
        //    this.projectService = _projectService;
        //    this.timesheetService = _timesheetService;
        //    this.virtualDeveloperService = _virtualDeveloperService;
        //}
        #endregion

        [Route("~/User/Test")]
        [HttpGet]
        public async Task<int> Hello(int a,int b)
        {
            return a+b;
        }

    }
}
