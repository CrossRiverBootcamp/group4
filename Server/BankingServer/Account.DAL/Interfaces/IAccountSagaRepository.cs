

namespace Account.DAL.Interfaces
{
    public interface IAccountSagaRepository
    {
        Task<bool> CheckIdValidAsync(int id);
        Task<bool> CheckBalanceAsync(int id, int amount);
        Task UpdateBalanceAsync(int fromAccount, int toAccount, int amount);
    }
}
