using Assessment_back_end.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
var connection = String.Empty;
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.development.json");
    connection = builder.Configuration.GetConnectionString("SQLCONNECTION");
}
else
{
    connection = Environment.GetEnvironmentVariable("SQLCONNECTION");
}
builder.Services.AddDbContext<DatabaseContext>(options => 
    options.UseSqlServer(connection));

var app = builder.Build();

app.MapControllers();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseHttpsRedirection();


app.Run();