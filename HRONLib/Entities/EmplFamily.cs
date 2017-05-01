namespace HRONLib
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [TrackChanges]
    [Table("EmplFamily")]
    public partial class EmplFamily : baseEntity
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int emplID { get; set; }

        [Key]
        [Column(Order = 1)]
        public int famID { get; set; }

        public string famName { get; set; }

        public string famFiscalCode { get; set; }

        public DateTime? famInsertDate { get; set; }

        public bool? famAssegniFamigliari { get; set; }

        public virtual Employee Employee { get; set; }

        public override int[] getKey()
        {
            return new int[] { emplID, famID };
        }
    }
}
