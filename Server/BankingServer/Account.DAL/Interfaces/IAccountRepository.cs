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
        Task CreateAccountAsync(AccountEntity account);
        Task<bool> CheckEmailExistsAsync(string email);
        Task<bool> CheckPasswordValidAsync(string email, string password);
        Task<int> GetAccountIdByEmailAsync(string email);
        Task<AccountEntity> GetAccountInfoByAccountIdAsync(int id);
        Task<CustomerEntity> GetCustomerByEmailAsync(string email);

    }
}
