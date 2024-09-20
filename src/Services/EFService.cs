using Microsoft.EntityFrameworkCore;
using utnfrsf.ds.orms.EFCore;
using utnfrsf.ds.orms.Entidades;

namespace utnfrsf.ds.orms.Services
{
    public class EFService : IService
    {

        private EFDbContext _dbContext;

        public EFService(EFDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<Rol> GetAllRoles()
        {

            return (from r in _dbContext.Roles
                    select r).ToList();

        }

        public Rol GetRol(int rolId)
        {
            return (from r in _dbContext.Roles
                    where r.Id == rolId
                    select r).FirstOrDefault();
        }

        public Usuario CreateUsuario(Usuario user)
        {

            user.Rol = _dbContext.Roles.FirstOrDefault(r => r.Id == user.Rol.Id);
            _dbContext.Usuarios.Add(user);
            _dbContext.SaveChanges();
            return user;

        }

        public Usuario GetUsuario(int id)
        {
            return (from u in _dbContext.Usuarios.Include(u => u.Domicilios).Include(u => u.Rol)
                    where u.Id == id
                    select u).FirstOrDefault();

        }

        public Usuario UpdateUsuario(Usuario user)
        {

            var usuarioDB = (from u in _dbContext.Usuarios
                             where u.Id == user.Id
                             select u).FirstOrDefault();

            var newRole = (from r in _dbContext.Roles
                           where r.Id == user.Rol.Id
                           select r).FirstOrDefault();

            usuarioDB.Nombre = user.Nombre;
            usuarioDB.Rol = newRole;

            _dbContext.SaveChanges();

            return usuarioDB;

        }

        public void DeleteUsuario(int usuarioId)
        {

            var usuario = _dbContext.Usuarios.Include(u => u.Domicilios).FirstOrDefault(u => u.Id == usuarioId);

            _dbContext.Usuarios.Remove(usuario);
            _dbContext.SaveChanges();

        }

        public void ConsultasVariasUsuario()
        {

            var consulta1 = (from u in _dbContext.Usuarios
                             where u.Id == 15
                             select u).FirstOrDefault();

            var consulta2 = from u in _dbContext.Usuarios.Include(us => us.Domicilios).Include(us => us.Rol)
                            where u.Domicilios.Count > 2
                            select u;

            consulta2.ToList().ForEach(u => Console.WriteLine(u.ToString()));

            var consulta3 = (from u in _dbContext.Usuarios.Include(us => us.Rol)
                             from d in u.Domicilios
                             where d.Numero > 5000
                             select u).OrderBy(u => u.Id).Distinct();

            consulta3.ToList().ForEach(u => Console.WriteLine(u.ToString()));

        }

    }
}
