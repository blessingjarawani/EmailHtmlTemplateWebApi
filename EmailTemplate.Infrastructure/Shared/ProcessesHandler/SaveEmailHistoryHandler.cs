using EmailTemplate.DAL.Entities;
using EmailTemplate.DAL.UnitOfWork;
using EmailTemplate.DAL.UnitOfWork.Abstractions;
using EmailTemplate.Infrastructure.Shared.Context;
using EmailTemplate.Infrastructure.Shared.ProcessesHandler.Abstracts;
using EmailTemplate.Infrastructure.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplate.Infrastructure.Shared.ProcessesHandler
{
    public class SaveEmailHistoryHandler : BaseProcessHandler<IContext>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SaveEmailHistoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async override Task<BaseResponse> Handle(IContext request)
        {
            if (request != null && request.Template != null)
            {
                var emailHistory = new EmailHistory
                {
                    Name = request.Name,
                    Status = request.SendingStatus,
                    Email = request.EmailAddress,
                    Template = request.Template,
                };
                await _unitOfWork.EmailHistory.Create(emailHistory);
                return await _unitOfWork.SaveAsync() ?
                     await base.Handle(request) : BaseResponse.CreateFail("Failed To Save");
            }
            return BaseResponse.CreateFail("Invalid Request");
        }
    }
}
