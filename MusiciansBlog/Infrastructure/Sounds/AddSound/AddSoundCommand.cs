using MediatR;
using MusiciansBlog.API.Infrastructure.Sounds.Common;

namespace MusiciansBlog.API.Infrastructure.Sounds.AddSound
{
    public class AddSoundCommand : IRequest<AddSoundResponse>
    {
        public byte[] RawData { get; set; } = new byte[0];

        public Note ReferentNote { get; set; }

        public OctaveName ReferentOctave { get; set; }
    }
}
