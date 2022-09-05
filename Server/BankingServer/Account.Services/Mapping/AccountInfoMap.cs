using Account.DAL.Entities;
using Account.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Services.Mapping
{
    public class AccountInfoMap : Profile
    {
        public AccountInfoMap()
        {
            CreateMap<AccountEntity, AccountInfoDTO>();
                //   .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.Id))
                //.IncludeMembers(a=> a.Customer);
        }
    }
}
