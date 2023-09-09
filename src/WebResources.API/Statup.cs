using Microsoft.EntityFrameworkCore;
using Serilog;
using WebResources.Application.Interfaces;
using WebResources.Infrastructure.Data;
using WebResources.Infrastructure.Repositories;

namespace WebResources.API
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
            // Setup DbContext for EF Core
            services.AddDbContext<WebResourceDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Register the repository for DI
            services.AddScoped<IWebResourceRepository, WebResourceRepository>();

            services.AddControllers();

            // Add other required services here, e.g. AutoMapper, Caching, etc.
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error"); // generic error route
            }

            app.UseSerilogRequestLogging(); // Log requests with Serilog

            app.UseRouting();

            // If CORS is required, you'd add UseCors() here.
            // app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
