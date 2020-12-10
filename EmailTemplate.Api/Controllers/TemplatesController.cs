using EmailTemplate.DAL.DTO;
using EmailTemplate.Infrastructure.Request.Commands;
using EmailTemplate.Infrastructure.Request.QueryHandlers;
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
    public class TemplatesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TemplatesController(IMediator mediator) => _mediator = mediator;

        [HttpGet("[action]")]
        public async Task<IResponse<IEnumerable<TemplateDTO>>> Get([FromBody] GetTemplatesQuery query)
             => await _mediator.Send(query);

        [HttpPost("[action]")]
        public async Task<IBaseResponse> Add([FromBody] AddTemplateCommand query)
             => await _mediator.Send(query);

        [HttpPut("[action]")]
        public async Task<IBaseResponse> Update([FromBody] EditTemplateCommand query)
            => await _mediator.Send(query);

        [HttpDelete("[action]")]
        public async Task<IBaseResponse> Remove([FromBody] DeleteTemplateCommand query)
            => await _mediator.Send(query);
    }
}
