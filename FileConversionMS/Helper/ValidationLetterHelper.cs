
using Core.Enum;
using Core.Models.BaseResponseCoreModel;
using Core.Models.RequestViewCoreModel;
using System.Net;


namespace FileConversionMS.Helper
{
    public static class ValidationLetterHelper
    {
        public static Result<bool> ValidateLetterAsync(CreateLetterRequestViewCoreModel requestModel)
        {
            List<LetterType> whiteList = new List<LetterType> { LetterType.Suspended, LetterType.Warning, LetterType.Diploma };

            if (string.IsNullOrEmpty(requestModel.StudentName))
            {
                return new Result<bool>()
                {
                    Error = "The provided student name is invalid.",
                    Success = false,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest
                };
            }

            if (string.IsNullOrEmpty(requestModel.StudentLastName))
            {
                return new Result<bool>()
                {
                    Error = "The provided student lastname is invalid.",
                    Success = false,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest
                };
            }

            if (!whiteList.Contains(requestModel.LetterType))
            {
                return new Result<bool>()
                {
                    Error = $"The provided letter type {requestModel.LetterType} is invalid.",
                    Success = false,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest
                };
            }

            if ((requestModel.LetterType == LetterType.Warning || requestModel.LetterType == LetterType.Suspended) && string.IsNullOrEmpty(requestModel.Description))
            {
                return new Result<bool>()
                {
                    Error = "The provided description can not be empty",
                    Success = false,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest
                };
            }

            return new Result<bool>()
            {
                Success = true
            };
        }
    }
}
