using AutoMapper;
using Transaction.DAL.Entities;
using Transaction.DTO;

namespace Transaction.Services.Mapping
{
    public class TransactionEntityMap:Profile
    {
        public TransactionEntityMap()
        {
            CreateMap<TransactionDto, TransactionEntity>().ReverseMap();
        }
    }
}
