using Core.Interfaces.IRepositories;
using Core.Interfaces.IUseCases;
using Core.Models.BaseResponseCoreModel;
using Core.Models.RequestViewCoreModel;

namespace Core.UseCases
{
    public class CreateCourseUseCase : ICreateCourseUseCase
    {
        private readonly ICourseRepository courseRepository;
        public CreateCourseUseCase(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        public async Task<Result<bool>> ExecuteUseCaseAsync(CreateCourseRequestViewCoreModel requestViewModel)
        {
            var courseResult = await this.courseRepository.CreateCourseAsync(requestViewModel).ConfigureAwait(false);
            if (!courseResult.Success)
            {
                return await Task.FromResult(new Result<bool>
                {
                    Error = courseResult.Error,
                    Success = courseResult.Success,
                    HttpStatusCode = courseResult.HttpStatusCode
                });
            }

            return await Task.FromResult(new Result<bool>
            {
                Success = courseResult.Success,
                HttpStatusCode = courseResult.HttpStatusCode
            });
        }
    }
}
