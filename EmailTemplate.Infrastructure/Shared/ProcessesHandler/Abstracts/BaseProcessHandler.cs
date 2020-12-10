using EmailTemplate.Infrastructure.Shared.Context;
using EmailTemplate.Infrastructure.Shared.Responses;
using System.Threading.Tasks;

namespace EmailTemplate.Infrastructure.Shared.ProcessesHandler.Abstracts
{
    public abstract class BaseProcessHandler<T> : IBaseProcessHandler<T> where T : IContext
    {
        protected IBaseProcessHandler<T> _process;
        public IBaseProcessHandler<T> SetNext(IBaseProcessHandler<T> process)
        {
            this._process = process;
            return _process;
        }

        public async virtual Task<BaseResponse> Handle(T request)
        {
            if (this._process == null)
            {
                return BaseResponse.CreateSuccess();

            }
            if (!Validate(request))
            {
                return BaseResponse.CreateFail("Bad validation");
            }
            return await this._process.Handle(request);
        }
        protected virtual bool Validate(T request)
        {
            return true;
        }
    }
}
