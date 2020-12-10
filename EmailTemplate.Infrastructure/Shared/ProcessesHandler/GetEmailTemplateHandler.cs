using EmailTemplate.DAL.Dictionary;
using EmailTemplate.DAL.DTO;
using EmailTemplate.DAL.Entities;
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
    public class GetEmailTemplateHandler : BaseProcessHandler<IContext>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetEmailTemplateHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async override Task<BaseResponse> Handle(IContext request)
        {
            try
            {
                if (request != null && request.TemplateId > 0)
                {
                    var template = await _unitOfWork.Template.FindFirst(x => x.Id == request.TemplateId);
                    if (template != null)
                    {
                        request.Template = template;
                        return await base.Handle(request);
                    }
                    request.SendingStatus = MessageStatus.TemplateNotFound;
                    return BaseResponse.CreateFail("Template Not Found");
                }
                return BaseResponse.CreateFail("Invalid Request");
            }
            catch (Exception ex)
            {
                return BaseResponse.CreateFail(ex.GetBaseException().Message);
            }
        }
    }
}
