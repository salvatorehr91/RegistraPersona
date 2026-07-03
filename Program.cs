using Microsoft.EntityFrameworkCore;
// using Microsoft.IdentityModel.Tokens;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Leer cadena de conexión desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("SqlConnection");

// Registrar DbContext con SQL Server
builder.Services.AddDbContext<RegistraPersona.Context.ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<RegistraPersona.Context.ConexionSqlServer>(options =>
    options.UseSqlServer(connectionString));

// Add authentication and authorization services with JWT
// Clave secreta para firmar el token (en producción usar un valor seguro en appsettings o Azure Key Vault)
// var jwtKey = builder.Configuration["Jwt:Key"] ?? "ClaveSuperSecreta123456789";
// var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "https://midominio.com";
// var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "https://midominio.com";

// Configuración de autenticación con JWT Bearer
// builder.Services.
//     AddAuthentication(options =>
//     {
//         options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//         options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//     }).
//     AddBearerToken(options =>
//     {
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = true,
//             ValidateAudience = true,
//             ValidateLifetime = true,
//             ValidateIssuerSigningKey = true,
//             ValidIssuer = jwtIssuer,
//             ValidAudience = jwtAudience,
//             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
//             ClockSkew = TimeSpan.Zero // Eliminar tolerancia de tiempo para expiración
//         };
//     });

builder.Services.AddAuthorization();
builder.Services.AddControllers();
//END


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

// app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
