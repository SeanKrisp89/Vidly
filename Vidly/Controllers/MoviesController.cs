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
            var movies = _context.Movies.Include(m => m.Genre).ToList();

            return View(movies);
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

        public ActionResult New()
		{
            var genres = new MovieFormViewModel //We need to pass in the MovieFormViewModel because we're loading in two types (Movie and Genre) which means at some point we'll need to eager load from Db...
            {
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", genres);
		}

        [HttpPost]
        public ActionResult Save(Movie movie)
		{
            //determine if new movie or existing movie
            if(movie.Id == 0)
			{
                _context.Movies.Add(movie);
			}
			else
			{
                //grab movie from Db first
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);

                //update movieInDb values to that of movie argument passed in
                movieInDb.Name = movie.Name;
                movieInDb.Genre = movie.Genre;
                movieInDb.DateAdded = movie.DateAdded;
                movieInDb.NumberInStock = movie.NumberInStock;
			}

            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
		}

        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie()
            {
                Name = "Shrek!"
            };

            var customers = new List<Customer>
            {
                new Customer {Name = "Sean"},
                new Customer {Name = "Megan"}
            };

            var viewModel = new RandomMovieViewModel()
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);

            //return RedirectToAction("Index", "Home", new { page = 1, sortBy = "Name" });
        }

        public ActionResult Edit(int id)
        {
            return Content("id=" + id);
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