using AutoMapper;
using Messages;
using Transaction.DAL.Entities;
using Transaction.DTO;

namespace Transaction.Services.Mapping
{
    public class TransactionMap:Profile
    {
        public TransactionMap()
        {
            CreateMap<TransactionDto, TransactionPayload>().ReverseMap();
            CreateMap<TransactionEntity, TransactionPayloaded>()
                .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

            CreateMap<TransactionPayloaded, TransactionPayload>().ReverseMap();
            
        }
    }
}
