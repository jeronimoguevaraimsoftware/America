using LiberacionProductoWeb.Models;
using MimeKit;
using System.Linq;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace LiberacionProductoWeb.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailData _emailConfig;
        public EmailSender(EmailData emailConfig)
        {
            _emailConfig = emailConfig;
        }
        public async Task SendEmailAsync(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            await SendAsync(emailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.Sender = MailboxAddress.Parse(_emailConfig.From);
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            var bodyBuilder = new BodyBuilder { HtmlBody = message.Content };
            if (message.Attachments != null && message.Attachments.Any())
            {
                foreach (var attachment in message.Attachments)
                {
                    bodyBuilder.Attachments.Add(attachment.FileName, attachment.Content.ToArray(), ContentType.Parse(attachment.ContentType));
                }
            }
            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }
        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
                    await client.SendAsync(mailMessage);
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}
