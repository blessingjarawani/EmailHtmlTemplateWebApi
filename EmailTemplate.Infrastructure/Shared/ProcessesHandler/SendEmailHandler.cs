using EmailTemplate.DAL.Dictionary;
using EmailTemplate.Infrastructure.DTO;
using EmailTemplate.Infrastructure.Shared.Configurations;
using EmailTemplate.Infrastructure.Shared.Context;
using EmailTemplate.Infrastructure.Shared.ProcessesHandler.Abstracts;
using EmailTemplate.Infrastructure.Shared.Responses;
using EmailTemplate.Infrastructure.Shared.Services.Abstracts;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace EmailTemplate.Infrastructure.Shared.ProcessesHandler
{
    public class SendEmailHandler : BaseProcessHandler<IContext>
    {
        private readonly IMailSenderService _mailSenderService;
        private readonly IOptions<MailClientConfig> _mailconfig;
        public SendEmailHandler(IMailSenderService mailSenderService, IOptions<MailClientConfig> mailConfig)
        {
            _mailSenderService = mailSenderService;
            _mailconfig = mailConfig;
        }
        public async override Task<BaseResponse> Handle(IContext request)
        {
            try
            {
                if (request != null && request.SendingStatus != MessageStatus.TemplateNotFound)
                {
                    var message = new EmailDTO
                    {
                        Body = request.Template.Subject.Replace("[Name]", request.Name),
                        From = _mailconfig.Value.MailFrom,
                        To = request.EmailAddress,
                        Topic = request.Template.Subject
                    };
                    var result = await Task.Run(() => _mailSenderService.Send(message, _mailconfig.Value));
                    request.SendingStatus = result.IsSuccess ? MessageStatus.Sent : MessageStatus.NotSent;
                    return await base.Handle(request);
                }
                request.SendingStatus = MessageStatus.NotSent;
                return BaseResponse.CreateFail("Invalid Request");
            }
            catch (Exception ex)
            {
                request.SendingStatus = MessageStatus.NotSent;
                return await base.Handle(request);
            }

        }
    }
}
