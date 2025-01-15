
using Core.Models.BaseResponseCoreModel;
using Core.Models.RequestViewCoreModel;

namespace Core.Interfaces.IUseCases
{
    public interface ICreateCourseUseCase
    {
        Task<Result<bool>> ExecuteUseCaseAsync(CreateCourseRequestViewCoreModel requestViewModel);
    }
}
