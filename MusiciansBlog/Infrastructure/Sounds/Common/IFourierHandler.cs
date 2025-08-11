namespace MusiciansBlog.API.Infrastructure.Sounds.Common
{
    public interface IFourierHandler
    {
        float GetFrequency(byte[] rawData);
    }
}
