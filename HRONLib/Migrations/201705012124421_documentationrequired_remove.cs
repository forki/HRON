namespace HRONLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class documentationrequired_remove : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EmplDocumentationRequired", "documentationID", "dbo.baDocumentation");
            DropForeignKey("dbo.EmplDocumentationRequired", "emplID", "dbo.Employee");
            DropIndex("dbo.EmplDocumentationRequired", new[] { "emplID" });
            DropIndex("dbo.EmplDocumentationRequired", new[] { "documentationID" });
            AddColumn("dbo.EmplDocumentation", "required", c => c.Boolean(nullable: false, defaultValue: false));
            DropTable("dbo.EmplDocumentationRequired");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EmplDocumentationRequired",
                c => new
                    {
                        emplID = c.Int(nullable: false),
                        documentationID = c.Int(nullable: false),
                        required = c.Boolean(nullable: false),
                        dateCreated = c.DateTime(nullable: false),
                        userCreated = c.String(),
                    })
                .PrimaryKey(t => new { t.emplID, t.documentationID });
            
            DropColumn("dbo.EmplDocumentation", "required");
            CreateIndex("dbo.EmplDocumentationRequired", "documentationID");
            CreateIndex("dbo.EmplDocumentationRequired", "emplID");
            AddForeignKey("dbo.EmplDocumentationRequired", "emplID", "dbo.Employee", "emplID", cascadeDelete: true);
            AddForeignKey("dbo.EmplDocumentationRequired", "documentationID", "dbo.baDocumentation", "documentationID", cascadeDelete: true);
        }
    }
}
