using Microsoft.AspNetCore.Mvc;
using StudentMS.Helper;
using Core.Interface.IUseCase;
using System.Net;

namespace StudentMS.Commands
{
    public class GetStudentsCommand
    {
        private readonly IGetStudentsUseCase getStudentsUseCase;
        public GetStudentsCommand(IGetStudentsUseCase getStudentsUseCase) 
        { 
            this.getStudentsUseCase = getStudentsUseCase;
        }
        public async Task<IActionResult> ExecuteCommandAsync()
        {
            var methodName = "The method GetStudentsCommand.ExecuteCommandAsync()";
            try
            {
                var usecaseResult = await this.getStudentsUseCase.ExecuteUseCaseAsync().ConfigureAwait(false);
                return new OkObjectResult(usecaseResult);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.CreateFailedResponse((int)HttpStatusCode.InternalServerError, $"{methodName} failed. Reason: {ex.Message}", false);
            }
        }
    }
}
