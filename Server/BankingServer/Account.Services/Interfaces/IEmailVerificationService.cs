using Account.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Services.Interfaces
{
    public interface IEmailVerificationService
    {
       Task AddEmailVerification(EmailVerificationDto emailVerification);
    }
}
