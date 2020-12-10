using EmailTemplate.DAL.DTO;
using EmailTemplate.Infrastructure.Shared.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplate.Infrastructure.Request.QueryHandlers
{
    public class GetTemplatesQuery : IRequest<IResponse<IEnumerable<TemplateDTO>>>
    {
        public int ? Id { get; set; }
    }
}
