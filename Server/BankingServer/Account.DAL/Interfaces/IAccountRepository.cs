using Account.DAL.Entities;


namespace Account.DAL.Interfaces
{
    public interface IAccountRepository
    {
        Task CreateAccountAsync(AccountEntity account);
        Task<bool> CheckEmailExistsAsync(string email);
        Task<bool> CheckPasswordValidAsync(string email, string password);
        Task<int> GetAccountIdByEmailAsync(string email);
        Task<AccountEntity> GetAccountInfoByAccountIdAsync(int id);
        Task<CustomerEntity> GetCustomerByEmailAsync(string email);
        Task<CustomerEntity> GetCustomerByAccountId(int accountId);

    }
}
