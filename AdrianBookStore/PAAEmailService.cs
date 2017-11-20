using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Net.Mail;

namespace AdrianBookStore
{
    internal class PAAEmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            string apiKey = System.Configuration.ConfigurationManager.AppSettings["SendGrid.Key"];
            SendGrid.SendGridClient client = new SendGrid.SendGridClient(apiKey);

        }
    }
}