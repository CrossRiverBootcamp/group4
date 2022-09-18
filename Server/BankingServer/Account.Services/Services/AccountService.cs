using Account.DAL.Entities;
using Account.DAL.Interfaces;
using Account.DTO;
using Account.Services.Interfaces;
using Account.Services.Mapping;
using AutoMapper;

namespace Account.Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AccountInfoMap>();
                cfg.AddProfile<CustomerMapping>();
            });
            _mapper = config.CreateMapper();
        }
        public async Task CreateAccountAsync(CustomerDTO customerDTO, int balanceInit)
        {
            CustomerEntity customer = _mapper.Map<CustomerEntity>(customerDTO);
            if (await _accountRepository.CheckEmailExistsAsync(customerDTO.Email))
            {
                throw new Exception("An account with this email address aleady exists.");
            }
            AccountEntity account = new AccountEntity();
            account.Customer = customer;
            account.OpenDate = DateTime.UtcNow;
            account.Balance = balanceInit;

        }

        public async Task<AccountInfoDTO> GetAccountInfoAsync(int id)
        {
            var account = await _accountRepository.GetAccountInfoByAccountIdAsync(id);
            AccountInfoDTO accountDTO = _mapper.Map<AccountInfoDTO>(account);
            return accountDTO;
        }

        public async Task<CustomerDTO> GetCustomerByAccountId(int accountId)
        {
            CustomerDTO customer = _mapper.Map<CustomerDTO>(await _accountRepository.GetCustomerByAccountId(accountId));
            return customer;
        }

        public async Task<int> LoginAsync(LoginDTO loginDTO)
        {
            if (await _accountRepository.CheckEmailExistsAsync(loginDTO.Email) && await _accountRepository.CheckPasswordValidAsync(loginDTO.Email, loginDTO.Password))
            {
                return await _accountRepository.GetAccountIdByEmailAsync(loginDTO.Email);
            }
            else
            {
                throw new Exception("Email or password not valid");
            }
        }
    }
}
