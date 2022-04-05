using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBasicMvc.Controllers
{
    public class CategoryController : Controller
    {
        public string Index()
        {
            return "Kategorilere hoşgeldiniz!";
        }
    }
}
