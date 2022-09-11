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
            OperationMapDTO operationFrom = new OperationMapDTO(message.FromAccountId, message.TransactionId, false, message.Amount,message.DateOfTransaction);
            OperationMapDTO operationTo = new OperationMapDTO(message.ToAccountId, message.TransactionId, true, message.Amount,message.DateOfTransaction);
            OperationEntity opEntityFrom = _mapper.Map<OperationEntity>(operationFrom);
            OperationEntity opEntityTo = _mapper.Map<OperationEntity>(operationTo);
            try
            {
                opEntityFrom.Balance = await _operationRepository.GetAccountBalanceByAccountID(message.FromAccountId);
                opEntityTo.Balance = await _operationRepository.GetAccountBalanceByAccountID(message.ToAccountId);
            }
            catch
            {
                throw;
            }
            try
            {
                 await _operationRepository.AddToHistoryTable(opEntityFrom, opEntityTo);
            }
            catch
            {
                throw;
            }
        }
    }
}
