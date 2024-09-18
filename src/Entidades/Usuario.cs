using System.Text;

namespace utnfrsf.ds.orms.Entidades
{
    public class Usuario
    {
        public virtual int Id { get; set; }
        public virtual string Nombre { get; set; }
        public virtual Rol Rol { get; set; }
        public virtual IList<Domicilio> Domicilios { get; set; }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.AppendLine($"Id: {Id}");
            result.AppendLine($"Nombre: {Nombre}");
            if (Rol != null)
            {
                result.AppendLine($"Rol: {Rol}"); // Asegúrate de que la clase Rol tenga una implementación adecuada de ToString()
            }
            if (Domicilios != null && Domicilios.Any())
            {
                result.AppendLine("Domicilios:");
                foreach (var domicilio in Domicilios)
                {
                    result.AppendLine($"- {domicilio}"); // Asegúrate de que la clase Domicilio tenga una implementación adecuada de ToString()
                }
            }

            return result.ToString();
        }
    }
}
