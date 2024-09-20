using utnfrsf.ds.orms.Entidades;

namespace utnfrsf.ds.orms.Services
{
    public interface IService
    {

        IList<Rol> GetAllRoles();

        Rol GetRol(int rolId);
        Usuario CreateUsuario(Usuario user);

        Usuario GetUsuario(int id);

        Usuario UpdateUsuario(Usuario user);

        void DeleteUsuario(int usuarioId);

        void ConsultasVariasUsuario();

    }
}
