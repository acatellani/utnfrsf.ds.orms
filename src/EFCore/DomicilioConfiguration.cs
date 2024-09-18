using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using utnfrsf.ds.orms.Entidades;

namespace utnfrsf.ds.orms.EFCore
{
    public class DomicilioConfiguration : IEntityTypeConfiguration<Domicilio>
    {
        public void Configure(EntityTypeBuilder<Domicilio> builder)
        {
            builder.ToTable("domicilios");

            builder.HasKey(d => d.Id);

            builder.Property(r => r.Id).HasColumnName("id");

            builder.Property(d => d.Calle)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasColumnName("calle");

            builder.Property(d => d.Numero)
                   .IsRequired()
                   .HasColumnName("numero");

            builder.HasOne(d => d.Usuario)
                   .WithMany(u => u.Domicilios)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
