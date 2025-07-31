namespace MusiciansBlog.API.Exceptions
{
    /// <summary>
    /// Сущность не найдена (исключение)
    /// </summary>
    public class EntityNotFoundException : ApplicationException
    {
        public EntityNotFoundException()
        {
            
        }

        public EntityNotFoundException(string message)
            : base(message)
        {
            
        }
    }
}
