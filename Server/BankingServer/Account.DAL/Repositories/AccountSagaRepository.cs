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
                using var context = _factory.CreateDbContext();
                return await context.Accounts.AnyAsync(a => a.Id == id);
        }
        //a func that checks if the balance is enough for the amount request in the transaction
        public async Task<bool> CheckBalanceAsync(int id, float amount)
        {
            using var context = _factory.CreateDbContext();
            AccountEntity account = await context.Accounts.FirstAsync(a => a.Id == id);
            return account.Balance >= amount;
        }
        //a func that updates the balance in both accounts
        public async Task UpdateBalanceAsync(int fromAccount, int toAccount, float amount, float amountPercent)
        {
            using var context = _factory.CreateDbContext();
            AccountEntity accountFrom = await context.Accounts.FirstAsync(a => a.Id == fromAccount);
            AccountEntity accountTo = await context.Accounts.FirstAsync(a => a.Id == toAccount);
            accountFrom.Balance -= amount;
            // add to account only the amount that doesn't go to cashbox
            accountTo.Balance += (amount - amountPercent);
            await context.SaveChangesAsync();
        }
    }
}
