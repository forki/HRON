namespace HRONLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bacompany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.baWorkPlace", "baCompany_compID", "dbo.baCompanyRights");
            CreateTable(
                "dbo.baCompanies",
                c => new
                    {
                        compID = c.Int(nullable: false, identity: true),
                        compDescription = c.String(),
                        compSocieta = c.Short(),
                        dateCreated = c.DateTime(nullable: false),
                        userCreated = c.String(),
                    })
                .PrimaryKey(t => t.compID);
            
            AddColumn("dbo.baWorkPlace", "baCompanyRights_compID", c => c.Int());
            AddColumn("dbo.baCompanyRights", "compCompany_compID", c => c.Int());
            CreateIndex("dbo.baWorkPlace", "baCompanyRights_compID");
            CreateIndex("dbo.baCompanyRights", "compCompany_compID");
            AddForeignKey("dbo.baCompanyRights", "compCompany_compID", "dbo.baCompanies", "compID");
            AddForeignKey("dbo.baWorkPlace", "baCompanyRights_compID", "dbo.baCompanyRights", "compID");
            DropColumn("dbo.baCompanyRights", "compSocieta");
        }
        
        public override void Down()
        {
            AddColumn("dbo.baCompanyRights", "compSocieta", c => c.Short());
            DropForeignKey("dbo.baWorkPlace", "baCompanyRights_compID", "dbo.baCompanyRights");
            DropForeignKey("dbo.baCompanyRights", "compCompany_compID", "dbo.baCompanies");
            DropIndex("dbo.baCompanyRights", new[] { "compCompany_compID" });
            DropIndex("dbo.baWorkPlace", new[] { "baCompanyRights_compID" });
            DropColumn("dbo.baCompanyRights", "compCompany_compID");
            DropColumn("dbo.baWorkPlace", "baCompanyRights_compID");
            DropTable("dbo.baCompanies");
            AddForeignKey("dbo.baWorkPlace", "baCompany_compID", "dbo.baCompanyRights", "compID");
        }
    }
}
