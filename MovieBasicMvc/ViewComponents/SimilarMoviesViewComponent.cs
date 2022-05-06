using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieBasicMvc.Data;
using MovieBasicMvc.Models;
using MovieBasicMvc.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBasicMvc.ViewComponents
{
    public class SimilarMoviesViewComponent : ViewComponent
    {
        private readonly MovieContext _context;

        public SimilarMoviesViewComponent(MovieContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string title, int movieId)
        {
            var selectedMovieCatId = await _context.CategoryMovies
                .Where(x => x.Movie.Id == movieId)
                .Select(x => x.Category.Id)
                .FirstOrDefaultAsync();

            var similarMovies= new List<Movie>();
            if(movieId == 0)
            {
                similarMovies = await _context.Movies.ToListAsync();
            }
            else
            {
                similarMovies = await _context.CategoryMovies
                .Where(x => x.Category.Id == selectedMovieCatId)
                .Include(x => x.Movie)
                .Select(x => x.Movie)
                .ToListAsync();
            }

            var similarViewModel = new SimiliarViewModel();
            similarViewModel.Title = title;
            similarViewModel.Movies = similarMovies;
            return View(similarViewModel);
        }
    }
}
