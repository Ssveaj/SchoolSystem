using Core.Models.RequestViewCoreModel;
using Core.Models.UIBaseResponseModel;
using Newtonsoft.Json;
using SchoolSystemUI.Enum;
using SchoolSystemUI.Helper;
using SchoolSystemUI.HttpSender;

namespace SchoolSystemUI.Services
{
    public class StudentService
    {
        private HttpRequestSender requestSender;
        public StudentService(HttpRequestSender requestSender)
        {
            this.requestSender = requestSender;
        }

        public async Task<BaseResponseCoreModel<bool>> CreateStudentAsync(CreateStudentRequestViewCoreModel requestViewModel)
        {
            try
            {
                var validationResult = ValidationStudentHelper.ValidateStudent(requestViewModel);
                if (!validationResult.Success)
                {
                    return ApiResponseHelper.Fail(validationResult.Error!, validationResult.Success);
                }

                string bodyToSend = JsonConvert.SerializeObject(requestViewModel);
                var responseResult = await this.requestSender.ExecuteSenderAsync(SenderOptions.CreateStudent, body: bodyToSend);
                if (responseResult is null)
                {
                    return ApiResponseHelper.Fail("The UI management failed to call to the endpoint - Create Student", false);
                }

                var responsBody = await responseResult!.Content.ReadAsStringAsync().ConfigureAwait(false);
                var result = JsonConvert.DeserializeObject<BaseResponseCoreModel<bool>>(responsBody);
                if(!result.Success)
                {
                    return ApiResponseHelper.Fail(result!.Errors!.Select(x => x.Message).FirstOrDefault() ?? "", result.Success);
                }

                return ApiResponseHelper.Ok(result.Value ?? "", result.Success);
            }
            catch(Exception ex)
            {
                return ApiResponseHelper.Fail(ex.Message, false);
            }
        }

        public async Task<List<GetListOfStudentRequestViewCoreModel>> GetListOfStudentAsync()
        {
            try
            {
                var responseResult = await this.requestSender.ExecuteSenderAsync(SenderOptions.GetStudents);
                if(responseResult is null)
                {
                    return new();
                }

                var responsBody = await responseResult!.Content.ReadAsStringAsync().ConfigureAwait(false);
                var result = JsonConvert.DeserializeObject<List<GetListOfStudentRequestViewCoreModel>>(responsBody);
                if(result is null)
                {
                    return new();
                }

                return result;
            }
            catch(Exception ex)
            {
                return new();
            }
        }
    }
}
