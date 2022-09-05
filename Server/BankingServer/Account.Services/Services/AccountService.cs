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
        public async Task<bool> CreateAccount(CustomerDTO customerDTO)
        {
            try
            {
                CustomerEntity customer = _mapper.Map<CustomerEntity>(customerDTO);
                //if (await _accountRepository.CheckEmailExists(customerDTO.Email))
                //{
                //    return false;
                //}
                var createdCustomer = await _accountRepository.CreateCustomer(customer);
                CustomerEntity newCustomer = await _accountRepository.GetCustomerByEmail(customer.Email);
                AccountEntity account = new AccountEntity();
                account.CustomerId =newCustomer.Id;
                account.Customer = newCustomer;
                account.OpenDate = DateTime.UtcNow;
                //i want to input here from appsettings.json instead of hardcoding
                account.Balance = 1000;
                var createdAccount = await _accountRepository.CreateAccount(account);
                if (createdAccount && createdCustomer)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<AccountInfoDTO> GetAccountInfo(int id)
        {
            var account = await _accountRepository.GetAccountInfoByAccountID(id);
            AccountInfoDTO accountDTO = _mapper.Map<AccountInfoDTO>(account);
            return accountDTO;
        }

        public async Task<int> Login(LoginDTO loginDTO)
        {
            string email = loginDTO.Email;
            string password = loginDTO.Password;
            if(await _accountRepository.CheckEmailExists(email) && await _accountRepository.CheckPasswordValid(email, password))
            {
                //where to validate that get back right response?
               return await _accountRepository.GetAccountIdByEmail(email);
            }
            else
            {
                throw new Exception("not valid email or password");
            }
        }
    }
}
