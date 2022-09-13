using Account.DAL.Entities;
using Account.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Account.DAL.Repositories
{
    public class AccountSagaRepository : IAccountSagaRepository
    {
        private readonly IDbContextFactory<AccountDBContext> _factory;
        public AccountSagaRepository(IDbContextFactory<AccountDBContext> factory)
        {
            _factory = factory;
        }
        public async Task<bool> CheckIdValidAsync(int id)
        {
            try
            {
                using var context = _factory.CreateDbContext();
                return await context.Accounts.AnyAsync(a => a.Id == id);
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> CheckBalanceAsync(int id, int amount)
        {
            using var context = _factory.CreateDbContext();
            AccountEntity account = await context.Accounts.FirstAsync(a => a.Id == id);
            return account.Balance >= amount;
        }
        public async Task UpdateBalanceAsync(int fromAccount, int toAccount, int amount)
        {
            using var context = _factory.CreateDbContext();
            AccountEntity accountFrom = await context.Accounts.FirstAsync(a => a.Id == fromAccount);
            AccountEntity accountTo = await context.Accounts.FirstAsync(a => a.Id == toAccount);
            accountFrom.Balance -= amount;
            accountTo.Balance += amount;
            await context.SaveChangesAsync();
        }
    }
}
