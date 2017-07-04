namespace HRONLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class emplNotes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmplNotes",
                c => new
                    {
                        noteEmplID = c.Int(nullable: false),
                        noteID = c.Int(nullable: false, identity: true),
                        noteNOTE = c.String(),
                        dateCreated = c.DateTime(nullable: false),
                        userCreated = c.String(),
                        dateModified = c.DateTime(nullable: false),
                        userModified = c.String(),
                    })
                .PrimaryKey(t => new { t.noteEmplID, t.noteID })
                .ForeignKey("dbo.Employee", t => t.noteEmplID)
                .Index(t => t.noteEmplID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmplNotes", "noteEmplID", "dbo.Employee");
            DropIndex("dbo.EmplNotes", new[] { "noteEmplID" });
            DropTable("dbo.EmplNotes");
        }
    }
}
