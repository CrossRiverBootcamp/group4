using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.DAL.Interfaces;
using Transaction.Services.Interfaces;

namespace Transaction.Services.Services
{
    public class UpdateTransactionStatusService: IUpdateTransactionStatusService
    {
        private readonly IUpdateTransactionStatusRepository _transaction;
        public UpdateTransactionStatusService(IUpdateTransactionStatusRepository transaction)
        {
            _transaction = transaction;
        }

        public async Task UpdateReasonFailed(string reason, Guid transactionId)
        {
            await _transaction.UpdateReasonFailed(reason, transactionId);
        }

        //private readonly TransactionService _transactionService;
        public async Task UpdateStatus(bool status, Guid transactionId)
        {
            await _transaction.UpdateTransaction(status, transactionId);
        }
    }
}
