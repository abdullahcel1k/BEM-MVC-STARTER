using MovieBasicMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBasicMvc.ViewModels
{
    public class DetailViewModel
    {
        public Movie Movie { get; set; }
        public Category Category { get; set; }
        public Comment Comment { get; set; }
        public List<Category> Categories { get; set; }
    }
}
