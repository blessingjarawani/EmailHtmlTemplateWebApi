using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplate.Infrastructure.Shared.Responses
{
    public interface IResponse <T> : IBaseResponse
    {
        T Result { get; }
    }
}
