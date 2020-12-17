﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    using System.Collections.Generic;

    public partial class TestExperisEntities1 : DbContext
    {
        public TestExperisEntities1()
            : base("name=TestDataContextConStr")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    
    
        public virtual List<sp_CheckInterviewIntersections_Result> sp_CheckInterviewIntersections(Nullable<System.DateTime> sDate, Nullable<short> duration)
        {
            var sDateParameter = sDate.HasValue ?
                new ObjectParameter("SDate", sDate) :
                new ObjectParameter("SDate", typeof(System.DateTime));
    
            var durationParameter = duration.HasValue ?
                new ObjectParameter("Duration", duration) :
                new ObjectParameter("Duration", typeof(short));
    
            return this.Database.SqlQuery<sp_CheckInterviewIntersections_Result>("EXEC sp_CheckInterviewIntersections {0}, {1}", sDate.Value, duration.Value).ToList();
            //return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_CheckInterviewIntersections_Result>("sp_CheckInterviewIntersections", sDateParameter, durationParameter);
        }
    
        public virtual ObjectResult<sp_GetCategoryList_Result> sp_GetCategoryList()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetCategoryList_Result>("sp_GetCategoryList");
        }
    
        public virtual ObjectResult<sp_GetTechnologyList_Result> sp_GetTechnologyList(Nullable<long> categoryId)
        {
            var categoryIdParameter = categoryId.HasValue ?
                new ObjectParameter("CategoryId", categoryId) :
                new ObjectParameter("CategoryId", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetTechnologyList_Result>("sp_GetTechnologyList", categoryIdParameter);
        }
    }
}