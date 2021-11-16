using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        //Create _context property
        private ApplicationDbContext _context;

		//Initialize context prop
		public MoviesController()
		{
            _context = new ApplicationDbContext();
		}

		protected override void Dispose(bool disposing)
		{
            _context.Dispose();
		}

		//Parameters sources can be passed in the url (/movies/edit/1), in the query string (/movies/edit?id=1), or in the form data (id=1)

		// GET: /movies
		public ActionResult Index()
		{
			//var movies = new List<Movie> //our initial hardcoding of movies prior to Db
			//{
			//   new Movie{ Name = "Troy", Id = 1},
			//   new Movie{ Name = "Star Wars V", Id = 2}
			//};

			//Reminder, since Genre is a type associated with our Movies class, and EF by default only loads Movie, we need to eager load genre with Movie by using "Include" method
			//var movies = _context.Movies.Include(m => m.Genre).ToList();

			if (User.IsInRole(RoleName.CanManageMovies)) //"User" gives us access to the current user
            {
                return View("List");
			}
			else
			{
                return View("ReadOnlyList");
			}

            //return View(/*movies*/);
		}

        public ActionResult Details(int id)
		{
            //var movies = new List<Movie> //hardcoded from prior to Db
            //{
            //   new Movie{ Name = "Troy", Id = 1},
            //   new Movie{ Name = "Star Wars V", Id = 2}
            //};

            //var movie = movies.Find(m => m.Id == id);

            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            return View(movie);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
		{
            var genres = new MovieFormViewModel
            {
                //Movie was erroring out because the Id wasn't defaulting to 0 (I think it was null). I don't even need to do new Movie{Id = 0} because when you pass 0 args the default constructor will initialize Id to 0 anyway.
                Genres = _context.Genres.ToList()
            };

            ViewBag.Title = "New Movie";

            return View("MovieForm", genres);
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
		{
			if (!ModelState.IsValid)
			{
                var viewModel = new MovieFormViewModel(movie) //we want to return the same movie object back to the view if modelstate is invalid
                {
                    Genres = _context.Genres.ToList()
                };

                return View("MovieForm", viewModel);
			}
            //determine if new movie or existing movie
            if(movie.Id == 0)
			{
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
			}
			else
			{
                //grab movie from Db first
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);

                //update movieInDb values to that of movie argument passed in
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.NumberInStock = movie.NumberInStock;
			}

            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
		}

        public ActionResult Edit(int id)
        {
            //get movie from db
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if(movie == null)
			{
                return HttpNotFound();
			}

            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = _context.Genres.ToList() //However here, we wanna grab all possible genre types/names for populating dropdown. Check out comment on LabelFor/DropDownFor in MovieForm.cshtml
            };

            ViewBag.Title = "Edit Movie";

            return View("MovieForm", viewModel);
        }

        //movies
        //public ActionResult Index(int? pageIndex, string sortBy) original index method 
        //{
        //    if (!pageIndex.HasValue)
        //    {
        //        pageIndex = 1;
        //    }
        //    if (String.IsNullOrWhiteSpace(sortBy))
        //    {
        //        sortBy = "Name";
        //    }

        //    return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        //}

        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")] //Google ASP.NET MVC Attribute Route COntraints
        public ActionResult ByReleaseDate(int year, int month)
		{
            return Content(year + "/" + month);
		}
    }
}