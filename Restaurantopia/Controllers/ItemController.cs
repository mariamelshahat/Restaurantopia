using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurantopia.Entities.Models;
using Restaurantopia.InterFaces;

namespace Restaurantopia.Controllers
{
    public class ItemController : Controller
    {
        private IGenericRepository<Item> _Rep_Item;
        private IGenericRepository<Category> _Rep_Category;

        public ItemController(IGenericRepository<Item> repository,IGenericRepository<Category> Rep)
        {
            _Rep_Item = repository;
            _Rep_Category = Rep;
        }

        public async Task<ActionResult> ListOfItems()
        {
            var Items = await _Rep_Item.GetAllAsync();
            ViewBag.C_s = await _Rep_Category.GetAllAsync();
            return View(Items);
        }

        public async Task<ActionResult> Details(int id)
        {
            Item item = await _Rep_Item.GetByIdAsync(id);
            ViewBag.C_s = await _Rep_Category.GetAllAsync();

            return View(item);
        }


        public async Task<ActionResult> Create()
        {
            ViewBag.C_s = await _Rep_Category.GetAllAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Item item)
        {
            try
            {
                await _Rep_Item.AddAsync(item);
                return RedirectToAction(nameof(ListOfItems));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.C_s = await _Rep_Category.GetAllAsync();
            if (id == null)
            {
                return NotFound();
            }
            Item item = await _Rep_Item.GetByIdAsync(id);
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
                await _Rep_Item.UpdateAsync(item);
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
            Item D_item = await _Rep_Item.GetByIdAsync(id);
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

                await _Rep_Item.DeleteAsync(id);
                return RedirectToAction(nameof(ListOfItems));
            }
            catch
            {
                return View(item);
            }
        }

    }
}
