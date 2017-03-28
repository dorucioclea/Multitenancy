using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MultiTenancyExperiment.Dal.Base;

namespace MultiTenancyExperiment.Dal.Configurations
{
    public class BaseConfiguration<T> : EntityTypeConfiguration<T> where T : BaseEntity
    {
        protected BaseConfiguration()
        {
            HasKey(x => x.Id);
            Property(x => x.Tenant).HasColumnName("Tenant").HasColumnType("nvarchar").HasMaxLength(24).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}