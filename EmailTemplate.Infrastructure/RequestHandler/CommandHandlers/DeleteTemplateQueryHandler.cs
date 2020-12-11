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
    public class DeleteTemplateQueryHandler : IRequestHandler<DeleteTemplateCommand, IBaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTemplateQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IBaseResponse> Handle(DeleteTemplateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null || !request.IsValid)
                    return BaseResponse.CreateFail("Invalid parameters");
                var template = await _unitOfWork.Template.FindFirst(y=>y.Id == request.Id);
                if (template != null)
                    template.IsActive = false;
                return await _unitOfWork.SaveAsync() ? BaseResponse.CreateSuccess()
                 : BaseResponse.CreateFail("Failed to Delete");

            }
            catch (Exception ex)
            {
                //logger
                return BaseResponse.CreateFail(ex.GetBaseException().Message);
            }
        }
    }
}
