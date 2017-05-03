namespace HRONLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class workplace_company : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.baWorkPlace", "baCompany_compID", c => c.Int());
            CreateIndex("dbo.baWorkPlace", "baCompany_compID");
            AddForeignKey("dbo.baWorkPlace", "baCompany_compID", "dbo.baCompanyRights", "compID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.baWorkPlace", "baCompany_compID", "dbo.baCompanyRights");
            DropIndex("dbo.baWorkPlace", new[] { "baCompany_compID" });
            DropColumn("dbo.baWorkPlace", "baCompany_compID");
        }
    }
}
