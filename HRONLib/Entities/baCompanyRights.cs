namespace HRONLib
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [TrackChanges]
    public partial class baCompanyRights : baseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public baCompanyRights()
        {
            EmplCompanyRights = new ObservableCollection<EmplCompanyRights>();
        }

        [Key]
        public int compID { get; set; }

        public string compDescription { get; set; }

        public short? compSocieta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmplCompanyRights> EmplCompanyRights { get; set; }

        public override int[] getKey()
        {
            return new int[] { compID };
        }
    }
}
