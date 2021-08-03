using IdSort.Restructure;

namespace IdSort.FileVars
{
    public class AlbumVar : IFileVar
    {
        public string Name => "album";
        public string Description => "Album";

        public string GetValue(AudioFile file, Restructurer.RestructureSettings settings)
        {
            return file.Tags?.Album;
        }
    }
}
