using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurantopia.Entities.Models;
using Restaurantopia.InterFaces;

namespace Restaurantopia.Controllers
{
    public class CategoryController : Controller
    {
        private IGenericRepository<Category> _repository;

        public CategoryController(IGenericRepository<Category> repository)
        {
            _repository = repository;
        }
        public async Task<ActionResult> ListOfCategories()
        {
            var Items = await _repository.GetAllAsync();
            return View(Items);
        }
        public async Task<ActionResult> Details(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            return View(category);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Category item)
        {
            try
            {
                await _repository.AddAsync(item);
                return RedirectToAction(nameof(ListOfCategories));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Category item)
        {
            try
            {
                await _repository.UpdateAsync(item);
                return RedirectToAction(nameof(ListOfCategories));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return RedirectToAction(nameof(ListOfCategories));
            }
            catch
            {
                return View();
            }
        }
    }
}
