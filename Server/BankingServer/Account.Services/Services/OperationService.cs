using Account.DAL.Entities;
using Account.DAL.Interfaces;
using Account.DTO;
using Account.Services.Interfaces;
using Account.Services.Mapping;
using AutoMapper;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Services.Services
{
    public class OperationService : IOperationService
    {
        private readonly IOperationRepository _operationRepository;
        private readonly IMapper _mapper;
        public OperationService(IOperationRepository operationRepository)
        {
            _operationRepository = operationRepository;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<OperationMapping>();
            });
            _mapper = config.CreateMapper();
        }
        public async Task AddToHistoryTableAsync(TransactionPayload message)
        {
            OperationMapDTO fromAccount = new OperationMapDTO(message.FromAccountId, message.TransactionId, false, message.Amount, message.DateOfTransaction);
            OperationMapDTO toAccount = new OperationMapDTO(message.ToAccountId, message.TransactionId, false, message.Amount, message.DateOfTransaction);
            OperationEntity accountFrom = _mapper.Map<OperationEntity>(fromAccount);
            OperationEntity accountTo = _mapper.Map<OperationEntity>(toAccount);
            try
            {
                accountFrom.Balance = await _operationRepository.GetAccountBalanceByAccountIdAsync(message.FromAccountId);
                accountTo.Balance = await _operationRepository.GetAccountBalanceByAccountIdAsync(message.ToAccountId);
            }
            catch
            {
                throw;
            }
            try
            {
                await _operationRepository.AddToHistoryTableAsync(accountFrom, accountTo);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<OperationDto>> GetOperationsByAccountIdAsync(int id, bool sortByDateDesc)
        {
            List<OperationEntity> operationsEnitity = await _operationRepository.GetOperationsByAccountIdAsync(id);
            var operationsDto = new List<OperationDto>();
            foreach (var operation in operationsEnitity)
            {
                try
                {
                    operationsDto.Add(await MapToOperationDtoAsync(operation));
                }
                catch
                {
                    //dont add
                }
            }
            if (sortByDateDesc)
                return sortOperations(operationsDto);
            return operationsDto;

        }
        public async Task<OperationDto> MapToOperationDtoAsync(OperationEntity operation)
        {
            OperationDto operationDto = _mapper.Map<OperationDto>(operation);
            operationDto.OtherSide = await _operationRepository.GetOtherSideIdAsync(operation.TransactionId, operation.AccountId);
            return operationDto;
        }
        public List<OperationDto> sortOperations(List<OperationDto> operationsDto)
        {
            operationsDto.Sort((x, y) => DateTime.Compare(x.OperationTime, y.OperationTime));
            return operationsDto;
        }
        public Task<List<OperationDto>> getOpertaionsByFilterPage(int accountId, bool sortByDateDesc, int pageNumber, int numOfRecrds)
        {
            throw new NotImplementedException();
        }
    }
}
