using System.Data.Entity;
using MultiTenancyExperiment.Dal.Base;
using MultiTenancyExperiment.Dal.Entities;
using MultiTenancyExperiment.Dal.Extensions;
using MultiTenancyExperiment.Dal.Interfaces;
using MultiTenancyExperiment.Dal.Multitenancy;
using MultiTenancyExperiment.IOC.Interfaces;

namespace MultiTenancyExperiment.Dal
{
    public class DatabaseContext : DbContextBase, IDatabaseContext
    {
        public DatabaseContext(IConfiguration configuration, TenancyConfiguration tenancyConfiguration) 
            : base(configuration, new ContextConfigurationModule())
        {
            DbConfiguration.SetConfiguration(tenancyConfiguration);
            this.DisableDatabaseInitialization();
        }

        public DatabaseContext(string connectionString)
            : base(connectionString, new ContextConfigurationModule())
        {
            this.DisableDatabaseInitialization();
        }

        public IDbSet<Note> Notes { get; set; }
    }
}
