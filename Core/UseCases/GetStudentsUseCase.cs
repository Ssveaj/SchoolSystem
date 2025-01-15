
using Core.DTOs;
using Core.Interface.IRepositories;
using Core.Interface.IUseCase;

namespace Core.UseCases
{
    public class GetStudentsUseCase : IGetStudentsUseCase
    {
        private readonly IStudentRepository studentRepository;
        public GetStudentsUseCase(IStudentRepository studentRepository) 
        { 
            this.studentRepository = studentRepository;
        }
        public async Task<List<GetStudentEntityDTO?>> ExecuteUseCaseAsync()
        {
            return await this.studentRepository.GetStudentsAsync().ConfigureAwait(false);
        }
    }
}
