using Core.Models.RequestViewCoreModel;
using CourseMS.Helper;
using Core.Interfaces.IUseCases;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CourseMS.Commands
{
    public class CreateCourseCommand
    {
        private readonly ICreateCourseUseCase createCourseUseCase;
        public CreateCourseCommand(ICreateCourseUseCase createCourseUseCase)
        {
            this.createCourseUseCase = createCourseUseCase;
        }

        public async Task<IActionResult> ExecuteCommandAsync(CreateCourseRequestViewCoreModel requestModel)
        {
            var methodName = "The method CreateCourseCommand.ExecuteCommandAsync()";
            try
            {
                var validationResult = ValidationCourseHelper.ValidateCourse(requestModel);
                if (!validationResult.Success)
                {
                    return ApiResponseHelper.CreateFailedResponse(validationResult.HttpStatusCode, validationResult.Error, validationResult.Success);
                }

                var usecaseResult = await this.createCourseUseCase.ExecuteUseCaseAsync(requestModel).ConfigureAwait(false);
                if (!usecaseResult.Success)
                {
                    return ApiResponseHelper.CreateFailedResponse(usecaseResult.HttpStatusCode, usecaseResult.Error, usecaseResult.Success);
                }

                return new OkObjectResult(usecaseResult);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.CreateFailedResponse((int)HttpStatusCode.InternalServerError, $"{methodName} failed. Reason: {ex.Message}", false);
            }
        }
    }
}
