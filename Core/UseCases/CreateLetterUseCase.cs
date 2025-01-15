
using Core.Models.BaseResponseCoreModel;
using Core.Models.RequestViewCoreModel;
using Core.Interface.IRepositories;
using Core.Interface.IUseCases;
using System.Net;
using Core.Interfaces.IUseCases;

namespace Core.UseCases
{
    public class CreateLetterUseCase : ICreateLetterUseCase
    {
        private readonly ILetterRepository letterRepository; 
        private readonly IPdfConversionUseCase pdfConversionService; 
        private readonly IHtmlGenerationUseCase htmlGenerationUseCase;
        public CreateLetterUseCase(ILetterRepository letterRepository, IPdfConversionUseCase pdfConversionService, IHtmlGenerationUseCase htmlGenerationUseCase)
        {
            this.letterRepository = letterRepository;
            this.pdfConversionService = pdfConversionService;
            this.htmlGenerationUseCase = htmlGenerationUseCase;
        }

        public async Task<Result<bool>> ExecuteUseCaseAsync(CreateLetterRequestViewCoreModel requestViewModel)
        {
            var methodName = "The method CreateLetterUseCase.ExecuteUseCaseAsync()";
            try
            {
                var letterTemplateResult = await this.letterRepository.GetLetterTemplateAsync(requestViewModel.LetterType).ConfigureAwait(false);
                if (letterTemplateResult is null)
                {
                    // If no template is found in the database, use a predefined fallback template 
                    // based on the specified LetterType. This ensures the application can still 
                    // generate letters even when the database is empty or unconfigured.

                    //var template = string.Empty; 
                    //if(requestViewModel.LetterType == Enum.LetterType.Diploma)
                    //{
                    //    template = "<div class='container-fluid' style='background: linear-gradient(to bottom, #e6f2ff, #ffffff);'>\r\n        <div style='height: 10rem; background-color: #003366; color: white; display: flex; justify-content: center; align-items: center;'>\r\n            <div class='row'>\r\n                <div class='col'>\r\n                    <h1>School Diploma</h1>\r\n                </div>\r\n            </div>\r\n        </div>\r\n        <div style='display: flex; justify-content: center; align-items: center;'>\r\n            <h1>Congratulations!</h1>\r\n        </div>\r\n        <div style='display: flex; flex-direction: column; justify-content: center; align-items: center;'>\r\n            <h3>~FirstName~ ~LastName~</h3>\r\n            <h3>~CreationDate~</h3>\r\n        </div>\r\n        <div style='display: flex; flex-direction: column; justify-content: center; align-items: center;'>\r\n            <p>We are thrilled to announce that you have successfully completed your examination!</p>\r\n            <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Velit provident praesentium impedit ducimus, sapiente, iste voluptate iusto eligendi dolorum asperiores fugit esse hic eum accusantium explicabo id. Non, placeat deleniti.</p>\r\n            <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Velit provident praesentium impedit ducimus, sapiente, iste voluptate iusto eligendi dolorum asperiores fugit esse hic eum accusantium explicabo id. Non, placeat deleniti.</p>\r\n        </div>\r\n    </div>";
                    //}
                    //if(requestViewModel.LetterType == Enum.LetterType.Warning)
                    //{
                    //    template = "<div class=\"container-fluid\" style=\"background: linear-gradient(to bottom, #e6f2ff, #ffffff);\">\r\n        <div style=\"height: 10rem; background-color: #003366; color: white; display: flex; justify-content: center; align-items: center;\">\r\n            <div class=\"row\">\r\n                <div class=\"col\">\r\n                    <h1>Warning</h1>\r\n                </div>\r\n            </div>\r\n        </div>\r\n        <div style=\"display: flex; justify-content: center; align-items: center;\">\r\n            <h1>You have been warned!</h1>\r\n        </div>\r\n        <div style=\"display: flex; flex-direction: column; justify-content: center; align-items: center;\">\r\n            <h3>~FirstName~ ~LastName~</h3>\r\n            <h3>~CreationDate~</h3>\r\n        </div>\r\n        <div style=\"display: flex; flex-direction: column; justify-content: center; align-items: center;\">\r\n            <p>We are notifying you that you have been issued a warning due to excessive absence.</p>\r\n            <p>~Description~<</p>\r\n            <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Velit provident praesentium impedit ducimus, sapiente, iste voluptate iusto eligendi dolorum asperiores fugit esse hic eum accusantium explicabo id. Non, placeat deleniti.</p>\r\n        </div>\r\n    </div>";
                    //}
                    //if(requestViewModel.LetterType == Enum.LetterType.Suspended)
                    //{
                    //    template = "<div class=\"container-fluid\" style=\"background: linear-gradient(to bottom, #e6f2ff, #ffffff);\">\r\n        <div style=\"height: 10rem; background-color: #003366; color: white; display: flex; justify-content: center; align-items: center;\">\r\n            <div class=\"row\">\r\n                <div class=\"col\">\r\n                    <h1>Suspended</h1>\r\n                </div>\r\n            </div>\r\n        </div>\r\n        <div style=\"display: flex; justify-content: center; align-items: center;\">\r\n            <h1>You have been suspended!</h1>\r\n        </div>\r\n        <div style=\"display: flex; flex-direction: column; justify-content: center; align-items: center;\">\r\n            <h3>~FirstName~ ~LastName~</h3>\r\n            <h3>~CreationDate~</h3>\r\n        </div>\r\n        <div style=\"display: flex; flex-direction: column; justify-content: center; align-items: center;\">\r\n            <p>We want to inform you that you have been Suspended.</p>\r\n            <p>~Description~<</p>\r\n            <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Velit provident praesentium impedit ducimus, sapiente, iste voluptate iusto eligendi dolorum asperiores fugit esse hic eum accusantium explicabo id. Non, placeat deleniti.</p>\r\n        </div>\r\n    </div>";
                    //}

                    return await Task.FromResult(new Result<bool>()
                    {
                        Error = $"The provided template with {requestViewModel.LetterType} can not be found.",
                        HttpStatusCode = (int)HttpStatusCode.BadRequest,
                        Success = false,

                    });
                }

                var htmlContent = letterTemplateResult.Template;

                var generatedHtmlContent = await this.htmlGenerationUseCase.ExecuteUseCaseAsync(requestViewModel, htmlContent).ConfigureAwait(false);
                if (!generatedHtmlContent.Success)
                {
                    return await Task.FromResult(new Result<bool>()
                    {
                        Error = generatedHtmlContent.Error,
                        HttpStatusCode = generatedHtmlContent.HttpStatusCode,
                        Success = generatedHtmlContent.Success
                    });
                }

                var pdfConversionResult = await this.pdfConversionService.ExecuteUseCaseAsync(generatedHtmlContent.Value!).ConfigureAwait(false);
                if(!pdfConversionResult.Success)
                {
                    return await Task.FromResult(new Result<bool>()
                    {
                        Error = pdfConversionResult.Error,
                        HttpStatusCode = pdfConversionResult.HttpStatusCode,
                        Success = pdfConversionResult.Success
                    });
                }

                var savePdfConversion = new CreateLetterFileViewCoreModel
                {
                    File = pdfConversionResult!.Value!.PdfByte!,
                    StudentExternalGuid = Guid.NewGuid().ToString(),
                };

                var saveLetterFileResult = await this.letterRepository.SaveLetterFileAsync(savePdfConversion).ConfigureAwait(false);
                if (!saveLetterFileResult.Success)
                {
                    return await Task.FromResult(new Result<bool>()
                    {
                        Error = saveLetterFileResult.Error,
                        HttpStatusCode = saveLetterFileResult.HttpStatusCode,
                        Success = saveLetterFileResult.Success
                    });
                }

                return await Task.FromResult(new Result<bool>()
                {
                    HttpStatusCode = (int)HttpStatusCode.OK,
                    Success = true
                });
            }
            catch(Exception ex) 
            {
                return await Task.FromResult(new Result<bool>()
                {
                    Error = $"{methodName} failed. Reason: {ex.Message}",
                    HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                    Success = false,
                });
            }
        }
    }
}
