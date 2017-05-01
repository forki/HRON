namespace HRONLib
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [TrackChanges]
    [Table("EmplSalaryFringeBenefit")]
    public partial class EmplSalaryFringeBenefit : baseEntity
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int salEmplID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int salID { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime salWriteDate { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int benefitID { get; set; }

        public bool value { get; set; }

        public virtual baFringeBenefit baFringeBenefit { get; set; }

        public virtual EmplSalary EmplSalary { get; set; }

        public override int[] getKey()
        {
            return new int[] { salEmplID, salID, benefitID };
        }
    }
}
