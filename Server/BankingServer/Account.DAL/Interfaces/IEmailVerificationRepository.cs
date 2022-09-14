using Account.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.DAL.Interfaces
{
    public interface IEmailVerificationRepository
    {
        Task AddEmailVerification(EmailVerificationEntity emailVerificationEntity);
        Task<bool> CheckVerificationAsync(string email, string verificationCode);
    }
}
