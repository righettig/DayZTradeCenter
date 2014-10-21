using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using RestSharp;

namespace DayZTradeCenter.DomainModel.Services.Messaging
{
    public class MailgunWebApiEmailService : IIdentityMessageService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MailgunWebApiEmailService"/> class.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <exception cref="System.ArgumentException">
        /// 'apiKey' cannot be null, empty or made of only whitespaces.;apiKey
        /// </exception>
        public MailgunWebApiEmailService(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException("'apiKey' cannot be null, empty or made of only whitespaces.", "apiKey");
            }

            _apiKey = apiKey;
        }

        public async Task SendAsync(IdentityMessage message)
        {
            var client = new RestClient
            {
                BaseUrl = "https://api.mailgun.net/v2",
                Authenticator =
                    new HttpBasicAuthenticator("api", _apiKey)
            };

            var request = new RestRequest();
            request.AddParameter("domain",
                "dayztradecenter.com", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "DayZ Trade Center <no-reply@dayztradecenter.com>");
            request.AddParameter("to", message.Destination);
            request.AddParameter("subject", message.Subject);
            request.AddParameter("text", message.Body);
            request.Method = Method.POST;

            await client.ExecuteTaskAsync(request);
        }

        private readonly string _apiKey;
    }
}