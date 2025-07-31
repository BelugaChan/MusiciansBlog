namespace MusiciansBlog.API.Exceptions
{
    /// <summary>
    /// Сущность уже существует (исключение)
    /// </summary>
    public sealed class EntityAlreadyExistsException : ApplicationException
    {
        public EntityAlreadyExistsException()
        {
            
        }

        public EntityAlreadyExistsException(string message)
            : base(message)
        {
            
        }
    }
}
