namespace HRONLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class emplNotes_identity : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.EmplNotes");
            AlterColumn("dbo.EmplNotes", "noteID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.EmplNotes", new[] { "noteEmplID", "noteID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.EmplNotes");
            AlterColumn("dbo.EmplNotes", "noteID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.EmplNotes", new[] { "noteEmplID", "noteID" });
        }
    }
}
