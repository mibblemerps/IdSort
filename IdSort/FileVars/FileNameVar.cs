using IdSort.Restructure;

namespace IdSort.FileVars
{
    public class FileNameVar : IFileVar
    {
        public string Name => "filename";
        public string Description => "Filename";
        public string GetValue(AudioFile file, Restructurer.RestructureSettings settings)
        {
            return file.Name;
        }
    }
}
