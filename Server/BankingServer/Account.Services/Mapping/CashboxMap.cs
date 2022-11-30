using Account.DAL.Entities;
using Account.DTO;
using AutoMapper;


namespace Account.Services.Mapping
{
    public class CashboxMap:Profile
    {
        public CashboxMap()
        {
            CreateMap<CashboxDTO, CashboxEntity>()
                   .ForMember(dest => dest.PercentageOfRevenue, opt => opt.MapFrom(src => src.Percentages))
                   .ForMember(dest => dest.ExpirationTime, opt => opt.MapFrom(src => src.Duration))
                   .ReverseMap();
        }
    }
}
