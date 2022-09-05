using Account.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.DAL.Interfaces
{
    public interface IAccountRepository
    {
        Task<bool> CreateCustomer(CustomerEntity customer);
        Task<bool> CreateAccount(AccountEntity account);
        Task<bool> CheckEmailExists(string email);
        Task<bool> CheckPasswordValid(string email, string password);
        Task<int> GetAccountIdByEmail(string email);
        Task<AccountEntity> GetAccountInfoByAccountID(int id);

    }
}
