namespace ASP.NET_Core_5._0_API_Firebase_Token_Authentication.Infrastructure.Models
{
    public class FirebaseUserToken
    {
        public string kind { get; set; }
        public string localId { get; set; }
        public string email { get; set; }
        public string displayName { get; set; }
        public string idToken { get; set; }
        public bool registered { get; set; }
        public string refreshToken { get; set; }
        public string expiresIn { get; set; }
    }
}
