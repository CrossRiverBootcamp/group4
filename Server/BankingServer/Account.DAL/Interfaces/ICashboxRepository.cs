using Account.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Account.DAL.Interfaces
{
    public interface ICashboxRepository
    {
        Task<int> CreateCashboxAsync(CashboxEntity cashbox);
        Task<CashboxEntity> GetCashboxAsync(int accountId);
        Task<bool> CheckCashboxExists(int accountId);
        Task<bool> CloseCashbox(int accountId);
        Task UpdateCahboxAsync(int accountId, CashboxEntity cashbox);
        Task UpdateAmountInCahboxAsync(int accountId, float addition);
    }
}