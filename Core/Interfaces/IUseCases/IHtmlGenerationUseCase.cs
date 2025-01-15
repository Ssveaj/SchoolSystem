using Core.Models.BaseResponseCoreModel;
using Core.Models.RequestViewCoreModel;

namespace Core.Interface.IUseCases
{
    public interface IHtmlGenerationUseCase
    {
        Task<Result<string>> ExecuteUseCaseAsync(CreateLetterRequestViewCoreModel requestViewModel, string htmlContent);
    }
}
