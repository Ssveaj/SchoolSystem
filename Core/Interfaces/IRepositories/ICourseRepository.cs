using Core.Models.BaseResponseCoreModel;
using Core.Models.DTOs;
using Core.Models.RequestViewCoreModel;

namespace Core.Interfaces.IRepositories
{
    public interface ICourseRepository
    {
        Task<Result<bool>> CreateCourseAsync(CreateCourseRequestViewCoreModel requestViewModel);
        Task<List<GetCourseEntityDTO?>> GetCoursesAsync();
        Task<Result<bool>> AddStudentToCourseAsync(AddStudentToCourseRequestViewCoreModel requestViewModel);
        Task<Result<GetCourseEntityDTO>> GetCourseByInternalGuid(string internalGuid);
    }
}
