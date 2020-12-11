using EmailTemplate.BLL.Client.Abstracts;
using EmailTemplate.DAL.UnitOfWork.Abstractions;
using EmailTemplate.Infrastructure.Shared.Configurations;
using EmailTemplate.Infrastructure.Shared.Context;
using EmailTemplate.Infrastructure.Shared.ProcessesHandler;
using EmailTemplate.Infrastructure.Shared.ProcessesHandler.Abstracts;
using EmailTemplate.Infrastructure.Shared.Responses;
using EmailTemplate.Infrastructure.Shared.Services.Abstracts;
using Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmailTemplate.BLL.Client
{
    public class EmailClient : IEmailClient
    {
        private readonly IMailSenderService _mailSenderService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOptions<MailClientConfig> _mailConfig;

        public EmailClient(IMailSenderService mailSenderService, IUnitOfWork unitOfWork, IOptions<MailClientConfig> mailConfig)
        {
            _mailSenderService = mailSenderService;
            _unitOfWork = unitOfWork;
            _mailConfig = mailConfig;
        }

        public async Task<BaseResponse> Execute(IContext emailContext)
        {
            return await StartProcess(InitHandlers(), emailContext);
        }

        private BaseProcessHandler<IContext> InitHandlers()
        {
            var handler = new GetEmailTemplateHandler(_unitOfWork);
            handler.SetNext(new SendEmailHandler(_mailSenderService, _mailConfig))
                   .SetNext(new SaveEmailHistoryHandler(_unitOfWork));
            return handler;
        }

        private async Task<BaseResponse> StartProcess(BaseProcessHandler<IContext> handler, IContext emailContext)
        {
            try
            {
                var validContext = IsValidContext(emailContext);
                if (!validContext)
                    return BaseResponse.CreateFail("Invalid Email Parameters");
                var processResult = await handler.Handle(emailContext);
                if (!processResult.IsSuccess)
                {
                    Log<EmailClient>.CreateMessage(processResult.Message, Logging.MessageType.Info);
                    return BaseResponse.CreateFail(processResult.Message);
                }
                Logging.Log<EmailClient>.CreateMessage($"{emailContext.EmailAddress} : Sent ", MessageType.Info);
                return BaseResponse.CreateSuccess();
            }
            catch (Exception ex)
            {
                Log<EmailClient>.CreateMessage(ex.Message, Logging.MessageType.Error);
                return BaseResponse.CreateFail(ex.GetBaseException().Message);
                
            }
        }

        private bool IsValidContext(IContext emailContext) =>
             emailContext != null && emailContext.TemplateId > 0 && !string.IsNullOrWhiteSpace(emailContext.Name)
             && !string.IsNullOrWhiteSpace(emailContext.EmailAddress)
             && new EmailAddressAttribute().IsValid(emailContext.EmailAddress);


    }
}
