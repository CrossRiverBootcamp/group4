using Account.DAL.Entities;
using Account.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.DAL.Repositories
{
    internal class AccountRepository : IAccountRepository
    {
        private readonly IDbContextFactory<AccountDBContext> _factory;
        public AccountRepository(IDbContextFactory<AccountDBContext> factory)
        {
            _factory = factory;
        }
        public async Task<bool> CheckEmailExists(string email)
        {
            using var context = _factory.CreateDbContext();
            //how can I get await in here with func ANY that returns bool?
                return context.Customers.Any(c => c.Email.Equals(email));
        }
        public async Task<bool> CheckPasswordValid(string email, string password)
        {
            using var context = _factory.CreateDbContext();
                var customer = await context.Customers.FirstAsync(c => c.Email.Equals(email));
            return customer.Password.Equals(password);
        }

        public Task<bool> CheckUniqueEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateAccount(AccountEntity account)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateCustomer(CustomerEntity customer)
        {
            throw new NotImplementedException();
        }
        public async Task<int> GetAccountIdByEmail(string email)
        {
            using var context = _factory.CreateDbContext();
                var customer = await context.Customers.FirstAsync(c => c.Email.Equals(email));
                var account = await context.Accounts.FirstAsync(a => a.CostomerId == customer.Id);
            return account.Id;
        }
        public async Task<AccountEntity> GetAccountInfoByAccountID(int id)
        {
            using var context = _factory.CreateDbContext();
                var account = await context.Accounts.FirstOrDefaultAsync(a => a.Id.Equals(id));
            return account;
        }
    }
}
