using Microsoft.AspNetCore.Mvc;
using MovieBasicMvc.Data;
using MovieBasicMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieBasicMvc.Controllers
{
    public class MovieController : Controller
    {
        private readonly MovieContext _context;

        public MovieController(MovieContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Movies.ToList());
        }

        public IActionResult Detail(int id)
        {

            var selectedMovie = _context.Movies
                                    .SingleOrDefault(m => m.Id == id);


            // ViewBag.Movie = selectedMovie;
            // TempData, ViewBag, ViewData
            return View(selectedMovie);
        }


        public IActionResult Save()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveMovie(Movie film)
        {
            if (!ModelState.IsValid)
            {
                return View("Create");
            }

            if(film.Id != 0)
            {
                var updatedMovie = _context.Movies
                                    .SingleOrDefault(m => m.Id == film.Id);
                updatedMovie.Name = film.Name;
                updatedMovie.ImgUrl = film.ImgUrl;
                updatedMovie.StarRate = film.StarRate;
                _context.Movies.Update(updatedMovie);
            }
            else
            {
                _context.Movies.Add(film);
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var deletedMovie = _context.Movies
                                    .SingleOrDefault(m => m.Id == id);
            _context.Movies.Remove(deletedMovie);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var updatedMovie = _context.Movies
                                    .SingleOrDefault(m => m.Id == id);
            return View("Save",updatedMovie);
        }

    }
}
