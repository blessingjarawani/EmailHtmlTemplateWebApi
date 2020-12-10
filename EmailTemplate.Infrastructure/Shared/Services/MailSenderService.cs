using EmailTemplate.DAL.Dictionary;
using EmailTemplate.Infrastructure.DTO;
using EmailTemplate.Infrastructure.Shared.Configurations;
using EmailTemplate.Infrastructure.Shared.Responses;
using EmailTemplate.Infrastructure.Shared.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplate.Infrastructure.Shared.Services
{
    public class MailSenderService : IMailSenderService
    {
        public BaseResponse Send(EmailDTO mail, MailClientConfig config)
        {
            try
            {
                using (var client = initializeClient(config))
                {
                    var mailMessage = createMailMessage(config.MailFrom, mail.To, mail.Topic, mail.Body, mail.IsHtml);
                    client.Send(mailMessage);
                    return BaseResponse.CreateSuccess();
                }
            }
            catch (Exception e)
            {
                return BaseResponse.CreateFail($"{MessageStatus.NotSent}  {e.GetBaseException().Message}");
            }
        }

        private SmtpClient initializeClient(MailClientConfig config)
        {
            SmtpClient client = new SmtpClient(config.Host, config.Port);
            client.UseDefaultCredentials = config.UseDefaultCredentials;
            client.EnableSsl = config.Ssl;
            if (!config.UseDefaultCredentials)
            {
                client.Credentials = new NetworkCredential(config.Credentials.Login, config.Credentials.Password);
            }
            return client;
        }

        private MailMessage createMailMessage(string from, string recipient, string title, string body, bool isHtml)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(from);
            mailMessage.To.Add(recipient);
            mailMessage.Body = body;
            mailMessage.Subject = title;
            mailMessage.IsBodyHtml = isHtml;
            return mailMessage;
        }
    }
}

