using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Model;
using Model.Model;

namespace Data
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }

        // Nuevo DbSet para logs de error
        public DbSet<ErrorLog> ErrorLogs { get; set; }
    }
}

