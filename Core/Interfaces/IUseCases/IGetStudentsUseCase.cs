

using Core.DTOs;

namespace Core.Interface.IUseCase
{
    public interface IGetStudentsUseCase
    {
        Task<List<GetStudentEntityDTO?>> ExecuteUseCaseAsync();
    }
}
