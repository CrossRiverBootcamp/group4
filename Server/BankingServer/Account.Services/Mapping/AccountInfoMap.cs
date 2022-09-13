using Account.DAL.Entities;
using Account.DTO;
using AutoMapper;

namespace Account.Services.Mapping
{
    public class AccountInfoMap : Profile
    {
        public AccountInfoMap()
        {
            CreateMap<AccountEntity, AccountInfoDTO>()
                   .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.Id))
                .IncludeMembers(a=> a.Customer);
            CreateMap<CustomerEntity, AccountInfoDTO>();
        }
    }
}
