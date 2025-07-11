using CourseRegistration.Application.Interfaces;
using CourseRegistration.Application.Repositories;
using CourseRegistration.Application.Services;
using CourseRegistration.Infrastructure.Data;
using CourseRegistration.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add SignalR
builder.Services.AddSignalR();

// Add HttpClient for external service calls
builder.Services.AddHttpClient<IStudentHttpService, StudentHttpService>();

builder.Services.AddDbContext<CourseRegistrationDbcontext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

// Register Repositories
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<IStudentClassRegistrationRepository, StudentClassRegistrationRepository>();
builder.Services.AddScoped<IStudentSubjectRepository, StudentSubjectRepository>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();

// Register Services
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IStudentHttpService, StudentHttpService>();
builder.Services.AddScoped<IStudentRegistrationService, StudentRegistrationService>();

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

// Map SignalR hub
app.MapHub<RegistrationHub>("/registrationHub");

app.Run();
