namespace HRONLib
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [TrackChanges]
    [Table("EmplDocumentation")]
    public partial class EmplDocumentation : baseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmplDocumentation()
        {
            EmplFiles = new ObservableCollection<EmplFiles>();
        }

        [Key]
        [Column(Order = 0)]
        public int emplID { get; set; }

        [Key]
        [Column(Order = 1)]
        public int documentationID { get; set; }

        public DateTime? dateSignature { get; set; }

        public virtual baDocumentation baDocumentation { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmplFiles> EmplFiles { get; set; }

        public override int[] getKey()
        {
            return new int[] { emplID, documentationID };
        }
    }
}
