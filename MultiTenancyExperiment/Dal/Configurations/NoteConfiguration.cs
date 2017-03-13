using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MultiTenancyExperiment.Dal.Entities;

namespace MultiTenancyExperiment.Dal.Configurations
{
    public class NoteConfiguration : EntityTypeConfiguration<Note>
    {
        private const string TableName = "Notes";

        public NoteConfiguration() : this("dbo")
        {
            
        }

        private NoteConfiguration(string schema)
        {
            ToTable(TableName, schema);

            HasKey(x => x.Id);

            Property(x => x.Timestamp).HasColumnName("Timestamp").HasColumnType("datetime").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Tenant).HasColumnName("Tenant").HasColumnType("nvarchar").HasMaxLength(24).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Message).HasColumnName("Message").HasColumnType("nvarchar").HasMaxLength(2000).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Author).HasColumnName("Author").HasColumnType("nvarchar").HasMaxLength(2000).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            HasMany(x => x.Adendums).WithRequired(x => x.Note).HasForeignKey(x => x.NoteId);
        }
    }
}
