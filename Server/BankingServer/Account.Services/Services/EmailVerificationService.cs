using Account.DAL.Entities;
using Account.DAL.Repositories;
using Account.DTO;
using Account.Services.Interfaces;
using Account.Services.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Services.Services
{
    public class EmailVerificationService : IEmailVerificationService
    {
        private readonly EmailVerificationRepository _emailVerificationRepository;
        private readonly IMapper _mapper;
        public EmailVerificationService(EmailVerificationRepository emailVerificationRepository)
        {
            _emailVerificationRepository = emailVerificationRepository;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EmailVerificationMap>();
            });
            _mapper = config.CreateMapper();
        }

        public async Task AddEmailVerification(EmailVerificationDto emailVerification)
        {
            EmailVerificationEntity emailVerificationEntity = _mapper.Map<EmailVerificationEntity>(emailVerification);
            await _emailVerificationRepository.AddEmailVerification(emailVerificationEntity);
        }
    }
}
