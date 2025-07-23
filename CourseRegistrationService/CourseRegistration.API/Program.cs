using CourseRegistration.Application.BackgroundProcessing;
using CourseRegistration.Application.Interfaces;
using CourseRegistration.Application.Repositories;
using CourseRegistration.Application.Services;
using CourseRegistration.Infrastructure.Data;
using CourseRegistration.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// =============================================================================
// CONFIGURATION SERVICES
// =============================================================================

// Add controllers with JSON configuration
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    });

// Add API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo 
    { 
        Title = "Course Registration API", 
        Version = "v1",
        Description = "API for managing course registrations, subjects, and student-teacher relationships"
    });
});

// =============================================================================
// INFRASTRUCTURE SERVICES
// =============================================================================

// Add Database Context
builder.Services.AddDbContext<CourseRegistrationDbcontext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database"),
        sqlOptions =>
        {
            sqlOptions.CommandTimeout(300);
            sqlOptions.EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
        }
    );
    
    // Configure query splitting for better performance with multiple includes
    
    // Configure warnings - suppress the multiple collection include warning since we're using split queries
    options.ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.MultipleCollectionIncludeWarning));
    
    // Enable sensitive data logging in development
    if (builder.Environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
    }
});

// Add Redis Caching
var redisConnectionString = builder.Configuration.GetConnectionString("Redis");
if (!string.IsNullOrEmpty(redisConnectionString))
{
    // Add StackExchange Redis for distributed caching
    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = redisConnectionString;
        options.InstanceName = "CourseRegistrationService";
    });
    
    // Add Redis Connection Multiplexer for direct Redis operations if needed
    builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    {
        var configuration = ConfigurationOptions.Parse(redisConnectionString, true);
        configuration.ReconnectRetryPolicy = new ExponentialRetry(5000); // 5 second retry
        configuration.KeepAlive = 180;
        return ConnectionMultiplexer.Connect(configuration);
    });
    
    // Register Cache Service
    builder.Services.AddSingleton<ICacheService, RedisCacheService>();
}
else
{
    // Fallback to in-memory cache if Redis is not configured
    builder.Services.AddMemoryCache();
    // You would need to implement an in-memory cache service here if needed
    // For now, we'll register the Redis service anyway but it might fail
    builder.Services.AddSingleton<ICacheService, RedisCacheService>();
}

// =============================================================================
// AUTHENTICATION & AUTHORIZATION
// =============================================================================

// Add Authentication (if you need it - remove this block if you don't need auth)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "your-super-secret-key-here-must-be-at-least-16-characters"))
        };

        // Configure JWT for SignalR
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/notificationHub"))
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    });

// Add Authorization
builder.Services.AddAuthorization();

// Add SignalR for real-time notifications
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = builder.Environment.IsDevelopment();
});

// Add HttpClient for external service calls
builder.Services.AddHttpClient<IStudentHttpService, StudentHttpService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ExternalServices:UserManagement:BaseUrl"] ?? "https://localhost:7033");
    client.Timeout = TimeSpan.FromSeconds(30);
});

builder.Services.AddHttpClient<ITeacherHttpService, TeacherHttpService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ExternalServices:UserManagement:BaseUrl"] ?? "https://localhost:7033");
    client.Timeout = TimeSpan.FromSeconds(30);
});

// =============================================================================
// REPOSITORY REGISTRATIONS
// =============================================================================

builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<IStudentClassRegistrationRepository, StudentClassRegistrationRepository>();
builder.Services.AddScoped<IStudentRegistrationSubjectRepository, StudentRegistrationSubjectRepository>();
builder.Services.AddScoped<IStudentSubjectRepository, StudentSubjectRepository>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<ITeacherClassRegistrationRepository, TeacherClassRegistrationRepository>();
builder.Services.AddScoped<ITeacherSubjectRepository, TeacherSubjectRepository>();

// =============================================================================
// APPLICATION SERVICE REGISTRATIONS
// =============================================================================

builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IStudentHttpService, StudentHttpService>();
builder.Services.AddScoped<ITeacherHttpService, TeacherHttpService>();
builder.Services.AddScoped<IStudentRegistrationService, StudentRegistrationService>();
builder.Services.AddScoped<ITeacherRegistrationService, TeacherRegistrationService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddSingleton<StudentRegistrationQueueService>();
builder.Services.AddHostedService(provider => provider.GetRequiredService<StudentRegistrationQueueService>());

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
    app.UseSwagger(c =>
    {
        c.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0;
    });
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

// Authentication must come before Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// ? Map your SignalR hub
app.MapHub<RegistrationHub>("/notificationHub");

app.Run();
