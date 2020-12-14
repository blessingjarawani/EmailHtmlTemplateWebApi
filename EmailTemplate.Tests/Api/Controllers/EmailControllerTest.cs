using EmailTemplate.Api.Controllers;
using EmailTemplate.BLL.Client.Abstracts;
using EmailTemplate.DAL.DTO;
using EmailTemplate.Infrastructure.Request.Queries;
using EmailTemplate.Infrastructure.RequestHandler.QueryHandlers;
using EmailTemplate.Infrastructure.Shared.Responses;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmailTemplate.Tests.Api.Controllers
{
    public class EmailControllerTest
    {
        private readonly IMediator _mediator;
        private readonly GetUserEmailHistoryQuery getUserEmailHistoryQuery;
        private readonly EmailController emailController;
        private readonly Mock<IEmailClient> mailClientMock;
        public EmailControllerTest()
        {
            _mediator = A.Fake<IMediator>();
            mailClientMock = new Mock<IEmailClient>();
            emailController = new EmailController(mailClientMock.Object, _mediator);
            getUserEmailHistoryQuery = new GetUserEmailHistoryQuery
            {
               Email ="blessingjarawani@gmail.com"
            };
           
            A.CallTo(() => _mediator.Send(A<GetUserEmailHistoryQuery>._, default)).Returns(new Response<IEnumerable<EmailHistoryDTO>>());
        }
        [Fact]
        public async Task Should_GetUser_TemplatesHistory_Succesfully()
        {
            var result = await emailController.Get(getUserEmailHistoryQuery);
            (result as BaseResponse).Message.Should().Be(null);
        }
    }
}
