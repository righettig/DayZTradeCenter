using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace DayZTradeCenter.DomainModel.Services.Messaging
{
    public class LocalEmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            const string path = @"c:\tmp\dayztradecenter-emails";

            Directory.CreateDirectory(path);

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
                DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                PickupDirectoryLocation = path,
                Host = "localhost"
            })
            {
                await client.SendMailAsync(email);
            }
        }
    }
}