using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplate.Infrastructure.Shared.Responses
{
    public interface IBaseResponse
    {
        bool IsSuccess { get; }
        string Message { get; }
    }
}
