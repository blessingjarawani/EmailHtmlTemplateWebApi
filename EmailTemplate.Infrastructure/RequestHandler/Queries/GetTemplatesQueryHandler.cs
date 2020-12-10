using EmailTemplate.DAL.DTO;
using EmailTemplate.DAL.UnitOfWork.Abstractions;
using EmailTemplate.Infrastructure.RequestHandlers.QueryHandlers;
using EmailTemplate.Infrastructure.Shared.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmailTemplate.Infrastructure.RequestHandler.Queries
{
    public class GetTemplatesQueryHandler : IRequestHandler<GetTemplatesQuery, IResponse<IEnumerable<TemplateDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetTemplatesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IResponse<IEnumerable<TemplateDTO>>> Handle(GetTemplatesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<TemplateDTO> result;
                if (request.Id.HasValue)
                {
                    result = (await _unitOfWork.Template.Find(y => y.Id == request.Id.Value))
                        ?.Select(x => TemplateDTO.Create(x));
                }
                else
                {
                    result = (await _unitOfWork.Template.GetAll())?
                        .Select(x => TemplateDTO.Create(x));
                }

                return !result?.Any() ?? false
                    ? Response<IEnumerable<TemplateDTO>>.CreateSuccess(result)
                    : Response<IEnumerable<TemplateDTO>>.CreateFail("No Items Found");
            }
            catch (Exception ex)
            {
                return Response<IEnumerable<TemplateDTO>>.CreateFail(ex.GetBaseException().Message);
            }
        }
    }
}
