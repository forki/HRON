namespace HRONLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ***REMOVED*** : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditLogs",
                c => new
                    {
                        AuditLogId = c.Long(nullable: false, identity: true),
                        UserName = c.String(),
                        EventDateUTC = c.DateTime(nullable: false),
                        EventType = c.Int(nullable: false),
                        TypeFullName = c.String(nullable: false, maxLength: 512),
                        RecordId = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.AuditLogId);
            
            CreateTable(
                "dbo.AuditLogDetails",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PropertyName = c.String(nullable: false, maxLength: 256),
                        OriginalValue = c.String(),
                        NewValue = c.String(),
                        AuditLogId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AuditLogs", t => t.AuditLogId, cascadeDelete: true)
                .Index(t => t.AuditLogId);
            
            CreateTable(
                "dbo.LogMetadata",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AuditLogId = c.Long(nullable: false),
                        Key = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AuditLogs", t => t.AuditLogId, cascadeDelete: true)
                .Index(t => t.AuditLogId);
            
            CreateTable(
                "dbo.baBusinessUnitID",
                c => new
                    {
                        businessUnitID = c.Int(nullable: false, identity: true),
                        businessUnitDescription = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.businessUnitID);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        emplID = c.Int(nullable: false, identity: true),
                        emplLastName = c.String(unicode: false),
                        emplName = c.String(unicode: false),
                        emplKey = c.String(unicode: false),
                        emplCdcID = c.Int(),
                        emplCountryID = c.Int(),
                        emplWorkPlaceID = c.Int(),
                        emplBusinessUnitID = c.Int(),
                        emplJobTitleID = c.Int(),
                        emplSpecializationID = c.Int(),
                        emplManagerID = c.Int(),
                        emplTimeTypeID = c.Int(),
                        emplTimePercentage = c.Decimal(precision: 5, scale: 2),
                        emplGender = c.String(maxLength: 1, unicode: false),
                        emplTypeID = c.Int(),
                        emplLevelID = c.Int(),
                        emplProbationaryEnd = c.DateTime(),
                        emplContractTypeID = c.Int(),
                        emplWorkEndDate = c.DateTime(),
                        emplWorkStartDate = c.DateTime(),
                        emplBirthPlace = c.String(unicode: false),
                        emplBirthDay = c.DateTime(),
                        emplFiscalCode = c.String(unicode: false),
                        emplNationalityID = c.Int(),
                        emplStudyTitleID = c.Int(),
                        emplFiscalCodePartner = c.String(unicode: false),
                        emplLivingPlace = c.String(unicode: false),
                        emplMobileNumberPrivate = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.emplID)
                .ForeignKey("dbo.baCDC", t => t.emplCdcID)
                .ForeignKey("dbo.baContractType", t => t.emplContractTypeID)
                .ForeignKey("dbo.baWorkPlace", t => t.emplWorkPlaceID)
                .ForeignKey("dbo.baCountry", t => t.emplCountryID)
                .ForeignKey("dbo.baJobTitle", t => t.emplJobTitleID)
                .ForeignKey("dbo.baLevel", t => t.emplLevelID)
                .ForeignKey("dbo.baNationality", t => t.emplNationalityID)
                .ForeignKey("dbo.baSpecialization", t => t.emplSpecializationID)
                .ForeignKey("dbo.baStudyTitle", t => t.emplStudyTitleID)
                .ForeignKey("dbo.baTimeType", t => t.emplTimeTypeID)
                .ForeignKey("dbo.baType", t => t.emplTypeID)
                .ForeignKey("dbo.Employee", t => t.emplManagerID)
                .ForeignKey("dbo.baBusinessUnitID", t => t.emplBusinessUnitID)
                .Index(t => t.emplCdcID)
                .Index(t => t.emplCountryID)
                .Index(t => t.emplWorkPlaceID)
                .Index(t => t.emplBusinessUnitID)
                .Index(t => t.emplJobTitleID)
                .Index(t => t.emplSpecializationID)
                .Index(t => t.emplManagerID)
                .Index(t => t.emplTimeTypeID)
                .Index(t => t.emplTypeID)
                .Index(t => t.emplLevelID)
                .Index(t => t.emplContractTypeID)
                .Index(t => t.emplNationalityID)
                .Index(t => t.emplStudyTitleID);
            
            CreateTable(
                "dbo.baCDC",
                c => new
                    {
                        cdcID = c.Int(nullable: false, identity: true),
                        cdcValue = c.String(maxLength: 10, unicode: false),
                        cdcDescription = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.cdcID);
            
            CreateTable(
                "dbo.baContractType",
                c => new
                    {
                        contractTypeID = c.Int(nullable: false, identity: true),
                        contractTypeDescription = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.contractTypeID);
            
            CreateTable(
                "dbo.baCountry",
                c => new
                    {
                        countryID = c.Int(nullable: false, identity: true),
                        countryISOCode = c.String(unicode: false),
                        countryTitle = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.countryID);
            
            CreateTable(
                "dbo.baWorkPlace",
                c => new
                    {
                        workPlaceId = c.Int(nullable: false, identity: true),
                        workPlaceName = c.String(unicode: false),
                        workPlaceCity = c.String(unicode: false),
                        workPlaceCountryID = c.Int(),
                        workPlaceAddress = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.workPlaceId)
                .ForeignKey("dbo.baCountry", t => t.workPlaceCountryID)
                .Index(t => t.workPlaceCountryID);
            
            CreateTable(
                "dbo.baJobTitle",
                c => new
                    {
                        jobTitleID = c.Int(nullable: false, identity: true),
                        jobTitleDescription = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.jobTitleID);
            
            CreateTable(
                "dbo.baLevel",
                c => new
                    {
                        levelID = c.Int(nullable: false, identity: true),
                        levelDescription = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.levelID);
            
            CreateTable(
                "dbo.baNationality",
                c => new
                    {
                        nationalityID = c.Int(nullable: false, identity: true),
                        nationalityDescription = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.nationalityID);
            
            CreateTable(
                "dbo.baSpecialization",
                c => new
                    {
                        specializationID = c.Int(nullable: false, identity: true),
                        specializationDescription = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.specializationID);
            
            CreateTable(
                "dbo.baStudyTitle",
                c => new
                    {
                        studyTitleID = c.Int(nullable: false, identity: true),
                        studyTitleDescription = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.studyTitleID);
            
            CreateTable(
                "dbo.baTimeType",
                c => new
                    {
                        timeTypeID = c.Int(nullable: false, identity: true),
                        timeTypeDescription = c.String(unicode: false),
                        timeTypeNeedsTimeSpec = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.timeTypeID);
            
            CreateTable(
                "dbo.baType",
                c => new
                    {
                        typeID = c.Int(nullable: false, identity: true),
                        typeDescription = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.typeID);
            
            CreateTable(
                "dbo.EmplCompanyRights",
                c => new
                    {
                        rightID = c.Int(nullable: false, identity: true),
                        comprightID = c.Int(),
                        emplID = c.Int(),
                        value = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.rightID)
                .ForeignKey("dbo.baCompanyRights", t => t.comprightID)
                .ForeignKey("dbo.Employee", t => t.emplID)
                .Index(t => t.comprightID)
                .Index(t => t.emplID);
            
            CreateTable(
                "dbo.baCompanyRights",
                c => new
                    {
                        compID = c.Int(nullable: false, identity: true),
                        compDescription = c.String(),
                        compSocieta = c.Short(),
                    })
                .PrimaryKey(t => t.compID);
            
            CreateTable(
                "dbo.EmplFamily",
                c => new
                    {
                        emplID = c.Int(nullable: false),
                        famID = c.Int(nullable: false),
                        famName = c.String(),
                        famFiscalCode = c.String(),
                        famInsertDate = c.DateTime(),
                        famAssegniFamigliari = c.Boolean(),
                    })
                .PrimaryKey(t => new { t.emplID, t.famID })
                .ForeignKey("dbo.Employee", t => t.emplID)
                .Index(t => t.emplID);
            
            CreateTable(
                "dbo.EmplFiles",
                c => new
                    {
                        EmplID = c.Int(nullable: false),
                        Progressive = c.Int(nullable: false, identity: true),
                        FileContent = c.Binary(),
                        FileName = c.String(maxLength: 255),
                        DateCreated = c.DateTime(),
                        UserCreated = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => new { t.EmplID, t.Progressive })
                .ForeignKey("dbo.Employee", t => t.EmplID)
                .Index(t => t.EmplID);
            
            CreateTable(
                "dbo.EmplFunctions",
                c => new
                    {
                        emplFuncID = c.Int(nullable: false, identity: true),
                        funcID = c.Int(),
                        emplID = c.Int(),
                        value = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.emplFuncID)
                .ForeignKey("dbo.baFunctions", t => t.funcID)
                .ForeignKey("dbo.Employee", t => t.emplID)
                .Index(t => t.funcID)
                .Index(t => t.emplID);
            
            CreateTable(
                "dbo.baFunctions",
                c => new
                    {
                        funcID = c.Int(nullable: false, identity: true),
                        funcDescription = c.String(),
                    })
                .PrimaryKey(t => t.funcID);
            
            CreateTable(
                "dbo.EmplSalary",
                c => new
                    {
                        salEmplID = c.Int(nullable: false),
                        salID = c.Int(nullable: false),
                        salWriteDate = c.DateTime(nullable: false),
                        salStartingFrom = c.DateTime(nullable: false),
                        salL104 = c.Boolean(),
                        salL68 = c.Boolean(),
                        salGrossSalaryMonth = c.Decimal(precision: 18, scale: 3),
                        salGrossSalaryMonthFT = c.Decimal(precision: 18, scale: 3),
                        salSuperMinimoAssorbibile = c.Boolean(),
                        salOvertimeForfait = c.Boolean(),
                        salOvertimeHours = c.Decimal(precision: 18, scale: 3),
                        salNetSalaryMonth = c.Decimal(precision: 18, scale: 3),
                        salGrossSalaryYear = c.Decimal(precision: 18, scale: 3),
                        salCostYear = c.Decimal(precision: 18, scale: 3),
                        salCostWithBonusYear = c.Decimal(precision: 18, scale: 3),
                        salBonus = c.Decimal(precision: 18, scale: 3),
                        salCar = c.Boolean(),
                        salCarPolicyClass = c.Int(),
                    })
                .PrimaryKey(t => new { t.salEmplID, t.salID, t.salWriteDate })
                .ForeignKey("dbo.baCarPolicy", t => t.salCarPolicyClass)
                .ForeignKey("dbo.Employee", t => t.salEmplID)
                .Index(t => t.salEmplID)
                .Index(t => t.salCarPolicyClass);
            
            CreateTable(
                "dbo.baCarPolicy",
                c => new
                    {
                        baCarPolicyID = c.Int(nullable: false, identity: true),
                        baCarPolicyDescription = c.String(),
                    })
                .PrimaryKey(t => t.baCarPolicyID);
            
            CreateTable(
                "dbo.EmplSalaryFringeBenefit",
                c => new
                    {
                        salEmplID = c.Int(nullable: false),
                        salID = c.Int(nullable: false),
                        salWriteDate = c.DateTime(nullable: false),
                        benefitID = c.Int(nullable: false),
                        value = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.salEmplID, t.salID, t.salWriteDate, t.benefitID })
                .ForeignKey("dbo.baFringeBenefit", t => t.benefitID)
                .ForeignKey("dbo.EmplSalary", t => new { t.salEmplID, t.salID, t.salWriteDate })
                .Index(t => new { t.salEmplID, t.salID, t.salWriteDate })
                .Index(t => t.benefitID);
            
            CreateTable(
                "dbo.baFringeBenefit",
                c => new
                    {
                        benefitID = c.Int(nullable: false, identity: true),
                        benefitDescription = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.benefitID);
            
            CreateTable(
                "dbo.baUser",
                c => new
                    {
                        userID = c.String(nullable: false, maxLength: 255),
                        userGroupID = c.Int(),
                    })
                .PrimaryKey(t => t.userID)
                .ForeignKey("dbo.baUserGroup", t => t.userGroupID)
                .Index(t => t.userGroupID);
            
            CreateTable(
                "dbo.baUserGroup",
                c => new
                    {
                        userGroup = c.Int(nullable: false, identity: true),
                        userGroupDescription = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.userGroup);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.baUser", "userGroupID", "dbo.baUserGroup");
            DropForeignKey("dbo.Employee", "emplBusinessUnitID", "dbo.baBusinessUnitID");
            DropForeignKey("dbo.EmplSalary", "salEmplID", "dbo.Employee");
            DropForeignKey("dbo.EmplSalaryFringeBenefit", new[] { "salEmplID", "salID", "salWriteDate" }, "dbo.EmplSalary");
            DropForeignKey("dbo.EmplSalaryFringeBenefit", "benefitID", "dbo.baFringeBenefit");
            DropForeignKey("dbo.EmplSalary", "salCarPolicyClass", "dbo.baCarPolicy");
            DropForeignKey("dbo.Employee", "emplManagerID", "dbo.Employee");
            DropForeignKey("dbo.EmplFunctions", "emplID", "dbo.Employee");
            DropForeignKey("dbo.EmplFunctions", "funcID", "dbo.baFunctions");
            DropForeignKey("dbo.EmplFiles", "EmplID", "dbo.Employee");
            DropForeignKey("dbo.EmplFamily", "emplID", "dbo.Employee");
            DropForeignKey("dbo.EmplCompanyRights", "emplID", "dbo.Employee");
            DropForeignKey("dbo.EmplCompanyRights", "comprightID", "dbo.baCompanyRights");
            DropForeignKey("dbo.Employee", "emplTypeID", "dbo.baType");
            DropForeignKey("dbo.Employee", "emplTimeTypeID", "dbo.baTimeType");
            DropForeignKey("dbo.Employee", "emplStudyTitleID", "dbo.baStudyTitle");
            DropForeignKey("dbo.Employee", "emplSpecializationID", "dbo.baSpecialization");
            DropForeignKey("dbo.Employee", "emplNationalityID", "dbo.baNationality");
            DropForeignKey("dbo.Employee", "emplLevelID", "dbo.baLevel");
            DropForeignKey("dbo.Employee", "emplJobTitleID", "dbo.baJobTitle");
            DropForeignKey("dbo.Employee", "emplCountryID", "dbo.baCountry");
            DropForeignKey("dbo.baWorkPlace", "workPlaceCountryID", "dbo.baCountry");
            DropForeignKey("dbo.Employee", "emplWorkPlaceID", "dbo.baWorkPlace");
            DropForeignKey("dbo.Employee", "emplContractTypeID", "dbo.baContractType");
            DropForeignKey("dbo.Employee", "emplCdcID", "dbo.baCDC");
            DropForeignKey("dbo.LogMetadata", "AuditLogId", "dbo.AuditLogs");
            DropForeignKey("dbo.AuditLogDetails", "AuditLogId", "dbo.AuditLogs");
            DropIndex("dbo.baUser", new[] { "userGroupID" });
            DropIndex("dbo.EmplSalaryFringeBenefit", new[] { "benefitID" });
            DropIndex("dbo.EmplSalaryFringeBenefit", new[] { "salEmplID", "salID", "salWriteDate" });
            DropIndex("dbo.EmplSalary", new[] { "salCarPolicyClass" });
            DropIndex("dbo.EmplSalary", new[] { "salEmplID" });
            DropIndex("dbo.EmplFunctions", new[] { "emplID" });
            DropIndex("dbo.EmplFunctions", new[] { "funcID" });
            DropIndex("dbo.EmplFiles", new[] { "EmplID" });
            DropIndex("dbo.EmplFamily", new[] { "emplID" });
            DropIndex("dbo.EmplCompanyRights", new[] { "emplID" });
            DropIndex("dbo.EmplCompanyRights", new[] { "comprightID" });
            DropIndex("dbo.baWorkPlace", new[] { "workPlaceCountryID" });
            DropIndex("dbo.Employee", new[] { "emplStudyTitleID" });
            DropIndex("dbo.Employee", new[] { "emplNationalityID" });
            DropIndex("dbo.Employee", new[] { "emplContractTypeID" });
            DropIndex("dbo.Employee", new[] { "emplLevelID" });
            DropIndex("dbo.Employee", new[] { "emplTypeID" });
            DropIndex("dbo.Employee", new[] { "emplTimeTypeID" });
            DropIndex("dbo.Employee", new[] { "emplManagerID" });
            DropIndex("dbo.Employee", new[] { "emplSpecializationID" });
            DropIndex("dbo.Employee", new[] { "emplJobTitleID" });
            DropIndex("dbo.Employee", new[] { "emplBusinessUnitID" });
            DropIndex("dbo.Employee", new[] { "emplWorkPlaceID" });
            DropIndex("dbo.Employee", new[] { "emplCountryID" });
            DropIndex("dbo.Employee", new[] { "emplCdcID" });
            DropIndex("dbo.LogMetadata", new[] { "AuditLogId" });
            DropIndex("dbo.AuditLogDetails", new[] { "AuditLogId" });
            DropTable("dbo.baUserGroup");
            DropTable("dbo.baUser");
            DropTable("dbo.baFringeBenefit");
            DropTable("dbo.EmplSalaryFringeBenefit");
            DropTable("dbo.baCarPolicy");
            DropTable("dbo.EmplSalary");
            DropTable("dbo.baFunctions");
            DropTable("dbo.EmplFunctions");
            DropTable("dbo.EmplFiles");
            DropTable("dbo.EmplFamily");
            DropTable("dbo.baCompanyRights");
            DropTable("dbo.EmplCompanyRights");
            DropTable("dbo.baType");
            DropTable("dbo.baTimeType");
            DropTable("dbo.baStudyTitle");
            DropTable("dbo.baSpecialization");
            DropTable("dbo.baNationality");
            DropTable("dbo.baLevel");
            DropTable("dbo.baJobTitle");
            DropTable("dbo.baWorkPlace");
            DropTable("dbo.baCountry");
            DropTable("dbo.baContractType");
            DropTable("dbo.baCDC");
            DropTable("dbo.Employee");
            DropTable("dbo.baBusinessUnitID");
            DropTable("dbo.LogMetadata");
            DropTable("dbo.AuditLogDetails");
            DropTable("dbo.AuditLogs");
        }
    }
}
