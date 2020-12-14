using EmailTemplate.DAL.Databases;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailTemplate.Tests
{
    public static class DbContextFactory
    {
        public static EmailContext GetInMemoryDbContext()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<EmailContext>()
                            .UseInMemoryDatabase(databaseName: "InMemoryArticleDatabase")
                            .UseInternalServiceProvider(serviceProvider).Options;
            ;
            var dbContext = new EmailContext(options);
            return dbContext;
        }
    }
}
