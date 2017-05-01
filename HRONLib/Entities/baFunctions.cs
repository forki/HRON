namespace HRONLib
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [TrackChanges]
    public partial class baFunctions : baseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public baFunctions()
        {
            EmplFunctions = new ObservableCollection<EmplFunctions>();
        }

        [Key]
        public int funcID { get; set; }

        public string funcDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmplFunctions> EmplFunctions { get; set; }

        public override int[] getKey()
        {
            return new int[] { funcID };
        }
    }
}
