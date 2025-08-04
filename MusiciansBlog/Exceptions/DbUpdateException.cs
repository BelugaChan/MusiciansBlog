namespace MusiciansBlog.API.Exceptions
{
    public class DbUpdateException : ApplicationException
    {
        public DbUpdateException()
        {
            
        }

        public DbUpdateException(string message)
            : base(message)
        {
            
        }
    }
}
