namespace Core.Dtos
{
    public class GetLetterFilesEntityDTO
    {
        public byte[] File { get; set; } = default!;
        public string StudentExternalGuid { get; set; } = default!;
        public string FileName { get; set; } = default!;
        public DateTime Created { get; set; } = default!;
    }
}
