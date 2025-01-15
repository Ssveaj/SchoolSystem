using Core.Models.DTOs;

namespace Core.Interfaces.IUseCases
{
    public interface IGetCoursesUseCase
    {
        Task<List<GetCourseEntityDTO?>> ExecuteUseCaseAsync();
    }
}
