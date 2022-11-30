using Account.DAL.Entities;
using Account.DTO;

namespace Account.Services.Interfaces
{
    public interface ICashboxService
    {
        Task<int> CreateCashboxAsync(CashboxDTO cashboxDTO);
        Task<CashboxDTO> GetCashboxAsync(int accountId);
        Task<bool> CheckCashboxExists(int accountId);
        void UpdateCahboxAsync(int accountId, CashboxDTO cashboxDTO);
    }
}