namespace HRONLib
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using PropertyChanged;
    using System.ComponentModel;

    [TrackChanges]
    [ImplementPropertyChanged]
    [Table("EmplCDC")]
    public partial class EmplCDC : baseEntity
    {
        public EmplCDC()
        {
            EmplCDCDetail = new ObservableCollection<EmplCDCDetail>();
            ((ObservableCollection<EmplCDCDetail>)EmplCDCDetail).CollectionChanged += EmplCDC_CollectionChanged;
        }

        private void EmplCDC_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Percentage = EmplCDCDetail.Where(em=>em.cdc!=null).Sum(em => em.cdcPercentage);
            foreach(EmplCDCDetail d in EmplCDCDetail)
            {
                d.PropertyChanged -= D_PropertyChanged;
                d.PropertyChanged += D_PropertyChanged;
            }
        }

        private void D_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Percentage = EmplCDCDetail.Where(em => em.cdc != null).Sum(em => em.cdcPercentage);
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int cdcEmplID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int cdcProgressive { get; set; }

        public DateTime cdcStartingDate { get; set; }

        public DateTime? cdcEndDate { get; set; }

        [ForeignKey("cdcEmplID")]
        public virtual Employee Employee { get; set; }

        [DependsOn("EmplCDCDetail")]
        public virtual ICollection<EmplCDCDetail> EmplCDCDetail { get; set; }

        private Decimal EmplCDCDetailPercentage;
        [NotMapped]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public virtual Decimal Percentage {
            get { return EmplCDCDetailPercentage; }
            private set { EmplCDCDetailPercentage = value; }
        }

        public override int[] getKey()
        {
            return new int[] { cdcEmplID, cdcProgressive };
        }
    }
}
