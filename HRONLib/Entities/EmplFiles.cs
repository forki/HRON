namespace HRONLib
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [TrackChanges]
    [Table("EmplFiles")]
    public partial class EmplFiles : baseEntity
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmplID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Progressive { get; set; }

        public byte[] FileContent { get; set; }

        public int? emplDocumentationId { get; set; }

        [StringLength(255)]
        public string FileName { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual EmplDocumentation EmplDocumentation { get; set; }

        public override int[] getKey()
        {
            return new int[] { EmplID, Progressive };
        }
    }
}
