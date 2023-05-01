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
    public class CashboxRepository : ICashboxRepository
    {
        private readonly IDbContextFactory<AccountDBContext> _factory;
        public CashboxRepository(IDbContextFactory<AccountDBContext> factory)
        {
            _factory = factory;
        }
        public async Task<int> CreateCashboxAsync(CashboxEntity cashbox)
        {
            using var context = _factory.CreateDbContext();
            await context.Cashboxes.AddAsync(cashbox);
            await context.SaveChangesAsync();
            return cashbox.Id;
        }
        public async Task<CashboxEntity> GetCashboxAsync(int accountId)
        {
            using var context = _factory.CreateDbContext();
            return await context.Cashboxes.FirstAsync(account => account.AccountId == accountId && account.Active);
        }
        public async Task<bool> CheckCashboxExists(int accountId)
        {
            var context = _factory.CreateDbContext();
            CashboxEntity? cashboxEntity =  await context.Cashboxes.FirstOrDefaultAsync(account => account.AccountId == accountId && account.Active);
            if (cashboxEntity == null)
                return false;
            else
            {
                if(cashboxEntity.ExpirationTime < DateTime.UtcNow) 
                { 
                    if(cashboxEntity.Amount > 0)
                    {
                        AccountEntity accountEntity = await context.Accounts.FirstOrDefaultAsync(account => account.Id == accountId);
                        accountEntity.Balance += cashboxEntity.Amount;
                        cashboxEntity.Amount = 0;
                    }
                    cashboxEntity.Active = false;
                    await context.SaveChangesAsync();
                }
                return cashboxEntity.Active;
            }
        }
        public async Task<bool> CloseCashbox(int accountId)
        {
            var context = _factory.CreateDbContext();
            CashboxEntity cashboxEntity = await context.Cashboxes.FirstAsync(account => account.AccountId == accountId && account.Active);
            cashboxEntity.Active = false;
            AccountEntity accountEntity = await context.Accounts.FirstOrDefaultAsync(account => account.Id == accountId);
            accountEntity.Balance += cashboxEntity.Amount;
            cashboxEntity.Amount = 0;
            await context.SaveChangesAsync();
            return true;
        }
        public async Task UpdateCahboxAsync(int accountId, CashboxEntity cashbox)
        {
            var context = _factory.CreateDbContext();
            CashboxEntity cashboxEntity = await context.Cashboxes.FirstAsync(account => account.AccountId == accountId && account.Active);
            cashboxEntity = cashbox;
            await context.SaveChangesAsync();
        }
        //update amount in cashbox
        public async Task UpdateAmountInCahboxAsync(int accountId, float addition)
        {
            var context = _factory.CreateDbContext();
            CashboxEntity cashboxEntity = await context.Cashboxes.FirstAsync(account => account.AccountId == accountId && account.Active);
            cashboxEntity.Amount += addition;
            await context.SaveChangesAsync();
        }

    }
}
