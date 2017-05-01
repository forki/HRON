namespace HRONLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class emplWorkflow2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EmplWorkflows", "Employee_emplID", "dbo.Employee");
            DropIndex("dbo.EmplWorkflows", new[] { "Employee_emplID" });
            DropColumn("dbo.EmplWorkflows", "Employee_emplID");
            CreateIndex("dbo.EmplWorkflows", "wfEmplID");
            AddForeignKey("dbo.EmplWorkflows", "wfEmplID", "dbo.Employee", "emplID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmplWorkflows", "wfEmplID", "dbo.Employee");
            DropIndex("dbo.EmplWorkflows", new[] { "wfEmplID" });
            AddColumn("dbo.EmplWorkflows", "Employee_emplID", c => c.Int(nullable: false));
            CreateIndex("dbo.EmplWorkflows", "Employee_emplID");
            AddForeignKey("dbo.EmplWorkflows", "Employee_emplID", "dbo.Employee", "emplID");
        }
    }
}
