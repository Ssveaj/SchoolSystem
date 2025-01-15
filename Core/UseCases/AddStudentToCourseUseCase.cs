
using Core.Interfaces.IRepositories;
using Core.Interfaces.IUseCases;
using Core.Models.BaseResponseCoreModel;
using Core.Models.RequestViewCoreModel;
using System.Net;

namespace Core.UseCases
{
    public class AddStudentToCourseUseCase : IAddStudentToCourseUseCase
    {
        private readonly ICourseRepository courseRepository;
        public AddStudentToCourseUseCase(ICourseRepository courseRepository) 
        { 
            this.courseRepository = courseRepository;
        }

        public async Task<Result<bool>> ExecuteUseCaseAsync(AddStudentToCourseRequestViewCoreModel requestViewModel)
        {
            var methodName = "The method AddStudentToCourseUseCase.ExecuteUseCaseAsync()";
            try
            {
                var courseResult = await this.courseRepository.GetCourseByInternalGuid(requestViewModel.CourseInternalGuid).ConfigureAwait(false);
                if(courseResult.Value is null)
                {
                    return await Task.FromResult(new Result<bool>
                    {
                        Error = $"The course {requestViewModel.CourseInternalGuid} can not be found.",
                        HttpStatusCode = (int)HttpStatusCode.BadRequest,
                        Success = false
                    });
                }

                var courseStudentResult = await this.courseRepository.AddStudentToCourseAsync(requestViewModel).ConfigureAwait(false);
                if(!courseStudentResult.Success)
                {
                    return await Task.FromResult(new Result<bool>
                    {
                        Error = courseStudentResult.Error,
                        HttpStatusCode = (int)HttpStatusCode.BadRequest,
                        Success = false
                    });
                }

                return await Task.FromResult(new Result<bool>
                {
                    HttpStatusCode = (int)HttpStatusCode.OK,
                    Success = true
                });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new Result<bool>
                {
                    Error = $"{methodName} failed. Reason: {ex.Message}",
                    HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                    Success = false
                });
            }
        }
    }
}
