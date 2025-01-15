 using Core.Models.RequestViewCoreModel;
using Core.Models.UIBaseResponseModel;
using Newtonsoft.Json;
using SchoolSystemUI.Enum;
using SchoolSystemUI.Helper;
using SchoolSystemUI.HttpSender;

namespace SchoolSystemUI.Services
{
    public class CourseService
    {
        private HttpRequestSender requestSender;
        public CourseService(HttpRequestSender requestSender)
        {
            this.requestSender = requestSender;
        }

        public async Task<BaseResponseCoreModel<bool>> CreateCourseAsync(CreateCourseRequestViewCoreModel requestViewModel)
        {
            try
            {
                var validationResult = ValidationCourseHelper.ValidateCourse(requestViewModel);
                if (!validationResult.Success)
                {
                    return ApiResponseHelper.Fail(validationResult.Error!, validationResult.Success);
                }

                string bodyToSend = JsonConvert.SerializeObject(requestViewModel);
                var responseResult = await this.requestSender.ExecuteSenderAsync(SenderOptions.CreateCourse, body: bodyToSend);
                if (responseResult is null)
                {
                    return ApiResponseHelper.Fail("The UI management failed to call to the endpoint - Create Course", false);
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
        public async Task<List<GetListOfCourseRequestViewCoreModel>> GetListOfCourseAsync()
        {
            try
            {
                var responseResult = await this.requestSender.ExecuteSenderAsync(SenderOptions.GetCourses);
                if (responseResult is null)
                {
                    return new();
                }

                var responsBody = await responseResult!.Content.ReadAsStringAsync().ConfigureAwait(false);
                var result = JsonConvert.DeserializeObject<List<GetListOfCourseRequestViewCoreModel>>(responsBody);
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
        public async Task<BaseResponseCoreModel<bool>> AddStudentsToCourse(CreateStudentCourseRequestViewCoreModel requestViewModel)
        {
            try
            {
                var validationResult = ValidationCourseHelper.ValidateStudentCourse(requestViewModel);
                if (!validationResult.Success)
                {
                    return ApiResponseHelper.Fail(validationResult.Error!, validationResult.Success);
                }

                string bodyToSend = JsonConvert.SerializeObject(requestViewModel);
                var responseResult = await this.requestSender.ExecuteSenderAsync(SenderOptions.AddStudentToCourse, body: bodyToSend);

                if (responseResult is null)
                {
                    return ApiResponseHelper.Fail("The UI management failed to call to the endpoint - Add student to course", false);
                }

                var responsBody = await responseResult!.Content.ReadAsStringAsync().ConfigureAwait(false);
                var result = JsonConvert.DeserializeObject<BaseResponseCoreModel<bool>>(responsBody);
                if(!result.Success)
                {
                    return ApiResponseHelper.Fail(result.Errors.Select(x => x.Message).FirstOrDefault() ?? "", false);
                }

                return ApiResponseHelper.Ok(result.Value ?? "", result.Success);

            }
            catch(Exception ex)
            {
                return ApiResponseHelper.Fail(ex.Message, false);
            }
        }
    }
}
