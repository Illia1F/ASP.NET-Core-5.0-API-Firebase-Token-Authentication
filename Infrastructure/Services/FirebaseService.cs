namespace ASP.NET_Core_5._0_API_Firebase_Token_Authentication.Infrastructure.Services
{
    using Microsoft.Extensions.Options;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Text.Json;
    using System.IO;

    using Infrastructure.Models;   
    using Infrastructure.Exceptions;

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
        Task<FirebaseUserToken> SignUpWithEmailAndPassword(string email, string password);
        Task<FirebaseUserToken> SignInWithEmailAndPassword(string email, string password);
    }

    public class FirebaseService : IFirebaseService
    {
        private readonly AppSettings _appSettings;

        private readonly IHttpClientFactory _httpClientFactory;

        public FirebaseService(IOptions<AppSettings> appSettings, IHttpClientFactory httpClientFactory)
        {
            _appSettings = appSettings.Value;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<FirebaseUserToken> SignInAnonymously()
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "returnSecureToken", "true" }
            });

            HttpClient httpClient = _httpClientFactory.CreateClient();

            HttpResponseMessage httpResponseMessage = 
                await httpClient.PostAsync($"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={_appSettings.WebAPIKey}", content);

            using Stream contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            if (httpResponseMessage.IsSuccessStatusCode == false)
            {
                var contentError = await JsonSerializer.DeserializeAsync<FirebaseContentError>(contentStream);

                throw new FirebaseException(contentError);
            }
            
            return await JsonSerializer.DeserializeAsync<FirebaseUserToken>(contentStream);
        }

        public async Task<FirebaseUserToken> SignInWithEmailAndPassword(string email, string password)
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "email", email },
                { "password", password },
                { "returnSecureToken", "true" }
            });

            var httpClient = _httpClientFactory.CreateClient();

            var httpResponseMessage = await httpClient.PostAsync($"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={_appSettings.WebAPIKey}", content);

            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            if (httpResponseMessage.IsSuccessStatusCode == false)
            {
                var contentError = await JsonSerializer.DeserializeAsync<FirebaseContentError>(contentStream);

                throw new FirebaseException(contentError);
            }
 
            return await JsonSerializer.DeserializeAsync<FirebaseUserToken>(contentStream);
        }

        public async Task<FirebaseUserToken> SignUpWithEmailAndPassword(string email, string password)
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "email", email },
                { "password", password },
                { "returnSecureToken", "true" }
            });

            var httpClient = _httpClientFactory.CreateClient();

            var httpResponseMessage = await httpClient.PostAsync($"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={_appSettings.WebAPIKey}", content);

            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            if (httpResponseMessage.IsSuccessStatusCode == false)
            {
                var contentError = await JsonSerializer.DeserializeAsync<FirebaseContentError>(contentStream);

                throw new FirebaseException(contentError);
            }
            
            return await JsonSerializer.DeserializeAsync<FirebaseUserToken>(contentStream);
        }
    }
}
