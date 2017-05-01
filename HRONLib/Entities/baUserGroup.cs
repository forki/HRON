namespace HRONLib
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [TrackChanges]
    [Table("baUserGroup")]
    public partial class baUserGroup : baseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public baUserGroup()
        {
            baUser = new ObservableCollection<baUser>();
        }

        [Key]
        public int userGroup { get; set; }

        public string userGroupDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<baUser> baUser { get; set; }

        public override int[] getKey()
        {
            return new int[] { userGroup };
        }
    }
}
