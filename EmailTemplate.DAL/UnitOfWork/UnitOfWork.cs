using EmailTemplate.DAL.Databases;
using EmailTemplate.DAL.Entities;
using EmailTemplate.DAL.Repositories;
using EmailTemplate.DAL.Repositories.Abstractions;
using EmailTemplate.DAL.UnitOfWork.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplate.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IBaseRepository<EmailHistory> emailHistory;
        private IBaseRepository<Template> template;
        private readonly EmailContext context;
        public UnitOfWork(EmailContext emailContext)
        {
            context = emailContext;
        }
        public async Task<bool> SaveAsync() => await (context.SaveChangesAsync()) > 0;
        public IBaseRepository<EmailHistory> EmailHistory => emailHistory ?? new BaseRepository<EmailHistory>(context);
        public IBaseRepository<Template> Template => template ?? new BaseRepository<Template>(context);


    }
}
