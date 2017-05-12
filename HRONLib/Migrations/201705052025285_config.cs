namespace HRONLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class config : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Config",
                c => new
                    {
                        confID = c.Int(nullable: false, identity: true),
                        target = c.String(),
                        key = c.String(),
                        value = c.String(),
                        valueDecimal = c.Decimal(precision: 18, scale: 2),
                        valueBool = c.Boolean(),
                        dateCreated = c.DateTime(nullable: false),
                        userCreated = c.String(),
                    })
                .PrimaryKey(t => t.confID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Config");
        }
    }
}
