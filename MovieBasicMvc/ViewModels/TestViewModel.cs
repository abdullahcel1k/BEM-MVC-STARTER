using MovieBasicMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBasicMvc.ViewModels
{
    public class TestViewModel
    {
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
