

namespace Account.DAL.Interfaces
{
    public interface IAccountSagaRepository
    {
        Task<bool> CheckIdValidAsync(int id);
        Task<bool> CheckBalanceAsync(int id, float amount);
        Task UpdateBalanceAsync(int fromAccount, int toAccount, float amount, float amountPercent);
    }
}
