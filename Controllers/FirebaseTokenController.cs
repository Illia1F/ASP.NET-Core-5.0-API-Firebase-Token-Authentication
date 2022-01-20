namespace ASP.NET_Core_5._0_API_Firebase_Token_Authentication.Controllers
{
    using FirebaseAdmin.Auth;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    using Infrastructure.Attributes;
    using Infrastructure.Models;
    using Infrastructure.Services;

    [Route("api/[controller]")]
    [ApiController]
    public class FirebaseTokenController : ControllerBase
    {
        private readonly IFirebaseService _service;

        public FirebaseTokenController(IFirebaseService service) : base()
        {
            _service = service;
        }

        /// <summary>
        /// In order to use this method you have to enable Anonymous Sign-in provider 
        /// in Firebase Console -> Authentication -> Sign-In Method
        /// https://console.firebase.google.com/u/0/project/[PROJECT_ID]/authentication/providers
        /// </summary>
        [HttpGet("SignInAnonymously")]
        public async Task<ActionResult<FirebaseUserToken>> SignInAnonymously()
        {
            return await _service.SignInAnonymously();
        }

        /// <summary>
        /// In order to use this method you have to enable Email/Password Sign-in provider 
        /// in Firebase Console -> Authentication -> Sign-In Method
        /// https://console.firebase.google.com/u/0/project/[PROJECT_ID]/authentication/providers
        /// </summary>
        [HttpGet("SignInWithEmailAndPassword")]
        public async Task<ActionResult<FirebaseUserToken>> SignInWithEmailAndPassword(string email, string password)
        {
            return await _service.SignInWithEmailAndPassword(email, password);
        }

        /// <summary>
        /// You have to be authorized in swagger to be able get data from firebase token.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetDataFromMyToken")]
        public async Task<ActionResult<FirebaseToken>> GetDataFromFirebaseToken()
        {
            return await Task.FromResult((FirebaseToken)HttpContext.Items["user"]);
        }
    }
}
