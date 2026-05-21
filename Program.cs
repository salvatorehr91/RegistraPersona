using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Leer cadena de conexión desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("SqlConnection");

// Registrar DbContext con SQL Server
builder.Services.AddDbContext<RegistraPersona.Context.ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<RegistraPersona.Context.ConexionSqlServer>(options =>
    options.UseSqlServer(connectionString));
    
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
