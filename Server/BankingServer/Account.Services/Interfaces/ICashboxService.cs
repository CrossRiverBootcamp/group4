using Account.DAL.Entities;
using Account.DTO;

namespace Account.Services.Interfaces
{
    public interface ICashboxService
    {
        Task<int> CreateCashboxAsync(CashboxDTO cashboxDTO);
        Task<CashboxDTO> GetCashboxAsync(int accountId);
        Task<bool> CheckCashboxExists(int accountId);
        Task<bool> CloseCashbox(int accountId);
        void UpdateCahboxAsync(int accountId, CashboxDTO cashboxDTO);
        Task<int> GetCashboxPercentsAsync(int accountId);
        void UpdateAmountInCahboxAsync(int accountId, float additionToCashboxAmount);
    }
}