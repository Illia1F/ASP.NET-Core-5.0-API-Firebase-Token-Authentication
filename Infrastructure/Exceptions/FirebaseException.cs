namespace ASP.NET_Core_5._0_API_Firebase_Token_Authentication.Infrastructure.Exceptions
{
    using System;

    using Infrastructure.Models;

    public class FirebaseException : Exception
    { 
        public int Code { get; set; }

        public FirebaseException(FirebaseContentError contentError) : base(contentError.error.message)
        {
            Code = contentError.error.code;
        }
    }
}
