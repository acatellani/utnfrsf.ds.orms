using System.Text;

namespace utnfrsf.ds.orms.Entidades
{
    public class Rol
    {
        public virtual int Id { get; set; }
        public virtual string Nombre { get; set; }
        public virtual IList<Usuario> Usuarios { get; set; }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.AppendLine($"Id: {Id}");
            result.AppendLine($"Nombre: {Nombre}");
            return result.ToString();
        }

    }
}
