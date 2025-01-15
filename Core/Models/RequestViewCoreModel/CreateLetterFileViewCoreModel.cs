
namespace Core.Models.RequestViewCoreModel
{
    public class CreateLetterFileViewCoreModel
    {
        public byte[] File { get; set; } = default!;
        public string StudentExternalGuid { get; set; } = default!;
    }
}
