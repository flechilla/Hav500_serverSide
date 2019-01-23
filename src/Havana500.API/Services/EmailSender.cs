using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using Microsoft.Extensions.Configuration;
using System;

namespace Havana500.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        private IConfiguration _config;

        public EmailSender(IConfiguration config)
        {
            _config = config;
        }
        public async Task SendEmailAsync(string email, string userFullName, string subject, string message)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("Havana500", "havana500@gmail.com"));
            mimeMessage.To.Add(new MailboxAddress(userFullName, email));
            mimeMessage.Subject = subject;

            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            try
            {
                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    var host = _config.GetValue<string>("Email:Smtp:Host");
                    var port = _config.GetValue<int>("Email:Smtp:Port");
                    var userName = _config.GetValue<string>("Email:Smtp:Username");
                    var password = _config.GetValue<string>("Email:Smtp:Password");

                    client.Connect(host, port, false);

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate(userName, password);

                    client.Send(mimeMessage);
                    client.Disconnect(true);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            var a = 1;
        }
    }
}
