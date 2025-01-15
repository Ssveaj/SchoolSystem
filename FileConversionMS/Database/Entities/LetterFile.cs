namespace FileConversionMS.Database.Entities
{
    public class LetterFile
    {
        public int Id { get; set; }
        public string FileName { get; set; } = default!;
        public byte[] File { get; set; } = default!;
        public DateTime Created {  get; set; }
        public string StudentExternalGuid { get; set; } = default!;
    }
}
