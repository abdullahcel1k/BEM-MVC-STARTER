using MovieBasicMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBasicMvc.ViewModels
{
    public class SimiliarViewModel
    {
        public string Title { get; set; }
        public List<Movie>  Movies { get; set; }
    }
}
