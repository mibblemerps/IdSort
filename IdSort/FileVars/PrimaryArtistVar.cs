using System.Linq;
using IdSort.Restructure;

namespace IdSort.FileVars
{
    public class PrimaryArtistVar : IFileVar
    {
        public string Name => "primary_artist";
        public string Description => "Primary Artist";

        public string GetValue(AudioFile file, Restructurer.RestructureSettings settings)
        {
            var s = file?.Tags.ContributingArtists.FirstOrDefault() ?? file?.Tags.AlbumArtists.FirstOrDefault();

            if (settings.NeverUseVariousArtists && s != null && Tags.IsVariousArtists(s))
                return null;

            return s;
        }
    }
}
