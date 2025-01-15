using Core.Interface.IUseCases;
using FileConversionMS.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FileConversionMS.Commands
{
    public class GetLetterFilesCommand
    {
        private readonly IGetLetterFilesUseCase getLetterFilesUseCase;
        public GetLetterFilesCommand(IGetLetterFilesUseCase getLetterFilesUseCase)
        {
            this.getLetterFilesUseCase = getLetterFilesUseCase;
        }

        public async Task<IActionResult> ExecuteCommandAsync()
        {
            var methodName = "The method GetLetterFileCommand.ExecuteCommandAsync()";
            try
            {
                var usecaseResult = await this.getLetterFilesUseCase.ExecuteUseCaseAsync().ConfigureAwait(false);
                return new OkObjectResult(usecaseResult);
            }
            catch(Exception ex)
            {
                return ApiResponseHelper.CreateFailedResponse((int)HttpStatusCode.InternalServerError, $"{methodName} Failed. Reason: {ex.Message}", false);
            }
        }
    }
}
