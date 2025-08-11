namespace MusiciansBlog.API.Infrastructure.Sounds.Common
{
    public static class NoteExtensions
    {
        private static Dictionary<Note, double> baseNotesFrequencies = new()
        {
            {Note.C, 16.35 },
            {Note.CSharp, 17.32 },
            {Note.D, 18.35 },
            {Note.DSharp, 19.45 },
            {Note.E, 20.60 },
            {Note.F, 21.83 },
            {Note.FSharp, 23.12 },
            {Note.G, 24.50 },
            {Note.GSharp, 25.96 },
            {Note.A, 27.50 },
            {Note.ASharp, 29.14 },
            {Note.B, 30.87 },
        };
        public static double GetReferentFrequency(this Note note, OctaveName octave)
        {

            return baseNotesFrequencies[note] * Math.Pow(2, octave.GetOctaveNum());
        }

        public static Note GetClosestNote(this float frequency, OctaveName octave)
        {
            var baseFrequency = frequency / Math.Pow(2, octave.GetOctaveNum());

            Note closest = Note.NotFound;
            double minfreqdiff = 1_000_000;

            foreach (var item in baseNotesFrequencies)
            {
                var diff = Math.Abs(item.Value - baseFrequency);
                if (diff < minfreqdiff)
                {
                    closest = item.Key;
                    minfreqdiff = diff;
                }
            }
            return closest;
        }
    }
}
