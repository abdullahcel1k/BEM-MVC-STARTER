using MovieBasicMvc.Models;
using System.Collections.Generic;

namespace MovieBasicMvc.ViewModels
{
    public class CategoryAndListViewModel
    {
        public Category Category { get; set; }
        public List<Category> Categories { get; set; }
    }
}
