using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentMS.Commands;
using StudentMS.Database;
using StudentMS.Database.Repositories;
using Core.Interface.IRepositories;
using Core.Interface.IUseCase;
using StudentMS.MappingProfile;
using Core.UseCases;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<CreateStudentCommand>();
builder.Services.AddScoped<GetStudentsCommand>();
builder.Services.AddScoped<ICreateStudentUseCase, CreateStudentUseCase>();
builder.Services.AddScoped<IGetStudentsUseCase, GetStudentsUseCase>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddDbContext<StudentMSContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("StudentDB")));

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
