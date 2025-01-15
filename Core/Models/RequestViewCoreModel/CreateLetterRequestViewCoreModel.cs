using Core.Enum;

namespace Core.Models.RequestViewCoreModel
{
    public class CreateLetterRequestViewCoreModel
    {
        public string StudentName { get; set; } = default!;
        public string StudentLastName { get; set; } = default!;
        public string StudentInternalGuid { get; set; } = default!;
        public LetterType LetterType { get; set; } = default!;
        public string? Description { get; set; }
    }
}
