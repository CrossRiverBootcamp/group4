using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Services.Interfaces
{
    public interface IUpdateTransactionStatusService
    {
        Task UpdateStatus(bool status, Guid transactionId);
        Task UpdateReasonFailed(string reason, Guid transactionId); 
    }
}
