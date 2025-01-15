using CurrieTechnologies.Razor.SweetAlert2;
using Radzen;
using SchoolSystemUI.Components;
using SchoolSystemUI.Helper;
using SchoolSystemUI.HttpSender;
using SchoolSystemUI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddRadzenComponents();
builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<LetterService>();
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<HttpRequestSender>();
builder.Services.AddScoped<JSInteropHelper>();
builder.Services.AddSweetAlert2();
builder.Services.AddSweetAlert2(options => {
    options.Theme = SweetAlertTheme.Default;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
