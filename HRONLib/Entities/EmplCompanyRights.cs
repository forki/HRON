namespace HRONLib
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [TrackChanges]
    public partial class EmplCompanyRights : baseEntity
    {
        [Key]
        public int rightID { get; set; }

        public int? comprightID { get; set; }

        public int? emplID { get; set; }

        public bool value { get; set; }

        public virtual baCompanyRights baCompanyRights { get; set; }

        public virtual Employee Employee { get; set; }

        public override int[] getKey()
        {
            return new int[] { rightID };
        }
    }
}
