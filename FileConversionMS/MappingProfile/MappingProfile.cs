

using AutoMapper;
using FileConversionMS.Database.Entities;
using Core.Dtos;

namespace FileConversionMS.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            _ = CreateMap<GetLetterTemplateEntityDTO, LetterTemplate>()
               .ForMember(x => x.Id, opt => opt.Ignore())
               .ForMember(x => x.FontType, opt => opt.Ignore());
            _ = CreateMap<LetterTemplate, GetLetterTemplateEntityDTO>();

            _ = CreateMap<GetLetterFilesEntityDTO, LetterFile>()
               .ForMember(x => x.Id, opt => opt.Ignore());
            _ = CreateMap<LetterFile, GetLetterFilesEntityDTO>();
        }
    }
}
