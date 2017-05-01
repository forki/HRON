namespace HRONLib
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [TrackChanges]
    [Table("baTimeType")]
    public partial class baTimeType : baseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public baTimeType()
        {
            Employee = new ObservableCollection<Employee>();
        }

        [Key]
        public int timeTypeID { get; set; }

        public string timeTypeDescription { get; set; }

        public bool timeTypeNeedsTimeSpec { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employee { get; set; }

        public override int[] getKey()
        {
            return new int[] { timeTypeID };
        }
    }
}
