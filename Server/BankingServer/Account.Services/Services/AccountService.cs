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
            });
            _mapper = config.CreateMapper();
        }
        public async Task<bool> CreateAccount(CustomerDTO customerDTO)
        {
            throw new NotImplementedException();
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
