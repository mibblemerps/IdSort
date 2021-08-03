namespace IdSort
{
    public class AudioFile : GenericFile
    {
        public Tags Tags;

        public TagsHelper.TagLevel TagLevel = TagsHelper.TagLevel.None;

        public string CorruptReason;
        public bool IsCorrupted => CorruptReason != null;

        public override string Description => "Audio File";

        public AudioFile(string path, Tags tags) : base(path)
        {
            Tags = tags;
        }
    }
}
