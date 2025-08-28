using QuizGame.Core;
using QuizGame.Data;
using QuizGame.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Service.BankAccount
{
    public class BankAccountService : IBankAccountService
    {

        #region "Fields"
        private IRepository<Data.BankAccount> repoBankAccount;
        private IRepository<Role> repoAdminRole;
        private IRepository<AccountType> repoAccountType;
        #endregion

        #region "Constructor"
        public BankAccountService(IRepository<Data.BankAccount> _repoBankAccount, IRepository<Role> _repoAdminRole, IRepository<AccountType> _repoAccountType)
        {
            this.repoBankAccount = _repoBankAccount;
            this.repoAdminRole = _repoAdminRole;
            this.repoAccountType = _repoAccountType;
        }
        #endregion

        public KeyValuePair<int, List<Data.BankAccount>> GetCompanyBanks(DataTableServerSide searchModel)
        {
            var predicate = CustomPredicate.BuildPredicate<Data.BankAccount>(searchModel, new Type[] { typeof(Data.BankAccount), typeof(AccountType) });

            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);

            List<Data.BankAccount> results = repoBankAccount
                .Query()
                .Filter(predicate.And(a => a.UserId == 1))
                .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(Data.BankAccount), typeof(AccountType) }))
                .GetPage(page, searchModel.length, out totalCount)
                .ToList();

            KeyValuePair<int, List<Data.BankAccount>> resultResponse = new KeyValuePair<int, List<Data.BankAccount>>(totalCount, results);

            return resultResponse;
        }

        public List<Data.BankAccount> GetBanks()
        {
            return repoBankAccount.Query()
                .Filter(x => x.IsActive == true).Get()
                .ToList();
        }
        public Data.BankAccount GetCompanyBank(int id)
        {
            return repoBankAccount.FindById(id);
        }

        public Data.BankAccount Save(Data.BankAccount bank)
        {
            if (bank.Id == 0)
            {
                bank.AddedDate = DateTime.Now;
                repoBankAccount.Insert(bank);
            }
            else
            {
                bank.UpdatedDate = DateTime.Now;
                repoBankAccount.Update(bank);
            }
            return bank;
        }
        public ICollection<AccountType> GetAccountTypeList()
        {
            return repoAccountType.Query().AsTracking().Get().ToList();
        }

        public bool IsAccountNoExist(string accountNumber, string ifscCode)
        {
            return repoBankAccount.Query().Get().Any(x => x.AccountNo == accountNumber && x.IFSCCode == ifscCode);
        }
    }
}
