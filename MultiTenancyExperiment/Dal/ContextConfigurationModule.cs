using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using MultiTenancyExperiment.Dal.Base;
using MultiTenancyExperiment.Dal.Configurations;
using MultiTenancyExperiment.Dal.Multitenancy;

namespace MultiTenancyExperiment.Dal
{
    public class ContextConfigurationModule : IConfigurationModule
    {
        public void Register(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new NoteConfiguration());

            // register multitenancy
            var conv =
                new AttributeToTableAnnotationConvention<TenantAttribute, string>(
                    MultitenancyConstants.TenantAnnotation, (type, attributes) => attributes.Single().ColumnName);

            modelBuilder.Conventions.Add(conv);
        }
    }
}