using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace TestExperis.Data
{
    public class DBStoredProcContext : DbContext
    {
        public DBStoredProcContext()
               : base("name=TestDataContextStrConn")
        {
        }

        public virtual List<Interview> sp_CheckInterviewIntersections(Nullable<System.DateTime> sDate, Nullable<short> duration)
        {
            var sDateParameter = sDate.HasValue ?
                new ObjectParameter("SDate", sDate) :
                new ObjectParameter("SDate", typeof(System.DateTime));

            var durationParameter = duration.HasValue ?
                new ObjectParameter("Duration", duration) :
                new ObjectParameter("Duration", typeof(short));

            return this.Database.SqlQuery<Interview>("EXEC sp_CheckInterviewIntersections {0}, {1}", sDate.Value, duration.Value).ToList();
        }

        public virtual ObjectResult<Category> sp_GetCategoryList()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Category>("sp_GetCategoryList");
        }

        public virtual ObjectResult<Technology> sp_GetTechnologyList(Nullable<long> categoryId)
        {
            var categoryIdParameter = categoryId.HasValue ?
                new ObjectParameter("CategoryId", categoryId) :
                new ObjectParameter("CategoryId", typeof(long));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Technology>("sp_GetTechnologyList", categoryIdParameter);
        }
    }
}