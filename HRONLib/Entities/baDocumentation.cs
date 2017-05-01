namespace HRONLib
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [TrackChanges]
    [Table("baDocumentation")]
    public partial class baDocumentation : baseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public baDocumentation()
        {
        }

        [Key]
        public int documentationID { get; set; }

        public string documentationDescription { get; set; }

        public bool documentationCreateOnOnboarding { get; set; }

        public byte[] documentationDocument { get; set; }

        public string documentationDocumentName { get; set; }

        public int documentationExpireTime { get; set; }

        public override int[] getKey()
        {
            return new int[] { documentationID };
        }
    }
}
