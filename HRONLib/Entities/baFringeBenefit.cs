namespace HRONLib
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [TrackChanges]
    [Table("baFringeBenefit")]
    public partial class baFringeBenefit : baseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public baFringeBenefit()
        {
            EmplSalaryFringeBenefit = new ObservableCollection<EmplSalaryFringeBenefit>();
        }

        [Key]
        public int benefitID { get; set; }

        public string benefitDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmplSalaryFringeBenefit> EmplSalaryFringeBenefit { get; set; }

        public override int[] getKey()
        {
            return new int[] { benefitID };
        }
    }
}
