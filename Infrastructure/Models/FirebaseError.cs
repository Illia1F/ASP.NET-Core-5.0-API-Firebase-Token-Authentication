namespace ASP.NET_Core_5._0_API_Firebase_Token_Authentication.Infrastructure.Models
{
    using System.Collections.Generic;

    public class FirebaseError
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<FirebaseInnerError> errors { get; set; }
    }
}
