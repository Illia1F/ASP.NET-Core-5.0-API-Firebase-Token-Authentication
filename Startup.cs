namespace ASP.NET_Core_5._0_API_Firebase_Token_Authentication
{ 
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Infrastructure.Extensions;
    using Infrastructure.Models;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddControllers();

            services.AddSwaggerDocumentation(GetAppSettings());
        }

        private AppSettings GetAppSettings()
        {
            var appSettings = new AppSettings();

            Configuration.Bind(nameof(AppSettings), appSettings);

            return appSettings;
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseSwaggerDocumentation();

            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
