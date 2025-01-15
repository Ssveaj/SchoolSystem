using CourseMS.Helper;
using Core.Interfaces.IUseCases;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CourseMS.Commands
{
    public class GetCoursesCommand
    {
        private readonly IGetCoursesUseCase getCoursesUseCase;
        public GetCoursesCommand(IGetCoursesUseCase getCoursesUseCase)
        {
            this.getCoursesUseCase = getCoursesUseCase;
        }

        public async Task<IActionResult> ExecuteCommandAsync()
        {
            var methodName = "The method GetCoursesCommand.ExecuteCommandAsync()";
            try
            {
                var usecaseResult = await this.getCoursesUseCase.ExecuteUseCaseAsync().ConfigureAwait(false);
                return new OkObjectResult(usecaseResult);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.CreateFailedResponse((int)HttpStatusCode.InternalServerError, $"{methodName} failed. Reason: {ex.Message}", false);
            }
        }
    }
}
