using FluentNHibernate.Mapping;
using utnfrsf.ds.orms.Entidades;

namespace utnfrsf.ds.orms.NHibernateHelper
{
    public class RolMap : ClassMap<Rol>
    {
        public RolMap()
        {
            Table("roles");

            Id(x => x.Id).Column("id").GeneratedBy.Identity();
            Map(x => x.Nombre).Column("nombre").Length(50).Not.Nullable();

            HasMany(x => x.Usuarios)
                .Cascade.All()
                .Inverse();
        }
    }
}
