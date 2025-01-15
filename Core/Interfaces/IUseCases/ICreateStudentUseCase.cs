

using Core.Models.BaseResponseCoreModel;
using Core.Models.RequestViewCoreModel;

namespace Core.Interface.IUseCase
{
    public interface ICreateStudentUseCase
    {
        Task<Result<bool>> ExecuteUseCaseAsync(CreateStudentRequestViewCoreModel requestViewModel);
    }
}
