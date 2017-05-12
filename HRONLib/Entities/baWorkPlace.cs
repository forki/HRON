namespace HRONLib
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [TrackChanges]
    [Table("baWorkPlace")]
    public partial class baWorkPlace : baseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public baWorkPlace()
        {
            Employee = new ObservableCollection<Employee>();
        }

        [Key]
        public int workPlaceId { get; set; }

        public string workPlaceCode { get; set; }

        public string workPlaceName { get; set; }

        public string workPlaceCity { get; set; }

        public int? workPlaceCountryID { get; set; }

        public string workPlaceAddress { get; set; }

        public virtual baCountry baCountry { get; set; }

        public virtual baCompany baCompany { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employee { get; set; }

        public override int[] getKey()
        {
            return new int[] { workPlaceId };
        }
    }
}
