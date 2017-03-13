using System.Data.Entity;
using MultiTenancyExperiment.Dal.Base;
using MultiTenancyExperiment.Dal.Configurations;

namespace MultiTenancyExperiment.Dal
{
    public class ContextConfigurationModule : IConfigurationModule
    {
        public void Register(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new NoteConfiguration());
        }
    }
}