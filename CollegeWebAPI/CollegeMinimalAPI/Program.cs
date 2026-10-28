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

var connectionString = app.Configuration.GetConnectionString("DefaultConnection") 
    ?? @"Server=.\SQLEXPRESS;Database=CollegeDB;Trusted_Connection=True;TrustServerCertificate=True;";

app.MapGet("/api/groups", () =>
{
    var repo = new GroupRepository(connectionString);
    var groups = repo.GetAll();
    return Results.Ok(groups);
});

app.Run();