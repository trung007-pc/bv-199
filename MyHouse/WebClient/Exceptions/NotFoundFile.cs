using System;

namespace WebClient.Exceptions
{
    public class NotFoundFile : Exception
    {
        public NotFoundFile(string message) : base(message)
        {
            
        }
    }
}