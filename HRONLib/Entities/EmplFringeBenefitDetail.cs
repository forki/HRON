namespace HRONLib
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [TrackChanges]
    [Table("EmplFringeBenefitDetail")]
    public partial class EmplFringeBenefitDetail : baseEntity
    {
        [Key]
        [ForeignKey("Employee")]
        public int frEmployeeId;

        [Key]
        [ForeignKey("baFringeBenefit")]
        public int frBenefitId;

        [Key]
        [Column(Order = 2)]
        public DateTime frWriteDate { get; set; }

        public DateTime? frEndDate { get; set; }

        public String frVersion { get; set; }

        public String frModel { get; set; }

        public String frSerialNumber { get; set; }

        public decimal frCostMonth { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual baFringeBenefit baFringeBenefit { get; set; }

        public override int[] getKey()
        {
            return new int[] { frEmployeeId, frBenefitId };
        }
    }
}
