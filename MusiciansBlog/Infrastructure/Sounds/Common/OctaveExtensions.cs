namespace MusiciansBlog.API.Infrastructure.Sounds.Common
{
    public static class OctaveExtensions
    {
        private static readonly Dictionary<OctaveName, (int, double, double)> octaves = new()
        {
            {OctaveName.Great, (1, 65.41, 123.47) },
            {OctaveName.Small, (2, 130.81, 246.94) },
            {OctaveName.OneLine, (3, 261.63, 493.88) },
            {OctaveName.TwoLine, (4, 523.25, 987.77) },
            {OctaveName.ThreeLine, (5, 1046.50, 1975.53) }
        };

        public static double GetOctaveNum(this OctaveName octaveName)
        {
            return octaves[octaveName].Item1;
        }

        public static OctaveName GetOctaveByFrequency(this float frequency)
        {
            foreach (var item in octaves)
            {
                if(frequency > item.Value.Item2
                    && frequency < item.Value.Item3)
                {
                    return item.Key;
                }
            }
            return OctaveName.NotFound;
        }
    }
}
