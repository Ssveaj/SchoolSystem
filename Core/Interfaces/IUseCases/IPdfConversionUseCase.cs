using Core.Models.BaseResponseCoreModel;
using Core.Models.RequestViewCoreModel;

namespace Core.Interfaces.IUseCases
{
    public interface IPdfConversionUseCase
    {
        Task<Result<PdfConversionViewCoreModel>> ExecuteUseCaseAsync(string htmlContent);
    }
}
