namespace Core.Models.BaseResponseCoreModel
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public string Error { get; set; } = string.Empty;
        public int HttpStatusCode { get; set; }
        public T? Value { get; set; }
        public Result(T value) => Value = value;
        public Result()
        {
        }
    }
}
