using Account.DAL.Entities;
using Account.DTO;
using AutoMapper;

namespace Account.Services.Mapping
{
    public class CustomerMapping : Profile
    {
        public CustomerMapping()
        {
            CreateMap<CustomerDTO, CustomerEntity>().ReverseMap();
            
        }
    }
}
