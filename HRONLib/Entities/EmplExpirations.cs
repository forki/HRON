namespace HRONLib
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [TrackChanges]
    [Table("EmplExpirations")]
    public partial class EmplExpirations : baseEntity
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Employee")]
        public int expEmplID { get; set; }

        [Key]
        [Column(Order = 1)]
        [ForeignKey("baExpiration")]
        public int expExpID { get; set; }

        public DateTime startDate { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual baExpiration baExpiration { get; set; }

        public override int[] getKey()
        {
            return new int[] { expEmplID, expExpID };
        }
    }
}
