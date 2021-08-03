using System;
using IdSort.Restructure;

namespace IdSort.FileVars
{
    public class ArtistVar : IFileVar
    {
        public string Name => "artist";
        public string Description => "Artist";

        public string GetValue(AudioFile file, Restructurer.RestructureSettings settings)
        {
            if (settings.NeverUseVariousArtists && file.Tags != null && Tags.IsVariousArtists(file.Tags.JoinedAlbumArtists))
                return null;

            return file.Tags?.JoinedAlbumArtists;
        }
    }
}
