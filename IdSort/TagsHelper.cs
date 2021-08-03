using TagLib;

namespace IdSort
{
    public static class TagsHelper
    {
        public static TagLevel GetTagLevel(this Tag tag)
        {
            TagLevel level = TagLevel.None;

            if (tag.Title != null && tag.Album != null)
            {
                level = TagLevel.Basic;

                if (tag.AlbumArtists is {Length: > 0} || tag.Performers is {Length: > 0})
                {
                    level = TagLevel.Acceptable;

                    if (tag.Track > 0 && tag.Year > 0)
                        level = TagLevel.Comprehensive;
                }
            }

            return level;
        }

        public enum TagLevel
        {
            None,
            Basic,
            Acceptable,
            Comprehensive
        }
    }
}
