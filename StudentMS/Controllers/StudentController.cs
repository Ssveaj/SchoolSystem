
using Core.Models.RequestViewCoreModel;
using Microsoft.AspNetCore.Mvc;
using StudentMS.Commands;

namespace StudentMS.Controllers
{
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpPost("/internal/student")]
        public async Task<IActionResult> CreateStudentAsync(
           [FromBody] CreateStudentRequestViewCoreModel requestModel,
           [FromServices] CreateStudentCommand command) => await command.ExecuteCommandAsync(requestModel).ConfigureAwait(false);
        
        [HttpGet("/internal/student")]
        public async Task<IActionResult> GetStudentsAsync(
           [FromServices] GetStudentsCommand command) => await command.ExecuteCommandAsync().ConfigureAwait(false);
    }
}
