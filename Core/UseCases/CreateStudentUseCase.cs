
using Core.Interface.IRepositories;
using Core.Interface.IUseCase;
using Core.Models.BaseResponseCoreModel;
using Core.Models.RequestViewCoreModel;
using System.Net;


namespace Core.UseCases
{
    public class CreateStudentUseCase : ICreateStudentUseCase
    {
        private readonly IStudentRepository studentRepository;
        public CreateStudentUseCase(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }
        public async Task<Result<bool>> ExecuteUseCaseAsync(CreateStudentRequestViewCoreModel requestViewModel)
        {
            var methodName = "The method CreateStudentUseCase.ExecuteUseCaseAsync()";
            try 
            {
                var studentResult = await this.studentRepository.CreateStudentAsync(requestViewModel).ConfigureAwait(false);
                if (!studentResult.Success)
                {
                    return await Task.FromResult(new Result<bool>
                    {
                        Error = studentResult.Error,
                        Success = studentResult.Success,
                        HttpStatusCode = studentResult.HttpStatusCode
                    });
                }

                return await Task.FromResult(new Result<bool>
                {
                    Success = studentResult.Success,
                    HttpStatusCode = studentResult.HttpStatusCode
                });
            }
            catch(Exception ex)
            {
                return await Task.FromResult(new Result<bool>
                {
                    Error = $"{methodName} failed. Reason: {ex.Message}",
                    Success = false,
                    HttpStatusCode = (int)HttpStatusCode.InternalServerError 
                });
            }
        }
    }
}
