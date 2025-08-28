using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizGame.Core;
using QuizGame.Data;

namespace QuizGame.Service
{
    public interface IBankAccountService
    {
       
        KeyValuePair<int, List<Data.BankAccount>> GetCompanyBanks(DataTableServerSide searchModel);
        List<Data.BankAccount> GetBanks();

        Data.BankAccount Save(Data.BankAccount bank);
        Data.BankAccount GetCompanyBank(int id);
        ICollection<AccountType> GetAccountTypeList();
        bool IsAccountNoExist(string accountNumber, string ifscCode);
    }
}
