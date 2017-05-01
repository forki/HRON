namespace HRONLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wfApproval : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WFApprovals",
                c => new
                    {
                        approverWFID = c.Guid(nullable: false),
                        approverActivityID = c.String(nullable: false, maxLength: 150),
                        mail = c.String(maxLength: 150),
                        subject = c.String(),
                        body = c.String(unicode: false, storeType: "text"),
                        starttime = c.DateTime(nullable: false),
                        endtime = c.DateTime(),
                        note = c.String(unicode: false, storeType: "text"),
                        approved = c.Boolean(),
                    })
                .PrimaryKey(t => new { t.approverWFID, t.approverActivityID });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WFApprovals");
        }
    }
}
