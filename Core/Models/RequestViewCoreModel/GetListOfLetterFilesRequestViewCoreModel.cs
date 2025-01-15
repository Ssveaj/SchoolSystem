namespace Core.Models.RequestViewCoreModel
{
    public class GetListOfLetterFilesRequestViewCoreModel
    {
        public byte[] File { get; set; } = default!;
        public string FileName { get; set; } = default!;
        public DateTime Created { get; set; } = default!;
        public string StudentExternalGuid { get; set; } = default!;
    }
}
