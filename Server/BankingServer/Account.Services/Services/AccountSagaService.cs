using Account.DAL.Interfaces;
using Account.Services.Interfaces;

namespace Account.Services.Services
{
    public class AccountSagaService: IAccountSagaService
    {
        private readonly IAccountSagaRepository _accountSagaRepository;
        public AccountSagaService(IAccountSagaRepository accountSagaRepository)
        {
            _accountSagaRepository = accountSagaRepository;
        }
      public async Task<bool> CheckIdValidAsync(int id)
        {
            return await _accountSagaRepository.CheckIdValidAsync(id);
           
        }
        public async Task<bool> CheckBalanceAsync(int id, int amount)
        {
            return await _accountSagaRepository.CheckBalanceAsync(id, amount);
        }
        public async Task UpdateBalanceAsync(int fromAccount, int toAccount, int amount)
        {
            await _accountSagaRepository.UpdateBalanceAsync(fromAccount, toAccount, amount);
        }
    }
}
