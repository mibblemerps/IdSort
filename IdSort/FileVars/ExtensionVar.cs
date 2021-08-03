using System.IO;
using IdSort.Restructure;

namespace IdSort.FileVars
{
    public class ExtensionVar : IFileVar
    {
        public string Name => "ext";
        public string Description => "Extension";

        public string GetValue(AudioFile file, Restructurer.RestructureSettings settings)
        {
            return Path.GetExtension(file.Path);
        }
    }
}
