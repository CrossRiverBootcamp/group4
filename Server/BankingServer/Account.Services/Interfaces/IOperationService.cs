using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Services.Interfaces
{
    public interface IOperationService
    {
        Task AddToHistoryTable(TransactionPayload message);
    }
}
