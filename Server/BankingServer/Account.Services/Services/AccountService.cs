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
        IMapper _mapper;
        IAccountRepository _accountRepository;
        public AccountService()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CustomerMapping>();
            });
            _mapper = config.CreateMapper();
        }


        public async Task<bool> CreateAccount(CustomerDTO customerDTO)
        {
            try
            {
                CustomerEntity customer = _mapper.Map<CustomerEntity>(customerDTO);
                if (!await _accountRepository.CheckEmailExists(customerDTO.Email))
                {
                    return false; 
                }
                AccountEntity account = new AccountEntity();
                account.CustomerId = customer.Id;
                account.Customer = customer;
                account.OpenDate = DateTime.UtcNow;
                //i want to input here from appsettings.json instead of hardcoding
                account.Balance = 1000;
                var createdCustomer = await _accountRepository.CreateCustomer(customer);
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

        public Task<AccountInfoDTO> GetAccountInfo(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Login(LoginDTO loginDTO)
        {
            throw new NotImplementedException();
        }
    }
}
