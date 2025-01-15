using Microsoft.AspNetCore.Mvc;
using StudentMS.Helper;
using Core.Interface.IUseCase;
using System.Net;
using Core.Models.RequestViewCoreModel;

namespace StudentMS.Commands
{
    public class CreateStudentCommand
    {
        private readonly ICreateStudentUseCase createStudentUseCase;
        public CreateStudentCommand(ICreateStudentUseCase createStudentUseCase) 
        { 
            this.createStudentUseCase = createStudentUseCase;
        }
        public async Task<IActionResult> ExecuteCommandAsync(CreateStudentRequestViewCoreModel requestModel)
        {
            var methodName = "The method CreateStudentCommand.ExecuteCommandAsync()";
            try
            {
                var validationResult = ValidationStudentHelper.ValidateStudent(requestModel);
                if (!validationResult.Success)
                {
                    return ApiResponseHelper.CreateFailedResponse(validationResult.HttpStatusCode, validationResult.Error, validationResult.Success);
                }

                var usecaseResult = await this.createStudentUseCase.ExecuteUseCaseAsync(requestModel).ConfigureAwait(false);
                if (!usecaseResult.Success)
                {
                    return ApiResponseHelper.CreateFailedResponse(usecaseResult.HttpStatusCode, usecaseResult.Error, usecaseResult.Success);
                }

                return new OkObjectResult(usecaseResult);
            }
            catch(Exception ex)
            {
                return ApiResponseHelper.CreateFailedResponse((int)HttpStatusCode.InternalServerError, $"{methodName} failed. Reason: {ex.Message}", false);
            }
        }
    }
}
