namespace TrueCarChallenge.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Migrations;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            context.Vehicles.AddOrUpdate(v => v.Make,
                new Vehicle() { Make = new Make { Name = "BMW" } },
                new Vehicle() { Make = new Make { Name = "Tesla" } },
                new Vehicle() { Make = new Make { Name = "Ford" } },
                new Vehicle() { Make = new Make { Name = "Audi" } }
                );

            context.Roles.AddOrUpdate(
              r => r.Name,
              new IdentityRole { Name = "Admin" },
              new IdentityRole { Name = "User" }
            );
        }
    }
}