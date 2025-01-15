using Microsoft.JSInterop;

namespace SchoolSystemUI.Helper
{
    public static class IJSRuntimeExtensions
    {
        public static ValueTask SaveAS(this IJSRuntime js, string filename, byte[] content)
        {
            return js.InvokeVoidAsync("SaveAsFile", filename, Convert.ToBase64String(content));
        }

        public static ValueTask DisplayMessage(this IJSRuntime js, string message)
        {
            return js.InvokeVoidAsync("Swal.fire", message);
        }

        public static ValueTask DisplayMessage(this IJSRuntime js, string title, string message, SweetAlertMessageType sweetAlertMessageType)
        {
            try
            {
                return js.InvokeVoidAsync("Swal.fire", title, message, sweetAlertMessageType.ToString());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return ValueTask.CompletedTask;
            }
        }
        public enum SweetAlertMessageType
        {
            question, warning, error, success, info
        }
    }
}
