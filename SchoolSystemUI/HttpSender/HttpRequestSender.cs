using SchoolSystemUI.Enum;
using System.Text;

namespace SchoolSystemUI.HttpSender
{
    public class HttpRequestSender
    {
        private IConfiguration configuration;
        public HttpRequestSender(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<HttpResponseMessage> ExecuteSenderAsync(SenderOptions httpRequest, string? body = null) 
        {
            var message = new HttpRequestMessage();

            switch (httpRequest)
            {
                case SenderOptions.CreateStudent:
                    message.RequestUri = new Uri($"{this.configuration.GetSection("ApiConfiguration:CreateStudent:Url").Value}");
                    message.Method = HttpMethod.Post;
                    message.Content = new StringContent(body!, Encoding.UTF8, "application/json");
                    break; 
                case SenderOptions.GetStudents:
                    message.RequestUri = new Uri($"{this.configuration.GetSection("ApiConfiguration:GetStudents:Url").Value}");
                    message.Method = HttpMethod.Get;
                    break;                
                case SenderOptions.CreateLetter:
                    message.RequestUri = new Uri($"{this.configuration.GetSection("ApiConfiguration:CreateLetter:Url").Value}");
                    message.Method = HttpMethod.Post;
                    message.Content = new StringContent(body!, Encoding.UTF8, "application/json");
                    break;
                case SenderOptions.GetLetterFiles:
                    message.RequestUri = new Uri($"{this.configuration.GetSection("ApiConfiguration:GetLetterFiles:Url").Value}");
                    message.Method = HttpMethod.Get;
                    break;
                case SenderOptions.CreateCourse:
                    message.RequestUri = new Uri($"{this.configuration.GetSection("ApiConfiguration:CreateCourse:Url").Value}");
                    message.Method = HttpMethod.Post;
                    message.Content = new StringContent(body!, Encoding.UTF8, "application/json");
                    break;
                case SenderOptions.GetCourses:
                    message.RequestUri = new Uri($"{this.configuration.GetSection("ApiConfiguration:GetCourses:Url").Value}");
                    message.Method = HttpMethod.Get;
                    break;
                case SenderOptions.AddStudentToCourse:
                    message.RequestUri = new Uri($"{this.configuration.GetSection("ApiConfiguration:AddStudentToCourse:Url").Value}");
                    message.Method = HttpMethod.Post;
                    message.Content = new StringContent(body!, Encoding.UTF8, "application/json");
                    break;
                default:
                    break;
            }

            var response = new HttpResponseMessage();
            try
            {
                using (var client = new HttpClient())
                {
                    response = await client.SendAsync(message).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return response;
        }
    }
}
