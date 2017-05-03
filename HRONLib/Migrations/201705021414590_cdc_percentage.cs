namespace HRONLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cdc_percentage : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employee", "emplCdcID", "dbo.baCDC");
            DropIndex("dbo.Employee", new[] { "emplCdcID" });
            CreateTable(
                "dbo.EmplCDC",
                c => new
                    {
                        cdcEmplID = c.Int(nullable: false),
                        cdcProgressive = c.Int(nullable: false, identity: true),
                        cdcStartingDate = c.DateTime(nullable: false),
                        cdcEndDate = c.DateTime(),
                        dateCreated = c.DateTime(nullable: false),
                        userCreated = c.String(),
                    })
                .PrimaryKey(t => new { t.cdcEmplID, t.cdcProgressive })
                .ForeignKey("dbo.Employee", t => t.cdcEmplID, cascadeDelete: true)
                .Index(t => t.cdcEmplID);
            
            CreateTable(
                "dbo.EmplCDCDetail",
                c => new
                    {
                        cdcDetailRow = c.Int(nullable: false, identity: true),
                        cdcPercentage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        dateCreated = c.DateTime(nullable: false),
                        userCreated = c.String(),
                        cdc_cdcID = c.Int(),
                        EmplCDC_cdcEmplID = c.Int(),
                        EmplCDC_cdcProgressive = c.Int(),
                    })
                .PrimaryKey(t => t.cdcDetailRow)
                .ForeignKey("dbo.baCDC", t => t.cdc_cdcID)
                .ForeignKey("dbo.EmplCDC", t => new { t.EmplCDC_cdcEmplID, t.EmplCDC_cdcProgressive })
                .Index(t => t.cdc_cdcID)
                .Index(t => new { t.EmplCDC_cdcEmplID, t.EmplCDC_cdcProgressive });
            
            DropColumn("dbo.Employee", "emplCdcID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employee", "emplCdcID", c => c.Int());
            DropForeignKey("dbo.EmplCDC", "cdcEmplID", "dbo.Employee");
            DropForeignKey("dbo.EmplCDCDetail", new[] { "EmplCDC_cdcEmplID", "EmplCDC_cdcProgressive" }, "dbo.EmplCDC");
            DropForeignKey("dbo.EmplCDCDetail", "cdc_cdcID", "dbo.baCDC");
            DropIndex("dbo.EmplCDCDetail", new[] { "EmplCDC_cdcEmplID", "EmplCDC_cdcProgressive" });
            DropIndex("dbo.EmplCDCDetail", new[] { "cdc_cdcID" });
            DropIndex("dbo.EmplCDC", new[] { "cdcEmplID" });
            DropTable("dbo.EmplCDCDetail");
            DropTable("dbo.EmplCDC");
            CreateIndex("dbo.Employee", "emplCdcID");
            AddForeignKey("dbo.Employee", "emplCdcID", "dbo.baCDC", "cdcID");
        }
    }
}
