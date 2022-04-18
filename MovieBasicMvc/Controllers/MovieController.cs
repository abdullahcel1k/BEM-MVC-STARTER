using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieBasicMvc.Data;
using MovieBasicMvc.Models;
using MovieBasicMvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                var result = _context.Movies
                     .Where(m => m.Name.Contains(key)).ToList();
                return View(result);
            }
            return View(_context.Movies.ToList());
        }

        public IActionResult Detail(int id)
        {
            DetailViewModel detailViewModel = new DetailViewModel();

            detailViewModel.Movie = _context.Movies
                                    .Where(m => m.Id == id)
                                    .Include(m => m.Comments)
                                    .FirstOrDefault();
            detailViewModel.Categories = _context.CategoryMovies
                                .Where(cm => cm.Movie.Id == id)
                                .Include(cm => cm.Category)
                                .Select(cm => cm.Category).ToList();

            return View(detailViewModel);
        }


        public IActionResult Save()
        {
            SaveMovieViewModel saveMovie = new SaveMovieViewModel();
            var categories = _context.Categories.ToList();
            ViewBag.Categories = categories;
            return View(saveMovie);
        }

        [HttpPost]
        public async Task<IActionResult> SaveMovie(SaveMovieViewModel saveMovie)
        {
            if (!ModelState.IsValid)
            {
                return View("Save");
            }

            if (saveMovie.Id != 0)
            {
                var updatedMovie = _context.Movies
                                    .SingleOrDefault(m => m.Id == saveMovie.Id);
                updatedMovie.Name = saveMovie.Name;
                updatedMovie.ImgUrl = saveMovie.ImgUrl;
                updatedMovie.StarRate = saveMovie.StarRate;

                var categoryLists = _context.CategoryMovies
                    .Where(cm => cm.Movie.Id == saveMovie.Id)
                    .Select(cm => cm.Category.Id).ToList();

                var deletedCategory = categoryLists.Except(saveMovie.CategoryIds).ToList();

                deletedCategory.ForEach(item =>
                {
                    var deletedMovieCategory = _context.CategoryMovies
                    .FirstOrDefault(c => c.Category.Id == item);
                    var delete = _context.CategoryMovies.Remove(deletedMovieCategory);
                });

                _context.SaveChanges();
                var addedCategory = saveMovie.CategoryIds.Except(categoryLists).ToList();
                addedCategory.ForEach(category =>
                {
                    CategoryMovie categoryMovie = new CategoryMovie();
                    categoryMovie.Movie = updatedMovie;
                    categoryMovie.Category = _context.Categories
                    .FirstOrDefault(c => c.Id == category);

                    _context.CategoryMovies.Add(categoryMovie);
                });

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

                saveMovie.CategoryIds.ForEach(category =>
                {
                    CategoryMovie categoryMovie = new CategoryMovie();
                    categoryMovie.Movie = movie;
                    categoryMovie.Category = _context.Categories.FirstOrDefault(c => c.Id == category);

                    _context.CategoryMovies.Add(categoryMovie);
                });
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SaveComment(Comment comment)
        {
            comment.Movie = _context.Movies.FirstOrDefault(m => m.Id == comment.Movie.Id);
            _context.Comments.Add(comment);
            _context.SaveChanges();

            return RedirectToAction("Detail", new { Id = comment.Movie.Id });
        }

        public IActionResult CommentDelete(Comment comment)
        {
            var deletedComment = _context.Comments
                .Where(c => c.Id == comment.Id).Include(c => c.Movie)
                .FirstOrDefault();
            var Id = deletedComment.Movie.Id;
            _context.Comments.Remove(deletedComment);
            _context.SaveChanges();
            return RedirectToAction("Detail", new { Id });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var deletedCategoryMovies = _context.CategoryMovies
                .Where(x => x.Movie.Id == id);
            _context.CategoryMovies.RemoveRange(deletedCategoryMovies);

            var deletedComments = _context.Comments
                .Where(x => x.Movie.Id == id);
            _context.Comments.RemoveRange(deletedComments);

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
            SaveMovieViewModel movieViewModel = new SaveMovieViewModel();
            movieViewModel.Id = updatedMovie.Id;
            movieViewModel.Name = updatedMovie.Name;
            movieViewModel.Description = updatedMovie.Description;
            movieViewModel.ImgUrl = updatedMovie.ImgUrl;
            movieViewModel.StarRate = updatedMovie.StarRate;

            movieViewModel.CategoryIds = _context.CategoryMovies
                .Where(cm => cm.Movie.Id == id)
                .Include(cm => cm.Category)
                .Select(cm => cm.Category.Id)
                .ToList();
            var categories = _context.Categories.ToList();
            ViewBag.Categories = categories;
            return View("Save", movieViewModel);
        }

    }
}
