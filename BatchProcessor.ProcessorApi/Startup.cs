using BatchProcessor.ProcessorApi.Interfaces.Services;
using BatchProcessor.ProcessorApi.Options;
using BatchProcessor.ProcessorApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BatchProcessor.ProcessorApi
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

            // Services
            services.AddSingleton<IWorkerService, WorkerService>();
            services.AddSingleton<INumberGeneratorService, NumberGeneratorService>();
            services.AddSingleton<INumberMultiplierService, NumberMultiplierService>();

            // Options
            services
                .AddSingleton(provider => Configuration.GetSection(nameof(NumberGeneratorOptions)).Get<NumberGeneratorOptions>())
                .AddSingleton(provider => Configuration.GetSection(nameof(NumberMultiplierOptions)).Get<NumberMultiplierOptions>());
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
