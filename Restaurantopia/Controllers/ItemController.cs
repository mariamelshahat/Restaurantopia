using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurantopia.Entities.Models;
using Restaurantopia.InterFaces;
using Restaurantopia.Repositories;

namespace Restaurantopia.Controllers
{
    public class ItemController : Controller
    {
        private IGenericRepository<Item> _Rep_Item;
        private IGenericRepository<Category> _Rep_Category;
        private IGenericRepository<OrderDetails> _Rep_Order;
        private IWebHostEnvironment _environment;
        private IUploadFile _uploadFile;

        public ItemController ( IGenericRepository<Item> repository, IGenericRepository<Category> Rep, IWebHostEnvironment environment, IUploadFile uploadFile, IGenericRepository<OrderDetails> rep_Order )
        {
            _Rep_Item = repository;
            _Rep_Category = Rep;
            _environment = environment;
            _uploadFile = uploadFile;
            _Rep_Order = rep_Order;
        }

        public async Task<ActionResult> Menu ()
        {
            IEnumerable<Item> Items;

            Items = await _Rep_Item.GetAllAsync ( inculdes: new[] { "Category" } );

            //var Items = await _Rep_Item.GetAllAsync();

            return View ( Items );

        }

        public async Task<ActionResult> Details ( int id )
        {
            Item item = await _Rep_Item.GetByIdAsync ( id );
            ViewBag.C_s = await _Rep_Category.GetAllAsync ();

            return View ( item );
        }


        public async Task<ActionResult> Create ()
        {

            var categories = await _Rep_Category.GetAllAsync ();
            var item = new Item () { categoryList = categories.ToList () };
            return View ( item );

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create ( Item item )
        {
            try
            {

                if (item.clientFile != null)
                {
                    MemoryStream stream = new MemoryStream ();
                    item.clientFile.CopyTo ( stream );
                    item.dbimage = stream.ToArray ();
                }

                await _Rep_Item.AddAsync ( item );
                return RedirectToAction ( nameof ( Menu ) );

            }
            catch (Exception ex)
            {
                ModelState.AddModelError ( string.Empty, ex.Message );
                return View ( item );

            }
        }

        public async Task<ActionResult> Edit ( int id )
        {
            ViewBag.C_s = await _Rep_Category.GetAllAsync ();
            if (id == null)
            {
                return NotFound ();
            }
            Item item = await _Rep_Item.GetByIdAsync ( id );
            if (item == null)
            {
                return NotFound ();
            }
            return View ( item );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit ( Item item )
        {
            try
            {
                await _Rep_Item.UpdateAsync ( item );

                return RedirectToAction ( nameof ( Menu ) );


            }
            catch
            {
                return View ( item );
            }
        }

        public async Task<ActionResult> Delete ( int id )
        {
            if (id == 0)
            {
                return NotFound ();
            }
            Item D_item = await _Rep_Item.GetByIdAsync ( id );
            if (D_item == null)
            {
                return NotFound ();
            }
            return View ( D_item );
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete ( int id, Item item )
        {
            try
            {
                if (id != item.Id)
                {
                    return BadRequest ();
                }

                await _Rep_Item.DeleteAsync ( id );

                return RedirectToAction ( nameof ( Menu ) );


            }
            catch
            {
                return View ( item );
            }
        }
        public async Task<ActionResult> Order ( int id )
        {
            if (id == 0)
            {
                return NotFound ();
            }
            Item item = await _Rep_Item.GetByIdAsync ( id );
            if (item == null)
            {
                return NotFound ();
            }
            return View ( item );
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Order ( Item item )
        {
            if (item.Quantity <= 0) 
            {
                ModelState.AddModelError ( "Quantity", "Quantity must be greater than zero." );
                return View ( item );
            }

            try
            {
                // Login 
                int customerId = 1;

                OrderDetails orderDetails = new OrderDetails
                {
                    ItemId = item.Id, 
                    CustomerId = customerId,
                    Quantity = item.Quantity,
                    Total = (int)item.ItemPrice * item.Quantity, 
                    Date = DateTime.Now
                };
                await _Rep_Order.AddAsync ( orderDetails );

                
                return RedirectToAction ( "Menu" );
            }
            catch
            {
                return View ( item );
            }
        }


    }
}