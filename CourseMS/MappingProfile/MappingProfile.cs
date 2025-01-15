

using AutoMapper;
using Core.Models.DTOs;
using CourseMS.Database.Entities;


namespace CourseMS.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            _ = CreateMap<Course, GetCourseEntityDTO>();

            _ = CreateMap<GetCourseEntityDTO, Course>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            _ = CreateMap<CourseStudent, GetCourseStudentEntityDTO>();

            _ = CreateMap<GetCourseStudentEntityDTO, CourseStudent>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Course, x => x.Ignore());
        }
    }
}
