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
            return await context.Customers.AnyAsync(c => c.Email.Equals(email));
        }

        public async Task<bool> CheckPasswordValidAsync(string email, string password)
        {
            using var context = _factory.CreateDbContext();
            var customer = await context.Customers.FirstAsync(c => c.Email.Equals(email));
            return customer.Password.Equals(password);

        }

        public async Task<int> CreateAccountAsync(AccountEntity account)
        {
            //try
            //{
                using var context = _factory.CreateDbContext();
                await context.Accounts.AddAsync(account);
                await context.SaveChangesAsync();
                return account.Id;
            //}
            //catch
            //{
            //    throw;
            //}
        }

        public  async Task<int> GetAccountIdByEmailAsync(string email)
        {
            using var context = _factory.CreateDbContext();
            //find customer with email
            var customer = await context.Customers.FirstAsync(c => c.Email.Equals(email));
            //find account id for customer
            var account = await context.Accounts.FirstAsync(a => a.CustomerId == customer.Id);
            return account.Id;
        }

        public async Task<AccountEntity> GetAccountInfoByAccountIdAsync(int id)
        {
            using var context = _factory.CreateDbContext();
            var account = await context.Accounts.Include(a => a.Customer).FirstOrDefaultAsync(a => a.Id.Equals(id));
            //if(account == null)
            //{
            //    throw new Exception("Account doesn't exisit");
            //}
            return account;
        }
        public async Task<CustomerEntity> GetCustomerByAccountId(int accountId)
        {
            using var context = _factory.CreateDbContext();
            AccountEntity account = await context.Accounts.Include(a => a.Customer).FirstAsync(a => a.Id == accountId);
            return account.Customer;
        }

        public async Task<CustomerEntity> GetCustomerByEmailAsync(string email)
        {
            //try { 
            using var context = _factory.CreateDbContext();
            var customer= await context.Customers.FirstOrDefaultAsync(c => c.Email.Equals(email));
                //if (customer != null)
                   return customer;
                //else throw new Exception();
            //}
            //catch
            //{
            //    throw;
            //}
        }
    }
}
