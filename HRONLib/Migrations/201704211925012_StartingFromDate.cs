namespace HRONLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StartingFromDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EmplSalary", "salStartingFrom", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmplSalary", "salStartingFrom", c => c.DateTime(nullable: false));
        }
    }
}
