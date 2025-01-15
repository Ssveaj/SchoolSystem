using Core.Enum;
using Core.Models.RequestViewCoreModel;
using Core.Models.ValidationModel;


namespace SchoolSystemUI.Helper
{
    public static class ValidationLetterHelper
    {
        public static ValidationResponseCoreModel ValidateLetter(CreateLetterRequestViewCoreModel viewModel)
        {
            List<LetterType> whiteList = new List<LetterType> { LetterType.Diploma, LetterType.Warning, LetterType.Suspended };
            if (string.IsNullOrEmpty(viewModel.StudentInternalGuid))
            {
                return new ValidationResponseCoreModel
                {
                    Error = "The provided student internal guid is invalid.",
                    Success = false,
                };
            }

            if (!whiteList.Contains(viewModel.LetterType))
            {
                return new ValidationResponseCoreModel
                {
                    Error = "The provided letter type is invalid.",
                    Success = false,
                };
            }

            if ((viewModel.LetterType == LetterType.Warning || viewModel.LetterType == LetterType.Suspended) && string.IsNullOrEmpty(viewModel.Description))
            {
                return new ValidationResponseCoreModel
                {
                    Error = "The provided description can not be empty",
                    Success = true,
                };
            }

            return new ValidationResponseCoreModel
            {
                Success = true
            };
        }
    }
}
