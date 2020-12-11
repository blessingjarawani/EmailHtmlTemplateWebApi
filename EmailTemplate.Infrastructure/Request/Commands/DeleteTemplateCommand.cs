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
    public class DeleteTemplateCommand : IRequest<IBaseResponse>
    {
        [Required]
        public int Id { get; set; }
        public bool IsValid => Id > 0;
    }
}
