namespace TextExperis.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Technology")]
    public partial class Technology
    {
        [Key]
        public long Technology_Id { get; set; }

        public byte Technology_CodeId { get; set; }

        public long Technology_Category_Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Technology_Name { get; set; }

        public virtual Category Category { get; set; }
    }
}
