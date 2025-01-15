
using Core.Models.BaseResponseCoreModel;
using Core.Models.RequestViewCoreModel;
using System.Net;

namespace StudentMS.Helper
{
    public static class ValidationStudentHelper
    {
        public static Result<bool> ValidateStudent(CreateStudentRequestViewCoreModel viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.StudentName))
            {
                return new Result<bool>
                {
                    Error = "The provided student firstname is invalid.",
                    Success = false,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest
                };
            }

            if (string.IsNullOrEmpty(viewModel.StudentLastName))
            {
                return new Result<bool>
                {
                    Error = "The provided student lastname is invalid.",
                    Success = false,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest
                };
            }

            return new Result<bool>
            {
                Success = true
            };
        }
    }
}
