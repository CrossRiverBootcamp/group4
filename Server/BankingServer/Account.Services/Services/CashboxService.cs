using Account.DAL.Entities;
using Account.DAL.Interfaces;
using Account.DTO;
using Account.Services.Interfaces;
using Account.Services.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Services.Services
{
    public class CashboxService : ICashboxService
    {
        private readonly ICashboxRepository _cashboxRepository;
        private readonly IMapper _mapper;
        public CashboxService(ICashboxRepository cashboxRepository)
        {
            _cashboxRepository = cashboxRepository;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CashboxMap>();
            });
            _mapper = config.CreateMapper();
        }
        public async Task<int> CreateCashboxAsync(CashboxDTO cashboxDTO)
        {
            CashboxEntity cashbox = _mapper.Map<CashboxEntity>(cashboxDTO);
            //if (await _cashboxRepository.CheckEmailExistsAsync(customerDTO.Email))
            //{
            //    throw new Exception("An account with this email address aleady exists.");
            //}
            //create account and customer (in db)
            cashbox.Active = true;
            //cashbox.Amount = 0;
            //cashbox.ExpirationTime = DateTime.UtcNow.AddMonths(cashboxDTO.Duration);
            return await _cashboxRepository.CreateCashboxAsync(cashbox);
        }
        public async Task<CashboxDTO> GetCashboxAsync(int accountId)
        {
            CashboxEntity cashbox = await _cashboxRepository.GetCashboxAsync(accountId);
            if(cashbox == null)
            {
                throw new Exception("cashbox doesn't exist");
            }
            CashboxDTO cashboxDTO = _mapper.Map<CashboxDTO>(cashbox);
            return cashboxDTO;
        }
        public async Task<int> GetCashboxPercentsAsync(int accountId)
        {
            CashboxEntity cashbox = await _cashboxRepository.GetCashboxAsync(accountId);
            if (cashbox == null)
            {
                throw new Exception("cashbox doesn't exist");
            }
            return cashbox.PercentageOfRevenue;
        }
        public async Task<bool> CheckCashboxExists(int accountId)
        {
            return await _cashboxRepository.CheckCashboxExists(accountId);
        }
        public async Task<bool> CloseCashbox(int accountId)
        {
            return await _cashboxRepository.CloseCashbox(accountId); 
            
        }
        public void  UpdateCahboxAsync(int accountId, CashboxDTO cashboxDTO)
        {
            CashboxEntity cashbox = _mapper.Map<CashboxEntity>(cashboxDTO);
             _cashboxRepository.UpdateCahboxAsync(accountId, cashbox);
        }
        public void UpdateAmountInCahboxAsync(int accountId, float additionToCashboxAmount)
        {
            _cashboxRepository.UpdateAmountInCahboxAsync(accountId, additionToCashboxAmount);
        }
    }
}
