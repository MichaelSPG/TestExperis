//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TextExperis.Infrastructure.Data
{
    using System;
    
    public partial class sp_CheckInterviewIntersections_Result
    {
        public long Interview_id { get; set; }
        public long Interview_Applicant_Id { get; set; }
        public long Interview_RecruiterUser_Id { get; set; }
        public System.DateTime Interview_StartDate { get; set; }
        public short Interview_Duration { get; set; }
        public long Interview_InterMode_id { get; set; }
        public long Interview_Category_Id { get; set; }
    }
}