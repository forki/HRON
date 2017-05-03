namespace HRONLib
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [TrackChanges]
    public partial class baCompany : baseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public baCompany()
        {
            baCompanyRights = new ObservableCollection<baCompanyRights>();
            baWorkPlace = new ObservableCollection<baWorkPlace>();
        }

        [Key]
        public int compID { get; set; }

        public string compDescription { get; set; }

        public short? compSocieta { get; set; }

        public virtual ICollection<baCompanyRights> baCompanyRights { get; set; }

        public virtual ICollection<baWorkPlace> baWorkPlace { get; set; }

        public override int[] getKey()
        {
            return new int[] { compID };
        }
    }
}
