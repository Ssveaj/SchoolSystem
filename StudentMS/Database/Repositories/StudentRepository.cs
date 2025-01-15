using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentMS.Database.Entities;
using Core.DTOs;
using Core.Interface.IRepositories;
using System.Net;
using Core.Models.BaseResponseCoreModel;
using Core.Models.RequestViewCoreModel;

namespace StudentMS.Database.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IServiceScopeFactory scopeFactory;
        private readonly IMapper mapper;
        public StudentRepository(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            this.scopeFactory = scopeFactory;
            this.mapper = mapper;
        }
        public async Task<Result<bool>> CreateStudentAsync(CreateStudentRequestViewCoreModel requestViewModel)
        {
            try
            {
                using (var scope = scopeFactory.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<StudentMSContext>();

                    var student = new Student
                    {
                        FirstName = requestViewModel.StudentName,
                        LastName = requestViewModel.StudentLastName,
                        Address = requestViewModel.Address,
                        Description = requestViewModel.Description,
                        Created = DateTimeOffset.Now,
                        StudentInternalGuid = Guid.NewGuid().ToString()
                    };

                    await db.Students.AddAsync(student);
                    await db.SaveChangesAsync();

                    return new Result<bool>
                    {
                        Success = true,
                        HttpStatusCode = (int)HttpStatusCode.OK
                    };
                }
            }
            catch(Exception ex)
            {
                return new Result<bool>
                {
                    Error = $" Failed to create student. Reason: {ex.Message}",
                    Success = false,
                    HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task<List<GetStudentEntityDTO?>> GetStudentsAsync()
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<StudentMSContext>();
                var student = await db.Students.ToListAsync().ConfigureAwait(false);
                return this.mapper.Map<List<GetStudentEntityDTO?>>(student);
            }
        }
    }
}