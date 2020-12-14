using EmailTemplate.BLL.Client;
using EmailTemplate.BLL.Client.Abstracts;
using EmailTemplate.DAL.Dictionary;
using EmailTemplate.DAL.Entities;
using EmailTemplate.DAL.UnitOfWork;
using EmailTemplate.Infrastructure.Shared.Configurations;
using EmailTemplate.Infrastructure.Shared.Context;
using EmailTemplate.Infrastructure.Shared.ProcessesHandler.Abstracts;
using EmailTemplate.Infrastructure.Shared.Responses;
using EmailTemplate.Infrastructure.Shared.Services;
using EmailTemplate.Infrastructure.Shared.Services.Abstracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmailTemplate.Tests.Api.EmailSendingProcess
{

    public class EmailSending
    {
        private readonly IEmailClient mailClient;
        private readonly IMailSenderService mailSendingService;
        private readonly UnitOfWork uow;
        private readonly MailClientConfig defaultMailClientConfig;
        private IContext context;
        private IOptions<MailClientConfig> _config;


        public EmailSending()
        {
            var configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.Development.json", false)
              .Build();

            _config = Options.Create(configuration.GetSection("MailClientConfig").Get<MailClientConfig>());

            mailSendingService = new MailSenderService();
            var db = DbContextFactory.GetInMemoryDbContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            uow = new UnitOfWork(db);

            defaultMailClientConfig = new MailClientConfig();
            mailClient = new EmailClient(mailSendingService, uow, _config);

        }

        [Fact]
        public async Task Process_CorrectData_SendMailAsync()
        {
            await PostTemplate();
            context = new EmailContext
            {

                EmailAddress = "blessingjarawani@gmail.com",
                TemplateId = 1,
                Name = "blessing"
            };

            var result = await mailClient.Execute(context);
            var sendStatus = (context.SendingStatus == MessageStatus.Sent) || (context.SendingStatus == MessageStatus.NotSent);
            Assert.True(sendStatus);

        }
        private async Task PostTemplate()
        {
            var post = new Template
            {
                Body = @$"<a href =mailto:abc @example.com? subject = Feedback & body = Message>Send Feedback> ",
                Subject = "Test Template 1",
                IsActive = true
            };
            await uow.Template.Create(post);
            await uow.SaveAsync();
        }
        [Fact]
        public async Task Process_TemplateNotFound_SendMailAsync()
        {
            context = new EmailContext
            {
                EmailAddress = "blessingjarawani@gmail.com",
                TemplateId = 10,
                Name = "blessing"
            };

            var result = await mailClient.Execute(context);
            Assert.Equal(MessageStatus.TemplateNotFound, context.SendingStatus);

        }

    }
}
