using BatchProcessor.ManagerApi.Factories;
using BatchProcessor.ManagerApi.HostedServices;
using BatchProcessor.ManagerApi.Interfaces.Factories;
using BatchProcessor.ManagerApi.Interfaces.Managers;
using BatchProcessor.ManagerApi.Interfaces.Repository;
using BatchProcessor.ManagerApi.Interfaces.Services;
using BatchProcessor.ManagerApi.Managers;
using BatchProcessor.ManagerApi.Options;
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
            services.AddScoped<IApplicationContext, ApplicationContext>();

            // Options
            services.AddSingleton(provider => Configuration.GetSection(nameof(HttpOptions)).Get<HttpOptions>());

            // Services
            services.AddScoped<IProcessService, ProcessService>();
            services.AddScoped<IBatchService, BatchService>();
            services.AddSingleton<IProcessQueueService, ProcessQueueService>();

            // Repositories
            services.AddScoped<IProcessRepository, ProcessRepository>();
            services.AddScoped<IBatchRepository, BatchRepository>();
            services.AddScoped<INumberRepository, NumberRepository>();

            // Factories
            services.AddTransient<IProcessFactory, ProcessFactory>();
            services.AddTransient<IBatchFactory, BatchFactory>();
            services.AddTransient<INumberFactory, NumberFactory>();

            // Managers
            services.AddScoped<INumberManager, NumberManager>();
            services.AddScoped<IMultiplyManager, MultiplyManager>();

            services.AddHostedService<ProcessHostedService>();

            services.AddCors(options =>
            {
                // FOR DEVELOPMENT ONLY
                options.AddPolicy("AllowAllOrigins",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAllOrigins");

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
