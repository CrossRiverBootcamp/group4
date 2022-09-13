using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Services.Interfaces
{
    public interface IUpdateTransactionStatusService
    {
        Task UpdateStatusAsync(bool status, Guid transactionId);
        Task UpdateReasonFailedAsync(string reason, Guid transactionId); 
    }
}
