using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tree.Core.Domain.UnitOfWork;
using Tree.Data.Repositories.ImportAndExport.Abstractions;
using Tree.Data.Repositories.Test;
using Tree.Data.Repositories.Test.Abstractions;
using Tree.Data.UnitOfWorks;
using Tree.IO;
using Tree.IO.Services.Abstractions;
using Treebank.Core.Autofac;

namespace TreeBank
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
            services.AddControllers();
            services.AddSingleton<ITestStudent, TestStudent>();
            services.AddSingleton<IImportAndExportService, ImportAndExportService>();
            services.AddSingleton<IImportAndExport, ImportAndExport > ();
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.Configure<DBOption>(Configuration.GetSection("DBOption"));
            services.Configure<Redis>(Configuration.GetSection("Redis"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ServiceLocator.Instance = app.ApplicationServices;
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
