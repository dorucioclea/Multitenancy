using System.ComponentModel.DataAnnotations.Schema;
using MultiTenancyExperiment.Dal.Entities;

namespace MultiTenancyExperiment.Dal.Configurations
{
    public class NoteConfiguration : BaseConfiguration<Note>
    {
        private const string TableName = "Notes";

        public NoteConfiguration() : this("dbo")
        {
            
        }

        private NoteConfiguration(string schema)
        {
            ToTable(TableName, schema);
            
            Property(x => x.Timestamp).HasColumnName("Timestamp").HasColumnType("datetime").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Message).HasColumnName("Message").HasColumnType("nvarchar").HasMaxLength(2000).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Author).HasColumnName("Author").HasColumnType("nvarchar").HasMaxLength(2000).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            HasMany(x => x.Adendums).WithRequired(x => x.Note).HasForeignKey(x => x.NoteId);
        }
    }
}
