using MovieBasicMvc.Models;
using System.Collections.Generic;

namespace MovieBasicMvc.ViewModels
{
    public struct CategoryAndListViewModel
    {
        public Category Category { get; set; }
        public List<Category> Categories { get; set; }
    }
}
