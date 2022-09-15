using Account.DTO;

namespace Account.Services.Interfaces
{
    public interface IEmailVerificationService
    {
       Task AddEmailVerification(string email);
        Task<bool> CheckVerificationAsync(string email, string verificationCode);
    }
}
