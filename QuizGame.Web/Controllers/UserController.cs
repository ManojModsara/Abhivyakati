using QuizGame.Core;
using QuizGame.Data;
using QuizGame.Dto;
using QuizGame.Dto.Model;
using QuizGame.Service;
using QuizGame.Web;
using QuizGame.Web.Controllers;
using QuizGame.Web.Models.Others;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static QuizGame.Core.Enums;

namespace QuizGame.Controllers
{
    public class UserController : BaseController
    {
        public ActionAllowedDto actionAllowedDto;
        private IUserService adminUserService;
        ActivityLogDto activityLogModel;
        public UserController(IUserService _adminUserService, IActivityLogService _activityLogService, IRoleService roleService) : base(_activityLogService, roleService)
        {
            this.adminUserService = _adminUserService;
            this.activityLogModel = new ActivityLogDto();
        }

        // GET: Admin/AdminUser

        public ActionResult Index()
        {
            ViewBag.actionAllowed = actionAllowedDto = ActionAllowed("User", CurrentUser.Roles.FirstOrDefault());
            try
            {
                activityLogModel.ActivityName = "User List REQUEST";
                activityLogModel.ActivityPage = "GET:User/Index/";
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
        public ActionResult GetAdminUsers(DataTableServerSide model)
        {
            ViewBag.actionAllowed = actionAllowedDto = ActionAllowed("User", CurrentUser.Roles.FirstOrDefault());

            KeyValuePair<int, List<User>> users = adminUserService.GetAdminRoleUser(model, CurrentUser.UserID, CurrentUser.Roles.FirstOrDefault());
            return Json(new
            {
                draw = model.draw,
                recordsTotal = users.Key,
                recordsFiltered = users.Key,
                data = users.Value.Select(c => new List<object> {

                    c.Id,
                    c.UserProfile?.FullName??"",
                    c.UserProfile?.MobileNumber??"",
                    c.UserProfile?.Email??"",
                    c.Role?.RoleName??"",
                     c.IsActive,
                     (actionAllowedDto.AllowEdit? DataTableButton.EditButton(Url.Action( "createedit", "user",new { id = c.Id })):string.Empty)
                    +"&nbsp;"+
                   (actionAllowedDto.AllowDelete? DataTableButton.DeleteButton(Url.Action( "delete","user", new { id = c.Id }),"modal-delete-adminuser"):string.Empty)
                })
            }, JsonRequestBehavior.AllowGet);


        }

        public ActionResult CreateEdit(int? id)
        {
            try
            {
                activityLogModel.ActivityName = "User Edit REQUEST";
                activityLogModel.ActivityPage = "GET:User/CreateEdit/" + id;
                activityLogModel.Remark = "";
                activityLogModel.UserId = CurrentUser?.UserID ?? 0;
                LogActivity(activityLogModel);
            }
            catch (Exception e)
            {
                LogException(e);
            }
            UserDto adminUserDto = new UserDto();
            User adminUser;

            if (id.HasValue && id.Value > 0)
            {
                adminUser = adminUserService.GetAdminUser(id.Value);
                adminUserDto.RoleId = (byte)adminUser.Role.Id;
                adminUserDto.MobileNumber = adminUser.UserProfile?.MobileNumber ?? "";
                adminUserDto.EmailOffice = adminUser.UserProfile?.Email ?? "";
                adminUserDto.UserName = adminUser.Username;
                adminUserDto.IsActive = adminUser.IsActive;
                adminUserDto.Name = adminUser.UserProfile?.FullName ?? "";
                adminUserDto.Uid = adminUser.Id;
                adminUserDto.Password = EncryptDecrypt.Decrypt(adminUser.Password);
            }
            ViewBag.Roles = adminUserService.GetAdminRole(0, true);//.Where(a => a.Id != (int)RoleType.Admin);
            return View("createedit", adminUserDto);
        }

        [HttpPost]
        public ActionResult CreateEdit(UserDto model, FormCollection FC)
        {
            try
            {
                activityLogModel.ActivityName = "User Edit REQUEST";
                activityLogModel.ActivityPage = "POST:User/CreateEdit/";
                activityLogModel.Remark = "";
                activityLogModel.UserId = CurrentUser?.UserID ?? 0;
                LogActivity(activityLogModel);
            }
            catch (Exception e)
            {
                LogException(e);
            }
            string message = string.Empty;
            try
            {
                //if (ModelState.IsValid)
                //{
                //    User adminUser = adminUserService.GetAdminUser(model.Uid) ?? new User();
                //    //adminUser.Id = (int)model.Uid;
                //    adminUser.Username = model.UserName;
                //    adminUser.Password = EncryptDecrypt.Encrypt(model.Password);
                //    adminUser.RoleId = (byte)model.RoleId;
                //    adminUser.AddedDate = DateTime.Now;
                //    adminUser.IsActive = model.IsActive;
                //    adminUser.AddedDate = DateTime.Now.AddDays(60);
                //    adminUserService.Save(adminUser);
                //    if (adminUser.UserProfile != null)
                //    {
                //        adminUser.UserProfile.UserId = model.Uid;
                //        adminUser.UserProfile.Email = model.EmailOffice;
                //        adminUser.UserProfile.FullName = model.Name;
                //        adminUser.UserProfile.MobileNumber = model.MobileNumber;
                //    }
                //    else
                //    {
                //        adminUser.UserProfile = new UserProfile();
                //        adminUser.UserProfile.UserId = adminUser.Id;
                //        adminUser.UserProfile.FullName = model.Name;
                //        adminUser.UserProfile.Email = model.EmailOffice;
                //        adminUser.UserProfile.MobileNumber = model.MobileNumber;
                //    }
                //    adminUserService.Save(adminUser);
                //    ShowSuccessMessage("Success!", "User has been saved", false);
                //    return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = true, RedirectUrl = Url.Action("Index") });

                //}



                if (ModelState.IsValid)
                {

                    User user = adminUserService.GetAdminUser(model.Uid) ?? new User();
                    user.Id = model.Uid;
                    user.RoleId = (byte)model.RoleId;
                    user.Username = model.UserName;
                    user.Password = EncryptDecrypt.Encrypt(model.Password);
                    user.IsActive = model.IsActive;
                    user.IsLocked = model.IsLocked;
                    int oldPackageId = user.PackageId ?? 0;

                    if (model.Uid == 0)
                    {
                        user.AddedDate = DateTime.Now;
                        user.TokenAPI = Guid.NewGuid().ToString();
                        user.HKey = Guid.NewGuid();
                        user.HPass = Guid.NewGuid();
                    }
                    else
                    {
                        user.TokenAPI = user.TokenAPI == null ? Guid.NewGuid().ToString() : user.TokenAPI;
                        user.HKey = user.HKey == null ? Guid.NewGuid() : user.HKey;
                        user.HPass = user.HPass == null ? Guid.NewGuid() : user.HPass;
                        user.UpdatedDate = DateTime.Now;
                    }
                    adminUserService.Save(user);
                    if (user.UserProfile != null)
                    {
                        user.UserProfile.FullName = model.Name;
                        user.UserProfile.Email = model.EmailOffice;
                        user.UserProfile.MobileNumber = model.MobileNumber;
                    }
                    else
                    {
                        user.UserProfile = new UserProfile();
                        user.UserProfile.FullName = model.Name;
                        user.UserProfile.Email = model.EmailOffice;
                        user.UserProfile.MobileNumber = model.MobileNumber;
                    }
                    adminUserService.Save(user);
                    ShowSuccessMessage("Success!", "User has been saved", false);
                    return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = true, RedirectUrl = Url.Action("Index") });

                }
                else
                {

                    ShowErrorMessage("Error!", "Check All Required Values.", true);

                }
            }
            catch (Exception Ex)
            {
                var msg = Ex.GetBaseException().ToString();
                if (msg.Contains("UNIQUE KEY"))
                {
                    message = "Email already exist.";
                    ModelState.AddModelError("error", message);
                }
                else
                {
                    message = "An internal error found during to process your requested data!";
                    ModelState.AddModelError("error", message);
                }
            }
            // return CreateModelStateErrors();
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                activityLogModel.ActivityName = "User Delete REQUEST";
                activityLogModel.ActivityPage = "GET:User/Delete/" + id;
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
        [ActionName("Delete")]
        public ActionResult DeleteAdminUser(int id)
        {
            try
            {
                activityLogModel.ActivityName = "User Delete REQUEST";
                activityLogModel.ActivityPage = "POST:User/Delete/" + id;
                activityLogModel.Remark = "";
                activityLogModel.UserId = CurrentUser?.UserID ?? 0;
                LogActivity(activityLogModel);
            }
            catch (Exception e)
            {
                LogException(e);
            }
            return RedirectToAction("index");
        }

        public bool Active(int id)
        {
            string message = string.Empty;
            try
            {
                var adminUser = adminUserService.GetAdminUser(id);
                adminUser.IsActive = !adminUser.IsActive;
                return adminUserService.Save(adminUser).IsActive;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public ActionResult ContactList()
        {
            ViewBag.actionAllowed = actionAllowedDto = ActionAllowed("ContactList", CurrentUser.Roles.FirstOrDefault());
            try
            {
                activityLogModel.ActivityName = "Contact List REQUEST";
                activityLogModel.ActivityPage = "GET:User/ContactList/";
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
        public ActionResult GetContactList(DataTableServerSide model)
        {
            ViewBag.actionAllowed = actionAllowedDto = ActionAllowed("ContactList", CurrentUser.Roles.FirstOrDefault());

            KeyValuePair<int, List<ContactU>> users = adminUserService.GetContactList(model);
            return Json(new
            {
                draw = model.draw,
                recordsTotal = users.Key,
                recordsFiltered = users.Key,
                data = users.Value.Select(c => new List<object> {
                    c.Id,
                    c?.Name??"",
                    c?.MobileNumber??"",
                    c?.Message??"",
                    c?.Date.ToString("dd MMM yyyy") ?? ""
                })
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLatestGalleries()
        {
            var gallery = adminUserService.GetGalleries().Take(10).OrderByDescending(x=>x.Id)
                        .Select(i => new PhotoGallery
                        {
                            Id = i.Id,
                            ImageURL = i?.ImageURL ?? ""
                        }).ToList();

            return PartialView("_showGalleries", gallery);
        }
        public ActionResult PhotoGallery()
        {
            ViewBag.actionAllowed = actionAllowedDto = ActionAllowed("PhotoGallery", CurrentUser.Roles.FirstOrDefault());
            UpdateActivity("PhotoGallery REQUEST", "Get:User/PhotoGallery/");

            return View();
        }

        [HttpPost]
        public ActionResult GetPhotoGalleryList(DataTableServerSide model)
        {
            ViewBag.actionAllowed = actionAllowedDto = ActionAllowed("PhotoGallery", CurrentUser.Roles.FirstOrDefault());
            UpdateActivity("GetPhotoGalleryList REQUEST", "POST:User/GetPhotoGalleryList/");
            KeyValuePair<int, List<PhotoGallery>> users = adminUserService.GetPhotoGalleryList(model);
            return Json(new
            {
                draw = model.draw,
                recordsTotal = users.Key,
                recordsFiltered = users.Key,
                data = users.Value.Select(c => new List<object> {
                    c.Id,
                    //c?.ImageURL??"",
                    string.IsNullOrEmpty(c?.ImageURL) ? "" : $"<img src='{c.ImageURL}' alt='Image' style='width: 100px; height: auto;' />",
                    c?.UploadedDate?.ToString(),
                    actionAllowedDto.AllowDelete ? DataTableButton.DeleteButton(Url.Action( "DeleteGalleryImage","user", new { Id = c.Id }),"modal-delete-photogalleryimage") :string.Empty
           

                    //(actionAllowedDto.AllowDelete? DataTableButton.DeleteButton(Url.Action( "DeleteGalleryImage","user", new { Id = c.Id }),"modal-delete-photogalleryimage"):string.Empty)
        })
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddPhotoGalleryImages()
        {
            ViewBag.actionAllowed = actionAllowedDto = ActionAllowed("PhotoGallery", CurrentUser.Roles.FirstOrDefault(), 2);
            UpdateActivity("AddPhotoGalleryImages REQUEST", "Get:User/AddPhotoGalleryImages/");

            return View();
        }

        [HttpPost]
        public ActionResult AddPhotoGalleryImages(ImageUploadDto model)
        {
            ViewBag.actionAllowed = actionAllowedDto = ActionAllowed("PhotoGallery", CurrentUser.Roles.FirstOrDefault(), 2);
            UpdateActivity("AddPhotoGalleryImages REQUEST", "POST:User/AddPhotoGalleryImages/");

            try
            {
                if (model.Files != null && model.Files.Any())
                {
                    foreach (var file in model.Files)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                            var ext = Path.GetExtension(file.FileName).ToLower();
                            if (!allowedExtensions.Contains(ext))
                                continue;

                            if (file.ContentLength > 1 * 1024 * 1024)
                                continue;
                            string folderPath = "~/PhotoGalleryImages/";
                            string filepath = System.Web.HttpContext.Current.Server.MapPath(folderPath);
                            if (!Directory.Exists(filepath))
                            {
                                Directory.CreateDirectory(filepath);
                            }

                            var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetFileName(file.FileName.Replace(" ", "_"));
                            var path = Path.Combine(filepath, fileName);
                            file.SaveAs(path);

                            // Save to DB logic here
                            PhotoGallery gallery = new PhotoGallery();
                            gallery.ImageURL = Url.Content(Path.Combine(folderPath, fileName));
                            gallery.UploadedDate = DateTime.Now;
                            adminUserService.Save(gallery);
                        }
                        ShowSuccessMessage("Success!", "Gallery images uploaded successfully.", false);
                    }

                    return RedirectToAction("PhotoGallery");
                }
                else
                {
                    ShowErrorMessage("Error!", "Please select at least one image to upload.", true);
                    return View();
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "excp in AddPhotoGalleryImages Method");
                ShowErrorMessage("Error!", "Something went wrong during gallery images upload process.", true);
            }
            return View();
        }

        [HttpGet]
        public ActionResult DeleteGalleryImage(int Id)
        {

            UpdateActivity("Delete DeleteGalleryImage", "GET:User/DeleteGalleryImage/", "id=" + Id);
            ViewBag.actionAllowed = actionAllowedDto = ActionAllowed("PhotoGallery", CurrentUser.RoleId, 4);
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are You Sure You Want To Delete?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Heading = "Delete" },
                Footer = new ModalFooter { SubmitButtonText = "Yes", CancelButtonText = "No" }
            });

        }
        [HttpPost]
        [ActionName("DeleteGalleryImage")]
        public ActionResult DeleteGalleryimage(int Id)
        {
            UpdateActivity("Delete DeleteGalleryImage", "POST:User/DeleteGalleryImage/", "id=" + Id);
            ViewBag.actionAllowed = actionAllowedDto = ActionAllowed("PhotoGallery", CurrentUser.RoleId, 4);
            try
            {
                adminUserService.DeleteGalleryImage(Id);
                ShowSuccessMessage("Success", "Deleted.", false);
            }
            catch (Exception)
            {
                ShowErrorMessage("Error", "Internal Server Error. ", false);
            }
            return RedirectToAction("PhotoGallery");
        }


        #region JoiningData
        public ActionResult JoiningData()
        {
            //ViewBag.actionAllowed = actionAllowedDto = ActionAllowed("JoiningData", CurrentUser.Roles.FirstOrDefault());
            try
            {
                activityLogModel.ActivityName = "Joining Data REQUEST";
                activityLogModel.ActivityPage = "GET:User/JoiningData/";
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
        public ActionResult GetJoiningData(DataTableServerSide model)
        {
            //ViewBag.actionAllowed = actionAllowedDto = ActionAllowed("JoiningData", CurrentUser.Roles.FirstOrDefault());
            KeyValuePair<int, List<JoiningDataModel>> joins = new KeyValuePair<int, List<JoiningDataModel>>();
            try
            {
                joins = adminUserService.GetJoiningDatas(model);
            }
            catch (Exception ex)
            {
                LogException(ex, "excp in GetJoiningDatas");
            }
            return Json(new
            {
                draw = model.draw,
                recordsTotal = joins.Key,
                recordsFiltered = joins.Key,
                data = joins.Value.Select(c => new List<object> {
                    c.Id,
                    c?.Name ?? "",
                    c?.GuardianName ?? "",
                    c?.MobileNumber ?? "",
                    c?.AlternateMobileNumber ?? "",
                    DateTime.TryParse(c?.DOB, out var dobDate) ? dobDate.ToString("dd/MM/yyyy") : "",
                    c?.DistrictName ?? "",
                    c?.BlockName ?? "",
                    c?.GramPanchayatName ?? "",
                    c?.Village ?? "",
                    c?.PinCode ?? "",
                    c?.Education ?? "",
                    c?.FullAddress ?? "",
                    c?.AadharNumber ?? "",
                    c?.PanNumber ?? "",
                    c?.UniqueId ?? "",
                    DateTime.TryParse(c?.JoiningTime, out var joinDate) ? joinDate.ToString("dd/MM/yyyy HH:mm:ss") : "",
                     $"<a href='/Site/DownloadOfferLetter?name={HttpUtility.UrlEncode(c?.Name)}&uniqueId={HttpUtility.UrlEncode(c?.UniqueId)}' " +
                     $"class='btn btn-sm btn-primary' target='_blank'>Download Offer Letter</a>"
                    //download offerletter
                    //((IsAdminRole) && action.AllowEdit? DataTableButton.HyperLink(Url.Action( "changestatus", "rechargeReport",new { id = c.Id,t=0 }),"modal-change-recharge-status", c.statustype,"Change Status("+(c.StatusId)+")",c.RequestTime==string.Empty?"":setColor(c.StatusId)): "<b style='color:"+setColor(c.StatusId,c?.compId ?? 0)+"'>"+c.statustype+"</b>"),
                  })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        //[HttpPost]
        //public bool DeleteGalleryImage(int Id)
        //{
        //    UpdateActivity("DeleteGalleryImage REQUEST", "POST:User/DeleteGalleryImage/", "Id=" + Id);
        //    ViewBag.actionAllowed = actionAllowedDto = ActionAllowed("PhotoGallery", CurrentUser.Roles.FirstOrDefault(), 4);
        //    try
        //    {
        //        return adminUserService.DeleteGalleryImage(Id);
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
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