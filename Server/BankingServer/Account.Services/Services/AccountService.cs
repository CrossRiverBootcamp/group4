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
        public async Task CreateAccountAsync(CustomerDTO customerDTO,int balanceInit)
        {
            try
            {
                //check verificatin code match
                CustomerEntity customer = _mapper.Map<CustomerEntity>(customerDTO);
                if (await _accountRepository.CheckEmailExistsAsync(customerDTO.Email))
                {
                    throw new Exception("An account with this email address aleady exists.");
                }
                AccountEntity account = new AccountEntity();
                account.Customer = customer;
                account.OpenDate = DateTime.UtcNow;
                //i want to input here from appsettings.json instead of hardcoding
                account.Balance = balanceInit;

                await _accountRepository.CreateAccountAsync(account);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
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
            string email = loginDTO.Email;
            string password = loginDTO.Password;
            if(await _accountRepository.CheckEmailExistsAsync(email) && await _accountRepository.CheckPasswordValidAsync(email, password))
            {
                //where to validate that get back right response?
               return await _accountRepository.GetAccountIdByEmailAsync(email);
            }
            else
            {
                throw new Exception("not valid email or password");
            }
        }
    }
}
