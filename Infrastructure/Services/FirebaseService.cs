namespace ASP.NET_Core_5._0_API_Firebase_Token_Authentication.Infrastructure.Services
{
    using Microsoft.Extensions.Options;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    using Infrastructure.Models;
    using System.Collections.Generic;

    public interface IFirebaseService
    {
        /// <summary>
        /// In order to use this method you have to enable Anonymous Sign-in provider 
        /// in Firebase Console -> Authentication -> Sign-In Method
        /// https://console.firebase.google.com/u/0/project/[PROJECT_ID]/authentication/providers
        /// </summary>
        Task<FirebaseUserToken> SignInAnonymously();
        /// <summary>
        /// In order to use this method you have to enable Email/Password Sign-in provider 
        /// in Firebase Console -> Authentication -> Sign-In Method
        /// https://console.firebase.google.com/u/0/project/[PROJECT_ID]/authentication/providers
        /// </summary>
        Task<FirebaseUserToken> SignInWithEmailAndPassword(string email, string password);
    }

    public class FirebaseService : IFirebaseService
    {
        private readonly HttpClient client;

        private readonly AppSettings _appSettings;

        public FirebaseService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;

            client = new HttpClient();
        }

        public async Task<FirebaseUserToken> SignInAnonymously()
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "returnSecureToken", "true" }
            });

            var response = await client.PostAsync($"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={_appSettings.WebAPIKey}", content);

            string json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<FirebaseUserToken>(json);
        }

        public async Task<FirebaseUserToken> SignInWithEmailAndPassword(string email, string password)
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "email", email },
                { "password", password },
                { "returnSecureToken", "true" }
            });

            var response = await client.PostAsync($"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={_appSettings.WebAPIKey}", content);

            string jsonString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<FirebaseUserToken>(jsonString);
        }
    }
}
