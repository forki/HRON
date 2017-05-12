namespace HRONLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "emplPhoto", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employee", "emplPhoto");
        }
    }
}
