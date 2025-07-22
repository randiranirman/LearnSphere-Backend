using FileStorage.API;
using FileStorage.Application.Interfaces;
using FileStorage.Application.Services;
using FileStorage.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<ICourseHttpService, CourseHttpService>();
builder.Services.AddScoped<ITeacherFilesRepository, TeacherFileRepository>();
builder.Services.AddScoped<IStudentFilesRepository, StudentFileRepository>();

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

builder.Services.AddAppDI(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins); 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
