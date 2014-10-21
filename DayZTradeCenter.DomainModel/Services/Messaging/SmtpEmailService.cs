using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using rg.Configuration;

namespace DayZTradeCenter.DomainModel.Services.Messaging
{
    public class SmtpEmailService : IIdentityMessageService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SmtpEmailService"/> class.
        /// </summary>
        public SmtpEmailService()
        {
            _username = ConfigurationProvider.AppSettings["smtp-username"];
            _password = ConfigurationProvider.AppSettings["smtp-password"];
        }

        public async Task SendAsync(IdentityMessage message)
        {
            var email =
                new MailMessage(
                    new MailAddress("no-reply@dayztradecenter.com", "DayZ Trade Center"),
                    new MailAddress(message.Destination))
                {
                    Subject = message.Subject,
                    Body = message.Body,
                    IsBodyHtml = true
                };
            
            using (var client = new SmtpClient
            {
                EnableSsl = true,
                Host = "smtp.gmail.com",
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_username, _password),
            })
            {
                await client.SendMailAsync(email);
            }
        }

        #region Private fields

        private readonly string _username;
        private readonly string _password;

        #endregion
    }
}
