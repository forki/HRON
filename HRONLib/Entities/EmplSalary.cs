namespace HRONLib
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [TrackChanges]
    [Table("EmplSalary")]
    public partial class EmplSalary : baseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmplSalary()
        {
            EmplSalaryFringeBenefit = new ObservableCollection<EmplSalaryFringeBenefit>();
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int salEmplID { get; set; }

        [Key]
        [Column(Order = 1)]
        public int salID { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime salWriteDate { get; set; }

        public DateTime? salStartingFrom { get; set; }

        public bool? salL104 { get; set; }

        public bool? salL68 { get; set; }

        public decimal? salGrossSalaryMonth { get; set; }

        public decimal? salGrossSalaryMonthFT { get; set; }

        public bool? salSuperMinimoAssorbibile { get; set; }

        public bool? salOvertimeForfait { get; set; }

        public decimal? salOvertimeHours { get; set; }

        public decimal? salNetSalaryMonth { get; set; }

        public decimal? salGrossSalaryYear { get; set; }

        public decimal? salCostYear { get; set; }

        public decimal? salCostWithBonusYear { get; set; }

        public decimal? salBonus { get; set; }

        public bool? salCar { get; set; }

        public int? salCarPolicyClass { get; set; }

        public virtual baCarPolicy baCarPolicy { get; set; }

        public virtual Employee Employee { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public bool isActualLine
        {
            get { return (salStartingFrom < DateTime.Now.Date); }
            private set { /* needed for EF */ }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmplSalaryFringeBenefit> EmplSalaryFringeBenefit { get; set; }

        public override int[] getKey()
        {
            return new int[] { salEmplID, salID };
        }
    }
}
