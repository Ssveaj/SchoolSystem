using Core.Models.BaseResponseCoreModel;
using Core.Models.RequestViewCoreModel;

namespace Core.Interfaces.IUseCases
{
    public interface IAddStudentToCourseUseCase
    {
        Task<Result<bool>> ExecuteUseCaseAsync(AddStudentToCourseRequestViewCoreModel requestViewModel);
    }
}
