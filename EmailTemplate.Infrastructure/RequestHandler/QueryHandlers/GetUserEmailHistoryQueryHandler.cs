using EmailTemplate.DAL.DTO;
using EmailTemplate.DAL.UnitOfWork.Abstractions;
using EmailTemplate.Infrastructure.Request.Queries;
using EmailTemplate.Infrastructure.Shared.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmailTemplate.Infrastructure.RequestHandler.QueryHandlers
{
    public class GetUserEmailHistoryQueryHandler : IRequestHandler<GetUserEmailHistoryQuery, IResponse<IEnumerable<EmailHistoryDTO>>>
    {
        private readonly IUnitOfWork _unitofWork;

        public GetUserEmailHistoryQueryHandler(IUnitOfWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        public async Task<IResponse<IEnumerable<EmailHistoryDTO>>> Handle(GetUserEmailHistoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request != null)
                {
                    var result = (await _unitofWork.EmailHistory
                          .Find(y => y.Email == request.Email))
                          ?.Select(y => EmailHistoryDTO.Create(y));
                    return (result?.Any() ?? false) ? Response<IEnumerable<EmailHistoryDTO>>.CreateSuccess(result)
                           : Response<IEnumerable<EmailHistoryDTO>>.CreateFail("No Emails Found For this User");
                }
                Logging.Log<GetUserEmailHistoryQueryHandler>.CreateMessage("Invalid Parameters", Logging.MessageType.Info);
                return Response<IEnumerable<EmailHistoryDTO>>.CreateFail("No Emails Found For this User");
            }
            catch (Exception ex)
            {
                Logging.Log<GetUserEmailHistoryQueryHandler>.CreateMessage(ex.Message, Logging.MessageType.Error);
                return Response<IEnumerable<EmailHistoryDTO>>.CreateFail(ex.GetBaseException().Message);
            }
        }
    }
}
