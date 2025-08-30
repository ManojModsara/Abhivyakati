using QuizGame.Core;
using QuizGame.Data;
using QuizGame.Dto;
using QuizGame.Service;
using QuizGame.Service.BankAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizGame.Web.Controllers
{
    public class BankAccountController : BaseController
    {
        public ActionAllowedDto action;
        private IBankAccountService bankAccountService;
        ActivityLogDto activityLogModel;
        public BankAccountController(IBankAccountService _bankAccountService, IActivityLogService _activityLogService, IRoleService roleService) : base(_activityLogService, roleService)
        {
            this.bankAccountService = _bankAccountService;
            this.activityLogModel = new ActivityLogDto();
            this.action = new ActionAllowedDto();
        }

        public ActionResult Index()
        {
            ViewBag.actionAllowed = action = ActionAllowed("BankAccount", CurrentUser.Roles.FirstOrDefault());
            try
            {
                activityLogModel.ActivityName = "Bank List REQUEST";
                activityLogModel.ActivityPage = "GET:BankAccount/Index/";
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
        public ActionResult GetCompanyBanks(DataTableServerSide model)
        {
            ViewBag.actionAllowed = action = ActionAllowed("BankAccount", CurrentUser.Roles.FirstOrDefault());

            KeyValuePair<int, List<BankAccount>> users = bankAccountService.GetCompanyBanks(model);
            return Json(new
            {
                draw = model.draw,
                recordsTotal = users.Key,
                recordsFiltered = users.Key,
                data = users.Value.Select(c => new List<object> {

                    c.Id,
                    c?.BankName??"",
                    c?.AccountNo??"",
                    c?.IFSCCode??"",
                    c?.HolderName??"",
                    c?.UpiAdress??"",
                    c?.BranchName??"",
                    c?.BranchAddress??"",
                    c?.Remark??"",
                    "",
                    //c?.AccountType?.Name ?? "",
                    c?.IsActive?? false,
                    c?.AddedDate?.ToString() ?? "",
                    c?.UpdatedDate?.ToString() ?? "",
                     (action.AllowEdit? DataTableButton.EditButton(Url.Action( "CreateEdit", "BankAccount",new { id = c.Id })):string.Empty)
                   // +"&nbsp;"+
                   //(actionAllowedDto.AllowDelete? DataTableButton.DeleteButton(Url.Action( "delete","user", new { id = c.Id }),"modal-delete-adminuser"):string.Empty)
                })
            }, JsonRequestBehavior.AllowGet);


        }
        public ActionResult GetBanks()
        {
            var banks = bankAccountService.GetBanks()
                        .Select(i => new AddEditBankAccountDto
                        {
                            Id = i.Id,
                            AccountHolderName = i?.HolderName ?? "",
                            AccountNumber = i?.AccountNo ?? "",
                            IFSCCode = i?.IFSCCode ?? "",
                            BankName = i?.BankName ?? "",
                            UpiAddress = i?.UpiAdress ?? ""
                        }).ToList();

            return PartialView("_showBank", banks);
        }
        public bool Active(int id)
        {
            try
            {
                var bank = bankAccountService.GetCompanyBank(id);
                bank.IsActive = !bank.IsActive;
                return bankAccountService.Save(bank).IsActive;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ActionResult CreateEdit(int? id)
        {
            ViewBag.actionAllowed = action = ActionAllowed("BankAccount", CurrentUser.Roles.FirstOrDefault(), id.HasValue ? 3 : 2);
            try
            {
                activityLogModel.ActivityName = "Bank Edit REQUEST";
                activityLogModel.ActivityPage = "GET:BankAccount/CreateEdit/" + id;
                activityLogModel.Remark = "";
                activityLogModel.UserId = CurrentUser?.UserID ?? 0;
                LogActivity(activityLogModel);
            }
            catch (Exception e)
            {
                LogException(e);
            }

            AddEditBankAccountDto bankAccountDto = new AddEditBankAccountDto();
            BankAccount bank;

            if (id.HasValue && id.Value > 0)
            {
                bank = bankAccountService.GetCompanyBank(id.Value);
                bankAccountDto.Id = bank.Id;
                bankAccountDto.AccountTypeId = bank?.AccountTypeId ?? 0;
                bankAccountDto.UserId = bank?.UserId ?? 0;
                bankAccountDto.BankName = bank.BankName;
                bankAccountDto.AccountHolderName = bank.HolderName;
                bankAccountDto.AccountNumber = bank.AccountNo;
                bankAccountDto.IsActive = bank?.IsActive ?? false;
                bankAccountDto.IFSCCode = bank.IFSCCode;
                bankAccountDto.BranchAddress = bank?.BranchAddress ?? "";
                bankAccountDto.BranchName = bank?.BranchName ?? "";
                bankAccountDto.UpiAddress = bank?.UpiAdress ?? "";
                bankAccountDto.Remark = bank?.Remark ?? "";
            }
            ViewBag.AccountTypes = bankAccountService.GetAccountTypeList();
            return View("createedit", bankAccountDto);
        }
        [HttpPost]
        public ActionResult CreateEdit(AddEditBankAccountDto model)
        {
            ViewBag.actionAllowed = action = ActionAllowed("BankAccount", CurrentUser.Roles.FirstOrDefault(), model.Id > 0 ? 3 : 2);
            try
            {
                activityLogModel.ActivityName = "Bank Edit REQUEST";
                activityLogModel.ActivityPage = "Post:BankAccount/CreateEdit/" + model.Id;
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
                    BankAccount bank = bankAccountService.GetCompanyBank(model.Id) ?? new BankAccount();
                    if (model.Id == 0)
                    {
                        bool isAccountNoAlreadyExist = bankAccountService.IsAccountNoExist(model.AccountNumber.Trim(), model.IFSCCode.Trim());
                        if (isAccountNoAlreadyExist)
                        {
                            ShowErrorMessage("Error!", "AccountNumber Already Exist for this IFSCCODE.", true);
                            ViewBag.AccountTypes = bankAccountService.GetAccountTypeList();
                            return View(model);
                        }
                    }

                    bank.Id = model.Id;
                    bank.BankName = model.BankName;
                    bank.AccountNo = model.AccountNumber;
                    bank.IFSCCode = model.IFSCCode;
                    bank.HolderName = model.AccountHolderName;
                    bank.UpiAdress = model.UpiAddress;
                    bank.Remark = model.Remark;
                    bank.BranchName = model.BranchName;
                    bank.BranchAddress = model.BranchAddress;
                    bank.UserId = model.Id > 0 ? model.UserId : CurrentUser.UserID;
                    bank.AccountTypeId = model.AccountTypeId;
                    if (model.Id == 0)
                        bank.AddedById = CurrentUser.UserID;
                    else
                        bank.UpdatedById = CurrentUser.UserID;
                    bankAccountService.Save(bank);
                    ShowSuccessMessage("Success!", "Bank has been saved", false);
                    return RedirectToAction("Index");
                }
                else
                {
                    ShowErrorMessage("Error!", "Fill All Required fields.", true);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                ShowErrorMessage("Error!", "Something went wrong during Bank Save Process.", true);
            }
            ViewBag.AccountTypes = bankAccountService.GetAccountTypeList();
            return View(model);
        }
    }
}