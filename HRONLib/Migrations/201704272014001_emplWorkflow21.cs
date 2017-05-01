namespace HRONLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class emplWorkflow21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmplWorkflows", "wfFinished", c => c.Boolean(nullable: false));
            AddColumn("dbo.EmplWorkflows", "wfStatus", c => c.String());
            AddColumn("dbo.EmplWorkflows", "wfStatusUpdated", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmplWorkflows", "wfStatusUpdated");
            DropColumn("dbo.EmplWorkflows", "wfStatus");
            DropColumn("dbo.EmplWorkflows", "wfFinished");
        }
    }
}
