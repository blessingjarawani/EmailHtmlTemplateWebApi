using EmailTemplate.DAL.Entities;
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
    public class AddTemplateQueryHandler : IRequestHandler<AddTemplateCommand, IBaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddTemplateQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IBaseResponse> Handle(AddTemplateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null || !request.IsValid.HasValue || !request.IsValid.Value)
                    return BaseResponse.CreateFail("Invalid Parameters");
                var template = new Template { Body = request.Body, Subject = request.Subject };
                await _unitOfWork.Template.Create(template);
                return await _unitOfWork.SaveAsync() ? BaseResponse.CreateSuccess()
                     : BaseResponse.CreateFail("Error On saving Template");
            }
            catch (Exception ex)
            {
                Logging.Log<AddTemplateQueryHandler>.CreateMessage(ex);
                return BaseResponse.CreateFail(ex.GetBaseException().Message);
            }
        }
    }
}
