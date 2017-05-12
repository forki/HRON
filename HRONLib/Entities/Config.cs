namespace HRONLib
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [TrackChanges]
    [Table("Config")]
    public partial class Config:baseEntity
    {
        public Config()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int confID { get; set; }

        public string target { get; set; }

        public string key { get; set; }

        public string value { get; set; }
        public decimal? valueDecimal { get; set; }
        public bool? valueBool { get; set; }

        public override int[] getKey()
        {
            return new int[] { confID };
        }
    }
}
