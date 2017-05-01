namespace HRONLib
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [TrackChanges]
    [Table("baCarPolicy")]
    public partial class baCarPolicy : baseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public baCarPolicy()
        {
            EmplSalary = new ObservableCollection<EmplSalary>();
        }

        public int baCarPolicyID { get; set; }

        public string baCarPolicyDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmplSalary> EmplSalary { get; set; }

        public override int[] getKey()
        {
            return new int[] { baCarPolicyID };
        }
    }
}
