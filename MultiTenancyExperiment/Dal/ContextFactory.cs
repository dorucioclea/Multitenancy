using System.Data.Entity.Infrastructure;

namespace MultiTenancyExperiment.Dal
{
#if DEBUG
    public class ContextFactory : IDbContextFactory<DatabaseContext>
    {
        public DatabaseContext Create()
        {
            return new DatabaseContext(Settings.DbConnection);
        }
    }
#endif
}