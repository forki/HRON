namespace HRONLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Unicode : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.EmplFiles", new[] { "EmplID", "emplDocumentationId" });
            AlterColumn("dbo.baBusinessUnitID", "businessUnitDescription", c => c.String());
            AlterColumn("dbo.Employee", "emplLastName", c => c.String());
            AlterColumn("dbo.Employee", "emplName", c => c.String());
            AlterColumn("dbo.Employee", "emplKey", c => c.String());
            AlterColumn("dbo.Employee", "emplGender", c => c.String(maxLength: 1));
            AlterColumn("dbo.Employee", "emplBirthPlace", c => c.String());
            AlterColumn("dbo.Employee", "emplFiscalCode", c => c.String());
            AlterColumn("dbo.Employee", "emplFiscalCodePartner", c => c.String());
            AlterColumn("dbo.Employee", "emplLivingPlace", c => c.String());
            AlterColumn("dbo.Employee", "emplMobileNumberPrivate", c => c.String());
            AlterColumn("dbo.baCDC", "cdcValue", c => c.String(maxLength: 10));
            AlterColumn("dbo.baCDC", "cdcDescription", c => c.String(maxLength: 50));
            AlterColumn("dbo.baContractType", "contractTypeDescription", c => c.String());
            AlterColumn("dbo.baCountry", "countryISOCode", c => c.String());
            AlterColumn("dbo.baCountry", "countryTitle", c => c.String());
            AlterColumn("dbo.baWorkPlace", "workPlaceName", c => c.String());
            AlterColumn("dbo.baWorkPlace", "workPlaceCity", c => c.String());
            AlterColumn("dbo.baWorkPlace", "workPlaceAddress", c => c.String());
            AlterColumn("dbo.baJobTitle", "jobTitleDescription", c => c.String());
            AlterColumn("dbo.baLevel", "levelDescription", c => c.String());
            AlterColumn("dbo.baNationality", "nationalityDescription", c => c.String());
            AlterColumn("dbo.baSpecialization", "specializationDescription", c => c.String());
            AlterColumn("dbo.baStudyTitle", "studyTitleDescription", c => c.String());
            AlterColumn("dbo.baTimeType", "timeTypeDescription", c => c.String());
            AlterColumn("dbo.baType", "typeDescription", c => c.String());
            AlterColumn("dbo.EmplFiles", "emplDocumentationId", c => c.Int());
            AlterColumn("dbo.EmplFiles", "dateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.EmplFiles", "userCreated", c => c.String());
            AlterColumn("dbo.EmplFiles", "DateCreated", c => c.DateTime());
            AlterColumn("dbo.EmplFiles", "UserCreated", c => c.String(maxLength: 50));
            AlterColumn("dbo.baFringeBenefit", "benefitDescription", c => c.String());
            AlterColumn("dbo.baUserGroup", "userGroupDescription", c => c.String());
            CreateIndex("dbo.EmplFiles", new[] { "EmplID", "emplDocumentationId" });
        }
        
        public override void Down()
        {
            DropIndex("dbo.EmplFiles", new[] { "EmplID", "emplDocumentationId" });
            AlterColumn("dbo.baUserGroup", "userGroupDescription", c => c.String(unicode: false));
            AlterColumn("dbo.baFringeBenefit", "benefitDescription", c => c.String(unicode: false));
            AlterColumn("dbo.EmplFiles", "UserCreated", c => c.String());
            AlterColumn("dbo.EmplFiles", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.EmplFiles", "userCreated", c => c.String(maxLength: 50));
            AlterColumn("dbo.EmplFiles", "dateCreated", c => c.DateTime());
            AlterColumn("dbo.EmplFiles", "emplDocumentationId", c => c.Int(nullable: false));
            AlterColumn("dbo.baType", "typeDescription", c => c.String(unicode: false));
            AlterColumn("dbo.baTimeType", "timeTypeDescription", c => c.String(unicode: false));
            AlterColumn("dbo.baStudyTitle", "studyTitleDescription", c => c.String(unicode: false));
            AlterColumn("dbo.baSpecialization", "specializationDescription", c => c.String(unicode: false));
            AlterColumn("dbo.baNationality", "nationalityDescription", c => c.String(unicode: false));
            AlterColumn("dbo.baLevel", "levelDescription", c => c.String(unicode: false));
            AlterColumn("dbo.baJobTitle", "jobTitleDescription", c => c.String(unicode: false));
            AlterColumn("dbo.baWorkPlace", "workPlaceAddress", c => c.String(unicode: false));
            AlterColumn("dbo.baWorkPlace", "workPlaceCity", c => c.String(unicode: false));
            AlterColumn("dbo.baWorkPlace", "workPlaceName", c => c.String(unicode: false));
            AlterColumn("dbo.baCountry", "countryTitle", c => c.String(unicode: false));
            AlterColumn("dbo.baCountry", "countryISOCode", c => c.String(unicode: false));
            AlterColumn("dbo.baContractType", "contractTypeDescription", c => c.String(unicode: false));
            AlterColumn("dbo.baCDC", "cdcDescription", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.baCDC", "cdcValue", c => c.String(maxLength: 10, unicode: false));
            AlterColumn("dbo.Employee", "emplMobileNumberPrivate", c => c.String(unicode: false));
            AlterColumn("dbo.Employee", "emplLivingPlace", c => c.String(unicode: false));
            AlterColumn("dbo.Employee", "emplFiscalCodePartner", c => c.String(unicode: false));
            AlterColumn("dbo.Employee", "emplFiscalCode", c => c.String(unicode: false));
            AlterColumn("dbo.Employee", "emplBirthPlace", c => c.String(unicode: false));
            AlterColumn("dbo.Employee", "emplGender", c => c.String(maxLength: 1, unicode: false));
            AlterColumn("dbo.Employee", "emplKey", c => c.String(unicode: false));
            AlterColumn("dbo.Employee", "emplName", c => c.String(unicode: false));
            AlterColumn("dbo.Employee", "emplLastName", c => c.String(unicode: false));
            AlterColumn("dbo.baBusinessUnitID", "businessUnitDescription", c => c.String(unicode: false));
            CreateIndex("dbo.EmplFiles", new[] { "EmplID", "emplDocumentationId" });
        }
    }
}
