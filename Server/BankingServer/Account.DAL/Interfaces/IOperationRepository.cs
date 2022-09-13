using Account.DAL.Entities;


namespace Account.DAL.Interfaces
{
    public interface IOperationRepository
    {
        Task AddToHistoryTableAsync(OperationEntity opEntityFrom, OperationEntity opEntityTo);
        Task<int> GetAccountBalanceByAccountIdAsync(int id);
        Task<int> GetOtherSideIdAsync(Guid transactionId, int accountId);
        Task<List<OperationEntity>> GetOperationsByAccountIdAsync(int id);
    }
}
