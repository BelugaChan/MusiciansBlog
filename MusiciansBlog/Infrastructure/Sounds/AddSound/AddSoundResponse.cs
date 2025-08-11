using MusiciansBlog.API.Infrastructure.Sounds.Common;

namespace MusiciansBlog.API.Infrastructure.Sounds.AddSound
{
    public class AddSoundResponse
    {
        public Note RecognizedNote { get; set; }

        public OctaveName RecognizedOctave { get; set; }

        public Note ReferentNote { get; set; }

        public OctaveName ReferentOctave { get; set; }

        public double Frequency { get; set; }

        public double ReferenceFrequency { get; set; }
    }
}
