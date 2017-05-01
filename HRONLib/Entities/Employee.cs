namespace HRONLib
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [TrackChanges]
    [Table("Employee")]
    public partial class Employee : baseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            EmplCompanyRights = new ObservableCollection<EmplCompanyRights>();
            EmplFamily = new ObservableCollection<EmplFamily>();
            EmplFunctions = new ObservableCollection<EmplFunctions>();
            Employee1 = new ObservableCollection<Employee>();
            EmplSalary = new ObservableCollection<EmplSalary>();
            EmplDocumentation = new ObservableCollection<EmplDocumentation>();
            EmplFiles = new ObservableCollection<EmplFiles>();
        }

        [Key]
        public int emplID { get; set; }

        public string emplLastName { get; set; }

        public string emplName { get; set; }

        public string emplKey { get; set; }

        public int? emplCdcID { get; set; }

        public int? emplCountryID { get; set; }

        public int? emplWorkPlaceID { get; set; }

        public int? emplBusinessUnitID { get; set; }

        public int? emplJobTitleID { get; set; }

        public int? emplSpecializationID { get; set; }

        public int? emplManagerID { get; set; }

        public int? emplTimeTypeID { get; set; }

        public decimal? emplTimePercentage { get; set; }

        [StringLength(1)]
        public string emplGender { get; set; }

        public int? emplTypeID { get; set; }

        public int? emplLevelID { get; set; }

        public DateTime? emplProbationaryEnd { get; set; }

        public int? emplContractTypeID { get; set; }

        public DateTime? emplWorkEndDate { get; set; }

        public DateTime? emplWorkStartDate { get; set; }

        public string emplBirthPlace { get; set; }

        public DateTime? emplBirthDay { get; set; }

        public string emplFiscalCode { get; set; }

        public int? emplNationalityID { get; set; }

        public int? emplStudyTitleID { get; set; }

        public string emplFiscalCodePartner { get; set; }

        public string emplLivingPlace { get; set; }

        public string emplMobileNumberPrivate { get; set; }

        public virtual baBusinessUnitID baBusinessUnitID { get; set; }

        public virtual baCDC baCDC { get; set; }

        public virtual baContractType baContractType { get; set; }

        public virtual baCountry baCountry { get; set; }

        public virtual baJobTitle baJobTitle { get; set; }

        public virtual baLevel baLevel { get; set; }

        public virtual baNationality baNationality { get; set; }

        public virtual baSpecialization baSpecialization { get; set; }

        public virtual baStudyTitle baStudyTitle { get; set; }

        public virtual baTimeType baTimeType { get; set; }

        public virtual baType baType { get; set; }

        public virtual baWorkPlace baWorkPlace { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmplCompanyRights> EmplCompanyRights { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmplFamily> EmplFamily { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmplFunctions> EmplFunctions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employee1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmplFiles> EmplFiles { get; set; }

        public virtual Employee Employee2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmplSalary> EmplSalary { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmplDocumentation> EmplDocumentation { get; set; }

        public override int[] getKey()
        {
            return new int[] { emplID };
        }
    }
}
