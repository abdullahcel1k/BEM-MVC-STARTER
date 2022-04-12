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
                                    .Select(m =>
                                        new DetailViewModel() {
                                            Id = m.Id,
                                            Name = m.Name, 
                                            Description = m.Description,
                                            ImgUrl = m.ImgUrl, 
                                            Comments = m.Comments 
                                        })
                                    .FirstOrDefault();
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
            SaveMovieViewModel saveMovie = new SaveMovieViewModel();
            var categories = _context.Categories.ToList();
            ViewBag.Categories = categories;
            return View(saveMovie);
        }

        [HttpPost]
        public IActionResult SaveMovie(SaveMovieViewModel saveMovie)
        {
            if (!ModelState.IsValid)
            {
                return View("Save");
            }

            if(saveMovie.Id != 0)
            {
                var updatedMovie = _context.Movies
                                    .SingleOrDefault(m => m.Id == saveMovie.Id);
                updatedMovie.Name = saveMovie.Name;
                updatedMovie.ImgUrl = saveMovie.ImgUrl;
                updatedMovie.StarRate = saveMovie.StarRate;
                _context.Movies.Update(updatedMovie);
            }
            else
            {
                Movie movie = new Movie();
                movie.Name = saveMovie.Name;
                movie.Description = saveMovie.Description;
                movie.StarRate = saveMovie.StarRate;
                movie.ImgUrl = saveMovie.ImgUrl;

                _context.Movies.Add(movie);

                //CategoryMovie categoryMovie = new CategoryMovie();
                //categoryMovie.Movie = movie;
                //categoryMovie.Category = _context.Categories.FirstOrDefault(c => c.Id == saveMovie.CategoryId);

                //_context.CategoryMovies.Add(categoryMovie);
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
