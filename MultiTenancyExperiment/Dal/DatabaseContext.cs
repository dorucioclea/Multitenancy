using System.Data.Entity;
using MultiTenancyExperiment.Dal.Base;
using MultiTenancyExperiment.Dal.Entities;
using MultiTenancyExperiment.Dal.Extensions;
using MultiTenancyExperiment.Dal.Interfaces;
using MultiTenancyExperiment.Dal.Multitenancy;
using MultiTenancyExperiment.Dal.Multitenancy.Infrastructure;
using MultiTenancyExperiment.Dal.Multitenancy.Interfaces;
using MultiTenancyExperiment.IOC.Interfaces;

namespace MultiTenancyExperiment.Dal
{
    public class DatabaseContext : DbContextBase, IDatabaseContext
    {
        public DatabaseContext(IConfiguration configuration, TenancyConfiguration tenancyConfiguration, IGuidGenerator generator) 
            : base(configuration, tenancyConfiguration, generator, new ContextConfigurationModule())
        {
            this.DisableDatabaseInitialization();
        }

        // only used in development
        internal DatabaseContext(string connectionString)
            : base(connectionString, new ContextConfigurationModule())
        {
            this.DisableDatabaseInitialization();
        }

        public IDbSet<Note> Notes { get; set; }
    }
}
