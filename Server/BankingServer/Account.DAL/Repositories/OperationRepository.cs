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
    public class OperationRepository : IOperationRepository
    {
        private readonly IDbContextFactory<AccountDBContext> _factory;
        public OperationRepository(IDbContextFactory<AccountDBContext> factory)
        {
            _factory = factory;
        }
        public async Task AddToHistoryTable(OperationEntity opEntityFrom, OperationEntity opEntityTo)
        {
            using var context = _factory.CreateDbContext();
            await context.Operations.AddAsync(opEntityFrom);
            await context.Operations.AddAsync(opEntityTo);
            await context.SaveChangesAsync();
        }
        public async Task<int> GetAccountBalanceByAccountID(int id)
        {
            using var context = _factory.CreateDbContext();
            var account = await context.Accounts.Include(a => a.Customer).FirstOrDefaultAsync(a => a.Id.Equals(id));
            return account.Balance;

        }
    }
}
