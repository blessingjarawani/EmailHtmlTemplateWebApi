using EmailTemplate.BLL.Client.Abstracts;
using EmailTemplate.BLL.Commands;
using EmailTemplate.DAL.DTO;
using EmailTemplate.Infrastructure.Request.Queries;
using EmailTemplate.Infrastructure.Shared.Context;
using EmailTemplate.Infrastructure.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailTemplate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailClient _emailClient;
        private readonly IMediator _mediator;
        public EmailController(IEmailClient emailClient, IMediator mediator)
        {
            _emailClient = emailClient;
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<BaseResponse> SendEmail([FromBody] SendEmailCommand sendEmailCommand)
        {
            if (sendEmailCommand != null)
            {
                var sendEmailContext = new EmailContext
                {
                    EmailAddress = sendEmailCommand.Email,
                    Name = sendEmailCommand.Name,
                    TemplateId = sendEmailCommand.TemplateId
                };
                return await _emailClient.Execute(sendEmailContext);
            }
            return BaseResponse.CreateFail("Invalid Object");
        }
        [HttpGet("[action]")]
        public async Task<IResponse<IEnumerable<EmailHistoryDTO>>> Get([FromBody] GetUserEmailHistoryQuery query)
        {
            return await _mediator.Send(query);
        }

    }
}
