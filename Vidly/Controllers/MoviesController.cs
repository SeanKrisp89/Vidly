using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        //Parameters sources can be passed in the url (/movies/edit/1), in the query string (/movies/edit?id=1), or in the form data (id=1)

        // GET: Movies
        public ActionResult Random()
        {
            var movie = new Movie()
            {
                Name = "Shrek!"
            };

            //return View(movie);

            return RedirectToAction("Index", "Home", new { page = 1, sortBy = "Name" });
        }

        public ActionResult Edit(int id)
		{
            return Content("id=" + id);
		}

        //movies
        public ActionResult Index(int? pageIndex, string sortBy)
		{
			if (!pageIndex.HasValue)
			{
                pageIndex = 1;
			}
			if (String.IsNullOrWhiteSpace(sortBy))
			{
                sortBy = "Name";
			}

            return View();
		}
    }
}