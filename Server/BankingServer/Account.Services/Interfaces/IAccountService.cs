using Account.DTO;

namespace Account.Services.Interfaces
{
    public interface IAccountService
    {
        Task CreateAccountAsync(CustomerDTO customerDTO);
        Task<int> LoginAsync(LoginDTO loginDTO);
        Task<AccountInfoDTO> GetAccountInfoAsync(int id);
        Task<CustomerDTO> GetCustomerByAccountId(int accountId);
    }
}
