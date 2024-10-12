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

        // GET: ReviewController
        public async Task<ActionResult> Index ()
        {
            return View ();
        }

        // GET: ReviewController/Details/5
        public ActionResult Details ( int id )
        {
            return View ();
        }

        // GET: ReviewController/Create
        public ActionResult Create ()
        {
            return View ();
        }

        // POST: ReviewController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create ( IFormCollection collection )
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

        // GET: ReviewController/Edit/5
        public ActionResult Edit ( int id )
        {
            return View ();
        }

        // POST: ReviewController/Edit/5
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

        // GET: ReviewController/Delete/5
        public ActionResult Delete ( int id )
        {
            return View ();
        }

        // POST: ReviewController/Delete/5
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
        public async Task<IActionResult> Add ()
        {
            return View ();
        }
        [HttpPost]
        public async Task<IActionResult> Add ( Review review )
        {


            var i = await _reviewrepository.AddAsync ( review );
            return RedirectToAction ( "Index" );
        }
    }
}
