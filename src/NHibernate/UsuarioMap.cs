using FluentNHibernate.Mapping;
using utnfrsf.ds.orms.Entidades;

namespace utnfrsf.ds.orms.NHibernate
{
    public class UsuarioMap : ClassMap<Usuario>
    {
        public UsuarioMap()
        {
            Table("usuarios");

            Id(x => x.Id).Column("id").GeneratedBy.Identity();
            Map(x => x.Nombre).Column("nombre").Length(50).Not.Nullable();

            References(x => x.Rol, "rolid").Cascade.None();

            HasMany(x => x.Domicilios)
                .Cascade.All()
                .Inverse();
        }
    }
}
