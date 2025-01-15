using AutoMapper;
using Core.Dtos;
using Core.Enum;
using Core.Models.BaseResponseCoreModel;
using Core.Models.RequestViewCoreModel;
using FileConversionMS.Database.Entities;
using Core.Interface.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FileConversionMS.Database.Repositories
{
    public class LetterRepository : ILetterRepository
    {
        private readonly IServiceScopeFactory scopeFactory;
        private readonly IMapper mapper;

        public LetterRepository(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            this.scopeFactory = scopeFactory;
            this.mapper = mapper;
        }

        public async Task<List<GetLetterFilesEntityDTO?>> GetLetterFilesAsync()
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<FileConversionMSContext>();
                var letterFiles = await db.LetterFiles.ToListAsync().ConfigureAwait(false);
                return this.mapper.Map<List<GetLetterFilesEntityDTO?>>(letterFiles);
            }
        }

        public async Task<GetLetterTemplateEntityDTO> GetLetterTemplateAsync(LetterType letterType)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<FileConversionMSContext>();
                var template = await db.LetterTemplates.FirstOrDefaultAsync(x => x.LetterType == letterType);
                return this.mapper.Map<GetLetterTemplateEntityDTO>(template);
            }
        }

        public async Task<Result<bool>> SaveLetterFileAsync(CreateLetterFileViewCoreModel fileRequestViewModel)
        {
            try
            {
                using (var scope = scopeFactory.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<FileConversionMSContext>();

                    var newLetterFile = new LetterFile
                    {
                        File = fileRequestViewModel.File,
                        Created = DateTime.Now,
                        FileName = fileRequestViewModel.StudentExternalGuid + DateTime.Now + ".pdf",
                        StudentExternalGuid = fileRequestViewModel.StudentExternalGuid,
                    };

                    await db.LetterFiles.AddAsync(newLetterFile);
                    await db.SaveChangesAsync();

                    return new Result<bool>()
                    {
                        HttpStatusCode = (int)HttpStatusCode.OK,
                        Success = true
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<bool>()
                {
                    Error = $"Failed to save the Letter file. Reason: {ex.Message}",
                    HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                    Success = false
                };
            }
        }
    }
}
