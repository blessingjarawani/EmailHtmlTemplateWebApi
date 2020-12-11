using EmailTemplate.Infrastructure.Shared.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplate.Infrastructure.Request.Commands
{
    public class AddTemplateCommand : IRequest<IBaseResponse>
    {
        [Required]
        public string Body { get; set; }
        [Required]
        public string Subject { get; set; }
        public bool ? IsValid => !String.IsNullOrWhiteSpace(Body)
                               && !String.IsNullOrWhiteSpace(Subject);
    }
}
