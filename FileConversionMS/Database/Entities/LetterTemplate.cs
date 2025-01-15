
using Core.Enum;

namespace FileConversionMS.Database.Entities
{
    public class LetterTemplate
    {
        public int Id { get; set; }
        public string Template { get; set; } = default!;
        public LetterType LetterType { get; set; } = default!;
        public FontType? FontType { get; set; } = default!;
    }
}
