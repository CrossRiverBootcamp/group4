using Account.DAL.Interfaces;
using Account.Services.Interfaces;

namespace Account.Services.Services
{
    public class AccountSagaService: IAccountSagaService
    {
        private readonly IAccountSagaRepository _accountSagaRepository;
        private readonly ICashboxService _cashboxService;
        public AccountSagaService(IAccountSagaRepository accountSagaRepository, ICashboxService cashboxService)
        {
            _accountSagaRepository = accountSagaRepository;
            _cashboxService = cashboxService;
        }
        public async Task<bool> CheckIdValidAsync(int id)
        {
            return await _accountSagaRepository.CheckIdValidAsync(id);
        }
        public async Task<bool> CheckBalanceAsync(int id, float amount)
        {
            return await _accountSagaRepository.CheckBalanceAsync(id, amount);
        }
        public async Task UpdateBalanceAsync(int fromAccount, int toAccount, float amount)
        {
            if(await _cashboxService.CheckCashboxExists(toAccount))
            {
                int percent = await _cashboxService.GetCashboxPercentsAsync(toAccount);
                float amountPercent = ((float)((float)percent / 100)) * (float)amount;
                await _accountSagaRepository.UpdateBalanceAsync(fromAccount, toAccount, amount, amountPercent);
                _cashboxService.UpdateAmountInCahboxAsync(toAccount, amountPercent);
            }
            else
                await _accountSagaRepository.UpdateBalanceAsync(fromAccount, toAccount, amount, 0);
        }
    }
}
