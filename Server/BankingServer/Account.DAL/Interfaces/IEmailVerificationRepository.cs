using Account.DAL.Entities;

namespace Account.DAL.Interfaces
{
    public interface IEmailVerificationRepository
    {
        Task AddEmailVerification(EmailVerificationEntity emailVerificationEntity);
        Task<bool> CheckVerificationAsync(string email, string verificationCode);
        Task<string> CodeForExistingEmail(string email);
    }
}
