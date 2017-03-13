namespace MultiTenancyExperiment.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class AddAdendums : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adendums",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AdendumContent = c.String(),
                        NoteId = c.Guid(nullable: false),
                        Tenant = c.String(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "TenantAnnotation", "Tenant" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Notes", t => t.NoteId, cascadeDelete: true)
                .Index(t => t.NoteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Adendums", "NoteId", "dbo.Notes");
            DropIndex("dbo.Adendums", new[] { "NoteId" });
            DropTable("dbo.Adendums",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "TenantAnnotation", "Tenant" },
                });
        }
    }
}
