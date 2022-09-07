using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.DAL.Entities;

namespace Transaction.DAL.Interfaces
{
    public interface ITransactionRepository
    {
        Task addTransaction(TransactionEntity transaction);
    }
}
