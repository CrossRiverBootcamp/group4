using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.DAL.Interfaces
{
    public interface IAccountSagaRepository
    {
        Task<bool> CheckIdValid(int id);
        Task<bool> CheckBalance(int id, int amount);
        Task UpdateBalance(int fromAccount, int toAccount, int amount);
       
    }
}
