namespace HRONLib
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [TrackChanges]
    [Table("baCountry")]
    public partial class baCountry : baseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public baCountry()
        {
            baWorkPlace = new ObservableCollection<baWorkPlace>();
            Employee = new ObservableCollection<Employee>();
        }

        [Key]
        public int countryID { get; set; }

        public string countryISOCode { get; set; }

        public string countryTitle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<baWorkPlace> baWorkPlace { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employee { get; set; }

        public override int[] getKey()
        {
            return new int[] { countryID };
        }
    }
}
