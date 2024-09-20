using NHibernate;
using NHibernate.Util;
using utnfrsf.ds.orms.Entidades;

namespace utnfrsf.ds.orms.Services
{
    public class NHService : IService
    {

        private ISession session;

        public NHService(ISession nhSession)
        {
            session = nhSession;
        }


        public IList<Rol> GetAllRoles()
        {
            return session.CreateQuery("from Rol as r").List<Rol>();
        }

        public Rol GetRol(int rolId)
        {
            return session.Get<Rol>(rolId);
        }


        public Usuario CreateUsuario(Usuario user)
        {
            user.Rol = session.Get<Rol>(user.Rol.Id);
            //user.Domicilios.ForEach(f => { f.Usuario = user; });
            session.Save(user);
            session.Flush();

            return user;
        }

        public Usuario GetUsuario(int id)
        {
            var usuario = session.CreateQuery("from Usuario u inner join fetch u.Domicilios inner join fetch u.Rol where u.Id = :id")
            .SetParameter("id", id)
            .List<Usuario>()
            .FirstOrDefault();
            return usuario;
        }

        public Usuario UpdateUsuario(Usuario user)
        {
            var usuarioDB = session.CreateQuery("from Usuario u inner join fetch u.Domicilios inner join fetch u.Rol where u.Id = :id")
            .SetParameter("id", user.Id)
            .List<Usuario>()
            .FirstOrDefault();
            var rolDB = session.Get<Rol>(user.Rol.Id);
            usuarioDB.Nombre = user.Nombre;
            usuarioDB.Rol = rolDB;
            session.Flush();
            session.Save(usuarioDB);

            return usuarioDB;
        }

        public void DeleteUsuario(int usuarioId)
        {
            var user = session.Get<Usuario>(usuarioId);
            session.Delete(user);
            session.Flush();
        }

        public void ConsultasVariasUsuarioHQL()
        {
            var consulta1 = session.CreateQuery("from Usuario as us where us.Id = ?")
            .SetInt32(0, 15)
            .List<Usuario>();
            Console.WriteLine(consulta1.FirstOrDefault());

            //var consulta2 = session.CreateQuery("from Usuario as us where (SELECT count(dom) from Domicilio as dom WHERE dom.UsuarioId = us.Id) > :cantidad")

            var consulta2 = session.CreateQuery("from Usuario as us where size(us.Domicilios) > :cantidad")
            .SetInt32("cantidad", 2)
            .List<Usuario>();
            consulta2.ToList().ForEach(u => Console.WriteLine(u));

            var consulta3 = session.CreateQuery("select distinct us from Usuario as us inner join us.Domicilios as dom where dom.Numero > 5000 order by us.Id")
            .List<Usuario>();

            consulta3.ToList().ForEach(u => Console.WriteLine(u));


        }

        public void ConsultasVariasUsuario()
        {

            var consulta1 = session.Query<Usuario>()
                            .Where(u => u.Id == 15)
                            .FirstOrDefault();
            Console.WriteLine(consulta1);

            var consulta2 = session.Query<Usuario>()
                            .Where(u => u.Domicilios.Count > 2);

            consulta2.ToList().ForEach(u => Console.WriteLine(u.ToString()));

            var consulta3 = session.Query<Usuario>()
                             .Where(u => u.Domicilios.Any(d => d.Numero > 5000))
                             .OrderBy(u => u.Id)
                             .Distinct();

            consulta3.ToList().ForEach(u => Console.WriteLine(u.ToString()));

        }

    }
}
