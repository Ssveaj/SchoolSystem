using Core.Models.BaseResponseCoreModel;
using Microsoft.AspNetCore.Mvc;

namespace CourseMS.Helper
{
    public class ApiResponseHelper
    {
        public static ObjectResult CreateFailedResponse(int statusCode, string message, bool success)
        {
            var responseModel = new BaseResponseModel()
            {
                HttpStatusCode = statusCode,
                Errors = new List<ApiErrorResponseModel>()
                {
                    new ApiErrorResponseModel
                    {
                        Message = message,
                    }
                },
                Success = success
            };

            return new ObjectResult(responseModel)
            {
                StatusCode = statusCode,
            };
        }
    }
}
