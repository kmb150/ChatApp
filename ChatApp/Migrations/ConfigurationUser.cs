namespace ChatApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class ConfigurationUser : DbMigrationsConfiguration<ChatApp.Models.ApplicationDbContext>
    {
        public ConfigurationUser()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ChatApp.Models.ApplicationDbContext";
        }

        protected override void Seed(ChatApp.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
