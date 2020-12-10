using EmailTemplate.Infrastructure.Shared.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplate.Infrastructure.Request.Commands
{
    public class DeleteTemplateCommand : IRequest<IBaseResponse>
    {
        public int Id { get; set; }
        public bool IsValid => Id > 0;
    }
}
