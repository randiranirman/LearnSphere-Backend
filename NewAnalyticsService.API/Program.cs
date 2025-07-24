using Microsoft.EntityFrameworkCore;
using NewAnalyticsServcie.Application.Interfaces;
using NewAnalyticsService.Infrastructure.Data;
using NewAnalyticsService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<IStudentDetailsHttpRepository, StudentDetailsHttpRepository>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7293"); // base URL only
    client.Timeout = TimeSpan.FromSeconds(30);
});

builder.Services.AddHttpClient<ITeacherSubjectHttpService, TeacherSubjectHttpService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7293"); // base URL only
    client.Timeout = TimeSpan.FromSeconds(30);
});

builder.Services.AddHttpClient<IAssignmentHttpService, AssignmentHttpService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7212"); // base URL only
    client.Timeout = TimeSpan.FromSeconds(30);
});

builder.Services.AddScoped<IStudentsRepository, StudentRepository>();
builder.Services.AddScoped<IAnalyticsRepository, AnalyticsRepository>();
builder.Services.AddScoped<IMarksRepository, MarksRepository>();

builder.Services.AddDbContext<NewAnalyticsServiceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database"))
);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // Allow frontend URL
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
