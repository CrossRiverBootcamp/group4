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
        private readonly ICashboxService _cashboxService;
        private readonly IMapper _mapper;
        public AccountService(IAccountRepository accountRepository, ICashboxService cashboxService)
        {
            _accountRepository = accountRepository;
            _cashboxService = cashboxService;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AccountInfoMap>();
                cfg.AddProfile<CustomerMapping>();
            });
            _mapper = config.CreateMapper();
        }
        public async Task<int> CreateAccountAsync(CustomerDTO customerDTO, int balanceInit)
        {
            CustomerEntity customer = _mapper.Map<CustomerEntity>(customerDTO);
            //checks if account with email already exists 
            if (await _accountRepository.CheckEmailExistsAsync(customerDTO.Email))
            {
                throw new Exception("An account with this email address aleady exists.");
            }
            //create account and customer (in db)
            AccountEntity account = new AccountEntity();
            account.Customer = customer;
            account.OpenDate = DateTime.UtcNow;
            account.Balance = balanceInit;
            //balanceInit;
            return await _accountRepository.CreateAccountAsync(account);

        }

        public async Task<AccountInfoDTO> GetAccountInfoAsync(int id)
        {
            var account = await _accountRepository.GetAccountInfoByAccountIdAsync(id);
            if (account == null)
            {
                throw new Exception("Account doesn't exisit");
            }
            await _cashboxService.CheckCashboxExists(id);
            AccountInfoDTO accountDTO = _mapper.Map<AccountInfoDTO>(account);
            return accountDTO;
        }

        public async Task<CustomerDTO> GetCustomerByAccountId(int accountId)
        {
            AccountEntity account = await _accountRepository.GetCustomerByAccountId(accountId);
            if (account == null || account.Customer == null)
            {
                throw new Exception("Account doesn't exisit");
            }
            CustomerDTO customer = _mapper.Map<CustomerDTO>(account.Customer);
            return customer;
        }

        public async Task<int> LoginAsync(LoginDTO loginDTO)
        {
            //check if email exists and check that password and email match
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
