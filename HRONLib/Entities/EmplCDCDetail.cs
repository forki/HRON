namespace HRONLib
{
    using PropertyChanged;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [TrackChanges]
    [Table("EmplCDCDetail")]
    public partial class EmplCDCDetail : baseEntity, INotifyPropertyChanged
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmplCDCDetail()
        {
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public EmplCDC EmplCDC { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int cdcDetailRow { get; set; }

        private baCDC _cdc;
        public baCDC cdc {
            get { return _cdc; }
            set { _cdc = value; OnPropertyChanged("cdc"); }
        }

        private Decimal _cdcPercentage;
        public Decimal cdcPercentage
        {
            get { return _cdcPercentage; }
            set { _cdcPercentage = value;  OnPropertyChanged("cdcPercentage"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override int[] getKey()
        {
            return new int[] { EmplCDC.cdcEmplID, EmplCDC.cdcProgressive, cdcDetailRow };
        }
        public virtual void OnPropertyChanged(string propertyName)
        {
            var propertyChanged = PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
