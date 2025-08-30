using QuizGame.Core;
using QuizGame.Data;
using QuizGame.Dto;
using QuizGame.Service;
using QuizGame.Web.Code.Attributes;
using QuizGame.Web.Models.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static QuizGame.Core.Enums;

namespace QuizGame.Web.Controllers
{
    [CustomAuthorization()]
    public class RoleController : BaseController
    {
        public ActionAllowedDto actionAllowedDto;
        private IRoleService roleService;
        ActivityLogDto activityLogModel;

        public RoleController(IRoleService _adminUserService, IActivityLogService _activityLogService, IRoleService _roleService) : base(_activityLogService, _roleService)
        {
            this.roleService = _adminUserService;
            this.actionAllowedDto = new ActionAllowedDto();
            this.activityLogModel = new ActivityLogDto();

        }

        // GET: Admin/AdminUser
        public ActionResult Index()
        {
            try
            {
                activityLogModel.ActivityName = "Role Index REQUEST";
                activityLogModel.ActivityPage = "GET:Role/Index/";
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
        public ActionResult GetRoles(DataTableServerSide model)
        {
            ViewBag.actionAllowed= actionAllowedDto =ActionAllowed("Role", CurrentUser.Roles.FirstOrDefault(),2);
            KeyValuePair<int, List<Role>> roles = roleService.GetAdminRoles(model);
            return Json(new
            {
                draw = model.draw,
                recordsTotal = roles.Key,
                recordsFiltered = roles.Key,
                data = roles.Value.Select(c => new List<object> {
                    c.Id,
                    c.RoleName,
                    //   DataTableButton.EditButton(Url.Action( "createedit", "role",new { id = c.Id }),"modal-add-edit-adminrole")
                    //+"&nbsp;"+
                    // DataTableButton.SettingButton(Url.Action( "permission","role", new { id = c.Id }),"Permission")
                    
                   (actionAllowedDto.AllowEdit? DataTableButton.EditButton(Url.Action( "createedit", "role",new { id = c.Id }),"modal-add-edit-adminrole"):string.Empty )
                    +"&nbsp;"+
                     (actionAllowedDto.AllowEdit?
                     DataTableButton.SettingButton(Url.Action( "permission","role", new { id = c.Id }),"Permission")
                     :string.Empty)
                })
            }, JsonRequestBehavior.AllowGet);

        }
       
        public ActionResult CreateEdit(int? id)
        {
            ViewBag.actionAllowed = actionAllowedDto = ActionAllowed("Role", CurrentUser.Roles.FirstOrDefault(),id> 0 ? 3 : 2);
            RoleDto roleDto = new RoleDto();
            try
            {
                activityLogModel.ActivityName = "Role CreateEdit REQUEST";
                activityLogModel.ActivityPage = "GET:Role/CreateEdit/"+id;
                activityLogModel.Remark = "";
                activityLogModel.UserId = CurrentUser?.UserID ?? 0;
                LogActivity(activityLogModel);
            }
            catch (Exception e)
            {
                LogException(e);
            }
            if (id.HasValue && id.Value > 0)
            {
                Role role = roleService.GetAdminRole(id.Value);
                roleDto.Id = role.Id;
                roleDto.RoleName = role.RoleName;
            }

            return PartialView("_createedit", roleDto);

        }

        [HttpPost]
        public ActionResult CreateEdit(RoleDto model, FormCollection FC)
        {
            ViewBag.actionAllowed = actionAllowedDto = ActionAllowed("Role", CurrentUser.Roles.FirstOrDefault(), model.Id > 0 ? 3 : 2);
            string message = string.Empty;
            try
            {
                activityLogModel.ActivityName = "Role CreateEdit REQUEST";
                activityLogModel.ActivityPage = "POST:Role/CreateEdit/";
                activityLogModel.Remark = "";
                activityLogModel.UserId = CurrentUser?.UserID ?? 0;
                LogActivity(activityLogModel);
            }
            catch (Exception e)
            {
                LogException(e);
            }
            try
            {
                if (ModelState.IsValid)
                {
                    Role role = roleService.GetAdminRole(model.Id) ?? new Role();

                    role.Id = (byte)model.Id;
                    role.RoleName = model.RoleName;
                    roleService.Save(role);

                    ShowSuccessMessage("Success!", "Role has been saved", false);
                }
            }
            catch (Exception Ex)
            {
                var msg = Ex.GetBaseException().ToString();
                if (msg.Contains("UNIQUE KEY"))
                {
                    message = "Role already exist.";
                    ShowErrorMessage("Error!", message, false);
                }
                else
                {
                    message = "An internal error found during to process your requested data!";
                    ShowErrorMessage("Error!", message, false);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            ViewBag.actionAllowed = actionAllowedDto = ActionAllowed("Role", CurrentUser.Roles.FirstOrDefault(), 4);

            try
            {
                activityLogModel.ActivityName = "Role DELETE REQUEST";
                activityLogModel.ActivityPage = "GET:Role/DELETE/"+id;
                activityLogModel.Remark = "";
                activityLogModel.UserId = CurrentUser?.UserID ?? 0;
                LogActivity(activityLogModel);
            }
            catch (Exception e)
            {
                LogException(e);
            }
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are you sure you want to delete this role?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Heading = "Delete Role" },
                Footer = new ModalFooter { SubmitButtonText = "Yes", CancelButtonText = "No" }
            });
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteAdminUser(int id)
        {
            ViewBag.actionAllowed = actionAllowedDto = ActionAllowed("Role", CurrentUser.Roles.FirstOrDefault(), 4);

            try
            {
                activityLogModel.ActivityName = "Role DELETE REQUEST";
                activityLogModel.ActivityPage = "POST:Role/DELETE/" + id;
                activityLogModel.Remark = "";
                activityLogModel.UserId = CurrentUser?.UserID ?? 0;
                LogActivity(activityLogModel);
            }
            catch (Exception e)
            {
                LogException(e);
            }
            try
            {
               
                ShowSuccessMessage("Success","Role cann't be deleted.", false);
            
            }
            catch (Exception)
            {
            }
            return RedirectToAction("index");
        }


        [HttpGet]
        public ActionResult Permission(int id)
        {
            ViewBag.actionAllowed = actionAllowedDto = ActionAllowed("Role", CurrentUser.Roles.FirstOrDefault(), id > 0 ? 3 : 2);

            try
            {
                activityLogModel.ActivityName = "Role Permission REQUEST";
                activityLogModel.ActivityPage = "GET:Role/Permission/" + id;
                activityLogModel.Remark = "";
                activityLogModel.UserId = CurrentUser?.UserID ?? 0;
                LogActivity(activityLogModel);
            }
            catch (Exception e)
            {
                LogException(e);
            }
            PermissionDto permissionDto = new PermissionDto();
            MenuDto menuDto = new MenuDto();

            Role adminRole = id > 0 ? roleService.GetAdminRole(id) : new Role();
            List<Menu> allMenus = roleService.GetMenu().OrderByDescending(x => x.Id).ToList();

            MenuMapDto menuMapDto;

            if (adminRole != null)
            {
                try
                {
                    List<MapMenuToRole> allowedmenus = adminRole.MapMenuToRoles.Where(x => x.RoleId == id).ToList();
                    List<int> menuids = new List<int>();
                    foreach (var menu in allMenus)
                    {
                        menuDto = new MenuDto();
                        menuDto.Id = menu.Id;
                        menuDto.Name = menu.Name;
                        menuDto.MenuId = menu.MenuId;
                        menuDto.ChildMenus = menu.ChildMenus;
                        menuDto.ParentId = menu.ParentId ?? 0;
                        permissionDto.MenuList.Add(menuDto);

                        menuMapDto = new MenuMapDto();
                        menuMapDto.RoleId = id;
                        menuMapDto.MenuId = menu.Id;
                        menuMapDto.IsCreate = false;
                        menuMapDto.IsEdit = false;
                        menuMapDto.IsDelete = false;

                        foreach (var allowedmenu in allowedmenus)
                        {
                            if (menu.Id == allowedmenu.MenuId)
                            {
                                menuids.Add(allowedmenu.MenuId);

                                menuMapDto.IsCreate = allowedmenu.AllowCreate;
                                menuMapDto.IsEdit = allowedmenu.AllowUpdate;
                                menuMapDto.IsDelete = allowedmenu.AllowDelete;
                            }
                        }

                        permissionDto.MenuMapList.Add(menuMapDto);
                    }
                    permissionDto.RoleName = adminRole.RoleName;
                    permissionDto.MenuIds = menuids;
                    permissionDto.CurrentRoleId = id;
                }
                catch (Exception ex)
                {
                    LogException(ex);
                }
            }

            return View(permissionDto);
        }

        [HttpPost]
        public bool Permission(List<MenuMapDto> data)
        {
            try
            {
                activityLogModel.ActivityName = "Role Permission REQUEST";
                activityLogModel.ActivityPage = "POST:Role/Permission/";
                activityLogModel.Remark = "";
                activityLogModel.UserId = CurrentUser?.UserID ?? 0;
                LogActivity(activityLogModel);
            }
            catch (Exception e)
            {
                LogException(e);
            }
            ViewBag.actionAllowed = actionAllowedDto = ActionAllowed("Role", CurrentUser.Roles.FirstOrDefault(), 2);

            string message = string.Empty;
            var Roleid=0;
            try
            {
                 Roleid = data.FirstOrDefault().RoleId;
                roleService.DeleteRolePermission(Roleid);
                List<MapMenuToRole> menulist = new List<MapMenuToRole>();
                foreach (var menuallwed in data.Where(x => x.IsMenuAllow).ToList())
                {
                    MapMenuToRole menu = new MapMenuToRole();
                    menu.RoleId = (byte)menuallwed.RoleId;
                    menu.MenuId = menuallwed.MenuId;
                    menu.AllowCreate = menuallwed.IsCreate;
                    menu.AllowDelete = menuallwed.IsDelete;
                    menu.AllowUpdate = menuallwed.IsEdit;
                    menulist.Add(menu);
                }
                roleService.AddRolePermission(menulist);
                ShowSuccessMessage("Success!", "Menu Permission has been saved", true);

                Cache.RemoveAll();
                return true; // RedirectToAction("Permission", new { id = Roleid });
            }
            catch (Exception ex)
            {
                LogException(ex);
                message = "An internal error found during to process your requested data!";
                ShowErrorMessage("Error!", message, true);
                return false;
            }
        }
    }
}