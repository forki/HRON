namespace HRONLib
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [TrackChanges]
    [Table("baExpiration")]
    public partial class baExpiration : baseEntity
    {
        public baExpiration()
        {
        }

        [Key]
        public int expID { get; set; }

        public string expDescription { get; set; }

        public TimeSpan expExpiration { get; set; }

        public baDocumentation expDocument { get; set; }

        public override int[] getKey()
        {
            return new int[] { expID };
        }
    }
}
