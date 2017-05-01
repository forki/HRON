namespace HRONLib
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [TrackChanges]
    [Table("baWorkflows")]
    public partial class baWorkflows : baseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public baWorkflows()
        {
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int wfID { get; set; }

        public string wfName { get; set; }

        public byte[] WorkFlowXAML { get; set; }

        public bool OnSave { get; set; }
        public bool OnOnBoarding { get; set; }

        public override int[] getKey()
        {
            return new int[] { wfID };
        }
    }
}
