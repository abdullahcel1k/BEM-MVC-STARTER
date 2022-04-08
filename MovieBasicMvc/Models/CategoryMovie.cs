using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBasicMvc.Models
{
    public class CategoryMovie
    {
        public int Id { get; set; }
        public Category Category { get; set; }
        public Movie Movie { get; set; }
    }
}
