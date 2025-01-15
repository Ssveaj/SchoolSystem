using AutoMapper;
using CourseMS.Commands;
using CourseMS.Database;
using CourseMS.Database.Repositories;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IUseCases;
using CourseMS.MappingProfile;
using Core.UseCases;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<CreateCourseCommand>();
builder.Services.AddScoped<GetCoursesCommand>();
builder.Services.AddScoped<AddStudentToCourseCommand>();
builder.Services.AddScoped<ICreateCourseUseCase, CreateCourseUseCase>();
builder.Services.AddScoped<IGetCoursesUseCase, GetCoursesUseCase>();
builder.Services.AddScoped<IAddStudentToCourseUseCase, AddStudentToCourseUseCase>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddDbContext<CourseMSContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CourseDB")));
builder.Services.AddControllers().AddJsonOptions(options => 
               options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
