using EmailTemplate.BLL.Client;
using EmailTemplate.BLL.Client.Abstracts;
using EmailTemplate.DAL.Databases;
using EmailTemplate.DAL.Repositories;
using EmailTemplate.DAL.Repositories.Abstractions;
using EmailTemplate.DAL.UnitOfWork;
using EmailTemplate.DAL.UnitOfWork.Abstractions;
using EmailTemplate.Infrastructure.Shared.Configurations;
using EmailTemplate.Infrastructure.Shared.Responses;
using EmailTemplate.Infrastructure.Shared.Services;
using EmailTemplate.Infrastructure.Shared.Services.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using System.Reflection;


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
            RegisterOptions(services);
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder
                        .WithOrigins(new string[] { "http://localhost:54501/" })
                        .WithHeaders(HeaderNames.ContentType, "application/json")
                        .WithHeaders(HeaderNames.ContentType, "x-custom-header"));
            });
        }

        private void RegisterOptions(IServiceCollection services)
        {
            services.Configure<MailClientConfig>(Configuration.GetSection("MailClientConfig"));
        }
        private void RegisterServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(IBaseResponse).GetTypeInfo().Assembly);
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IMailSenderService, MailSenderService>();
            services.AddTransient<IEmailClient, EmailClient>();
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
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
