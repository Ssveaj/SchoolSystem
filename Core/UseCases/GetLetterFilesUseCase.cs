
using Core.Dtos;
using Core.Interface.IUseCases;
using Core.Interface.IRepositories;


namespace Core.UseCases
{
    public class GetLetterFilesUseCase : IGetLetterFilesUseCase
    {
        private readonly ILetterRepository letterRepository;
        public GetLetterFilesUseCase(ILetterRepository letterRepository)
        {
            this.letterRepository = letterRepository;
        }
        public async Task<List<GetLetterFilesEntityDTO?>> ExecuteUseCaseAsync()
        {
            return await this.letterRepository.GetLetterFilesAsync().ConfigureAwait(false);
        }
    }
}
