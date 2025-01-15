
using Core.Models.RequestViewCoreModel;
using Core.Models.ValidationModel;

namespace SchoolSystemUI.Helper
{
    public static class ValidationCourseHelper
    {
        public static ValidationResponseCoreModel ValidateCourse(CreateCourseRequestViewCoreModel requestViewModel)
        {
            if (string.IsNullOrEmpty(requestViewModel.CourseName))
            {
                return new ValidationResponseCoreModel
                {
                    Error = "The provided course name is invalid.",
                    Success = false,
                };
            }

            return new ValidationResponseCoreModel
            {
                Success = true
            };
        } 
        public static ValidationResponseCoreModel ValidateStudentCourse(CreateStudentCourseRequestViewCoreModel requestViewModel)
        {
            if (!requestViewModel.Students.Any())
            {
                return new ValidationResponseCoreModel
                {
                    Error = "The provided student/s are empty.",
                    Success = false,
                };
            }

            return new ValidationResponseCoreModel
            {
                Success = true
            };
        }
    }
}
