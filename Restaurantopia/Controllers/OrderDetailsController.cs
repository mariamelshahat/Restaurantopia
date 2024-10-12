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
        private IGenericRepository<Item> _itemrepository;

        public OrderDetailsController ( IGenericRepository<OrderDetails> orderrepository, IGenericRepository<Item> itemrepository )
        {
            _orderrepository = orderrepository;
            _itemrepository = itemrepository;
        }

        public async Task<ActionResult> Index ()
        {
            var orderDetailsList = await _orderrepository.GetAllAsync ();
            return View ( orderDetailsList );
        }

        // GET: OrderDetailsController/Details/5
        public ActionResult Details ( int id )
        {
            return View ();
        }

        // GET: OrderDetailsController/Create
        public ActionResult Create ()
        {
            return View ();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create ( OrderDetails item )
        {
            try
            {
                await _orderrepository.AddAsync ( item );
                return RedirectToAction ( nameof ( Index ) );
            }
            catch
            {
                return View ();
            }
        }
        // GET: OrderDetailsController/Edit/5
        public ActionResult Edit ( int id )
        {
            return View ();
        }

        // POST: OrderDetailsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit ( int id, IFormCollection collection )
        {
            try
            {
                return RedirectToAction ( nameof ( Index ) );
            }
            catch
            {
                return View ();
            }
        }

        // GET: OrderDetailsController/Delete/5
        public ActionResult Delete ( int id )
        {
            return View ();
        }

        // POST: OrderDetailsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete ( int id, IFormCollection collection )
        {
            try
            {
                return RedirectToAction ( nameof ( Index ) );
            }
            catch
            {
                return View ();
            }
        }
        


    }
}
