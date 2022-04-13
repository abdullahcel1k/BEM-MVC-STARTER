using Microsoft.AspNetCore.Mvc;
using MovieBasicMvc.Data;
using MovieBasicMvc.Models;
using MovieBasicMvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBasicMvc.Controllers
{
    public class CategoryController : Controller
    {
        private readonly MovieContext _context;

        public CategoryController(MovieContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            CategoryAndListViewModel categoryAndList = new CategoryAndListViewModel();
            categoryAndList.Category = new Category();
            categoryAndList.Categories = _context.Categories.ToList();
            
            return View(categoryAndList);
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
                
            }
            _context.Categories.Add(category);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int CategoryId)
        {
            var deletedCategory = _context.Categories.First(c => c.Id == CategoryId);
            if(deletedCategory != null)
            {
                _context.Categories.Remove(deletedCategory);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
