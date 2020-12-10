using EmailTemplate.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmailTemplate.DAL.Databases
{
    public class EmailContext : DbContext
    {
        public virtual DbSet<Template> Template { get; set; }
        public virtual DbSet<EmailHistory> EmailHistory { get; set; }
        public EmailContext(DbContextOptions<EmailContext> options) : base(options) { }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var addedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Added).ToList();

            addedEntities.ForEach(E =>
            {
                E.Property("CreatedDate").CurrentValue = DateTime.Now;
                E.Property("CreatedUser").CurrentValue = "system";
            });

            var editedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();

            editedEntities.ForEach(E =>
            {
                E.Property("CreatedDate").IsModified = false;
                E.Property("CreatedUser").IsModified = false;
                E.Property("UpdatedDate").CurrentValue = DateTime.Now;
                E.Property("UpdatedUser").CurrentValue = "system";
            });

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}