using QuizGame.Core;
using QuizGame.Data;
using QuizGame.Dto;
using QuizGame.Service;
using QuizGame.Web;
using QuizGame.Web.Controllers;
using QuizGame.Web.Models.Others;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using static QuizGame.Core.Enums;

namespace APIBOXDNS.Web.Controllers
{
    public class EventController : BaseController
    {
        public ActionAllowedDto action;
        private IEventService eventService;
        private IRoleService roleService;
        ActivityLogDto activityLogModel;

        public EventController(IEventService _eventService, IActivityLogService _activityLogService, IRoleService _roleService) : base(_activityLogService, _roleService)
        {
            this.eventService = _eventService;
            this.roleService = _roleService;
            this.action = new ActionAllowedDto();
            this.activityLogModel = new ActivityLogDto();
        }

        // GET: Event
        public ActionResult Index()
        {
            UpdateActivity("EventList REQUEST", "GET:Event/Index", string.Empty);
            ViewBag.actionAllowed = action = ActionAllowed("Event", CurrentUser.Roles.FirstOrDefault());
            try
            {
                activityLogModel.ActivityName = "Event List REQUEST";
                activityLogModel.ActivityPage = "GET:Event/Index/";
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
        public ActionResult GetEvents(DataTableServerSide model)
        {
            UpdateActivity("EventList REQUEST", "POST:Event/GetEvents", string.Empty);
            ViewBag.actionAllowed = action = ActionAllowed("Event", CurrentUser.Roles.FirstOrDefault());
            KeyValuePair<int, List<Event>> users = eventService.GetEvents(model);
            action.AllowEdit = true;
            action.AllowDelete = true;
            return Json(new
            {
                draw = model.draw,
                recordsTotal = users.Key,
                recordsFiltered = users.Key,
                data = users.Value.Select(c => new List<object> {
                    c.Id,
                    c.Title,
                    c.EventDate?.ToString("dd MMM yyyy") ?? "",
                    c.Descriptions,
                    $"<img src='{c?.ImageUrl}' alt='Event Image' style='height:60px;' />",
                    c.AddedDate.ToString(),
                    c.User.UserProfile.FullName,
                    c?.UpdatedDate?.ToString() ?? "",
                    c?.User1?.UserProfile?.FullName??"",
                     (action.AllowEdit? DataTableButton.EditButton(Url.Action( "createedit", "Event",new { id = c.Id })):string.Empty)
                    +"&nbsp;"+
                   (action.AllowDelete? DataTableButton.DeleteButton(Url.Action( "delete","Event", new { id = c.Id }),"modal-delete-event"):string.Empty)
                })
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult createedit(int? id)
        {
            UpdateActivity("AddEditEvent REQUEST", "Get:Event/createedit/", "Id=" + id);
            ViewBag.actionAllowed = action = ActionAllowed("Event", CurrentUser.Roles.FirstOrDefault(), id.HasValue ? 3 : 2);
            EventDto blogdto = new EventDto();
            Event blog = eventService.GetEventById(id ?? 0) ?? new Event();

            if (blog != null)
            {
                blogdto.ID = blog.Id;
                blogdto.Description = blog.Descriptions;
                blogdto.EventDate = blog.EventDate;
                blogdto.Title = blog.Title;
                blogdto.ShowImg = blog.ImageUrl;
            }
            return View(blogdto);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult createedit(EventDto model)
        {
            UpdateActivity("AddEditEvent", "Post:Event/createedit/");
            ViewBag.actionAllowed = action = ActionAllowed("Event", CurrentUser.Roles.FirstOrDefault(), model.ID > 0 ? 3 : 2);

            EventDto blogDto = new EventDto();
            Event obj = eventService.GetEventById(model.ID) ?? new Event();
            if (ModelState.IsValid && (model.Image != null || model.ShowImg != null))
            {
                obj.Id = model.ID;
                obj.Title = model.Title;
                obj.Descriptions = model.Description;
                obj.EventDate = model.EventDate;
                if(model.ID == 0)
                {
                    obj.AddedById = CurrentUser.UserID;
                }
                else
                {
                    obj.UpdatedById = CurrentUser.UserID;
                }
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

                eventService.SaveEvent(obj);
                ShowSuccessMessage("Success!", "Event has been Saved Successfully!!", false);

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

        [HttpGet]
        public ActionResult delete(int Id)
        {

            UpdateActivity("Delete Event", "GET:Event/delete/", "id=" + Id);
            ViewBag.actionAllowed = action = ActionAllowed("Event", CurrentUser.Roles.FirstOrDefault(), 4);
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are You Sure You Want To Delete?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Heading = "Delete" },
                Footer = new ModalFooter { SubmitButtonText = "Yes", CancelButtonText = "No" }
            });

        }
        [HttpPost]
        [ActionName("delete")]
        public ActionResult Delete(int Id)
        {
            UpdateActivity("Delete Event", "POST:Event/Delete/", "id=" + Id);
            ViewBag.actionAllowed = action = ActionAllowed("Event", CurrentUser.Roles.FirstOrDefault(), 4);
            try
            {
                eventService.DeleteEvent(Id);
                ShowSuccessMessage("Success", "Deleted.", false);
            }
            catch (Exception)
            {
                ShowErrorMessage("Error", "Internal Server Error. ", false);
            }
            return RedirectToAction("Index");
        }



        //[HttpPost]
        //public bool delete(int Id)
        //{
        //    UpdateActivity("DeleteEvent REQUEST", "POST:Event/delete/", "Id=" + Id);
        //    //ViewBag.actionAllowed = action = ActionAllowed("Event", CurrentUser.RoleId, 4);
        //    try
        //    {
        //        return eventService.DeleteEvent(Id);
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }

        //    }
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
                    bool folderExists = Directory.Exists(Server.MapPath("~/img/EventImg/"));
                    if (!folderExists)
                    {
                        Directory.CreateDirectory(Server.MapPath("~/img/EventImg/"));
                    }
                    string _path = Path.Combine(Server.MapPath("~/img/EventImg/"), _FileName);
                    file.SaveAs(_path);
                    _FileName = "/img/EventImg/" + _FileName;
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