using Core.Models.RequestViewCoreModel;
using Core.Models.UIBaseResponseModel;
using Newtonsoft.Json;
using SchoolSystemUI.Enum;
using SchoolSystemUI.Helper;
using SchoolSystemUI.HttpSender;

namespace SchoolSystemUI.Services
{
    public class LetterService
    {
        private HttpRequestSender requestSender;
        private readonly StudentService studentService;

        public LetterService(HttpRequestSender requestSender, StudentService studentService)
        {
            this.requestSender = requestSender;
            this.studentService = studentService;
        }
        public async Task<BaseResponseCoreModel<bool>> CreateLetterAsync(CreateLetterRequestViewCoreModel requestViewModel)
        {
            try
            {
                var validationResult = ValidationLetterHelper.ValidateLetter(requestViewModel);
                if (!validationResult.Success)
                {
                    return ApiResponseHelper.Fail(validationResult.Error!, validationResult.Success);
                }

                string bodyToSend = JsonConvert.SerializeObject(requestViewModel);
                var responseResult = await this.requestSender.ExecuteSenderAsync(SenderOptions.CreateLetter, body: bodyToSend);
                if (!responseResult.IsSuccessStatusCode)
                {
                    return ApiResponseHelper.Fail("The UI management failed to call to the endpoint - Create Letter", false);
                }

                var responsBody = await responseResult!.Content.ReadAsStringAsync().ConfigureAwait(false);
                var result = JsonConvert.DeserializeObject<BaseResponseCoreModel<bool>>(responsBody);
                if(!result.Success)
                {
                    return ApiResponseHelper.Fail(result!.Errors!.Select(x => x.Message).FirstOrDefault() ?? "", result.Success);
                }

                return ApiResponseHelper.Ok(result.Value ?? "", result.Success);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Fail(ex.Message, false);
            }
        }
        public async Task<List<GetListOfLetterFilesRequestViewCoreModel>> GetListOfLetterFiles()
        {
            try
            {
                var responseResult = await this.requestSender.ExecuteSenderAsync(SenderOptions.GetLetterFiles);
                if (responseResult is null)
                {
                    return new();
                }

                var responsBody = await responseResult.Content.ReadAsStringAsync().ConfigureAwait(false);
                var result = JsonConvert.DeserializeObject<List<GetListOfLetterFilesRequestViewCoreModel>>(responsBody);
                return result;
            }
            catch(Exception ex)
            {
                return new();
            }
        }
    }
}
