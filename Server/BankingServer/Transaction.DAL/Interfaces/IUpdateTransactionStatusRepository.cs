using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.DAL.Interfaces
{
    public interface IUpdateTransactionStatusRepository
    {
        Task UpdateTransactionAsync(bool status, Guid transactionId);
        Task UpdateReasonFailedAsync(string reason, Guid transactionId);
    }
}
