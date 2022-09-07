using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Services.Interfaces
{
    public interface IAccountSagaService
    {
        Task<bool> CheckIdValid(int id);
        Task<bool> CheckBalance(int id, int amount);
        Task UpdateBalance(int fromAccount, int toAccount, int amount);
    }
}
