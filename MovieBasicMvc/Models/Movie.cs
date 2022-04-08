using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBasicMvc.Models
{
    public class Movie
    {
        public Movie()
        {
            Comments = new List<Comment>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "Lütfen en fazla 250 karakter giriniz.")]
        public string Name { get; set; }
        [MaxLength(1500, ErrorMessage = "Lütfen en fazla 1500 karakter giriniz.")]
        public string Description { get; set; }
        [Required]
        public string ImgUrl { get; set; }
        public double StarRate { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
