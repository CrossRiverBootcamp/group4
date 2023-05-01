using Account.DAL.Entities;
using Account.DAL.Interfaces;
using Account.DTO;
using Account.Services.Interfaces;
using Account.Services.Mapping;
using AutoMapper;
using Messages;

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
            
            try
            {
                OperationMapDTO fromAccount = new OperationMapDTO(message.FromAccountId, message.TransactionId, false, message.Amount, message.DateOfTransaction);
                OperationMapDTO toAccount = new OperationMapDTO(message.ToAccountId, message.TransactionId, true, message.Amount, message.DateOfTransaction);
                OperationEntity accountFrom = _mapper.Map<OperationEntity>(fromAccount);
                OperationEntity accountTo = _mapper.Map<OperationEntity>(toAccount);
                accountFrom.Balance = await _operationRepository.GetAccountBalanceByAccountIdAsync(message.FromAccountId);
                accountTo.Balance = await _operationRepository.GetAccountBalanceByAccountIdAsync(message.ToAccountId);
                await _operationRepository.AddToHistoryTableAsync(accountFrom, accountTo);
            }
            catch(Exception ex)
            {
                throw new Exception("couldn't add to history table", ex);
            }
        }

        public async Task<List<OperationDto>> MapToOperationDto(List<OperationEntity> operations)
        {
            List<OperationDto> operationDtoList = new();
            foreach(OperationEntity operation in operations)
            {
                OperationDto operationDto = _mapper.Map<OperationDto>(operation);
                operationDto.OtherSide = await _operationRepository.GetOtherSideIdAsync(operation.TransactionId, operation.AccountId);
                operationDtoList.Add(operationDto);
            }
            return operationDtoList;
        }

        //sort operations by date
        public List<OperationDto> sortOperations(List<OperationDto> operationsDto)
        {
            operationsDto.Sort((x, y) => DateTime.Compare(x.OperationTime, y.OperationTime));
            return operationsDto;
        }

        public async Task<List<OperationDto>> getOperationsByFilterPageAsync(int accountId, bool sortByDateDesc, int pageNumber, int numOfRecrds)
        {
            List<OperationEntity> operationList = await _operationRepository.getOpertaionsByFilterPageAsync(accountId, pageNumber, numOfRecrds);
            List<OperationDto> operationsListDTO = await MapToOperationDto(operationList);
            if (sortByDateDesc)
                return sortOperations(operationsListDTO);
            return operationsListDTO;
        }

        public async Task<int> countOperationsByIdAsync(int accountId)
        {
           return await _operationRepository.countOpertaionsByIdAsync(accountId);
        }
    }
}
