using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MultiTenancyExperiment.Dal.Entities;

namespace MultiTenancyExperiment.Dal.Configurations
{
    public class AdendumConfiguration : EntityTypeConfiguration<Adendum>
    {
        private const string TableName = "Adendums";

        public AdendumConfiguration() : this("dbo")
        {
            
        }

        private AdendumConfiguration(string schema)
        {
            ToTable(TableName, schema);

            HasKey(x => x.Id);
            Property(x => x.Tenant).HasColumnName("Tenant").HasColumnType("nvarchar").HasMaxLength(24).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.AdendumContent).HasColumnName("AdendumContent").HasColumnType("nvarchar").HasMaxLength(2000).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}
