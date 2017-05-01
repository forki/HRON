namespace HRONLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class baWorkflow : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.baWorkflows",
                c => new
                    {
                        wfID = c.Int(nullable: false, identity: true),
                        wfName = c.String(),
                        WorkFlowXAML = c.Binary(),
                        OnSave = c.Boolean(nullable: false),
                        dateCreated = c.DateTime(nullable: false),
                        userCreated = c.String(),
                    })
                .PrimaryKey(t => t.wfID);
            
            AddColumn("dbo.EmplWorkflows", "wfType_wfID", c => c.Int());
            CreateIndex("dbo.EmplWorkflows", "wfType_wfID");
            AddForeignKey("dbo.EmplWorkflows", "wfType_wfID", "dbo.baWorkflows", "wfID");
            DropColumn("dbo.EmplWorkflows", "wfType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EmplWorkflows", "wfType", c => c.Int(nullable: false));
            DropForeignKey("dbo.EmplWorkflows", "wfType_wfID", "dbo.baWorkflows");
            DropIndex("dbo.EmplWorkflows", new[] { "wfType_wfID" });
            DropColumn("dbo.EmplWorkflows", "wfType_wfID");
            DropTable("dbo.baWorkflows");
        }
    }
}
