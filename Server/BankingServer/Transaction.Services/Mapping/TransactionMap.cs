using AutoMapper;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.DAL.Entities;
using Transaction.DTO;

namespace Transaction.Services.Mapping
{
    public class TransactionMap:Profile
    {
        public TransactionMap()
        {
            CreateMap<TransactionDto, TransactionPayload>().ReverseMap();
            CreateMap<TransactionEntity, TransactionPayloaded>().ReverseMap();

            CreateMap<TransactionPayloaded, TransactionPayload>().ReverseMap();
            
        }
    }
}
