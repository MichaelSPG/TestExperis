namespace TestExperis.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RecruiterUser")]
    public partial class RecruiterUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RecruiterUser()
        {
            Interview = new HashSet<Interview>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long RecruiterUser_Id { get; set; }

        [Required]
        [StringLength(50)]
        public string RecruiterUser_Names { get; set; }

        [StringLength(50)]
        public string RecruiterUser_Surnames { get; set; }

        [Required]
        [StringLength(50)]
        public string RecruiterUser_User { get; set; }

        [Required]
        [StringLength(50)]
        public string RecruiterUser_Password { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Interview> Interview { get; set; }
    }
}
