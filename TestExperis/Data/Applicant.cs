namespace TestExperis.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Applicant")]
    public partial class Applicant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Applicant()
        {
            Interview = new HashSet<Interview>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        [Required]
        [StringLength(50)]
        public string username { get; set; }

        [Required]
        [StringLength(50)]
        public string email { get; set; }

        [Required]
        [StringLength(50)]
        public string address_street { get; set; }

        [Required]
        [StringLength(50)]
        public string address_suite { get; set; }

        [Required]
        [StringLength(50)]
        public string address_city { get; set; }

        [Required]
        [StringLength(50)]
        public string address_zipcode { get; set; }

        [Column(TypeName = "numeric")]
        public decimal address_geo_lat { get; set; }

        [Column(TypeName = "numeric")]
        public decimal address_geo_lng { get; set; }

        [Required]
        [StringLength(50)]
        public string phone { get; set; }

        [Required]
        [StringLength(50)]
        public string website { get; set; }

        [Required]
        [StringLength(50)]
        public string company_name { get; set; }

        [Required]
        [StringLength(50)]
        public string company_catchPhrase { get; set; }

        [Required]
        [StringLength(50)]
        public string company_bs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Interview> Interview { get; set; }
    }
}
