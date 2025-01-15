using Microsoft.JSInterop;

namespace SchoolSystemUI.Helper
{
    public class JSInteropHelper
    {
        private readonly IJSRuntime jsRuntime;   

        public JSInteropHelper(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public async Task OpenPdfFileAsync(byte[] file)
        {
            var base64File = Convert.ToBase64String(file);
            await jsRuntime.InvokeVoidAsync("openPdfBlob", base64File);
        }
    }
}
