using Microsoft.AspNetCore.Mvc;
using MovieBasicMvc.Models;
using System;
using System.Collections.Generic;

namespace MovieBasicMvc.Controllers
{
    public class MovieController : Controller
    {
        public static List<Movie> Movies { get; set; } = new List<Movie>()
        {
            new Movie() { Id= 1, Name= "The Batman", 
                ImgUrl= "https://m.media-amazon.com/images/M/MV5BOGE2NWUwMDItMjA4Yi00N2Y3LWJjMzEtMDJjZTMzZTdlZGE5XkEyXkFqcGdeQXVyODk4OTc3MTY@._V1_.jpg",
                StarRate=8.3 },
            new Movie() { Id= 2, Name= "The Batman2",
                ImgUrl="https://m.media-amazon.com/images/M/MV5BOGE2NWUwMDItMjA4Yi00N2Y3LWJjMzEtMDJjZTMzZTdlZGE5XkEyXkFqcGdeQXVyODk4OTc3MTY@._V1_.jpg",
                StarRate=8.2 },
            new Movie() { Id= 3, Name= "The Batman3",
                ImgUrl= "https://m.media-amazon.com/images/M/MV5BOGE2NWUwMDItMjA4Yi00N2Y3LWJjMzEtMDJjZTMzZTdlZGE5XkEyXkFqcGdeQXVyODk4OTc3MTY@._V1_.jpg",
                StarRate =8.1 },
            new Movie() { Id= 4, Name= "The Batman4",
                ImgUrl= "https://m.media-amazon.com/images/M/MV5BOGE2NWUwMDItMjA4Yi00N2Y3LWJjMzEtMDJjZTMzZTdlZGE5XkEyXkFqcGdeQXVyODk4OTc3MTY@._V1_.jpg",
                StarRate =8.3 },
            new Movie() { Id= 5, Name= "The Batman5",
                ImgUrl= "https://m.media-amazon.com/images/M/MV5BOGE2NWUwMDItMjA4Yi00N2Y3LWJjMzEtMDJjZTMzZTdlZGE5XkEyXkFqcGdeQXVyODk4OTc3MTY@._V1_.jpg",
                StarRate =7.9 },
            new Movie() { Id= 6, Name= "The Batman6",
                ImgUrl= "https://m.media-amazon.com/images/M/MV5BOGE2NWUwMDItMjA4Yi00N2Y3LWJjMzEtMDJjZTMzZTdlZGE5XkEyXkFqcGdeQXVyODk4OTc3MTY@._V1_.jpg",
                StarRate =9.4 },
            new Movie() { Id= 7, Name= "The Batman7",
                ImgUrl= "https://m.media-amazon.com/images/M/MV5BOGE2NWUwMDItMjA4Yi00N2Y3LWJjMzEtMDJjZTMzZTdlZGE5XkEyXkFqcGdeQXVyODk4OTc3MTY@._V1_.jpg",
                StarRate =8.9 },
        };

        public IActionResult Index()
        {
            return View(Movies);
        }

        public IActionResult Detail(int id)
        {

            var selectedMovie = 
                Movies.Find(m => m.Id == id);

            // ViewBag.Movie = selectedMovie;
            // TempData, ViewBag, ViewData
            return View(selectedMovie);
        }

 
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePost(Movie film)
        {
            if (!ModelState.IsValid)
            {
                return View("Create");
            }
            Random rastgele = new Random();
            film.Id = rastgele.Next(1000, 100000);
            Movies.Add(film);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var deletedMovie =
                Movies.Find(m => m.Id == id);
            Movies.Remove(deletedMovie);
            return RedirectToAction("Index");
        }
    }
}
