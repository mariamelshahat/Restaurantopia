using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurantopia.Entities.Models;
using Restaurantopia.InterFaces;
using Restaurantopia.Repositories;

namespace Restaurantopia.Controllers
{
    // Controller for managing Items (Loay Elsayed)
    public class ItemController : Controller
    {
        private readonly IGenericRepository<Item> _Rep_Item;
        private readonly IGenericRepository<Category> _Rep_Category;
        private readonly IGenericRepository<OrderDetails> _Rep_Order;
        private readonly IWebHostEnvironment _environment;
        private readonly IUploadFile _uploadFile;

        public ItemController ( IGenericRepository<Item> repository, IGenericRepository<Category> categoryRep,
            IWebHostEnvironment environment, IUploadFile uploadFile, IGenericRepository<OrderDetails> rep_Order )
        {
            _Rep_Item = repository;
            _Rep_Category = categoryRep;
            _environment = environment;
            _uploadFile = uploadFile;
            _Rep_Order = rep_Order;
        }

        // GET: Menu (Displays items with optional category and search filters)
        public async Task<ActionResult> Menu ( int? categoryId, string searchQuery )
        {
            IEnumerable<Item> Items = await _Rep_Item.GetAllAsync ( includes: new[] { "Category" } );

            ViewBag.SelectedCategoryId = categoryId;
            ViewBag.SelectedCategoryName = null;

            if (categoryId.HasValue && categoryId.Value != 0)
            {
                Items = Items.Where ( item => item.CategoryId == categoryId.Value );
                var selectedCategory = await _Rep_Category.GetByIdAsync ( categoryId.Value );
                ViewBag.SelectedCategoryName = selectedCategory?.CategoryName;
            }
            else
            {
                ViewBag.SelectedCategoryName = "All Categories";
            }

            if (!string.IsNullOrWhiteSpace ( searchQuery ))
            {
                Items = Items.Where ( item => item.ItemTitle.ToLower ().Contains ( searchQuery.ToLower () ) );
                ViewBag.SearchQuery = searchQuery;
            }

            ViewBag.C_s = await _Rep_Category.GetAllAsync ();
            return View ( Items );
        }

        // GET: Details of an item
        public async Task<ActionResult> Details ( int id )
        {
            Item item = await _Rep_Item.GetByIdAsync ( id );
            if (item == null)
            {
                return NotFound ();
            }

            ViewBag.C_s = await _Rep_Category.GetAllAsync ();
            return View ( item );
        }

        // GET: Create a new item
        public async Task<ActionResult> Create ()
        {
            var categories = await _Rep_Category.GetAllAsync ();
            var item = new Item () { categoryList = categories.ToList () };
            return View ( item );
        }

        // POST: Create a new item
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create ( Item item )
        {
            try
            {
                if (item.ImageFile != null)
                {
                    string filePath = await _uploadFile.UploadFileAsync ( "\\Images\\ItemImage\\", item.ImageFile );
                    item.ItemImage = filePath;
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

        // GET: Edit an item
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
                if (item.ImageFile != null)
                {
                    // If a new image is uploaded, replace the old one
                    string filePath = await _uploadFile.UploadFileAsync ( "\\Images\\ItemImage\\", item.ImageFile );
                    item.ItemImage = filePath;
                }

                // If no new image is uploaded, the existing image path (item.ItemImage) remains the same

                await _Rep_Item.UpdateAsync ( item );

                return RedirectToAction ( nameof ( Menu ) );
            }
            catch (Exception ex)
            {
                ModelState.AddModelError ( string.Empty, ex.Message );
                ViewBag.C_s = await _Rep_Category.GetAllAsync ();
                return View ( item );
            }
        }


        // GET: Delete confirmation view for an item
        public async Task<ActionResult> Delete ( int id )
        {
            var item = await _Rep_Item.GetByIdAsync ( id );
            if (item == null)
            {
                return NotFound ();
            }

            ViewBag.C_s = await _Rep_Category.GetAllAsync ();
            return View ( item );
        }

        // POST: Delete an item
        [HttpPost, ActionName ( "Delete" )]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed ( int id )
        {
            try
            {
                var item = await _Rep_Item.GetByIdAsync ( id );
                if (item == null)
                {
                    return NotFound ();
                }

                await _Rep_Item.DeleteAsync ( id );
                return RedirectToAction ( nameof ( Menu ) );
            }
            catch (Exception ex)
            {
                ModelState.AddModelError ( string.Empty, ex.Message );
                var item = await _Rep_Item.GetByIdAsync ( id );
                return View ( item );
            }
        }

        // GET: Place an order for an item
        public async Task<ActionResult> Order ( int id )
        {
            var item = await _Rep_Item.GetByIdAsync ( id );
            if (item == null)
            {
                return NotFound ();
            }

            return View ( item );
        }

        // POST: Place an order
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
                int customerId = 1; // Assuming a logged-in customer ID

                OrderDetails orderDetails = new OrderDetails
                {
                    ItemId = item.Id,
                    CustomerId = customerId,
                    Quantity = item.Quantity,
                    Total = (int)item.ItemPrice * item.Quantity,
                    Date = DateTime.Now
                };

                await _Rep_Order.AddAsync ( orderDetails );
                return RedirectToAction ( "Index", "OrderDetails" );
            }
            catch (Exception ex)
            {
                ModelState.AddModelError ( string.Empty, ex.Message );
                return View ( item );
            }
        }
    }
}
