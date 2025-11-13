using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Model;

namespace Data
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions options) : base(options)
        {
        }

 

        public DbSet<Cliente> Clientes { get; set; }
    }

}

