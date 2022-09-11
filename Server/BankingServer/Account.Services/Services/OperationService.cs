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
    public class OperationService:IOperationService
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
        public async Task<List<OperationDto>> GetOperationsByAccountId(int id,bool sortByDateDesc)
        {
            List<OperationEntity> operationsEnitity= await _operationRepository.GetOperationsByAccountId(id);
            var operationsDto=new List<OperationDto>();
            foreach(var operation in operationsEnitity)
            {
                operationsDto.Add(_mapper.Map<OperationDto>(operation));
            }
            if(sortByDateDesc)
                operationsDto.Sort((x, y) => DateTime.Compare(x.OperationTime, y.OperationTime));
            return operationsDto;

        }
    }
}
