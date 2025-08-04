namespace MusiciansBlog.API.Exceptions
{
    public class UnvalidRefreshTokenException : ApplicationException
    {
        public UnvalidRefreshTokenException()
        {
            
        }

        public UnvalidRefreshTokenException(string message)
            : base(message)
        {
            
        }
    }
}
