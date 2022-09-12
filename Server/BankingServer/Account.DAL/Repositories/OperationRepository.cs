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

        public async Task<int> countOpertaionsById(int accountId)
        {
            using var context = _factory.CreateDbContext();
            return await context.Operations.CountAsync(operation => operation.AccountId == accountId);
        }

        public async Task<int> GetAccountBalanceByAccountID(int id)
        {
            using var context = _factory.CreateDbContext();
            var account = await context.Accounts.Include(a => a.Customer).FirstOrDefaultAsync(a => a.Id.Equals(id));
            return account.Balance;
        }

        public async Task<List<OperationEntity>> GetOperationsByAccountId(int id)
        {
            using var context = _factory.CreateDbContext();
            var operations =await context.Operations.Where(operation => operation.AccountId == id).ToListAsync();
            return operations;
        }

        public async Task<List<OperationEntity>> getOpertaionsByFilterPage(int accountId, int pageNumber, int numOfRecrds)
        {
            using var context = _factory.CreateDbContext();
            List<OperationEntity> operationList = await context.Operations
                .Where(operation => operation.AccountId == accountId)
                .Skip((pageNumber - 1) * numOfRecrds + 1).Take(numOfRecrds).ToListAsync();
            return operationList;
        }

        public async Task<int> GetOtherSideId(Guid transactionId, int accountId)
        {
            using var context = _factory.CreateDbContext();
            OperationEntity operation = await context.Operations.FirstAsync(a => a.TransactionId == transactionId && a.AccountId != accountId);
            return operation.AccountId;
        }
    }
}
