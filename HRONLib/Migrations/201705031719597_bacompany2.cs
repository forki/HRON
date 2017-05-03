namespace HRONLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bacompany2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.baWorkPlace", "baCompanyRights_compID", "dbo.baCompanyRights");
            DropIndex("dbo.baWorkPlace", new[] { "baCompanyRights_compID" });
            DropColumn("dbo.baWorkPlace", "baCompanyRights_compID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.baWorkPlace", "baCompanyRights_compID", c => c.Int());
            CreateIndex("dbo.baWorkPlace", "baCompanyRights_compID");
            AddForeignKey("dbo.baWorkPlace", "baCompanyRights_compID", "dbo.baCompanyRights", "compID");
        }
    }
}
