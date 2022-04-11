using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBasicMvc.ViewModels
{
    public class SaveCommentViewModel
    {
        public int MovieId { get; set; }
        public string Comment { get; set; }
    }
}
