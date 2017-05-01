namespace HRONLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class empfDocumentation_dateRegistration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmplDocumentation", "dateSignature", c => c.DateTime());
            DropColumn("dbo.EmplDocumentation", "dateRegistration");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EmplDocumentation", "dateRegistration", c => c.DateTime(nullable: false));
            DropColumn("dbo.EmplDocumentation", "dateSignature");
        }
    }
}
