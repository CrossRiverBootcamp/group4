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

        public async Task CreateAccount(AccountEntity account)
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

        public  async Task<int> GetAccountIdByEmail(string email)
        {
            using var context = _factory.CreateDbContext();
            var customer = await context.Customers.FirstAsync(c => c.Email.Equals(email));
            var account = await context.Accounts.FirstAsync(a => a.CustomerId == customer.Id);
            return account.Id;

        }

        public async Task<AccountEntity> GetAccountInfoByAccountID(int id)
        {
            using var context = _factory.CreateDbContext();
            var account = await context.Accounts.Include(a => a.Customer).FirstOrDefaultAsync(a => a.Id.Equals(id));
            return account;

        }

        public async Task<CustomerEntity> GetCustomerByEmail(string email)
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
