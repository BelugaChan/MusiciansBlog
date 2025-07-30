namespace MusiciansBlog.Logic.Exceptions
{
    /// <summary>
    /// Сущность уже существует (исключение)
    /// </summary>
    public sealed class EntityAlreadyExistsException : Exception
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
