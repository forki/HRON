namespace HRONLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mailPrivate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "emplEmailPrivate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employee", "emplEmailPrivate");
        }
    }
}
