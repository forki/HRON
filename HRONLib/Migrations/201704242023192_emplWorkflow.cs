namespace HRONLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class emplWorkflow : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmplWorkflows",
                c => new
                    {
                        wfEmplID = c.Int(nullable: false),
                        wfID = c.String(nullable: false, maxLength: 128),
                        wfProgressive = c.Int(nullable: false, identity: true),
                        wfType = c.Int(nullable: false),
                        wfStartingDate = c.DateTime(nullable: false),
                        dateCreated = c.DateTime(nullable: false),
                        userCreated = c.String(),
                        Employee_emplID = c.Int(),
                    })
                .PrimaryKey(t => new { t.wfEmplID, t.wfID, t.wfProgressive })
                .ForeignKey("dbo.Employee", t => t.Employee_emplID)
                .Index(t => t.Employee_emplID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmplWorkflows", "Employee_emplID", "dbo.Employee");
            DropIndex("dbo.EmplWorkflows", new[] { "Employee_emplID" });
            DropTable("dbo.EmplWorkflows");
        }
    }
}
