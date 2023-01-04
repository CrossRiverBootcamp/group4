using Account.DAL.Entities;

namespace Account.DAL.Interfaces
{
    public interface IOperationRepository
    {
        Task AddToHistoryTableAsync(OperationEntity opEntityFrom, OperationEntity opEntityTo);
        Task<float> GetAccountBalanceByAccountIdAsync(int id);
        Task<int> GetOtherSideIdAsync(Guid transactionId, int accountId);
        Task<List<OperationEntity>> GetOperationsByAccountIdAsync(int id);
        Task<List<OperationEntity>> getOpertaionsByFilterPageAsync(int accountId,int pageNumber,int numOfRecrds);
        Task<int> countOpertaionsByIdAsync(int accountId);
    }
}
