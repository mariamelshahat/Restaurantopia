using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Core.Types;
using Restaurantopia.Entities.Models;
using Restaurantopia.InterFaces;

namespace Restaurantopia.Controllers
{
    public class OrderDetailsController : Controller
    {
        // GET: OrderDetailsController
        private IGenericRepository<OrderDetails> _orderrepository;
        private IGenericRepository<Item> _Rep_Item;


        public OrderDetailsController(IGenericRepository<OrderDetails> orderrepository, IGenericRepository<Item> rep_Item)
        {
            _orderrepository = orderrepository;
            _Rep_Item = rep_Item;
        }

        public async Task<ActionResult> Index()
        {
            var orderDetailsList = await _orderrepository.GetAllAsync();
            ViewBag.Orders = _Rep_Item.GetAllAsync();
            return View(orderDetailsList);
        }

        // GET: OrderDetailsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var orderDetailsList = await _orderrepository.GetByIdAsync(id);
            ViewBag.Orders = _Rep_Item.GetAllAsync();
            return View(orderDetailsList);
        }

        // GET: OrderDetailsController/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OrderDetails item)
        {
            try
            {
                await _orderrepository.AddAsync(item);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: OrderDetailsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderDetailsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderDetailsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            OrderDetails D_item = await _orderrepository.GetByIdAsync(id);
            ViewBag.Orders = _Rep_Item.GetAllAsync();
            if (D_item == null)
            {
                return NotFound();
            }
            return View(D_item);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, OrderDetails order)
        {
            try
            {
                if (id != order.Id)
                {
                    return BadRequest();
                }

                await _orderrepository.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(order);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add(int Id)
        {
            var i = await _orderrepository.GetByIdAsync(Id);
            if (i == null)
            {
                return NotFound();
            }
            await _orderrepository.UpdateAsync(i);
            return RedirectToAction("Index");
        }



    }
}