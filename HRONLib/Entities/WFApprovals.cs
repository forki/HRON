namespace HRONLib
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WFApprovals
    {
        [Key]
        [Column(Order = 0)]
        public Guid approverWFID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(150)]
        public string approverActivityID { get; set; }

        [StringLength(150)]
        public string mail { get; set; }

        public EmplWorkflows Parent { get; set; }

        public string subject { get; set; }

        [Column(TypeName = "text")]
        public string body { get; set; }

        public DateTime starttime { get; set; }

        public DateTime? endtime { get; set; }

        [Column(TypeName = "text")]
        public string note { get; set; }

        public bool? approved { get; set; }
    }
}
