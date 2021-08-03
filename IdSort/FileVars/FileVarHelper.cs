using System.Collections.Generic;

namespace IdSort.FileVars
{
    public static class FileVarHelper
    {
        public static List<IFileVar> Vars = new()
        {
            new AlbumVar(),
            new TitleVar(),
            new ArtistVar(),
            new YearVar(),

            new DiscVar(),
            new TrackVar(),

            new FileNameVar(),
            new ExtensionVar(),
        };
    }
}
