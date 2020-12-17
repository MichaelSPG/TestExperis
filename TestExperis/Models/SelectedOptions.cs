using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestExperis.Data;

namespace TestExperis.Models
{
    public class SelectedOptions
    {
        public Category Category { get; set; }
        public List<long> Technologies { get; set; }
        public User User { get; set; }
    }
}