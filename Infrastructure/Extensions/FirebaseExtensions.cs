namespace ASP.NET_Core_5._0_API_Firebase_Token_Authentication.Infrastructure.Extensions
{  
    using Microsoft.Extensions.DependencyInjection;
    using FirebaseAdmin;
    using Google.Apis.Auth.OAuth2;

    public static class FirebaseExtensions
    {
        public static void AddFirebaseAdminWithCredentialFromFile(this IServiceCollection _, string path)
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(path)
            });
        }
    }
}
