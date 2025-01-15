using Core.Enum;

namespace Core.Dtos
{
    public class GetLetterTemplateEntityDTO
    {
        public string Template { get; set; } = default!;
        public LetterType LetterType { get; set; } = default!;
    }
}
