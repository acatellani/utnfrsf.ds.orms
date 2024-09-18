using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using utnfrsf.ds.orms.Entidades;

namespace utnfrsf.ds.orms.EFCore
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuarios");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("id");

            builder.Property(u => u.Nombre)
                   .IsRequired()
                   .HasMaxLength(50)
                   .HasColumnName("nombre");

            builder.HasOne(u => u.Rol)
                   .WithMany(r => r.Usuarios)
                   .HasForeignKey("rolid")
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(u => u.Domicilios)
                   .WithOne(d => d.Usuario)
                   .HasForeignKey("usuarioid")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
