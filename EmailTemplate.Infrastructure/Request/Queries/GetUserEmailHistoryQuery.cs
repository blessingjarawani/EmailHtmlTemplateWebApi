using EmailTemplate.DAL.DTO;
using EmailTemplate.Infrastructure.Shared.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplate.Infrastructure.Request.Queries
{
    public class GetUserEmailHistoryQuery : IRequest<IResponse<IEnumerable<EmailHistoryDTO>>>
    {
        [Required]
        [EmailAddressAttribute]
        public string Email { get; set; }
    }
}
