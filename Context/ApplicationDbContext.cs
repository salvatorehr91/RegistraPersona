using Microsoft.EntityFrameworkCore;
using RegistraPersona.Models;

namespace RegistraPersona.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<DTOESTUDIANTE> DTOESTUDIANTE { get; set; } 

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {   
        }
    }

    public class ConexionSqlServer : DbContext
    {
        public ConexionSqlServer(DbContextOptions<ConexionSqlServer> options) : base(options)
        {
        }

        public DbSet<DTOESTUDIANTE> DTOESTUDIANTE { get; set; } 
    }
}
