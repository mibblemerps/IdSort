using IdSort.Restructure;

namespace IdSort.FileVars
{
    public class TrackVar : IFileVar
    {
        public string Name => "track";
        public string Description => "Track #";

        public string GetValue(AudioFile file, Restructurer.RestructureSettings settings)
        {
            return file.Tags?.Track != null ? file.Tags?.Track.ToString() : null;
        }
    }
}
