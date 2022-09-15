using Account.DTO;

namespace Account.Services.Interfaces
{
    public interface IEmailVerificationService
    {
       Task AddEmailVerificationAsync(string email);
        Task<bool> CheckVerificationAsync(string email, string verificationCode);
        Task ResendCodeAsync(string email);
    }
}
