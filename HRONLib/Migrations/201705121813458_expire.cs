namespace HRONLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class expire : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.baExpiration",
                c => new
                    {
                        expID = c.Int(nullable: false, identity: true),
                        expDescription = c.String(),
                        expExpiration = c.Time(nullable: false, precision: 7),
                        dateCreated = c.DateTime(nullable: false),
                        userCreated = c.String(),
                        dateModified = c.DateTime(nullable: false),
                        userModified = c.String(),
                        expDocument_documentationID = c.Int(),
                    })
                .PrimaryKey(t => t.expID)
                .ForeignKey("dbo.baDocumentation", t => t.expDocument_documentationID)
                .Index(t => t.expDocument_documentationID);
            
            CreateTable(
                "dbo.EmplExpirations",
                c => new
                    {
                        expEmplID = c.Int(nullable: false),
                        expExpID = c.Int(nullable: false),
                        startDate = c.DateTime(nullable: false),
                        dateCreated = c.DateTime(nullable: false),
                        userCreated = c.String(),
                        dateModified = c.DateTime(nullable: false),
                        userModified = c.String(),
                    })
                .PrimaryKey(t => new { t.expEmplID, t.expExpID })
                .ForeignKey("dbo.baExpiration", t => t.expExpID, cascadeDelete: true)
                .ForeignKey("dbo.Employee", t => t.expEmplID, cascadeDelete: true)
                .Index(t => t.expEmplID)
                .Index(t => t.expExpID);
            
            CreateTable(
                "dbo.EmplFringeBenefitDetail",
                c => new
                    {
                        frWriteDate = c.DateTime(nullable: false),
                        frEndDate = c.DateTime(),
                        frVersion = c.String(),
                        frModel = c.String(),
                        frSerialNumber = c.String(),
                        frCostMonth = c.Decimal(nullable: false, precision: 18, scale: 2),
                        dateCreated = c.DateTime(nullable: false),
                        userCreated = c.String(),
                        dateModified = c.DateTime(nullable: false),
                        userModified = c.String(),
                        baFringeBenefit_benefitID = c.Int(),
                        Employee_emplID = c.Int(),
                    })
                .PrimaryKey(t => t.frWriteDate)
                .ForeignKey("dbo.baFringeBenefit", t => t.baFringeBenefit_benefitID)
                .ForeignKey("dbo.Employee", t => t.Employee_emplID)
                .Index(t => t.baFringeBenefit_benefitID)
                .Index(t => t.Employee_emplID);
            
            AddColumn("dbo.baBusinessUnitID", "dateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.baBusinessUnitID", "userModified", c => c.String());
            AddColumn("dbo.Employee", "dateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.Employee", "userModified", c => c.String());
            AddColumn("dbo.baContractType", "dateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.baContractType", "userModified", c => c.String());
            AddColumn("dbo.baCountry", "dateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.baCountry", "userModified", c => c.String());
            AddColumn("dbo.baWorkPlace", "workPlaceCode", c => c.String());
            AddColumn("dbo.baWorkPlace", "dateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.baWorkPlace", "userModified", c => c.String());
            AddColumn("dbo.baCompanies", "dateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.baCompanies", "userModified", c => c.String());
            AddColumn("dbo.baCompanyRights", "dateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.baCompanyRights", "userModified", c => c.String());
            AddColumn("dbo.EmplCompanyRights", "dateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.EmplCompanyRights", "userModified", c => c.String());
            AddColumn("dbo.baJobTitle", "dateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.baJobTitle", "userModified", c => c.String());
            AddColumn("dbo.baLevel", "dateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.baLevel", "userModified", c => c.String());
            AddColumn("dbo.baNationality", "dateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.baNationality", "userModified", c => c.String());
            AddColumn("dbo.baSpecialization", "dateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.baSpecialization", "userModified", c => c.String());
            AddColumn("dbo.baStudyTitle", "dateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.baStudyTitle", "userModified", c => c.String());
            AddColumn("dbo.baTimeType", "dateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.baTimeType", "userModified", c => c.String());
            AddColumn("dbo.baType", "dateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.baType", "userModified", c => c.String());
            AddColumn("dbo.EmplCDC", "dateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.EmplCDC", "userModified", c => c.String());
            AddColumn("dbo.EmplCDCDetail", "dateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.EmplCDCDetail", "userModified", c => c.String());
            AddColumn("dbo.EmplCDCDetail", "EmplWorkplace_workPlaceId", c => c.Int());
            AddColumn("dbo.baCDC", "dateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.baCDC", "userModified", c => c.String());
            AddColumn("dbo.EmplDocumentation", "dateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.EmplDocumentation", "userModified", c => c.String());
            AddColumn("dbo.baDocumentation", "dateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.baDocumentation", "userModified", c => c.String());
            AddColumn("dbo.EmplFiles", "dateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.EmplFiles", "userModified", c => c.String());
            AddColumn("dbo.EmplFamily", "dateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.EmplFamily", "userModified", c => c.String());
            AddColumn("dbo.EmplFunctions", "dateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.EmplFunctions", "userModified", c => c.String());
            AddColumn("dbo.baFunctions", "dateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.baFunctions", "userModified", c => c.String());
            AddColumn("dbo.EmplSalary", "dateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.EmplSalary", "userModified", c => c.String());
            AddColumn("dbo.baCarPolicy", "dateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.baCarPolicy", "userModified", c => c.String());
            AddColumn("dbo.EmplSalaryFringeBenefit", "dateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.EmplSalaryFringeBenefit", "userModified", c => c.String());
            AddColumn("dbo.baFringeBenefit", "dateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.baFringeBenefit", "userModified", c => c.String());
            AddColumn("dbo.baUser", "dateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.baUser", "userModified", c => c.String());
            AddColumn("dbo.baUserGroup", "dateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.baUserGroup", "userModified", c => c.String());
            AddColumn("dbo.baWorkflows", "dateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.baWorkflows", "userModified", c => c.String());
            AddColumn("dbo.Config", "dateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.Config", "userModified", c => c.String());
            AddColumn("dbo.EmplWorkflows", "dateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.EmplWorkflows", "userModified", c => c.String());
            CreateIndex("dbo.EmplCDCDetail", "EmplWorkplace_workPlaceId");
            AddForeignKey("dbo.EmplCDCDetail", "EmplWorkplace_workPlaceId", "dbo.baWorkPlace", "workPlaceId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmplFringeBenefitDetail", "Employee_emplID", "dbo.Employee");
            DropForeignKey("dbo.EmplFringeBenefitDetail", "baFringeBenefit_benefitID", "dbo.baFringeBenefit");
            DropForeignKey("dbo.EmplExpirations", "expEmplID", "dbo.Employee");
            DropForeignKey("dbo.EmplExpirations", "expExpID", "dbo.baExpiration");
            DropForeignKey("dbo.baExpiration", "expDocument_documentationID", "dbo.baDocumentation");
            DropForeignKey("dbo.EmplCDCDetail", "EmplWorkplace_workPlaceId", "dbo.baWorkPlace");
            DropIndex("dbo.EmplFringeBenefitDetail", new[] { "Employee_emplID" });
            DropIndex("dbo.EmplFringeBenefitDetail", new[] { "baFringeBenefit_benefitID" });
            DropIndex("dbo.EmplExpirations", new[] { "expExpID" });
            DropIndex("dbo.EmplExpirations", new[] { "expEmplID" });
            DropIndex("dbo.baExpiration", new[] { "expDocument_documentationID" });
            DropIndex("dbo.EmplCDCDetail", new[] { "EmplWorkplace_workPlaceId" });
            DropColumn("dbo.EmplWorkflows", "userModified");
            DropColumn("dbo.EmplWorkflows", "dateModified");
            DropColumn("dbo.Config", "userModified");
            DropColumn("dbo.Config", "dateModified");
            DropColumn("dbo.baWorkflows", "userModified");
            DropColumn("dbo.baWorkflows", "dateModified");
            DropColumn("dbo.baUserGroup", "userModified");
            DropColumn("dbo.baUserGroup", "dateModified");
            DropColumn("dbo.baUser", "userModified");
            DropColumn("dbo.baUser", "dateModified");
            DropColumn("dbo.baFringeBenefit", "userModified");
            DropColumn("dbo.baFringeBenefit", "dateModified");
            DropColumn("dbo.EmplSalaryFringeBenefit", "userModified");
            DropColumn("dbo.EmplSalaryFringeBenefit", "dateModified");
            DropColumn("dbo.baCarPolicy", "userModified");
            DropColumn("dbo.baCarPolicy", "dateModified");
            DropColumn("dbo.EmplSalary", "userModified");
            DropColumn("dbo.EmplSalary", "dateModified");
            DropColumn("dbo.baFunctions", "userModified");
            DropColumn("dbo.baFunctions", "dateModified");
            DropColumn("dbo.EmplFunctions", "userModified");
            DropColumn("dbo.EmplFunctions", "dateModified");
            DropColumn("dbo.EmplFamily", "userModified");
            DropColumn("dbo.EmplFamily", "dateModified");
            DropColumn("dbo.EmplFiles", "userModified");
            DropColumn("dbo.EmplFiles", "dateModified");
            DropColumn("dbo.baDocumentation", "userModified");
            DropColumn("dbo.baDocumentation", "dateModified");
            DropColumn("dbo.EmplDocumentation", "userModified");
            DropColumn("dbo.EmplDocumentation", "dateModified");
            DropColumn("dbo.baCDC", "userModified");
            DropColumn("dbo.baCDC", "dateModified");
            DropColumn("dbo.EmplCDCDetail", "EmplWorkplace_workPlaceId");
            DropColumn("dbo.EmplCDCDetail", "userModified");
            DropColumn("dbo.EmplCDCDetail", "dateModified");
            DropColumn("dbo.EmplCDC", "userModified");
            DropColumn("dbo.EmplCDC", "dateModified");
            DropColumn("dbo.baType", "userModified");
            DropColumn("dbo.baType", "dateModified");
            DropColumn("dbo.baTimeType", "userModified");
            DropColumn("dbo.baTimeType", "dateModified");
            DropColumn("dbo.baStudyTitle", "userModified");
            DropColumn("dbo.baStudyTitle", "dateModified");
            DropColumn("dbo.baSpecialization", "userModified");
            DropColumn("dbo.baSpecialization", "dateModified");
            DropColumn("dbo.baNationality", "userModified");
            DropColumn("dbo.baNationality", "dateModified");
            DropColumn("dbo.baLevel", "userModified");
            DropColumn("dbo.baLevel", "dateModified");
            DropColumn("dbo.baJobTitle", "userModified");
            DropColumn("dbo.baJobTitle", "dateModified");
            DropColumn("dbo.EmplCompanyRights", "userModified");
            DropColumn("dbo.EmplCompanyRights", "dateModified");
            DropColumn("dbo.baCompanyRights", "userModified");
            DropColumn("dbo.baCompanyRights", "dateModified");
            DropColumn("dbo.baCompanies", "userModified");
            DropColumn("dbo.baCompanies", "dateModified");
            DropColumn("dbo.baWorkPlace", "userModified");
            DropColumn("dbo.baWorkPlace", "dateModified");
            DropColumn("dbo.baWorkPlace", "workPlaceCode");
            DropColumn("dbo.baCountry", "userModified");
            DropColumn("dbo.baCountry", "dateModified");
            DropColumn("dbo.baContractType", "userModified");
            DropColumn("dbo.baContractType", "dateModified");
            DropColumn("dbo.Employee", "userModified");
            DropColumn("dbo.Employee", "dateModified");
            DropColumn("dbo.baBusinessUnitID", "userModified");
            DropColumn("dbo.baBusinessUnitID", "dateModified");
            DropTable("dbo.EmplFringeBenefitDetail");
            DropTable("dbo.EmplExpirations");
            DropTable("dbo.baExpiration");
        }
    }
}
