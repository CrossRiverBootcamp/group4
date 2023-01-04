using Account.DAL.Entities;
using Account.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Account.DAL.Repositories
{
    public class OperationRepository : IOperationRepository
    {
        private readonly IDbContextFactory<AccountDBContext> _factory;
        public OperationRepository(IDbContextFactory<AccountDBContext> factory)
        {
            _factory = factory;
        }
        public async Task AddToHistoryTableAsync(OperationEntity opEntityFrom, OperationEntity opEntityTo)
        {
            using var context = _factory.CreateDbContext();
            await context.Operations.AddAsync(opEntityFrom);
            await context.Operations.AddAsync(opEntityTo);
            await context.SaveChangesAsync();
        }

        public async Task<int> countOpertaionsByIdAsync(int accountId)
        {
            using var context = _factory.CreateDbContext();
            return await context.Operations.CountAsync(operation => operation.AccountId == accountId);
        }

        public async Task<float> GetAccountBalanceByAccountIdAsync(int id)
        {
            using var context = _factory.CreateDbContext();
            var account = await context.Accounts.FirstOrDefaultAsync(a => a.Id.Equals(id));
            if(account == null)
            {
                throw new Exception("Account doesn't exist");
            }
            return account.Balance;
        }

        public async Task<List<OperationEntity>> GetOperationsByAccountIdAsync(int id)
        {
            using var context = _factory.CreateDbContext();
            var operations =await context.Operations.Where(operation => operation.AccountId == id).ToListAsync();
            return operations;
        }

        public async Task<List<OperationEntity>> getOpertaionsByFilterPageAsync(int accountId, int pageNumber, int numOfRecrds)
        {
            //get operations by pagination requirments
            using var context = _factory.CreateDbContext();
            List<OperationEntity> operationList = await context.Operations
                .Where(operation => operation.AccountId == accountId)
                .Skip((pageNumber * numOfRecrds )).Take(numOfRecrds).ToListAsync();
            return operationList;
        }

        public async Task<int> GetOtherSideIdAsync(Guid transactionId, int accountId)
        {
            using var context = _factory.CreateDbContext();
            OperationEntity operation = await context.Operations.FirstAsync(a => a.TransactionId == transactionId && a.AccountId != accountId);
            return operation.AccountId;
        }
    }
}
