namespace HRONLib
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [TrackChanges]
    [Table("baCDC")]
    public partial class baCDC:baseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public baCDC()
        {
            EmplCDCDetail = new ObservableCollection<EmplCDCDetail>();
        }

        [Key]
        public int cdcID { get; set; }

        [StringLength(10)]
        public string cdcValue { get; set; }

        [StringLength(50)]
        public string cdcDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmplCDCDetail> EmplCDCDetail { get; set; }

        public override int[] getKey()
        {
            return new int[] { cdcID };
        }
    }
}
