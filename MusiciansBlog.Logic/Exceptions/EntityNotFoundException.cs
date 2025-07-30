namespace MusiciansBlog.Logic.Exceptions
{
    /// <summary>
    /// Сущность не найдена (исключение)
    /// </summary>
    public class EntityNotFoundException : Exception
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
