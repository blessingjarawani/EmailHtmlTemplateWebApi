using EmailTemplate.Infrastructure.Shared.Context;
using EmailTemplate.Infrastructure.Shared.Responses;
using System.Threading.Tasks;

namespace EmailTemplate.Infrastructure.Shared.ProcessesHandler.Abstracts
{
    public interface IBaseProcessHandler<T> where T : IContext
    {
        IBaseProcessHandler<T> SetNext(IBaseProcessHandler<T> process);
        Task<BaseResponse> Handle(T request);
    }
}
