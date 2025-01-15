using Core.Models.UIBaseResponseModel;

namespace SchoolSystemUI.Helper
{
    public class ApiResponseHelper
    {
        public string? Messasge {  get; set; }
        public bool Sucess { get; set; }


        public static BaseResponseCoreModel<bool> Fail(string message, bool success)
        {
            var responseModel = new BaseResponseCoreModel<bool>
            {
                Success = success,
                Errors = new List<BaseResponseErrorCoreModel>
                {
                   new BaseResponseErrorCoreModel
                   {
                       Message = message,
                   }
                },
            };

            return responseModel;
        }
        public static BaseResponseCoreModel<bool> Ok(string value, bool success) 
        {
            var responseModel = new BaseResponseCoreModel<bool>
            {
                Success = success,
                Value = value,
            };

            return responseModel;
        }   
    }
}
