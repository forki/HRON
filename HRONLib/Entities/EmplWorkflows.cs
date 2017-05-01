namespace HRONLib
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [TrackChanges]
    [Table("EmplWorkflows")]
    public partial class EmplWorkflows : baseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmplWorkflows()
        {
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int wfEmplID { get; set; }

        [Key]
        [Column(Order = 1)]
        public String wfID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int wfProgressive { get; set; }

        public baWorkflows wfType { get; set; }

        public DateTime wfStartingDate { get; set; }

        public bool wfFinished { get; set; }

        public string wfStatus { get; set; }

        public DateTime? wfStatusUpdated { get; set; }

        [ForeignKey("wfEmplID")]
        public virtual Employee Employee { get; set; }

        public override int[] getKey()
        {
            return new int[] { wfEmplID, wfProgressive };
        }
    }
}
