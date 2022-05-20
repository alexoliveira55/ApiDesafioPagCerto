using api.Extensions;
using api.Infrastructure.Context;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace api
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
            services.AddDbContext<DbContextApi>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();

            services.AddAutoMapper(typeof(Startup));
            services.ResolveDependencies();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Transação com cartão e antecipação de recebíveis",
                    Description = "ASP.NET Core Web API<br /><br />"
                                 + "<b>ACESSO E UTILIZAÇÃO</b><br />"
                                 + "Acesso livre sem necessidade autenticação - necesário ser feita configuração com BD SQL Server.<br />",
                    TermsOfService = new Uri("https://github.com/alexoliveira55/"),
                    Contact = new OpenApiContact
                    {
                        Name = "Contato",
                        Url = new Uri("https://github.com/alexoliveira55/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Licença de uso",
                        Url = new Uri("https://github.com/alexoliveira55/")
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger(options =>
                {
                    options.SerializeAsV2 = true;
                });
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
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
