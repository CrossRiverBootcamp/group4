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
            return await context.Cashboxes.FirstAsync(c => c.AccountId == accountId);
        }
        public async Task<bool> CheckCashboxExists(int accountId)
        {
            var context = _factory.CreateDbContext();
            return await context.Cashboxes.AnyAsync(account => account.AccountId == accountId);
        }
        public async Task UpdateCahboxAsync(int accountId, CashboxEntity cashbox)
        {
            var context = _factory.CreateDbContext();
            CashboxEntity cashboxEntity = await context.Cashboxes.FirstAsync(account => account.AccountId == accountId);
            cashboxEntity = cashbox;
            await context.SaveChangesAsync();
        }
    }
}
