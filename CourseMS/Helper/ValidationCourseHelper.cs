using Core.Models.BaseResponseCoreModel;
using Core.Models.RequestViewCoreModel;
using System.Net;

namespace CourseMS.Helper
{
    public static class ValidationCourseHelper
    {
        public static Result<bool> ValidateCourse(CreateCourseRequestViewCoreModel requestModel)
        {
            if (string.IsNullOrEmpty(requestModel.CourseName))
            {
                return new Result<bool>
                {
                    Error = "The provided course name is invalid.",
                    Success = false,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest
                };
            }

            return new Result<bool>
            {
                Success = true
            };
        }
        public static Result<bool> ValidateStudentCourse(AddStudentToCourseRequestViewCoreModel requestModel)
        {
            if (string.IsNullOrEmpty(requestModel.CourseInternalGuid))
            {
                return new Result<bool>
                {
                    Error = "The provided course is invalid.",
                    Success = false,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest
                };
            } 
            
            if (!requestModel.Students.Any())
            {
                return new Result<bool>
                {
                    Error = "The provided student/s are empty.",
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
