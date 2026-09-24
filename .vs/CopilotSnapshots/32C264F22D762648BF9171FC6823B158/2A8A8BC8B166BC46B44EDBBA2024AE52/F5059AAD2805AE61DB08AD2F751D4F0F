using Backend.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Incidencia> Incidencias { get; set; }
        public DbSet<Comentarios> Comentarios { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
    }
}//relacionamos las clases con la base de datos, para que se creen las tablas y se pueda hacer el CRUD( Crear, Leer, Actualizar y Borrar) o manejarlas desde la base de datos, para eso se usa el DbContext de Entity Framework Core.