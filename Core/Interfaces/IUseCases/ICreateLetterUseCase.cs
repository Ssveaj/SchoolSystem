using Core.Models.BaseResponseCoreModel;
using Core.Models.RequestViewCoreModel;

namespace Core.Interface.IUseCases
{
    public interface ICreateLetterUseCase
    {
        Task<Result<bool>> ExecuteUseCaseAsync(CreateLetterRequestViewCoreModel requestViewModel);
    }
}
