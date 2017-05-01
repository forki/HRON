namespace HRONLib
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [TrackChanges]
    public partial class EmplFunctions : baseEntity
    {
        [Key]
        public int emplFuncID { get; set; }

        public int? funcID { get; set; }

        public int? emplID { get; set; }

        public bool value { get; set; }

        public virtual baFunctions baFunctions { get; set; }

        public virtual Employee Employee { get; set; }

        public override int[] getKey()
        {
            return new int[] { emplFuncID };
        }
    }
}
