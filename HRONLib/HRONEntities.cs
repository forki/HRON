namespace HRONLib
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class HRONEntities : TrackerEnabledDbContext.TrackerContext
    {
        public HRONEntities()
            : base("name=HRONDip")
        {
            this.ConfigureUsername(System.Security.Principal.WindowsIdentity.GetCurrent().Name);
        }

        public virtual DbSet<baBusinessUnitID> baBusinessUnitID { get; set; }
        public virtual DbSet<baCarPolicy> baCarPolicy { get; set; }
        public virtual DbSet<baCDC> baCDC { get; set; }
        public virtual DbSet<baCompanyRights> baCompanyRights { get; set; }
        public virtual DbSet<baContractType> baContractType { get; set; }
        public virtual DbSet<baCountry> baCountry { get; set; }
        public virtual DbSet<baDocumentation> baDocumentation { get; set; }
        public virtual DbSet<baFringeBenefit> baFringeBenefit { get; set; }
        public virtual DbSet<baFunctions> baFunctions { get; set; }
        public virtual DbSet<baJobTitle> baJobTitle { get; set; }
        public virtual DbSet<baLevel> baLevel { get; set; }
        public virtual DbSet<baNationality> baNationality { get; set; }
        public virtual DbSet<baSpecialization> baSpecialization { get; set; }
        public virtual DbSet<baStudyTitle> baStudyTitle { get; set; }
        public virtual DbSet<baTimeType> baTimeType { get; set; }
        public virtual DbSet<baType> baType { get; set; }
        public virtual DbSet<baUser> baUser { get; set; }
        public virtual DbSet<baUserGroup> baUserGroup { get; set; }
        public virtual DbSet<baWorkPlace> baWorkPlace { get; set; }
        public virtual DbSet<baWorkflows> baWorkflows { get; set; }
        public virtual DbSet<EmplCompanyRights> EmplCompanyRights { get; set; }
        public virtual DbSet<EmplFamily> EmplFamily { get; set; }
        public virtual DbSet<EmplFiles> EmplFiles { get; set; }
        public virtual DbSet<EmplDocumentation> EmplDocumentation { get; set; }
        public virtual DbSet<EmplFunctions> EmplFunctions { get; set; }
        public virtual DbSet<EmplWorkflows> EmplWorkflows { get; set; }
        public virtual DbSet<EmplCDC> EmplCDC { get; set; }
        public virtual DbSet<EmplCDCDetail> EmplCDCDetail { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmplSalary> EmplSalary { get; set; }
        public virtual DbSet<WFApprovals> WFApprovals { get; set; }
        public virtual DbSet<baExpiration> baExpiration { get; set; }
        public virtual DbSet<EmplExpirations> EmplExpirations { get; set; }
        public virtual DbSet<EmplFringeBenefitDetail> EmplFringeBenefitDetail { get; set; }
        public virtual DbSet<EmplSalaryFringeBenefit> EmplSalaryFringeBenefit { get; set; }

        public virtual DbSet<Config> Config { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<baBusinessUnitID>()
                .HasMany(e => e.Employee)
                .WithOptional(e => e.baBusinessUnitID)
                .HasForeignKey(e => e.emplBusinessUnitID);

            modelBuilder.Entity<baCarPolicy>()
                .HasMany(e => e.EmplSalary)
                .WithOptional(e => e.baCarPolicy)
                .HasForeignKey(e => e.salCarPolicyClass);

            modelBuilder.Entity<baCompanyRights>()
                .HasMany(e => e.EmplCompanyRights)
                .WithOptional(e => e.baCompanyRights)
                .HasForeignKey(e => e.comprightID);

            modelBuilder.Entity<baContractType>()
                .HasMany(e => e.Employee)
                .WithOptional(e => e.baContractType)
                .HasForeignKey(e => e.emplContractTypeID);

            modelBuilder.Entity<baCountry>()
                .HasMany(e => e.baWorkPlace)
                .WithOptional(e => e.baCountry)
                .HasForeignKey(e => e.workPlaceCountryID);

            modelBuilder.Entity<baCompany>()
                .HasMany(e => e.baWorkPlace)
                .WithOptional(e => e.baCompany);

            modelBuilder.Entity<baCompany>()
                .HasMany(e => e.baCompanyRights)
                .WithOptional(e => e.compCompany);

            modelBuilder.Entity<baCountry>()
                .HasMany(e => e.Employee)
                .WithOptional(e => e.baCountry)
                .HasForeignKey(e => e.emplCountryID);

            modelBuilder.Entity<baFringeBenefit>()
                .HasMany(e => e.EmplSalaryFringeBenefit)
                .WithRequired(e => e.baFringeBenefit)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<baJobTitle>()
                .HasMany(e => e.Employee)
                .WithOptional(e => e.baJobTitle)
                .HasForeignKey(e => e.emplJobTitleID);

            modelBuilder.Entity<baLevel>()
                .HasMany(e => e.Employee)
                .WithOptional(e => e.baLevel)
                .HasForeignKey(e => e.emplLevelID);

            modelBuilder.Entity<baNationality>()
                .HasMany(e => e.Employee)
                .WithOptional(e => e.baNationality)
                .HasForeignKey(e => e.emplNationalityID);

            modelBuilder.Entity<baSpecialization>()
                .HasMany(e => e.Employee)
                .WithOptional(e => e.baSpecialization)
                .HasForeignKey(e => e.emplSpecializationID);

            modelBuilder.Entity<baStudyTitle>()
                .HasMany(e => e.Employee)
                .WithOptional(e => e.baStudyTitle)
                .HasForeignKey(e => e.emplStudyTitleID);

            modelBuilder.Entity<baTimeType>()
                .HasMany(e => e.Employee)
                .WithOptional(e => e.baTimeType)
                .HasForeignKey(e => e.emplTimeTypeID);

            modelBuilder.Entity<baType>()
                .HasMany(e => e.Employee)
                .WithOptional(e => e.baType)
                .HasForeignKey(e => e.emplTypeID);

            modelBuilder.Entity<baUserGroup>()
                .HasMany(e => e.baUser)
                .WithOptional(e => e.baUserGroup)
                .HasForeignKey(e => e.userGroupID);

            modelBuilder.Entity<baWorkPlace>()
                .HasMany(e => e.Employee)
                .WithOptional(e => e.baWorkPlace)
                .HasForeignKey(e => e.emplWorkPlaceID);

            modelBuilder.Entity<Employee>()
                .Property(e => e.emplTimePercentage)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmplFamily)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Employee1)
                .WithOptional(e => e.Employee2)
                .HasForeignKey(e => e.emplManagerID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmplSalary)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.salEmplID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmplFiles)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.EmplID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EmplSalary>()
                .Property(e => e.salGrossSalaryMonth)
                .HasPrecision(18, 3);

            modelBuilder.Entity<EmplSalary>()
                .Property(e => e.salGrossSalaryMonthFT)
                .HasPrecision(18, 3);

            modelBuilder.Entity<EmplSalary>()
                .Property(e => e.salOvertimeHours)
                .HasPrecision(18, 3);

            modelBuilder.Entity<EmplSalary>()
                .Property(e => e.salNetSalaryMonth)
                .HasPrecision(18, 3);

            modelBuilder.Entity<EmplSalary>()
                .Property(e => e.salGrossSalaryYear)
                .HasPrecision(18, 3);

            modelBuilder.Entity<EmplSalary>()
                .Property(e => e.salCostYear)
                .HasPrecision(18, 3);

            modelBuilder.Entity<EmplSalary>()
                .Property(e => e.salCostWithBonusYear)
                .HasPrecision(18, 3);

            modelBuilder.Entity<EmplSalary>()
                .Property(e => e.salBonus)
                .HasPrecision(18, 3);

            modelBuilder.Entity<EmplSalary>()
                .HasMany(e => e.EmplSalaryFringeBenefit)
                .WithRequired(e => e.EmplSalary)
                .HasForeignKey(e => new { e.salEmplID, e.salID, e.salWriteDate })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EmplDocumentation>()
                .HasMany(e => e.EmplFiles)
                .WithOptional(e => e.EmplDocumentation)
                .HasForeignKey(e => new { e.EmplID, e.emplDocumentationId })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmplDocumentation)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.emplID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmplCDC)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.cdcEmplID);

        }

        public override int SaveChanges()
        {
            var trackables = ChangeTracker.Entries<baseEntity>();

            if (trackables != null)
            {
                foreach (var item in trackables.Where(t => t.State == EntityState.Added))
                    item.Entity.onInsert();
                foreach (var item in trackables.Where(t => t.State == EntityState.Modified))
                    item.Entity.onUpdate();
            }

            return base.SaveChanges();
        }
    }
}
