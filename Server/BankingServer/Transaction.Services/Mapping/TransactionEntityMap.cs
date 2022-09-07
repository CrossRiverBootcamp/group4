using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.DAL.Entities;
using Transaction.DTO;

namespace Transaction.Services.Mapping
{
    public class TransactionEntityMap:Profile
    {
        public TransactionEntityMap()
        {
            CreateMap<TransactionDto, TransactionEntity>();
        }
    }
}
