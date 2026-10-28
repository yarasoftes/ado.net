using CollegeMinimalAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Включаем поддержку файлов из папки wwwroot
app.UseDefaultFiles();
app.UseStaticFiles();

// Строка подключения к вашей базе данных
var connectionString = app.Configuration.GetConnectionString("DefaultConnection") 
    ?? @"Server=.\SQLEXPRESS;Database=CollegeDB;Trusted_Connection=True;TrustServerCertificate=True;";

// Маршрут для получения всех групп (14 урок)
app.MapGet("/api/groups", () =>
{
    var repo = new GroupRepository(connectionString);
    var groups = repo.GetAll();
    return Results.Ok(groups);
});

// НОВЫЙ маршрут для получения всех студентов (15 урок, пункт 6)
app.MapGet("/api/students/all", () =>
{
    var repo = new StudentRepository(connectionString);
    var students = repo.GetAll();
    return Results.Ok(students);
});

app.Run();