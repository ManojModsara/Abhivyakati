using QuizGame.Data;
using QuizGame.Dto;
using QuizGame.Service;
using QuizGame.Web.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace APIBOXDNS.Web.Controllers
{
    public class BlogController : BaseController
    {
        public ActionAllowedDto action;
        private IBlogService blogService;
        private IRoleService roleService;
        ActivityLogDto activityLogModel;

        public BlogController(IBlogService _blogService, IActivityLogService _activityLogService, IRoleService _roleService) : base(_activityLogService, _roleService)
        {
            this.blogService = _blogService;
            this.roleService = _roleService;
            this.action = new ActionAllowedDto();
            this.activityLogModel = new ActivityLogDto();
        }

        // GET: Blog
        [HttpGet]
        public ActionResult Index()
        {
            UpdateActivity("BlogList REQUEST", "GET:Blog/Index", string.Empty);
            ViewBag.actionAllowed = action = ActionAllowed("BlogList", CurrentUser.Roles.FirstOrDefault());

            List<BlogDto> blogDtos = new List<BlogDto>();
            List<Blog> blogs = blogService.GetBlog().ToList();
            if (blogs.Count != 0)
            {
                foreach (var blog in blogs)
                {
                    BlogDto blogDto = new BlogDto
                    {
                        ID = blog.Id,
                        Description = blog.Descriptions,
                        Title = blog.Title,
                        Url = blog.Url,
                        ShowImg = blog.ImageUrl,
                        IsActive = (bool)blog.IsActive,
                    };
                    blogDtos.Add(blogDto);
                }
            }
            return View(blogDtos);

        }
        [HttpGet]
        public ActionResult AddBlog(int? id)
        {
            UpdateActivity("AddBlog REQUEST", "Get:Blog/AddBlog/", "Id=" + id);
            ViewBag.actionAllowed = action = ActionAllowed("BlogList", CurrentUser.Roles.FirstOrDefault(), id.HasValue ? 3 : 2);
            BlogDto blogdto = new BlogDto();
            Blog blog = blogService.GetBlogById(id ?? 0) ?? new Blog();

            if (blog != null)
            {
                blogdto.ID = blog.Id;
                blogdto.Description = blog.Descriptions;
                blogdto.ShortDescription = blog.ShortDescription;
                blogdto.Title = blog.Title;
                blogdto.Url = blog.Url;
                blogdto.ShowImg = blog.ImageUrl;
                blogdto.IsActive = blog.IsActive ?? false;
            }
            return View(blogdto);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddBlog(BlogDto model)
        {
            UpdateActivity("AddBlog", "Post:Blog/AddBlog/");
            ViewBag.actionAllowed = action = ActionAllowed("BlogList", CurrentUser.Roles.FirstOrDefault(), model.ID > 0 ? 3 : 2);

            BlogDto blogDto = new BlogDto();
            Blog obj = blogService.GetBlogById(model.ID) ?? new Blog();
            if (ModelState.IsValid && (model.Image != null || model.ShowImg != null))
            {
                obj.Id = model.ID;
                obj.Url = model.Url == null ? model.Title.Replace(" ", "-").Replace("&", "-") : model.Url.Replace(" ", "-").Replace("&", "-");
                obj.Title = model.Title;
                obj.Descriptions = model.Description;
                obj.ShortDescription = model.ShortDescription;
                obj.IsActive = true;
                string filename = null;
                string response = "";
                if (model.Image != null)
                {
                    FileUpdoad(model.Image, ref filename, ref response);
                    obj.ImageUrl = filename;
                }
                if (response != "FileUpload Successfull" && model.ShowImg == null)
                {
                    ShowSuccessMessage("Error!", response, false);
                    return View(blogDto);
                }

                blogService.SaveBlog(obj);
                ShowSuccessMessage("Success!", "Blog has been Saved Successfully!!", false);

                return RedirectToAction("Index");
            }
            else if (model.ShowImg == null && model.Image == null)
            {
                ShowErrorMessage("Error!", "Select An Image!!", false);
                return View(blogDto);
            }
            ShowErrorMessage("Error!", "Fill All Requred Filled!!", false);
            return View(blogDto);
        }

        public bool Active(int id)
        {
            UpdateActivity("Active/Inactive REQUEST", "Get:Blog/Active/", "Id=" + id);
            ViewBag.actionAllowed = action = ActionAllowed("BlogList", CurrentUser.Roles.FirstOrDefault(), 3);
            try
            {
                var cRoute = blogService.GetBlogById(id);
                cRoute.IsActive = !(cRoute.IsActive ?? false);
                cRoute.ActiveUpdatedById = CurrentUser.UserID;
                return blogService.SaveBlog(cRoute).IsActive ?? false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpPost]
        public bool DeleteRoute(int Id)
        {
            UpdateActivity("DeleteBlog REQUEST", "POST:Blog/DeleteRoute/", "Id=" + Id);
            ViewBag.actionAllowed = action = ActionAllowed("BlogList", CurrentUser.Roles.FirstOrDefault(), 4);
            try
            {
                return blogService.DeleteBlog(Id);
            }
            catch (Exception)
            {
                return false;
            }
        }
        private void FileUpdoad(HttpPostedFileBase file, ref string _FileName, ref string Response)
        {
            string ext = System.IO.Path.GetExtension(file.FileName);
            int maxlength = 1024 * 512 * 1; // 1 MB;
            if (ext != ".jpg" && ext != ".png" && ext != ".jpeg")
            {
                Response = "Please choose only .jpg, .png ,.jpeg image types!";
            }
            else if (file.ContentLength > maxlength)
            {
                Response = "Please Upload a file upto 1 MB.";
            }
            else
            {
                if (file != null && file.ContentLength > 0)
                {
                    byte[] upload = new byte[file.ContentLength];
                    file.InputStream.Read(upload, 0, file.ContentLength);
                    string Name = DateTime.Now.ToString("yyyyMMddHHmmss");
                    _FileName = "_" + Name + Path.GetFileName(file.FileName.Replace(" ", "_"));
                    bool folderExists = Directory.Exists(Server.MapPath("~/img/BlogImg/"));
                    if (!folderExists)
                    {
                        Directory.CreateDirectory(Server.MapPath("~/img/BlogImg/"));
                    }
                    string _path = Path.Combine(Server.MapPath("~/img/BlogImg/"), _FileName);
                    file.SaveAs(_path);
                    _FileName = "/img/BlogImg/" + _FileName;
                }
                Response = "FileUpload Successfull";
            }
        }

        private void UpdateActivity(string activityName, string ativityPage, string remark = "")
        {
            try
            {
                activityLogModel.ActivityName = activityName;
                activityLogModel.ActivityPage = ativityPage;
                activityLogModel.Remark = remark;
                activityLogModel.UserId = CurrentUser?.UserID ?? 0;
                LogActivity(activityLogModel);
            }
            catch (Exception ex)
            {

                LogException(ex);
            }
        }
    }
}