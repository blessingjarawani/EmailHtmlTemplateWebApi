using EmailTemplate.DAL.Databases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailTemplate.Tests
{
    public static class DbContextFactory
    {
        public static EmailContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<EmailContext>()
                            .UseInMemoryDatabase(databaseName: "InMemoryArticleDatabase")
                            .Options;
            var dbContext = new EmailContext(options);
            return dbContext;
        }
    }
}
