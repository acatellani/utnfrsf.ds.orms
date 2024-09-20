using System.Text;

namespace utnfrsf.ds.orms.Entidades
{
    public class Domicilio
    {
        public virtual int Id { get; set; }
        public virtual string Calle { get; set; }
        public virtual int Numero { get; set; }
        public virtual Usuario Usuario { get; set; }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.AppendLine($"Id: {Id}");
            result.AppendLine($"Calle: {Calle}");
            result.AppendLine($"Número: {Numero}");

            return result.ToString();
        }
    }

}
