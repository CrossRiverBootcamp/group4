using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.DAL.Entities;
using Account.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Account.DAL.Repositories
{
    public class AccountRepository : IAccountRepository

    {
        private readonly IDbContextFactory<AccountDBContext> _factory;
        public AccountRepository(IDbContextFactory<AccountDBContext> factory)
        {
            _factory = factory;
        }
        public Task<bool> CheckEmailExists(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckPasswordValid(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckUniqueEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateAccount(AccountEntity account)
        {
            try
            {
                using var context = _factory.CreateDbContext();
                await context.Accounts.AddAsync(account);
                context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }


        }

        public async Task<bool> CreateCustomer(CustomerEntity customer)
        {
            try
            {
                using var context = _factory.CreateDbContext();
                await context.Customers.AddAsync(customer);
                context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public Task<int> GetAccountIdByEmailAndPassword(string email)
        {
            throw new NotImplementedException();
        }

        public Task<AccountEntity> GetAccountInfoByAccountID(int id)
        {
            throw new NotImplementedException();
        }
    }
}
