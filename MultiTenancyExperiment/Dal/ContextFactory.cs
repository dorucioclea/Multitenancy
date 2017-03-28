using System.Data.Entity.Infrastructure;

namespace MultiTenancyExperiment.Dal
{
    public class ContextFactory : IDbContextFactory<DatabaseContext>
    {
#if DEBUG

        public DatabaseContext Create()
        {
            return new DatabaseContext(Settings.DbConnection);
        }

#endif

    }
}