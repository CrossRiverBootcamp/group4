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
    public class EmailVerificationMap:Profile
    {
        public EmailVerificationMap()
        {
            CreateMap<EmailVerificationDto,EmailVerificationEntity>().ReverseMap();
        }
    }
}
