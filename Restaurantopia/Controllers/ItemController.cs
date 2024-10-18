using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<ActionResult> Menu ( int? categoryId, string searchQuery )
        {
            // Get all items initially
            IEnumerable<Item> Items = await _Rep_Item.GetAllAsync ( includes: new[] { "Category" } );

            // Save selected category ID in ViewBag
            ViewBag.SelectedCategoryId = categoryId;
            ViewBag.SelectedCategoryName = null; // Initialize to null

            // Apply category filter if a categoryId is provided (i.e., not null or empty)
            if (categoryId.HasValue && categoryId.Value != 0) // Adjust this line to check against a specific value for "All Categories"
            {
                Items = Items.Where ( item => item.CategoryId == categoryId.Value );

                // Get the selected category name if filtering by a specific category
                var selectedCategory = await _Rep_Category.GetByIdAsync ( categoryId.Value );
                ViewBag.SelectedCategoryName = selectedCategory?.CategoryName; // Store the selected category name
            }
            else
            {
                // When "All Categories" is selected or no category is selected, we don't filter items
                ViewBag.SelectedCategoryName = "All Categories"; // Optional: you can set a default value
            }

            // Apply search filter if a search query is provided
            if (!string.IsNullOrWhiteSpace ( searchQuery ))
            {
                Items = Items.Where ( item => item.ItemTitle.ToLower ().Contains ( searchQuery.ToLower () ) );
                ViewBag.SearchQuery = searchQuery;
            }
            else
            {
                ViewBag.SearchQuery = null;
            }

            ViewBag.C_s = await _Rep_Category.GetAllAsync ();
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



        //public async Task<ActionResult> Edit ( int id )
        //{
        //    //// Fetching categories for ViewBag for dropdown
        //    //ViewBag.C_s = await _Rep_Category.GetAllAsync ();

        //    //// Check if id is null
        //    //if (id == 0)  // id is an integer, so we check if it's 0 instead of null
        //    //{
        //    //    return NotFound ();
        //    //}

        //    //// Get the item by id
        //    //Item item = await _Rep_Item.GetByIdAsync ( id );

        //    //// If no item is found, return not found result
        //    //if (item == null)
        //    //{
        //    //    return NotFound ();
        //    //}

        //    //// Return the view with the item
        //    //return View ( item );

        //    //var categories = await _Rep_Category.GetAllAsync();
        //    //var items = await _Rep_Item.GetByIdAsync(id);
        //    //items.categoryList = categories.ToList();
        //    //return View(items);


        //    var item = await _Rep_Item.GetByIdAsync(id);
        //        if (item == null)
        //        {
        //            return NotFound();
        //        }

        //       var categories = await _Rep_Category.GetAllAsync();
        //    item.categoryList = categories.ToList();// Make sure to initialize this list
        //        return View(item);


        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit ( Item item )
        //{
        //    ViewBag.C_s = await _Rep_Category.GetAllAsync ();  // Re-populate category list in case of validation errors

        //    if (!ModelState.IsValid)
        //    {
        //        return View ( item );
        //    }

        //    try
        //    {
        //        // Handle file upload if provided
        //        //if (item.clientFile != null && item.clientFile.Length > 0)
        //        //{
        //        //    using (var stream = new MemoryStream ())
        //        //    {
        //        //        await item.clientFile.CopyToAsync ( stream );
        //        //        // Save the image as needed, either in a database or file system
        //        //        item.dbimage = stream.ToArray ();  // or store the file path
        //        //    }
        //        //}
        //        if (item.clientFile != null)
        //        {
        //            MemoryStream stream = new MemoryStream();
        //            item.clientFile.CopyTo(stream);
        //            item.dbimage = stream.ToArray();
        //        }
        //        else
        //        {
        //            item.dbimage = item.dbimage;
        //        }
        //        // Update item in repository
        //        await _Rep_Item.UpdateAsync ( item );
        //        return RedirectToAction ( nameof ( Menu ) );
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the error (optional)
        //        ModelState.AddModelError ( "", "Unable to save changes. Please try again." );
        //        return View ( item );  // Return back to the view in case of an exception
        //    }
        //}


        public async Task<ActionResult> Delete ( int id )
        {
            if (id == 0)
            {
                return NotFound ();
            }

            ViewBag.C_s = await _Rep_Category.GetAllAsync ();
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
        //public async Task<ActionResult> Order ( int id )
        //{
        //    if (id == 0)
        //    {
        //        return NotFound ();
        //    }
        //    Item item = await _Rep_Item.GetByIdAsync ( id );
        //    if (item == null)
        //    {
        //        return NotFound ();
        //    }
        //    return View ( item );
        //}



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Order ( Item item )
        //{
        //    if (item.Quantity <= 0) 
        //    {
        //        ModelState.AddModelError ( "Quantity", "Quantity must be greater than zero." );
        //        return View ( item );
        //    }

        //    try
        //    {
        //        //.
        //        // Login 
        //        int customerId = 1;

        //        OrderDetails orderDetails = new OrderDetails
        //        {
        //            ItemId = item.Id, 
        //            CustomerId = customerId,
        //            Quantity = item.Quantity,
        //            Total = (int)item.ItemPrice * item.Quantity, 
        //            Date = DateTime.Now
        //        };
        //        await _Rep_Order.AddAsync ( orderDetails );


        //        return RedirectToAction ( "Menu" );
        //    }
        //    catch
        //    {
        //        return View ( item );
        //    }
        //}
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