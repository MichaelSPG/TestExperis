namespace TestExperis.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using TestExperis.Validations;

    [Table("Interview")]
    public partial class Interview
    {
        [Key]
        public long Interview_id { get; set; }

        public long Interview_Applicant_Id { get; set; }

        public long Interview_RecruiterUser_Id { get; set; }
        [Display(Name ="Fecha entrevista")]
        [Required]
        [DataType(DataType.DateTime)]
        [TestDateValidation("La fecha de la entrevista debe ser mayor a la fecha actual")]
        public DateTime Interview_StartDate { get; set; }
        [Display(Name = "Duración entrevista")]
        public short Interview_Duration { get; set; }

        public long Interview_InterMode_id { get; set; }

        public long Interview_Category_Id { get; set; }

        public virtual Applicant Applicant { get; set; }

        public virtual Category Category { get; set; }

        public virtual InterviewMode InterviewMode { get; set; }

        public virtual RecruiterUser RecruiterUser { get; set; }
    }
}
