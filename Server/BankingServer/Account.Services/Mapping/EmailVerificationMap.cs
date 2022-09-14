using Account.DAL.Entities;
using Account.DTO;
using AutoMapper;

namespace Account.Services.Mapping
{
    public class EmailVerificationMap:Profile
    {
        public EmailVerificationMap()
        {
            CreateMap<EmailVerificationDto,EmailVerificationEntity>().ReverseMap();
        }
    }
}
