using EmailTemplate.DAL.Entities;
using EmailTemplate.DAL.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplate.DAL.UnitOfWork.Abstractions
{
    public interface IUnitOfWork
    {
        IRepository<EmailHistory> EmailHistory { get; }
        IRepository<Template> Template { get; }
        Task<bool> SaveAsync();
    }
}
