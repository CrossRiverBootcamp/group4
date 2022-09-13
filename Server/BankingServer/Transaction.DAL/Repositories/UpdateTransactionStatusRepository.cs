﻿using Microsoft.EntityFrameworkCore;
using Transaction.DAL.Entities;
using Transaction.DAL.Interfaces;

namespace Transaction.DAL.Repositories
{
    public class UpdateTransactionStatusRepository : IUpdateTransactionStatusRepository
    {
        private readonly IDbContextFactory<TransactionDBContext> _factory;
        public UpdateTransactionStatusRepository(IDbContextFactory<TransactionDBContext> factory)
        {
            _factory = factory;
        }

        public async Task UpdateReasonFailedAsync(string reason, Guid transactionId)
        {
            using var context = _factory.CreateDbContext();
            var transaction = await context.Transactions.FirstOrDefaultAsync(t => t.Id.Equals(transactionId));
            if (transaction == null)
            {
                throw new Exception("Couldn't find transaction");
            }
            transaction.FailureReason = reason;
            await context.SaveChangesAsync();

        }

        public async Task UpdateTransactionAsync(bool status, Guid transactionId)
        {
            using var context = _factory.CreateDbContext();
            var transaction = await context.Transactions.FirstOrDefaultAsync(t => t.Id.Equals(transactionId));
            if (transaction == null)
            {
                throw new InvalidOperationException();
            }
            else
            {
                if (status)
                {
                    transaction.Status = TransactionStatus.Succeeded;
                }
                else
                {
                    transaction.Status = TransactionStatus.Failed;
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
