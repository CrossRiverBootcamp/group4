using Account.DAL.Entities;
using Account.DTO;
using AutoMapper;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Services.Mapping
{
    public class OperationMapping:Profile
    {
        public OperationMapping()
        {
            CreateMap<OperationEntity, OperationDto>()
                   .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.TransactionAmount))
                .ReverseMap();
            CreateMap<OperationMapDTO, OperationEntity>()
                   .ForMember(dest => dest.OperationTime, opt => opt.MapFrom(src => src.DateOfTransaction))
                .ReverseMap();
        }
    }
}
