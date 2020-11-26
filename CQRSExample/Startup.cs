using System;
using CQRSExample.Context;
using CQRSExample.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using CQRSExample.DAL.UnitOfWork;

namespace CQRSExample
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
            services.AddMediatR(typeof(Startup));
            services.AddDbContext<MasterContext>(x => x.UseMySQL(Environment.GetEnvironmentVariable("MYSQL_URI")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            using (IServiceScope serviceScope = app.ApplicationServices.CreateScope())
            {
                MasterContext context = serviceScope.ServiceProvider.GetRequiredService<MasterContext>();
                context.Database.Migrate();
            }

            using (DatabaseInit database = new DatabaseInit())
            {
                database.InitializeDb();
            }
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
