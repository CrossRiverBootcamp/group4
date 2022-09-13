using Account.DAL.Interfaces;
using Account.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            /* if(!await _accountSagaRepository.CheckIdValid(id))
             * return transaction failed Because account id is not valid.
             */
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
