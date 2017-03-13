using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace MultiTenancyExperiment.Dal
{
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Tenant = c.String(nullable: false, maxLength: 24),
                        Message = c.String(nullable: false, maxLength: 2000),
                        Timestamp = c.DateTime(nullable: false),
                        Author = c.String(nullable: false, maxLength: 2000),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "TenantAnnotation", "Tenant" },
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Notes",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "TenantAnnotation", "Tenant" },
                });
        }
    }
}
