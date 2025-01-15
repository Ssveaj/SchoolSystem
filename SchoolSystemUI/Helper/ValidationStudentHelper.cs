
using Core.Models.RequestViewCoreModel;
using Core.Models.ValidationModel;


namespace SchoolSystemUI.Helper
{
    public static class ValidationStudentHelper
    {
        public static ValidationResponseCoreModel ValidateStudent(CreateStudentRequestViewCoreModel requestViewModel)
        {
            if (string.IsNullOrEmpty(requestViewModel.StudentName))
            {
                return new ValidationResponseCoreModel
                {
                    Error = "The provided student name is invalid.",
                    Success = false,
                };
            }

            if (string.IsNullOrEmpty(requestViewModel.StudentLastName))
            {
                return new ValidationResponseCoreModel
                {
                    Error = "The provided student lastname is invalid.",
                    Success = false,
                };
            }            
            
            if (string.IsNullOrEmpty(requestViewModel.Address))
            {
                return new ValidationResponseCoreModel
                {
                    Error = "The provided address is invalid.",
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
