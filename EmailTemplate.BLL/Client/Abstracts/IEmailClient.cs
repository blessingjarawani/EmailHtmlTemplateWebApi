using EmailTemplate.Infrastructure.Shared.Context;
using EmailTemplate.Infrastructure.Shared.ProcessesHandler.Abstracts;
using EmailTemplate.Infrastructure.Shared.Responses;
using System.Threading.Tasks;

namespace EmailTemplate.BLL.Client.Abstracts
{
    public interface IEmailClient
    {
        Task<BaseResponse> Execute(IContext emailContext);
    }
}
