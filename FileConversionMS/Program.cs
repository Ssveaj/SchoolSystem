using AutoMapper;
using FileConversionMS.Commands;
using FileConversionMS.Database;
using FileConversionMS.Database.Repositories;
using Core.Interface.IRepositories;
using Core.Interface.IUseCases;
using FileConversionMS.MappingProfile;
using Core.UseCases;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces.IUseCases;


var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<CreateLetterCommand>();
builder.Services.AddScoped<GetLetterFilesCommand>();
builder.Services.AddScoped<ICreateLetterUseCase, CreateLetterUseCase>();
builder.Services.AddScoped<IPdfConversionUseCase, PdfConversionUseCase>();
builder.Services.AddScoped<IHtmlGenerationUseCase, HtmlGenerationUseCase>();
builder.Services.AddScoped<IGetLetterFilesUseCase,  GetLetterFilesUseCase>();
builder.Services.AddScoped<ILetterRepository, LetterRepository>();
builder.Services.AddDbContext<FileConversionMSContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("FileConversionDB")));


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
