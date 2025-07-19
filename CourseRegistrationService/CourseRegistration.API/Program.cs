using CourseRegistration.Application.Interfaces;
using CourseRegistration.Application.Repositories;
using CourseRegistration.Application.Services;
using CourseRegistration.Infrastructure.Data;
using CourseRegistration.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add SignalR
builder.Services.AddSignalR();

// Add HttpClient for external service calls
builder.Services.AddHttpClient<IStudentHttpService, StudentHttpService>();
builder.Services.AddHttpClient<ITeacherHttpService, TeacherHttpService>();

// Add DbContext
builder.Services.AddDbContext<CourseRegistrationDbcontext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database"),
        sqlOptions =>
        {
            sqlOptions.CommandTimeout(300);
        }));

// Register Repositories
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<IStudentClassRegistrationRepository, StudentClassRegistrationRepository>();
builder.Services.AddScoped<IStudentRegistrationSubjectRepository, StudentRegistrationSubjectRepository>();
builder.Services.AddScoped<IStudentSubjectRepository, StudentSubjectRepository>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<ITeacherClassRegistrationRepository, TeacherClassRegistrationRepository>();
builder.Services.AddScoped<ITeacherSubjectRepository, TeacherSubjectRepository>();

// Register Services
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IStudentHttpService, StudentHttpService>();
builder.Services.AddScoped<ITeacherHttpService, TeacherHttpService>();
builder.Services.AddScoped<IStudentRegistrationService, StudentRegistrationService>();
builder.Services.AddScoped<ITeacherRegistrationService, TeacherRegistrationService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IClassService, ClassService>();

// CORS Policies
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        policy => policy.WithOrigins(
                            "http://localhost:5173",
                            "https://localhost:5173",
                            "http://localhost:3000",
                            "https://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // ? Apply correct CORS policy for development
    app.UseCors("AllowLocalhost");
}
else
{
    //  Apply the same policy in production (or a secure one for deployed frontend)
    app.UseCors("AllowLocalhost");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// ? Map your SignalR hub
app.MapHub<RegistrationHub>("/notificationHub");

app.Run();
