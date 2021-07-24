using BatchProcessor.ManagerApi.Factories;
using BatchProcessor.ManagerApi.Interfaces.Factories;
using BatchProcessor.ManagerApi.Interfaces.Repository;
using BatchProcessor.ManagerApi.Interfaces.Services;
using BatchProcessor.ManagerApi.Repository;
using BatchProcessor.ManagerApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BatchProcessor.ManagerApi
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

            // Context
            services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("BachProcessor"));
            services.AddTransient<IApplicationContext, ApplicationContext>();

            services.AddTransient<IProcessService, ProcessService>();
            services.AddTransient<IBatchService, BatchService>();

            // Repositories
            services.AddTransient<IProcessRepository, ProcessRepository>();
            services.AddTransient<IBatchRepository, BatchRepository>();
            services.AddTransient<INumberRepository, NumberRepository>();

            // Factories
            services.AddSingleton<IProcessFactory, ProcessFactory>();
            services.AddSingleton<IBatchFactory, BatchFactory>();
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
