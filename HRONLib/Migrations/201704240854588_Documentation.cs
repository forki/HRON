namespace HRONLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Documentation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.baDocumentation",
                c => new
                {
                    documentationID = c.Int(nullable: false, identity: true),
                    documentationDescription = c.String(),
                    documentationCreateOnOnboarding = c.Boolean(nullable: false),
                    documentationDocument = c.Binary(),
                    documentationDocumentName = c.String(),
                    documentationExpireTime = c.Time(nullable: false, precision: 7),
                })
                .PrimaryKey(t => t.documentationID);

            DropIndex("dbo.EmplFiles", new[] { "EmplID" });
            CreateTable(
                "dbo.EmplDocumentation",
                c => new
                    {
                        emplID = c.Int(nullable: false),
                        documentationID = c.Int(nullable: false),
                        dateRegistration = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.emplID, t.documentationID })
                .ForeignKey("dbo.baDocumentation", t => t.documentationID, cascadeDelete: false)
                .ForeignKey("dbo.Employee", t => t.emplID, cascadeDelete: true)
                .Index(t => t.emplID)
                .Index(t => t.documentationID);
                        
            CreateTable(
                "dbo.EmplDocumentationRequired",
                c => new
                    {
                        emplID = c.Int(nullable: false),
                        documentationID = c.Int(nullable: false),
                        required = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.emplID, t.documentationID })
                .ForeignKey("dbo.baDocumentation", t => t.documentationID, cascadeDelete: true)
                .ForeignKey("dbo.Employee", t => t.emplID, cascadeDelete: true)
                .Index(t => t.emplID)
                .Index(t => t.documentationID);
            
            AddColumn("dbo.EmplFiles", "emplDocumentationId", c => c.Int(nullable: true));
            CreateIndex("dbo.EmplFiles", new[] { "EmplID", "emplDocumentationId" });
            AddForeignKey("dbo.EmplFiles", new[] { "EmplID", "emplDocumentationId" }, "dbo.EmplDocumentation", new[] { "emplID", "documentationID" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmplDocumentationRequired", "emplID", "dbo.Employee");
            DropForeignKey("dbo.EmplDocumentationRequired", "documentationID", "dbo.baDocumentation");
            DropForeignKey("dbo.EmplDocumentation", "emplID", "dbo.Employee");
            DropForeignKey("dbo.EmplFiles", new[] { "EmplID", "emplDocumentationId" }, "dbo.EmplDocumentation");
            DropForeignKey("dbo.EmplDocumentation", "documentationID", "dbo.baDocumentation");
            DropIndex("dbo.EmplDocumentationRequired", new[] { "documentationID" });
            DropIndex("dbo.EmplDocumentationRequired", new[] { "emplID" });
            DropIndex("dbo.EmplFiles", new[] { "EmplID", "emplDocumentationId" });
            DropIndex("dbo.EmplDocumentation", new[] { "documentationID" });
            DropIndex("dbo.EmplDocumentation", new[] { "emplID" });
            DropColumn("dbo.EmplFiles", "emplDocumentationId");
            DropTable("dbo.EmplDocumentationRequired");
            DropTable("dbo.baDocumentation");
            DropTable("dbo.EmplDocumentation");
            CreateIndex("dbo.EmplFiles", "EmplID");
        }
    }
}
