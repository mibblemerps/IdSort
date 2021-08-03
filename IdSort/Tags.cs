using System;
using System.Linq;

namespace IdSort
{
    [Serializable]
    public class Tags
    {
        [TagField("Title")]  public string Title;

        [TagField("Album")]  public string Album;

        [TagField("Comment")]  public string Comment;

        [TagField("Album Artists")]  public string[] AlbumArtists;

        [TagField("Contributing Artists")]  public string[] ContributingArtists;

        [TagField("Genres")] public string[] Genres;

        [TagField("Year")] public uint? Year;

        [TagField("Track #")] public uint? Track;

        [TagField("Track Count")] public uint? TrackCount;

        [TagField("Disc #")] public uint? Disc;

        [TagField("Disc Count")] public uint? DiscCount;

        [TagField("Lyrics")] public string Lyrics;

        [TagField("Beats Per Minute")] public uint? BeatsPerMinute;

        [TagField("Copyright")] public string Copyright;

        [TagField("Date Tagged")] public DateTime? DateTagged;

        public string JoinedAlbumArtists => string.Join(", ", AlbumArtists);
        public string JoinedContributingArtists => string.Join(", ", ContributingArtists);
        public string JoinedGenres => string.Join(", ", Genres);

        public static bool IsVariousArtists(string tag)
        {
            return tag.Equals("Various Artists", StringComparison.InvariantCultureIgnoreCase) ||
                   tag.Equals("Various", StringComparison.InvariantCultureIgnoreCase);
        }

        public static Tags FromTagLibSharp(TagLib.Tag tlib) => new()
        {
            Title = tlib.Title,
            Album = tlib.Album,
            Comment = tlib.Comment,
            AlbumArtists = tlib.AlbumArtists,
            ContributingArtists = tlib.Performers?.Distinct().ToArray(),
            Genres = tlib.Genres,
            Year = tlib.Year > 0 ? tlib.Year : null,
            Track = tlib.Track > 0 ? tlib.Track : null,
            TrackCount = tlib.TrackCount > 0 ? tlib.TrackCount : null,
            Disc = tlib.Disc > 0 ? tlib.Disc : null,
            DiscCount = tlib.DiscCount > 0 ? tlib.DiscCount : null,
            Lyrics = tlib.Lyrics,
            BeatsPerMinute = tlib.BeatsPerMinute > 0 ? tlib.BeatsPerMinute : null,
            Copyright = tlib.Copyright,
            DateTagged = tlib.DateTagged,
        };

        [AttributeUsage(AttributeTargets.Field)]
        public class TagFieldAttribute : Attribute
        {
            public string Name;

            public TagFieldAttribute(string name)
            {
                Name = name;
            }
        }
    }
}
