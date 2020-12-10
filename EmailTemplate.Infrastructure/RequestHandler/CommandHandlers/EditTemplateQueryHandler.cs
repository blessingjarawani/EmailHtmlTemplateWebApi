using EmailTemplate.DAL.UnitOfWork.Abstractions;
using EmailTemplate.Infrastructure.Request.Commands;
using EmailTemplate.Infrastructure.Shared.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmailTemplate.Infrastructure.RequestHandler.CommandHandlers
{
    public class EditTemplateQueryHandler : IRequestHandler<EditTemplateCommand, IBaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditTemplateQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IBaseResponse> Handle(EditTemplateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null || !request.IsValid)
                    return BaseResponse.CreateFail("Invalid Parameters");
                var template = await _unitOfWork.Template.Get(request.Id);
                if (template != null)
                {
                    return await UpdateTemplate(request, template);
                }
                return BaseResponse.CreateFail("Template Not Found");
            }
            catch (Exception ex)
            {
                // logger
                return BaseResponse.CreateFail(ex.GetBaseException().Message);
            }
        }

        private async Task<IBaseResponse> UpdateTemplate(EditTemplateCommand request, DAL.Entities.Template template)
        {
            template.Body = request.Body;
            template.Subject = request.Subject;
            return await _unitOfWork.SaveAsync() ? BaseResponse.CreateSuccess()
              : BaseResponse.CreateFail("Error On Updating Template");
        }
    }
}
