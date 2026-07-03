using Microsoft.EntityFrameworkCore;
using RegistraPersona.Models;

namespace RegistraPersona.Context
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<DTOESTUDIANTE> DTOESTUDIANTE { get; set; }
    }

    public class ConexionSqlServer : DbContext
    {
        public ConexionSqlServer(DbContextOptions<ConexionSqlServer> options) : base(options)
        {
        }

        public DbSet<DTOESTUDIANTE> DTOESTUDIANTE { get; set; } 
    }
}
