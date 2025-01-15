

using Core.Interfaces.IRepositories;
using Core.Interfaces.IUseCases;
using Core.Models.DTOs;

namespace Core.UseCases
{
    public class GetCoursesUseCase : IGetCoursesUseCase
    {
        private readonly ICourseRepository courseRepository;
        public GetCoursesUseCase(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        public async Task<List<GetCourseEntityDTO?>> ExecuteUseCaseAsync()
        {
            return await this.courseRepository.GetCoursesAsync().ConfigureAwait(false);
        }
    }
}
