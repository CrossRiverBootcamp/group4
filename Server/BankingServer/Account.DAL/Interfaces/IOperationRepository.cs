using Account.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.DAL.Interfaces
{
    public interface IOperationRepository
    {
        Task AddToHistoryTable(OperationEntity opEntityFrom, OperationEntity opEntityTo);
        Task<int> GetAccountBalanceByAccountID(int id);
        Task<int>  GetOtherSideId(Guid transactionId, int accountId);
        Task<List<OperationEntity>> GetOperationsByAccountId(int id);
        Task<List<OperationEntity>> getOpertaionsByFilterPage(int accountId,int pageNumber,int numOfRecrds);
        Task<int> countOpertaionsById(int accountId);
    }
}
