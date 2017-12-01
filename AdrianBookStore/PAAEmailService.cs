using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Net.Mail;
using System;

namespace AdrianBookStore
{
    internal class PAAEmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            string apiKey = System.Configuration.ConfigurationManager.AppSettings["SendGrid.Key"];
            SendGrid.SendGridClient client = new SendGrid.SendGridClient(apiKey);

            SendGrid.Helpers.Mail.SendGridMessage mail = new SendGrid.Helpers.Mail.SendGridMessage();
            mail.SetFrom(new SendGrid.Helpers.Mail.EmailAddress { Name = "PAA Admin", Email = "ledadrian@gmail.com" });
            mail.AddTo(message.Destination);
            mail.SetSubject(message.Subject);
            mail.AddContent("text/plain", message.Body);
            mail.AddContent("text/html", message.Body);
            //Set this to a template ID generated from your SendGrid transactional Email Templates
            mail.SetTemplateId("5e43d3e0-b987-4221-aede-c9c7a581c5d9");

            mail.AddSubstitution("<%copyright%>", string.Format("©{0} PAA", DateTime.Now.Year.ToString()));

            return client.SendEmailAsync(mail);
        }
    }
}