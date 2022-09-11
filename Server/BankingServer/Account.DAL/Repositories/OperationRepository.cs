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
    public class OperationRepository:IOperationRepository
    {
        private readonly IDbContextFactory<AccountDBContext> _factory;
        public OperationRepository(IDbContextFactory<AccountDBContext> factory)
        {
            _factory = factory;
        }
        public async Task<List<OperationEntity>> GetOperationsByAccountId(int id)
        {
            using var context = _factory.CreateDbContext();
            //Need to make async
            var operations =  context.Operations.ToList().FindAll(operation => operation.AccountId == id);
            return operations;


        }
    }
}
