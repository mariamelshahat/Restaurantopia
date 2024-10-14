using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurantopia.Entities.Models;
using Restaurantopia.InterFaces;

namespace Restaurantopia.Controllers
{
    public class ReviewController : Controller
    {
        private IGenericRepository<Review> _reviewrepository;

        public ReviewController ( IGenericRepository<Review> reviewrepository )
        {
            _reviewrepository = reviewrepository;
        }
        public async Task<IActionResult> Add ()
        {
            return View ();
        }
        [HttpPost]
        public async Task<IActionResult> Add ( Review review )
        {


            var i = await _reviewrepository.AddAsync ( review );
            return RedirectToAction ( "Add" );
        }
    }
}
