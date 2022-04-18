using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBasicMvc.ViewModels
{
    public class SaveMovieViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public double StarRate { get; set; }
        public List<int> CategoryIds { get; set; }
    }
}
