

using AutoMapper;
using StudentMS.Database.Entities;
using Core.DTOs;

namespace StudentMS.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            _ = CreateMap<GetStudentEntityDTO, Student>()
               .ForMember(x => x.Id, opt => opt.Ignore());
            _ = CreateMap<Student, GetStudentEntityDTO>();
        }
    }
}
