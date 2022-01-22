namespace ASP.NET_Core_5._0_API_Firebase_Token_Authentication.Infrastructure.Models
{
    public class FirebaseOAuthUserToken
    {
        public string federatedId { get; set; }
        public string providerId { get; set; }
        public string localId { get; set; }
        public bool emailVerified { get; set; }
        public string email { get; set; }
        public string oauthIdToken { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string fullName { get; set; }
        public string displayName { get; set; }
        public string idToken { get; set; }
        public string photoUrl { get; set; }
        public string refreshToken { get; set; }
        public string expiresIn { get; set; }
        public string rawUserInfo { get; set; }
    }
}
