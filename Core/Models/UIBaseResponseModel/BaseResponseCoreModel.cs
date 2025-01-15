namespace Core.Models.UIBaseResponseModel
{
    public class BaseResponseCoreModel<T>
    {
        public int HttpStatusCode { get; set; }
        public bool Success { get; set; }
        public string? Value { get; set; }
        public List<BaseResponseErrorCoreModel>? Errors { get; set; }

        public BaseResponseCoreModel()
        {
        }
    }
}
