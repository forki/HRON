namespace HRONLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class documentationExpireInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.baDocumentation", "documentationExpireTime", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.baDocumentation", "documentationExpireTime", c => c.Time(nullable: false, precision: 7));
        }
    }
}
