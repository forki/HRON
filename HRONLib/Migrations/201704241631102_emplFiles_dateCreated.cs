namespace HRONLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class emplFiles_dateCreated : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EmplFiles", "dateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.EmplFiles", "userCreated", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmplFiles", "userCreated", c => c.String(maxLength: 50));
            AlterColumn("dbo.EmplFiles", "dateCreated", c => c.DateTime());
        }
    }
}
