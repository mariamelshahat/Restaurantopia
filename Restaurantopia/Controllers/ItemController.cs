using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurantopia.Entities.Models;
using Restaurantopia.InterFaces;

namespace Restaurantopia.Controllers
{
    public class ItemController : Controller
    {
        private IGenericRepository<Item> _repository;

        public ItemController(IGenericRepository<Item> repository)
        {
            _repository = repository;
        }

        public async Task<ActionResult> ListOfItems()
        {
            var Items = await _repository.GetAllAsync();
            return View(Items);
        }

        public async Task<ActionResult> Details(int id)
        {
            Item item = await _repository.GetByIdAsync(id);
            return View(item);
        }


        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Item item)
        {
            try
            {
                await _repository.AddAsync(item);
                return RedirectToAction(nameof(ListOfItems));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Item item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Item item)
        {
            try
            {
                await _repository.UpdateAsync(item);
                return RedirectToAction(nameof(ListOfItems));
            }
            catch
            {
                return View(item);
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            Item D_item = await _repository.GetByIdAsync(id);
            if (D_item == null)
            {
                return NotFound();
            }
            return View(D_item);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Item item)
        {
            try
            {
                if (id != item.Id)
                {
                    return BadRequest();
                }

                await _repository.DeleteAsync(id);
                return RedirectToAction(nameof(ListOfItems));
            }
            catch
            {
                return View(item);
            }
        }

    }
}
