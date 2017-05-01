namespace HRONLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class baWorkflow2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.baWorkflows", "OnOnBoarding", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.baWorkflows", "OnOnBoarding");
        }
    }
}
