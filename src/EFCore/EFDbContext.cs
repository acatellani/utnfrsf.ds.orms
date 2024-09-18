using Microsoft.EntityFrameworkCore;
using utnfrsf.ds.orms.Entidades;

namespace utnfrsf.ds.orms.EFCore
{
    public class EFDbContext : DbContext
    {
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Domicilio> Domicilios { get; set; }

        public EFDbContext(DbContextOptions<EFDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RolConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new DomicilioConfiguration());
        }
    }
}
