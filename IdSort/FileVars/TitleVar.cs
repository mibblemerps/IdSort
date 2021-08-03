using IdSort.Restructure;

namespace IdSort.FileVars
{
    public class TitleVar : IFileVar
    {
        public string Name => "title";
        public string Description => "Title";

        public string GetValue(AudioFile file, Restructurer.RestructureSettings settings)
        {
            return file.Tags?.Title;
        }
    }
}
