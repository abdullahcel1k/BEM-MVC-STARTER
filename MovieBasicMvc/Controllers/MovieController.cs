using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieBasicMvc.Data;
using MovieBasicMvc.Models;
using MovieBasicMvc.ViewModels;
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

        public IActionResult Index(string key)
        {
            if (!String.IsNullOrEmpty(key))
            {
                // select * from Movies where Name like "%key%"
               var result =  _context.Movies
                    .Where(m => m.Name.Contains(key)).ToList();
                return View(result);
            }
            return View(_context.Movies.ToList());
        }

        public IActionResult Detail(int id)
        {

            //var selectedMovie = _context.Movies
            //                        .SingleOrDefault(m => m.Id == id);

            var movieAndComments = _context.Movies
                                    .Where(m => m.Id == id)
                                    .Include(m => m.Comments)
                                    .Select(m => new TestViewModel() { Name = m.Name, ImgUrl = m.ImgUrl, Comments = m.Comments }).FirstOrDefault();
            // ViewBag.Movie = selectedMovie;
            // TempData, ViewBag, ViewData
            //ViewBag.Movie = selectedMovie;
            //ViewBag.Comments = _context.Comments.Where(c => c.Movie.Id == id).ToList();
            //TestViewModel testViewModel = new TestViewModel();
            //testViewModel = movieAndComments;
            ViewBag.movieAndComments = movieAndComments;
            return View();
            }


        public IActionResult Save()
        {
            Movie movie = new Movie();
            return View(movie);
        }

        [HttpPost]
        public IActionResult SaveMovie(Movie film)
        {
            if (!ModelState.IsValid)
            {
                return View("Save");
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
        public IActionResult SaveComment(SaveCommentViewModel saveComment)
        {
            Comment comment = new Comment();
            comment.Text = saveComment.Comment;
            comment.Movie = _context.Movies.SingleOrDefault(m => m.Id == saveComment.MovieId);

            _context.Comments.Add(comment);
            _context.SaveChanges();

            return RedirectToAction("Detail", new { Id = saveComment.MovieId });
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
