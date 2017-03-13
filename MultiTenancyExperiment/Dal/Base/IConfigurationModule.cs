using System.Data.Entity;

namespace MultiTenancyExperiment.Dal.Base
{
    public interface IConfigurationModule
    {
        void Register(DbModelBuilder modelBuilder);
    }
}