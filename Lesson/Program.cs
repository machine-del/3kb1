using Lesson.Middlewares;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Formatting.Json;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Infrastructure.Data;
using TestingPlatform.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")
));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ITestRepository, TestRepository>();
builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<ITestRepository, TestRepository>();
builder.Services.AddScoped<IAttemptRepository, AttemptRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

builder.Services.AddAutoMapper(cfg => cfg.AddMaps("TestingPlatform.Infrastructure"));
builder.Services.AddAutoMapper(cfg => cfg.AddMaps("Lesson"));

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File(formatter: new JsonFormatter(), path: "logs/structured.json")
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();
app.Run();
