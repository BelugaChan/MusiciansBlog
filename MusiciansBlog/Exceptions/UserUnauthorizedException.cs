namespace MusiciansBlog.API.Exceptions
{
    public class UserUnauthorizedException : ApplicationException
    {
        public UserUnauthorizedException()
        {
            
        }

        public UserUnauthorizedException(string message)
            : base(message)
        {
            
        }
    }
}
