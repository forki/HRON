namespace HRONLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmplSalary_isActual : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.EmplSalary", "isActualLine", c => c.Boolean(nullable: false));
            Sql("ALTER TABLE dbo.EmplSalary ADD isActualLine AS CONVERT(bit,isnull(case when salStartingFrom < getDate() then 0x1 else 0x0 end, 0x0))");
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmplSalary", "isActualLine");
        }
    }
}
