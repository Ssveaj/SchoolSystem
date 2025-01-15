using Core.Models.RequestViewCoreModel;
using FileConversionMS.Commands;
using Microsoft.AspNetCore.Mvc;

namespace FileConversionMS.Controllers
{
    public class LetterController : ControllerBase
    {
        [HttpPost("/internal/letter")]
        public async Task<IActionResult> CreateStudentLetterAsync(
           [FromBody] CreateLetterRequestViewCoreModel requestModel,
           [FromServices] CreateLetterCommand command) => await command.ExecuteCommandAsync(requestModel).ConfigureAwait(false);
        
        [HttpGet("/internal/letter")]
        public async Task<IActionResult> GetLetterFilesAsync(
           [FromServices] GetLetterFilesCommand command) => await command.ExecuteCommandAsync().ConfigureAwait(false);
    }
}
    