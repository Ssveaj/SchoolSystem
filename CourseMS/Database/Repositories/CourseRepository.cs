using AutoMapper;
using Core.Models.BaseResponseCoreModel;
using Core.Models.DTOs;
using Core.Models.RequestViewCoreModel;
using CourseMS.Database.Entities;
using Core.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CourseMS.Database.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly IServiceScopeFactory scopeFactory;
        private readonly IMapper mapper;
        public CourseRepository(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            this.scopeFactory = scopeFactory;
            this.mapper = mapper;
        }

        public async Task<Result<bool>> AddStudentToCourseAsync(AddStudentToCourseRequestViewCoreModel requestViewModel)
        {
            var methodName = "The method CourseRepository.AddStudentToCourseAsync()";
            try
            {
                using (var scope = scopeFactory.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<CourseMSContext>();
                    var course = await db.Courses.Include(c => c.Students).FirstOrDefaultAsync(x => x.CourseInternalGuid == requestViewModel.CourseInternalGuid).ConfigureAwait(false);
                    var getCourseStudents = course!.Students.Select(s => s.StudentInternalGuid).ToList();

                    foreach (var student in requestViewModel.Students)
                    {
                        if (getCourseStudents.Contains(student.StudentInternalGuid))
                        {
                            return new Result<bool>
                            {
                                Success = false,
                                Error = $"The Student {student.StudentName} already exists in this course.",
                                HttpStatusCode = (int)HttpStatusCode.BadRequest
                            };
                        }

                        course.Students.Add(new CourseStudent
                        {
                            StudentInternalGuid = student.StudentInternalGuid,
                            StudentName = student.StudentName
                        });
                    }

                    await db.SaveChangesAsync().ConfigureAwait(false);
                    return new Result<bool>
                    {
                        Success = true,
                        HttpStatusCode = (int)HttpStatusCode.OK
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<bool>
                {
                    Error = $"{methodName} failed. Reason: {ex.Message}",
                    Success = false,
                    HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task<Result<bool>> CreateCourseAsync(CreateCourseRequestViewCoreModel requestViewModel)
        {
            try
            {
                using (var scope = scopeFactory.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<CourseMSContext>();
                    
                    var course = new Course
                    {
                        CourseName = requestViewModel.CourseName,
                        CourseInternalGuid = Guid.NewGuid().ToString(),
                        Created = DateTimeOffset.Now,
                    };

                    await db.Courses.AddAsync(course);
                    await db.SaveChangesAsync();

                    return new Result<bool>
                    {
                        Success = true,
                        HttpStatusCode = (int)HttpStatusCode.OK
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<bool>
                {
                    Error = $" Failed to create course. Reason: {ex.Message}",
                    Success = false,
                    HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task<Result<GetCourseEntityDTO>> GetCourseByInternalGuid(string internalGuid)
        {
            try
            {
                using (var scope = scopeFactory.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<CourseMSContext>();
                    var course = await db.Courses.FirstOrDefaultAsync(x => x.CourseInternalGuid == internalGuid).ConfigureAwait(false);
                    if (course is null)
                    {
                        return new Result<GetCourseEntityDTO>
                        {
                            Value = null,
                            Error = "The provided course can not be found."
                        };
                    }

                    return new Result<GetCourseEntityDTO>
                    {
                        Value = this.mapper.Map<GetCourseEntityDTO>(course),
                        Success = true,
                    };
                }
            }
            catch(Exception ex)
            {
                return new Result<GetCourseEntityDTO>
                {
                    Error = $"Failed to fetch course for InternalGuid - {internalGuid}. Reason: {ex.Message}",
                    Success = false,
                    HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task<List<GetCourseEntityDTO?>> GetCoursesAsync()
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<CourseMSContext>();
                var course = await db.Courses.Include(x => x.Students).ToListAsync().ConfigureAwait(false);
                return this.mapper.Map<List<GetCourseEntityDTO?>>(course);
            }
        }
    }
}
