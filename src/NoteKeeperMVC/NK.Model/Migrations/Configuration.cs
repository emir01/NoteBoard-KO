namespace NK.Model.Migrations
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Auto generated by EF Migrations functionality. No changes here.
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<NK.Model.Context.NoteContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(NK.Model.Context.NoteContext context)
        {
            //  This method will be called after migrating to the latest version.
                
            // ===== EXAMPLE CODE : 
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
