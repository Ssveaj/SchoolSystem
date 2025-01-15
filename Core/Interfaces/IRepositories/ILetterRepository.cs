using Core.Dtos;
using Core.Enum;
using Core.Models.BaseResponseCoreModel;
using Core.Models.RequestViewCoreModel;


namespace Core.Interface.IRepositories
{
    public interface ILetterRepository
    {
        Task<GetLetterTemplateEntityDTO> GetLetterTemplateAsync(LetterType letterType);
        Task<Result<bool>> SaveLetterFileAsync(CreateLetterFileViewCoreModel fileRequestViewModel);
        Task<List<GetLetterFilesEntityDTO?>> GetLetterFilesAsync();
    }
}
