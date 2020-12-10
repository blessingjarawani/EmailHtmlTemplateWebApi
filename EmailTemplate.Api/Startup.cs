using EmailTemplate.DAL.Databases;
using EmailTemplate.DAL.Repositories;
using EmailTemplate.DAL.Repositories.Abstractions;
using EmailTemplate.DAL.UnitOfWork;
using EmailTemplate.DAL.UnitOfWork.Abstractions;
using EmailTemplate.Infrastructure.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace EmailTemplate.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            RegisterDbContexts(services);
            RegisterServices(services);
            services.AddControllers();
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(IBaseResponse).GetTypeInfo().Assembly);
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
        private void RegisterDbContexts(IServiceCollection services)
        {
            services.AddDbContext<EmailContext>(options => options.UseSqlServer(Configuration.GetConnectionString("EmailContext")));
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
