using Account.DAL.Entities;
using Account.DAL.Interfaces;
using Account.DAL.Repositories;
using Account.DTO;
using Account.Services.Interfaces;
using Account.Services.Mapping;
using AutoMapper;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace Account.Services.Services
{
    public class EmailVerificationService : IEmailSender, IEmailVerificationService
    {
        private readonly IEmailVerificationRepository _emailVerificationRepository;
        private readonly IMapper _mapper;
        public EmailVerificationService(IEmailVerificationRepository emailVerificationRepository)
        {
            _emailVerificationRepository = emailVerificationRepository;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EmailVerificationMap>();
            });
            _mapper = config.CreateMapper();
        }

        public async Task AddEmailVerificationAsync(string email)
        {
            //create new verification
            EmailVerificationDto emailVerification = new EmailVerificationDto();
            emailVerification.Email = email;
            emailVerification.VerificationCode = new Random().Next(1000, 9999).ToString();
            emailVerification.ExpirationTime = DateTime.UtcNow.AddMinutes(5);
            EmailVerificationEntity emailVerificationEntity = _mapper.Map<EmailVerificationEntity>(emailVerification);
            //send verification to Dal
            await _emailVerificationRepository.AddEmailVerification(emailVerificationEntity);
            removeFromDB(emailVerificationEntity);
            await SendEmailAsync(emailVerification.Email, "Verify your email address", $"Your verification code is {emailVerification.VerificationCode}");
        }
        private void removeFromDB(EmailVerificationEntity emailVerificationEntity)
        {
            //remove verification from Verification table five mi utes after it was created
            Task.Delay(300000).ContinueWith(async _ =>
            {
                await _emailVerificationRepository.RemoveEmailVerification(emailVerificationEntity);
            });
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            string fromMail = "sendemail081@gmail.com";
            string fromPassword = "qsszgtsvvsukdxay";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = subject;
            message.To.Add(new MailAddress(email));
            message.Body = "<html><body> " + htmlMessage + " </body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };
            smtpClient.Send(message);
        }
        public async Task<bool> CheckVerificationAsync(string email, string verificationCode)
        {
            return await _emailVerificationRepository.CheckVerificationAsync(email, verificationCode);
        }
        public async Task ResendCodeAsync(string email)
        {
            //removes exisiting verification code from db
            await _emailVerificationRepository.ResendCodeForExistingEmail(email);
            //creates new code for email
            await AddEmailVerificationAsync(email);
        }
    }
}

