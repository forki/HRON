namespace HRONLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeNewFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "emplEmailWork", c => c.String());
            AddColumn("dbo.Employee", "emplSamAccountName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employee", "emplSamAccountName");
            DropColumn("dbo.Employee", "emplEmailWork");
        }
    }
}
