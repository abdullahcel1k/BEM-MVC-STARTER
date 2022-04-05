using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBasicMvc.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "Lütfen en fazla 250 karakter giriniz.")]
        public string Name { get; set; }
        [Required]
        public string ImgUrl { get; set; }
        public double StarRate { get; set; }
    }
}
