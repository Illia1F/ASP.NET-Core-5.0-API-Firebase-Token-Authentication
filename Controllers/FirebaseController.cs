namespace ASP.NET_Core_5._0_API_Firebase_Token_Authentication.Controllers
{
    using FirebaseAdmin.Auth;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    using Infrastructure.Attributes;
    using Infrastructure.Models;
    using Infrastructure.Services;

    /// In order to be able use SignIn meethods you have to enable Sign-in provider 
    /// in Firebase Console -> Authentication -> Sign-In Method
    /// https://console.firebase.google.com/u/0/project/[PROJECT_ID]/authentication/providers
    [Route("api/[controller]")]
    [ApiController]
    public class FirebaseController : ControllerBase
    {
        private readonly IFirebaseService _service;

        public FirebaseController(IFirebaseService service) : base()
        {
            _service = service;
        }

        [HttpGet("SignInAnonymously")]
        public async Task<ActionResult<FirebaseUserToken>> SignInAnonymously()
        {
            return await _service.SignInAnonymously();
        }

        [HttpGet("SignUpWithEmailAndPassword")]
        public async Task<ActionResult<FirebaseUserToken>> SignUpWithEmailAndPassword(string email, string password)
        {
            return await _service.SignUpWithEmailAndPassword(email, password);
        }

        [HttpGet("SignInWithEmailAndPassword")]
        public async Task<ActionResult<FirebaseUserToken>> SignInWithEmailAndPassword(string email, string password)
        {
            return await _service.SignInWithEmailAndPassword(email, password);
        }

        [HttpGet("SignInWithGoogleAccessToken")]
        public async Task<ActionResult<FirebaseOAuthUserToken>> SignInWithGoogleAccessToken(string googleIdToken)
        {
            return await _service.SignInWithGoogleAccessToken(googleIdToken);
        }

        /// <summary>
        /// You have to be authorized in swagger to be able get data from firebase token.
        /// </summary>
        [Authorize]
        [HttpGet("GetDataFromMyToken")]
        public async Task<ActionResult<FirebaseToken>> GetDataFromFirebaseToken()
        {
            return await Task.FromResult((FirebaseToken)HttpContext.Items["user"]);
        }
    }
}
