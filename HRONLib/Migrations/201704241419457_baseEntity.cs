namespace HRONLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class baseEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.baBusinessUnitID", "dateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.baBusinessUnitID", "userCreated", c => c.String());
            AddColumn("dbo.Employee", "dateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Employee", "userCreated", c => c.String());
            AddColumn("dbo.baCDC", "dateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.baCDC", "userCreated", c => c.String());
            AddColumn("dbo.baContractType", "dateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.baContractType", "userCreated", c => c.String());
            AddColumn("dbo.baCountry", "dateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.baCountry", "userCreated", c => c.String());
            AddColumn("dbo.baWorkPlace", "dateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.baWorkPlace", "userCreated", c => c.String());
            AddColumn("dbo.baJobTitle", "dateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.baJobTitle", "userCreated", c => c.String());
            AddColumn("dbo.baLevel", "dateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.baLevel", "userCreated", c => c.String());
            AddColumn("dbo.baNationality", "dateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.baNationality", "userCreated", c => c.String());
            AddColumn("dbo.baSpecialization", "dateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.baSpecialization", "userCreated", c => c.String());
            AddColumn("dbo.baStudyTitle", "dateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.baStudyTitle", "userCreated", c => c.String());
            AddColumn("dbo.baTimeType", "dateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.baTimeType", "userCreated", c => c.String());
            AddColumn("dbo.baType", "dateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.baType", "userCreated", c => c.String());
            AddColumn("dbo.EmplCompanyRights", "dateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.EmplCompanyRights", "userCreated", c => c.String());
            AddColumn("dbo.baCompanyRights", "dateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.baCompanyRights", "userCreated", c => c.String());
            AddColumn("dbo.EmplDocumentation", "dateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.EmplDocumentation", "userCreated", c => c.String());
            AddColumn("dbo.baDocumentation", "dateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.baDocumentation", "userCreated", c => c.String());
            AddColumn("dbo.EmplDocumentationRequired", "dateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.EmplDocumentationRequired", "userCreated", c => c.String());
            AddColumn("dbo.EmplFamily", "dateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.EmplFamily", "userCreated", c => c.String());
            AddColumn("dbo.EmplFunctions", "dateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.EmplFunctions", "userCreated", c => c.String());
            AddColumn("dbo.baFunctions", "dateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.baFunctions", "userCreated", c => c.String());
            AddColumn("dbo.EmplSalary", "dateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.EmplSalary", "userCreated", c => c.String());
            AddColumn("dbo.baCarPolicy", "dateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.baCarPolicy", "userCreated", c => c.String());
            AddColumn("dbo.EmplSalaryFringeBenefit", "dateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.EmplSalaryFringeBenefit", "userCreated", c => c.String());
            AddColumn("dbo.baFringeBenefit", "dateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.baFringeBenefit", "userCreated", c => c.String());
            AddColumn("dbo.baUser", "dateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.baUser", "userCreated", c => c.String());
            AddColumn("dbo.baUserGroup", "dateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.baUserGroup", "userCreated", c => c.String());
            AlterColumn("dbo.EmplFiles", "dateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.EmplFiles", "userCreated", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmplFiles", "userCreated", c => c.String(maxLength: 50));
            AlterColumn("dbo.EmplFiles", "dateCreated", c => c.DateTime());
            DropColumn("dbo.baUserGroup", "userCreated");
            DropColumn("dbo.baUserGroup", "dateCreated");
            DropColumn("dbo.baUser", "userCreated");
            DropColumn("dbo.baUser", "dateCreated");
            DropColumn("dbo.baFringeBenefit", "userCreated");
            DropColumn("dbo.baFringeBenefit", "dateCreated");
            DropColumn("dbo.EmplSalaryFringeBenefit", "userCreated");
            DropColumn("dbo.EmplSalaryFringeBenefit", "dateCreated");
            DropColumn("dbo.baCarPolicy", "userCreated");
            DropColumn("dbo.baCarPolicy", "dateCreated");
            DropColumn("dbo.EmplSalary", "userCreated");
            DropColumn("dbo.EmplSalary", "dateCreated");
            DropColumn("dbo.baFunctions", "userCreated");
            DropColumn("dbo.baFunctions", "dateCreated");
            DropColumn("dbo.EmplFunctions", "userCreated");
            DropColumn("dbo.EmplFunctions", "dateCreated");
            DropColumn("dbo.EmplFamily", "userCreated");
            DropColumn("dbo.EmplFamily", "dateCreated");
            DropColumn("dbo.EmplDocumentationRequired", "userCreated");
            DropColumn("dbo.EmplDocumentationRequired", "dateCreated");
            DropColumn("dbo.baDocumentation", "userCreated");
            DropColumn("dbo.baDocumentation", "dateCreated");
            DropColumn("dbo.EmplDocumentation", "userCreated");
            DropColumn("dbo.EmplDocumentation", "dateCreated");
            DropColumn("dbo.baCompanyRights", "userCreated");
            DropColumn("dbo.baCompanyRights", "dateCreated");
            DropColumn("dbo.EmplCompanyRights", "userCreated");
            DropColumn("dbo.EmplCompanyRights", "dateCreated");
            DropColumn("dbo.baType", "userCreated");
            DropColumn("dbo.baType", "dateCreated");
            DropColumn("dbo.baTimeType", "userCreated");
            DropColumn("dbo.baTimeType", "dateCreated");
            DropColumn("dbo.baStudyTitle", "userCreated");
            DropColumn("dbo.baStudyTitle", "dateCreated");
            DropColumn("dbo.baSpecialization", "userCreated");
            DropColumn("dbo.baSpecialization", "dateCreated");
            DropColumn("dbo.baNationality", "userCreated");
            DropColumn("dbo.baNationality", "dateCreated");
            DropColumn("dbo.baLevel", "userCreated");
            DropColumn("dbo.baLevel", "dateCreated");
            DropColumn("dbo.baJobTitle", "userCreated");
            DropColumn("dbo.baJobTitle", "dateCreated");
            DropColumn("dbo.baWorkPlace", "userCreated");
            DropColumn("dbo.baWorkPlace", "dateCreated");
            DropColumn("dbo.baCountry", "userCreated");
            DropColumn("dbo.baCountry", "dateCreated");
            DropColumn("dbo.baContractType", "userCreated");
            DropColumn("dbo.baContractType", "dateCreated");
            DropColumn("dbo.baCDC", "userCreated");
            DropColumn("dbo.baCDC", "dateCreated");
            DropColumn("dbo.Employee", "userCreated");
            DropColumn("dbo.Employee", "dateCreated");
            DropColumn("dbo.baBusinessUnitID", "userCreated");
            DropColumn("dbo.baBusinessUnitID", "dateCreated");
        }
    }
}
