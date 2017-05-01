namespace HRONLib
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [TrackChanges]
    [Table("baUser")]
    public partial class baUser : baseEntity
    {
        [Key]
        [StringLength(255)]
        public string userID { get; set; }

        public int? userGroupID { get; set; }

        public virtual baUserGroup baUserGroup { get; set; }

        public override int[] getKey()
        {
            throw new NotImplementedException();
        }
    }
}
