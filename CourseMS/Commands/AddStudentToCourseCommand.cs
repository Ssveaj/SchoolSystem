
using Core.Models.RequestViewCoreModel;
using CourseMS.Helper;
using Core.Interfaces.IUseCases;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CourseMS.Commands
{
    public class AddStudentToCourseCommand
    {
        private readonly IAddStudentToCourseUseCase addStudentToCourseUseCase;
        public AddStudentToCourseCommand(IAddStudentToCourseUseCase addStudentToCourseUseCase)
        {
            this.addStudentToCourseUseCase = addStudentToCourseUseCase;
        }

        public async Task<IActionResult> ExecuteCommandAsync(AddStudentToCourseRequestViewCoreModel requestViewModel)
        {
            var methodName = "The method AddStudentToCourseCommand.ExecuteCommandAsync()";
            try
            {
                var validationResult = ValidationCourseHelper.ValidateStudentCourse(requestViewModel);
                if(!validationResult.Success)
                {
                    return ApiResponseHelper.CreateFailedResponse(validationResult.HttpStatusCode, validationResult.Error, validationResult.Success);
                }

                var useCaseResult = await this.addStudentToCourseUseCase.ExecuteUseCaseAsync(requestViewModel).ConfigureAwait(false);
                if(!useCaseResult.Success) 
                {
                    return ApiResponseHelper.CreateFailedResponse(useCaseResult.HttpStatusCode, useCaseResult.Error, useCaseResult.Success);
                }

                return new OkObjectResult(useCaseResult);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.CreateFailedResponse((int)HttpStatusCode.InternalServerError, $"{methodName} failed. Reason: {ex.Message}", false);
            }
        }
    }
}
