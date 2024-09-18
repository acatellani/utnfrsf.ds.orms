using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using utnfrsf.ds.orms.Entidades;

namespace utnfrsf.ds.orms.EFCore
{
    public class RolConfiguration : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.ToTable("roles");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id).HasColumnName("id");

            builder.Property(r => r.Nombre)
                   .IsRequired()
                   .HasMaxLength(50)
                   .HasColumnName("nombre");

            builder.HasMany(r => r.Usuarios)
                   .WithOne(u => u.Rol)
                   .OnDelete(DeleteBehavior.SetNull);
                   
        }
    }
}
