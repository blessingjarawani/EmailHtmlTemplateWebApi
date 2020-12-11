using EmailTemplate.Api.Controllers;
using EmailTemplate.Infrastructure.Request.Commands;
using EmailTemplate.Infrastructure.Shared.Responses;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmailTemplate.Tests.Api.Controllers
{
    public class TemplateControllerTest
    {
        private readonly IMediator mediator;
        private readonly AddTemplateCommand addTemplateCommand;
        private readonly EditTemplateCommand updateTemplateCommand;
        private readonly TemplatesController templatesController;
        public TemplateControllerTest()
        {
            mediator = A.Fake<IMediator>();
            templatesController = new TemplatesController(mediator);
            addTemplateCommand = new AddTemplateCommand
            {
                Body = "$< a href = mailto:abc@pl.pl ? subject = Feedback & body = Message > Send Feedback </ a > ",
                Subject = "Testing Template"
            };
            updateTemplateCommand = new EditTemplateCommand
            {
                Id = 1,
                Body = $"<a href =mailto:abc@example.com ? subject = Feedback & body = Message>Send Feedback</ a >",
                Subject = "FeedBack Template"
            };
            A.CallTo(() => mediator.Send(A<AddTemplateCommand>._, default)).Returns(new BaseResponse());
            A.CallTo(() => mediator.Send(A<EditTemplateCommand>._, default)).Returns(new BaseResponse());
        }

        [Fact]
        public async Task Should_Add_Template_Succesfully()
        {
            var result = await templatesController.Add(addTemplateCommand);
           (result as BaseResponse).Message.Should().Be(null);
        }

        [Fact]
        public async Task Should_Update_Template_Succesfully()
        {
            var result = await templatesController.Update(updateTemplateCommand);
            (result as BaseResponse).Message.Should().Be(null);
        }

        [Fact]
        public async Task Should_Update_Template_Fail()
        {
            updateTemplateCommand.Id = 0;
               var result = await templatesController.Update(updateTemplateCommand);
            (updateTemplateCommand).IsValid.Should().Be(false);
        }

    }
}
