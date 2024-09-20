using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using utnfrsf.ds.orms.EFCore;
using utnfrsf.ds.orms.Entidades;
using utnfrsf.ds.orms.NHibernateHelper;
using utnfrsf.ds.orms.Services;

namespace utnfrsf.ds.orms
{
    public enum ORMs
    {
        EntityFramework,
        NHibernate
    }

    internal class Program
    {
        static void ConfigurePSQL(HostApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("ejemploormspsql");

            builder.Services.AddScoped<IInterceptor, SqlLoggingInterceptor>();

            // Configurar NHibernate
            builder.Services.AddSingleton<ISessionFactory>(factory =>
            {
                var configuration = new Configuration();
                configuration.DataBaseIntegration(c =>
                {
                    c.Dialect<PostgreSQL83Dialect>();
                    c.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                    c.SchemaAction = SchemaAutoAction.Validate;
                    c.LogFormattedSql = true;
                });
                return Fluently.Configure(configuration)
                    .Database(PostgreSQLConfiguration.Standard
                        .ConnectionString(connectionString))
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Rol>())
                    .BuildSessionFactory();
            });


            // Registrar ISession como servicio inyectable con un ciclo de vida por solicitud
            builder.Services.AddScoped(factory =>
            {
                var sessionFactory = factory.GetService<ISessionFactory>();
                return sessionFactory.OpenSession()
                    .SessionWithOptions()
                    .Interceptor(factory.GetService<IInterceptor>())
                    .OpenSession();
            });

            builder.Services.AddDbContext<EFDbContext>(options =>
                options.UseNpgsql(connectionString)
            );

            builder.Services.AddTransient<IService, EFService>();
            builder.Services.AddTransient<IService, NHService>();
        }

        static void ConfigureSQLServer(HostApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("ejemploormssqls");

            builder.Services.AddScoped<IInterceptor, SqlLoggingInterceptor>();

            // Configurar NHibernate
            builder.Services.AddSingleton<ISessionFactory>(factory =>
            {
                var configuration = new Configuration();
                configuration.DataBaseIntegration(c =>
                {
                    c.Dialect<MsSql2012Dialect>();
                    c.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                    c.SchemaAction = SchemaAutoAction.Validate;
                    c.LogFormattedSql = true;
                });
                return Fluently.Configure(configuration)
                    .Database(MsSqlConfiguration.MsSql2012
                        .ConnectionString(connectionString))
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Rol>())
                    .ExposeConfiguration(cfg =>
                    {
                        cfg.SetProperty(NHibernate.Cfg.Environment.ShowSql, "true");
                        cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                        cfg.SetProperty(NHibernate.Cfg.Environment.GenerateStatistics, "true");
                    })
                    .BuildSessionFactory();
            });


            // Registrar ISession como servicio inyectable con un ciclo de vida por solicitud
            builder.Services.AddScoped(factory =>
            {
                var sessionFactory = factory.GetService<ISessionFactory>();
                return sessionFactory.WithOptions()
                    .Interceptor(factory.GetService<IInterceptor>())
                    .OpenSession();
            });

            builder.Services.AddDbContext<EFDbContext>(options =>
                options.UseSqlServer(connectionString)
            );

            builder.Services.AddTransient<IService, EFService>();
            builder.Services.AddTransient<IService, NHService>();
        }

        static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            //ConfigurePSQL(builder);
            ConfigureSQLServer(builder);

            var app = builder.Build();

            // Resolviendo el servicio EFService
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider.GetRequiredService<IEnumerable<IService>>();
                IService service = services.ElementAt((int)ORMs.EntityFramework);

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
