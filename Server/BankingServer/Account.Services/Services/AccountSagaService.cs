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
        //a func for checking if the balance is enough for the amount request in the transaction
        public async Task<bool> CheckBalanceAsync(int id, float amount)
        {
            return await _accountSagaRepository.CheckBalanceAsync(id, amount);
        }
        //a func that updates the balance in both accounts
        public async Task UpdateBalanceAsync(int fromAccount, int toAccount, float amount)
        {
            //check if cashbox exists
            if(await _cashboxService.CheckCashboxExists(toAccount))
            {
                //get the percentage of the casbox settings in order to know the amount to put aside in the cashbox
                int percent = await _cashboxService.GetCashboxPercentsAsync(toAccount);
                //calculate the amount to put aside in cashbox
                float amountPercent = ((float)((float)percent / 100)) * (float)amount;
                //update the general balance
                await _accountSagaRepository.UpdateBalanceAsync(fromAccount, toAccount, amount, amountPercent);
                //update the amount in cashbox
                _cashboxService.UpdateAmountInCahboxAsync(toAccount, amountPercent);
            }
            else
                await _accountSagaRepository.UpdateBalanceAsync(fromAccount, toAccount, amount, 0);
        }
    }
}
