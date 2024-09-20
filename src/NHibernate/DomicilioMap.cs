using FluentNHibernate.Mapping;
using utnfrsf.ds.orms.Entidades;

namespace utnfrsf.ds.orms.NHibernateHelper
{
    public class DomicilioMap : ClassMap<Domicilio>
    {
        public DomicilioMap()
        {
            Table("domicilios");

            Id(x => x.Id).Column("id").GeneratedBy.Identity();
            Map(x => x.Calle).Column("calle").Length(100).Not.Nullable();
            Map(x => x.Numero).Column("numero").Not.Nullable();

            References(x => x.Usuario)
                .Column("usuarioid")
                .Cascade.None();
        }
    }
}
