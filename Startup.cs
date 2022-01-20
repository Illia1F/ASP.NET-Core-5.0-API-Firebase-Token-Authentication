namespace ASP.NET_Core_5._0_API_Firebase_Token_Authentication
{ 
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Infrastructure.Extensions;
    using Infrastructure.Models;
    using Infrastructure.Utils;
    using Infrastructure.Middleware;
    using Infrastructure.Services;

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

            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddSwaggerDocumentation(GetAppSettings());

            // Firebase Admin Secret Key you can find at link (Replace [YOUR_PROJECT_ID] by your project ID)
            // https://console.firebase.google.com/u/0/project/[YOUR_PROJECT_ID]/settings/serviceaccounts/adminsdk
            services.AddFirebaseAdminWithCredentialFromFile("firebase-adminsdk-secret-key.json");

            // configure DI for application services
            services.AddScoped<IFirebaseAdminUtils, FirebaseAdminUtils>();
            services.AddScoped<IFirebaseService, FirebaseService>();
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

            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            // custom auth middleware
            app.UseMiddleware<AuthorizationMiddleware>();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
