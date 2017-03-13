using System.Data.Entity;
using System.Reflection;

namespace MultiTenancyExperiment.Dal.Extensions
{
    public static class DBContextExtensions
    {
        public static void DisableDatabaseInitialization(this DbContext dbContext)
        {
            var databaseType = typeof(Database);
            var setInitializer = databaseType.GetMethod("SetInitializer", BindingFlags.Static | BindingFlags.Public);
            var type = dbContext.GetType();
            var setInitializerT = setInitializer.MakeGenericMethod(type);

            setInitializerT.Invoke(null, new object[] { null });
        }
    }
}
