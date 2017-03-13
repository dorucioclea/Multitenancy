using System.Data.Entity.Migrations;

namespace MultiTenancyExperiment.Dal
{
    public class ContextConfiguration : DbMigrationsConfiguration<DatabaseContext>
    {
        public ContextConfiguration()
        {
            // disable automatic migrations
            AutomaticMigrationsEnabled = false;
        }
    }
}
