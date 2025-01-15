using FileConversionMS.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Core.Models.RequestViewCoreModel;
using Core.Interface.IUseCases;

namespace FileConversionMS.Commands
{
    public class CreateLetterCommand
    {
        private readonly ICreateLetterUseCase createLetterUseCase;
        public CreateLetterCommand(ICreateLetterUseCase createLetterUseCase) 
        { 
            this.createLetterUseCase = createLetterUseCase;
        }

        public async Task<IActionResult> ExecuteCommandAsync(CreateLetterRequestViewCoreModel requestModel)
        {
            var methodName = "The method CreateLetterCommand.ExecuteCommandAsync()";
            try
            {
                var validationResult = ValidationLetterHelper.ValidateLetterAsync(requestModel);
                if (!validationResult.Success)
                {
                    return ApiResponseHelper.CreateFailedResponse(validationResult.HttpStatusCode, validationResult.Error, validationResult.Success);
                }

                var useCaseResult = await this.createLetterUseCase.ExecuteUseCaseAsync(requestModel).ConfigureAwait(false);
                if (!useCaseResult.Success)
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
