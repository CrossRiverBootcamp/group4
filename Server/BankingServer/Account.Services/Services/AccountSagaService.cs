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
      public async Task<bool> CheckIdValid(int id)
        {
            return await _accountSagaRepository.CheckIdValid(id);
            /* if(!await _accountSagaRepository.CheckIdValid(id))
             * return transaction failed Because account id is not valid.
             */
        }
        public async Task<bool> CheckBalance(int id, int amount)
        {
            return await _accountSagaRepository.CheckBalance(id, amount);
        }
        public async Task UpdateBalance(int fromAccount, int toAccount, int amount)
        {
            await _accountSagaRepository.UpdateBalance(fromAccount, toAccount, amount);
        }
    }
}
