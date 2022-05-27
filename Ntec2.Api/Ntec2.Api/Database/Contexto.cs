using Microsoft.EntityFrameworkCore;
using Ntec2.Api.Models;

namespace Ntec2.Api.Database
{
    public class Contexto : DbContext
    {

        public Contexto(DbContextOptions options) : base(options)
        {

        }

        //Clase + Tabla
        public DbSet<Usuario> Usuarios { get; set; }


    }
}
