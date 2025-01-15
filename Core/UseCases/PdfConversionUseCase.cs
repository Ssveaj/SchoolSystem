using iText.Html2pdf;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf;
using System.Net;
using iText.Layout;
using iText.Kernel.Geom;
using iText.IO.Font.Constants;
using Core.Models.BaseResponseCoreModel;
using Core.Models.RequestViewCoreModel;
using Core.Interfaces.IUseCases;

namespace Core.UseCases
{
    public class PdfConversionUseCase : IPdfConversionUseCase
    {
        public async Task<Result<PdfConversionViewCoreModel>> ExecuteUseCaseAsync(string htmlContent)
        {
            try
            {
                if (string.IsNullOrEmpty(htmlContent))
                {
                    return await Task.FromResult(new Result<PdfConversionViewCoreModel>()
                    {
                        Error = "The provided html content is invalid.",
                        HttpStatusCode = (int)HttpStatusCode.BadRequest,
                        Success = false
                    });
                }

                using (var memoryStream = new MemoryStream())
                {
                    var properties = new ConverterProperties();
                    HtmlConverter.ConvertToPdf(htmlContent, memoryStream, properties);

                    var customizedPdfResult = await CustomizePdfAsync(memoryStream).ConfigureAwait(false);
                    if (!customizedPdfResult.Success)
                    {
                        return await Task.FromResult(new Result<PdfConversionViewCoreModel>()
                        {
                            Error = customizedPdfResult.Error,
                            HttpStatusCode = customizedPdfResult.HttpStatusCode,
                            Success = customizedPdfResult.Success
                        });
                    }

                    return await Task.FromResult(new Result<PdfConversionViewCoreModel>()
                    {
                        Value = new PdfConversionViewCoreModel { PdfByte = customizedPdfResult!.Value!.PdfByte },
                        HttpStatusCode = (int)HttpStatusCode.OK,
                        Success = true
                    });
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new Result<PdfConversionViewCoreModel>()
                {
                    Error = $"Failed to convert pdf. Reason: {ex.Message}",
                    HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                    Success = false
                });
            }
        }
        private async Task<Result<PdfConversionViewCoreModel>> CustomizePdfAsync(MemoryStream pdfFile)
        {
            try
            {
                var customizedPdf = new MemoryStream();
                PdfDocument pdfDoc = new PdfDocument(new PdfReader(new MemoryStream(pdfFile.ToArray())), new PdfWriter(customizedPdf));
                Document document = new Document(pdfDoc);

                var numberOfPages = pdfDoc.GetNumberOfPages();
                for (int i = 1; i <= numberOfPages; i++)
                {
                    var pdfPage = pdfDoc.GetPage(i);
                    PdfCanvas pdfCanvas = new PdfCanvas(pdfPage.NewContentStreamAfter(), pdfPage.GetResources(), pdfDoc);
                    Rectangle pageSize = pdfPage.GetPageSize();

                    pdfCanvas.SaveState();
                    pdfCanvas.SetFillColor(new DeviceRgb(13, 60, 85));
                    pdfCanvas.Rectangle(pageSize.GetLeft(), pageSize.GetBottom(), pageSize.GetWidth(), 0);
                    pdfCanvas.Fill();
                    pdfCanvas.RestoreState();

                    pdfCanvas.BeginText();
                    pdfCanvas.SetFillColor(new DeviceRgb(255, 255, 255));
                    pdfCanvas.SetFontAndSize(PdfFontFactory.CreateFont(StandardFonts.HELVETICA), 10);

                    pdfCanvas.MoveText(pageSize.GetLeft() + 60, pageSize.GetBottom() + 16);
                    pdfCanvas.ShowText("SchoolSystem - Joel Ferm");
                    pdfCanvas.EndText();

                    pdfCanvas.BeginText();
                    pdfCanvas.MoveText(pageSize.GetRight() - 40, pageSize.GetBottom() + 16);
                    pdfCanvas.ShowText($"{i}/{numberOfPages}");
                    pdfCanvas.EndText();
                }
                document.Close();

                return await Task.FromResult(new Result<PdfConversionViewCoreModel>()
                {
                    Value = new PdfConversionViewCoreModel { PdfByte = customizedPdf.ToArray() },
                    HttpStatusCode = (int)HttpStatusCode.OK,
                    Success = true
                });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new Result<PdfConversionViewCoreModel>()
                {
                    Error = $"PdfConversionUseCase.CustomizePdfAsync() failed to customize pdf. Reason: {ex.Message}",
                    HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                    Success = true
                });
            }
        }
    }
}
