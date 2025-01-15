

using Core.DTOs;
using Core.Models.BaseResponseCoreModel;
using Core.Models.RequestViewCoreModel;

namespace Core.Interface.IRepositories
{
    public interface IStudentRepository
    {
        Task<Result<bool>> CreateStudentAsync(CreateStudentRequestViewCoreModel requestModel);
        Task<List<GetStudentEntityDTO?>> GetStudentsAsync();
    }
}
