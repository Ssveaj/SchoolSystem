

using Core.Dtos;

namespace Core.Interface.IUseCases
{
    public interface IGetLetterFilesUseCase
    {
        Task<List<GetLetterFilesEntityDTO?>> ExecuteUseCaseAsync();
    }
}
