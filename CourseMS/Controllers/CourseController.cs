
using Core.Models.RequestViewCoreModel;
using CourseMS.Commands;
using Microsoft.AspNetCore.Mvc;

namespace CourseMS.Controllers
{
    [ApiController]
    public class CourseController : ControllerBase
    {
        [HttpPost("/internal/course")]
        public async Task<IActionResult> CreateCourseAsync(
          [FromBody] CreateCourseRequestViewCoreModel requestModel,
          [FromServices] CreateCourseCommand command) => await command.ExecuteCommandAsync(requestModel).ConfigureAwait(false);

        [HttpGet("/internal/course")]
        public async Task<IActionResult> GetCoursesAsync(
           [FromServices] GetCoursesCommand command) => await command.ExecuteCommandAsync().ConfigureAwait(false);

        [HttpPost("/internal/course/addstudenttocourse")]
        public async Task<IActionResult> AddStudentToCourseAsync(
           [FromBody] AddStudentToCourseRequestViewCoreModel requestModel,
           [FromServices] AddStudentToCourseCommand command) => await command.ExecuteCommandAsync(requestModel).ConfigureAwait(false);
    }
}
