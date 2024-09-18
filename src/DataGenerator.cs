using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using utnfrsf.ds.orms.Entidades;
using Bogus;

namespace utnfrsf.ds.orms
{
    public static class DataGenerator
    {
        public static (IList<Rol>, IList<Usuario>) GenerateFakeData(int amount)
        {
            int roleNameIndex = 0;
            var roleNames = new[] { "Admin", "User", "Developer", "DBAdmin", "Owner" };

            var rolGen = new Faker<Rol>()
            .StrictMode(false)
            .RuleFor(r => r.Nombre, f => roleNames[roleNameIndex++]);

            var roles = rolGen.GenerateBetween(5, 5);

            var randNumber = new Random(DateTime.Now.Millisecond);

            var dirGen = new Faker<Domicilio>()
            .RuleFor(d => d.Calle, f => f.Address.StreetName())
            .RuleFor(d => d.Numero, f => f.Random.Number(0, 9999));

            var userGen = new Faker<Usuario>()
            .RuleFor(d => d.Nombre, f => f.Internet.UserName())
            .RuleFor(d => d.Rol, f => f.PickRandom(roles))
            .RuleFor(d => d.Domicilios, f => dirGen.GenerateBetween(1, 4));

            var usuarios = userGen.Generate(amount);

            return (roles, usuarios);
        }

        public static IList<Usuario> GenerateFakeUsuario(int amount, IList<Rol> roles)
        {

            var rand = new Random();

            var userGen = new Faker<Usuario>()
            .RuleFor(d => d.Nombre, f => f.Internet.UserName())
            .RuleFor(d => d.Rol, f => f.PickRandom(roles))
            .RuleFor(d => d.Domicilios, f => GenerateFakeDomicilio(rand.Next(1, 4)));

            return userGen.Generate(amount);

        }

        public static IList<Domicilio> GenerateFakeDomicilio(int amount)
        {

            var randNumber = new Random();
            var dirGen = new Faker<Domicilio>()
            .RuleFor(d => d.Calle, f => f.Address.StreetName())
            .RuleFor(d => d.Numero, randNumber.Next(0, 9999));

            return dirGen.Generate(amount);
        }

    }
}
