using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NHibernate;
using NHibernate.Linq.Functions;
using utnfrsf.ds.orms.EFCore;
using utnfrsf.ds.orms.Entidades;
using utnfrsf.ds.orms.Services;

namespace utnfrsf.ds.orms
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("ejemploorms");

            // Configurar NHibernate
            builder.Services.AddSingleton<ISessionFactory>(factory =>
            {
                return Fluently.Configure()
                    .Database(PostgreSQLConfiguration.Standard
                        .ConnectionString(connectionString))
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Rol>())
                    .BuildSessionFactory();
            });

            // Registrar ISession como servicio inyectable con un ciclo de vida por solicitud
            builder.Services.AddScoped(factory =>
            {
                var sessionFactory = factory.GetService<ISessionFactory>();
                return sessionFactory.OpenSession();
            });

            builder.Services.AddDbContext<EFDbContext>(options =>
                options.UseNpgsql(connectionString)
            );

            builder.Services.AddTransient<IService, EFService>();
            builder.Services.AddTransient<IService, NHService>();

            var app = builder.Build();

            // Resolviendo el servicio EFService
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider.GetRequiredService<IEnumerable<IService>>();
                IService service = services.ElementAt(0);
                //IService service = scope.ServiceProvider.GetRequiredService<NHService>();

                // Ejecutar método en EFService
                var roles = service.GetAllRoles();
                foreach (var item in roles)
                {
                    Console.WriteLine(item);
                }
                var usuario = DataGenerator.GenerateFakeUsuario(1, roles).FirstOrDefault();           

                var getUser = service.GetUsuario(1);
    Console.WriteLine(getUser);


                Console.WriteLine(usuario);
                service.CreateUsuario(usuario);
                var userDB = service.GetUsuario(usuario.Id);
                Console.WriteLine(userDB);
                usuario.Nombre = "Agustin Catellani";
                usuario.Rol = service.GetRol(1);
                var updatedUser = service.UpdateUsuario(usuario);
                Console.WriteLine(updatedUser);
                service.DeleteUsuario(userDB.Id);
            }

            app.Run();

            
        }
    }
}
