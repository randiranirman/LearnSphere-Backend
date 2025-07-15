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
builder.Services.AddHttpClient<ITeacherHttpService, TeacherHttpService>();

builder.Services.AddDbContext<CourseRegistrationDbcontext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

// Register Repositories
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<IStudentClassRegistrationRepository, StudentClassRegistrationRepository>();
builder.Services.AddScoped<IStudentRegistrationSubjectRepository, StudentRegistrationSubjectRepository>();
builder.Services.AddScoped<IStudentSubjectRepository, StudentSubjectRepository>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();

// Register Services
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IStudentHttpService, StudentHttpService>();
builder.Services.AddScoped<ITeacherHttpService, TeacherHttpService>();
builder.Services.AddScoped<IStudentRegistrationService, StudentRegistrationService>();

builder.Services.AddScoped<IClassService,ClassService>();
// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        policy => policy.WithOrigins("http://localhost:5173", "https://localhost:5173", "http://localhost:3000", "https://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());
    
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // Use permissive CORS in development
    app.UseCors("AllowAll");
}
else
{
    // Use restrictive CORS in production
    app.UseCors("AllowLocalhost");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Map SignalR hub
app.MapHub<RegistrationHub>("/registrationHub");

app.Run();
