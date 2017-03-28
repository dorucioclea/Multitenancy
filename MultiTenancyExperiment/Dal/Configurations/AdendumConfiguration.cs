using System.ComponentModel.DataAnnotations.Schema;
using MultiTenancyExperiment.Dal.Entities;

namespace MultiTenancyExperiment.Dal.Configurations
{
    public class AdendumConfiguration : BaseConfiguration<Adendum>
    {
        private const string TableName = "Adendums";

        public AdendumConfiguration() : this("dbo")
        {
            
        }

        private AdendumConfiguration(string schema)  : base(schema)
        {
            ToTable(TableName, schema);
            Property(x => x.AdendumContent).HasColumnName("AdendumContent").HasColumnType("nvarchar").HasMaxLength(2000).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}
