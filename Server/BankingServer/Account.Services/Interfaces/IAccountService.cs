using Account.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Services.Interfaces
{
    public interface IAccountService
    {
        Task CreateAccountAsync(CustomerDTO customerDTO);
        Task<int> LoginAsync(LoginDTO loginDTO);
        Task<AccountInfoDTO> GetAccountInfoAsync(int id);
    }
}
