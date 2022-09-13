using Account.DAL.Entities;
using Account.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
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
        public async Task<bool> CheckEmailExistsAsync(string email)
        {

            using var context = _factory.CreateDbContext();
            //how can I get await in here with func ANY that returns bool?
            return await context.Customers.AnyAsync(c => c.Email.Equals(email));

        }

        public async Task<bool> CheckPasswordValidAsync(string email, string password)
        {
            using var context = _factory.CreateDbContext();
            var customer = await context.Customers.FirstAsync(c => c.Email.Equals(email));
            return customer.Password.Equals(password);

        }

        public async Task CreateAccountAsync(AccountEntity account)
        {
            try
            {
                using var context = _factory.CreateDbContext();
                await context.Accounts.AddAsync(account);
                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }


        }

        public  async Task<int> GetAccountIdByEmailAsync(string email)
        {
            using var context = _factory.CreateDbContext();
            var customer = await context.Customers.FirstAsync(c => c.Email.Equals(email));
            var account = await context.Accounts.FirstAsync(a => a.CustomerId == customer.Id);
            return account.Id;

        }

        public async Task<AccountEntity> GetAccountInfoByAccountIdAsync(int id)
        {
            using var context = _factory.CreateDbContext();
            var account = await context.Accounts.Include(a => a.Customer).FirstOrDefaultAsync(a => a.Id.Equals(id));
            return account;

        }

        public async Task<CustomerEntity> GetCustomerByEmailAsync(string email)
        {
            try { 
            using var context = _factory.CreateDbContext();
            var customer= await context.Customers.FirstOrDefaultAsync(c => c.Email.Equals(email));
                if (customer != null)
                    return customer;
                else throw new Exception();
            }
            catch
            {
                throw;
            }
             
        }
    }
}
