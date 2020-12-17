using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestExperis.Data;

namespace TestExperis.Models
{
    public class InterviewRequest
    {
        public Interview Interview { get; set; }
        public List<long> SelectedUsers { get; set; }
        public long SelectedCategory { get; set; }
        public List<InterviewMode> InterviewModes { get; set; }
    }
}