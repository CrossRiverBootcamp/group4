namespace Account.Services.Interfaces
{
    public interface IAccountSagaService
    {
        Task<bool> CheckIdValidAsync(int id);
        Task<bool> CheckBalanceAsync(int id, float amount);
        Task UpdateBalanceAsync(int fromAccount, int toAccount, float amount);
    }
}
