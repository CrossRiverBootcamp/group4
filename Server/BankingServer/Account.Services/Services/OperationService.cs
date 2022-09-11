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
        public async Task AddToHistoryTable(TransactionPayload message)
        {
            OperationMapDTO fromAccount = new OperationMapDTO(message.FromAccountId, message.TransactionId, false, message.Amount, message.DateOfTransaction);
            OperationMapDTO toAccount = new OperationMapDTO(message.ToAccountId, message.TransactionId, false, message.Amount, message.DateOfTransaction);
            OperationEntity accountFrom = _mapper.Map<OperationEntity>(fromAccount);
            OperationEntity accountTo = _mapper.Map<OperationEntity>(toAccount);
            try
            {
                accountFrom.Balance = await _operationRepository.GetAccountBalanceByAccountID(message.FromAccountId);
                accountTo.Balance = await _operationRepository.GetAccountBalanceByAccountID(message.ToAccountId);
            }
            catch
            {
                throw;
            }
            try
            {
                await _operationRepository.AddToHistoryTable(accountFrom, accountTo);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<OperationDto>> GetOperationsByAccountId(int id, bool sortByDateDesc)
        {
            List<OperationEntity> operationsEnitity = await _operationRepository.GetOperationsByAccountId(id);
            var operationsDto = new List<OperationDto>();
            foreach (var operation in operationsEnitity)
            {
                OperationDto operationDto = _mapper.Map<OperationDto>(operation);
                operationDto.OtherSide = await _operationRepository.GetOtherSideId(operation.TransactionId, operation.AccountId);
                operationsDto.Add(operationDto);
            }
            if (sortByDateDesc)
                operationsDto.Sort((x, y) => DateTime.Compare(x.OperationTime, y.OperationTime));
            return operationsDto;

        }
    }
}
