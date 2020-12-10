using EmailTemplate.BLL.Client.Abstracts;
using EmailTemplate.BLL.Commands;
using EmailTemplate.Infrastructure.Shared.Context;
using EmailTemplate.Infrastructure.Shared.Responses;
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
        public EmailController(IEmailClient emailClient) => _emailClient = emailClient;

        [HttpPost("[action]")]
        public async Task<BaseResponse> SendEmail([FromBody]SendEmailCommand sendEmailCommand)
        {
            if (sendEmailCommand != null)
            {
                var sendEmailContext = new EmailContext
                {
                    EmailAddress = sendEmailCommand.EmailAddress,
                    Name = sendEmailCommand.Name,
                    TemplateId = sendEmailCommand.TemplateId
                };
                return await _emailClient.Execute(sendEmailContext);
            }
            return BaseResponse.CreateFail("Invalid Object");
        }

    }
}
