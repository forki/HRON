namespace HRONLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wfApprovals2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WFApprovals", "Parent_wfEmplID", c => c.Int());
            AddColumn("dbo.WFApprovals", "Parent_wfID", c => c.String(maxLength: 128));
            AddColumn("dbo.WFApprovals", "Parent_wfProgressive", c => c.Int());
            CreateIndex("dbo.WFApprovals", new[] { "Parent_wfEmplID", "Parent_wfID", "Parent_wfProgressive" });
            AddForeignKey("dbo.WFApprovals", new[] { "Parent_wfEmplID", "Parent_wfID", "Parent_wfProgressive" }, "dbo.EmplWorkflows", new[] { "wfEmplID", "wfID", "wfProgressive" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WFApprovals", new[] { "Parent_wfEmplID", "Parent_wfID", "Parent_wfProgressive" }, "dbo.EmplWorkflows");
            DropIndex("dbo.WFApprovals", new[] { "Parent_wfEmplID", "Parent_wfID", "Parent_wfProgressive" });
            DropColumn("dbo.WFApprovals", "Parent_wfProgressive");
            DropColumn("dbo.WFApprovals", "Parent_wfID");
            DropColumn("dbo.WFApprovals", "Parent_wfEmplID");
        }
    }
}
