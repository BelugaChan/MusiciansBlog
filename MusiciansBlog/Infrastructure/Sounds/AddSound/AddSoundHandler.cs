using MediatR;
using MusiciansBlog.API.Infrastructure.Sounds.Common;

namespace MusiciansBlog.API.Infrastructure.Sounds.AddSound
{
    public class AddSoundHandler : IRequestHandler<AddSoundCommand, AddSoundResponse>
    {
        private readonly IFourierHandler _fourierHandler;

        public AddSoundHandler(IFourierHandler fourierHandler)
        {
            _fourierHandler = fourierHandler;
        }

        public Task<AddSoundResponse> Handle(AddSoundCommand request, CancellationToken cancellationToken)
        {
            var foundFreq = _fourierHandler.GetFrequency(request.RawData);

            var recognizedOctave = foundFreq.GetOctaveByFrequency();

            return Task.FromResult(new AddSoundResponse
            {
                Frequency = foundFreq,
                ReferenceFrequency = request.ReferentNote.GetReferentFrequency(request.ReferentOctave),
                ReferentNote = request.ReferentNote,
                ReferentOctave = request.ReferentOctave,
                RecognizedNote = foundFreq.GetClosestNote(recognizedOctave),
                RecognizedOctave = foundFreq.GetOctaveByFrequency(),
            });
        }
    }
}
