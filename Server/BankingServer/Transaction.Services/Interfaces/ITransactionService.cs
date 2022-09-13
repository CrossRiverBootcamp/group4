using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.DTO;

namespace Transaction.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<bool> SendTransactionAsync(TransactionDto transactionDto, IMessageSession messageSession);
    }
}
