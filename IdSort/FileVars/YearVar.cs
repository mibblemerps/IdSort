using IdSort.Restructure;

namespace IdSort.FileVars
{
    public class YearVar : IFileVar
    {
        public string Name => "year";
        public string Description => "Year";

        public string GetValue(AudioFile file, Restructurer.RestructureSettings settings)
        {
            return file.Tags?.Year != null ? file.Tags?.Year.ToString() : null;
        }
    }
}
