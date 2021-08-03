using IdSort.Restructure;

namespace IdSort.FileVars
{
    public interface IFileVar
    {
        string Name { get; }

        string Description { get; }

        string GetValue(AudioFile file, Restructurer.RestructureSettings settings);
    }
}
