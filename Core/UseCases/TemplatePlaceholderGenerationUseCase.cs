using Core.Enum;
using Core.Interface.IUseCases;
using Core.Models.BaseResponseCoreModel;
using Core.Models.RequestViewCoreModel;
using System.Net;

namespace Core.UseCases
{
    public class HtmlGenerationUseCase : IHtmlGenerationUseCase
    {
        public async Task<Result<string>> ExecuteUseCaseAsync(CreateLetterRequestViewCoreModel requestViewModel, string htmlContent)
        {
            try
            {
                if (string.IsNullOrEmpty(htmlContent))
                {
                    return await Task.FromResult(new Result<string>
                    {
                        Error = "The provided template is invalid.",
                        HttpStatusCode = (int)HttpStatusCode.BadRequest,
                        Success = false,
                    });
                }

                var placeholders = new Dictionary<string, string>();

                switch (requestViewModel.LetterType)
                {
                    case LetterType.Diploma:
                        placeholders.Add("~FirstName~", requestViewModel.StudentName);
                        placeholders.Add("~LastName~", requestViewModel.StudentLastName);
                        placeholders.Add("~CreationDate~", DateTime.UtcNow.ToString("yyyy-MM-dd"));
                        break;

                    case LetterType.Suspended:
                        placeholders.Add("~FirstName~", requestViewModel.StudentName);
                        placeholders.Add("~LastName~", requestViewModel.StudentLastName);
                        placeholders.Add("~CreationDate~", DateTime.UtcNow.ToString("yyyy-MM-dd"));
                        placeholders.Add("~Description~", requestViewModel!.Description!);
                        break;

                    case LetterType.Warning:
                        placeholders.Add("~FirstName~", requestViewModel.StudentName);
                        placeholders.Add("~LastName~", requestViewModel.StudentLastName);
                        placeholders.Add("~CreationDate~", DateTime.UtcNow.ToString("yyyy-MM-dd"));
                        placeholders.Add("~Description~", requestViewModel!.Description!);
                        break;

                    default:
                        return new Result<string>
                        {
                            Error = $"The provided {requestViewModel.LetterType} is invalid.",
                            HttpStatusCode = (int)HttpStatusCode.BadRequest,
                            Success = false,
                        };
                }

                foreach (var placeholder in placeholders)
                {
                    htmlContent = htmlContent.Replace(placeholder.Key, placeholder.Value);
                }

                return await Task.FromResult(new Result<string>
                {
                    Value = htmlContent,
                    HttpStatusCode = (int)HttpStatusCode.OK,
                    Success = true
                });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new Result<string>
                {
                    Error = $"HtmlGenerationUseCase.ExecuteUseCaseAsync() failed to generate placeholders for the template. Reason: {ex.Message}",
                    HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                    Success = false,
                });
            }
        }
    }
}
