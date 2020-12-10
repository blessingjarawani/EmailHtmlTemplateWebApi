using EmailTemplate.Infrastructure.DTO;
using EmailTemplate.Infrastructure.Shared.Configurations;
using EmailTemplate.Infrastructure.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplate.Infrastructure.Shared.Services.Abstracts
{
   public interface IMailSenderService
    {
        BaseResponse Send(EmailDTO mail, MailClientConfig config);
    }
}
