using System;
using System.Linq;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public NewRentalsController()
		{
            _context = new ApplicationDbContext();
		}

		protected override void Dispose(bool disposing)
		{
            _context.Dispose();
		}

		// POST: /api/NewRentals/
		[HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {
			//Edge case 1: No MovieIds - defensive programming
			if (newRental.MoviesIds.Count == 0)
			{
				return BadRequest("No movieIds have been given.");
			}

			var customer = _context.Customers.SingleOrDefault(c => c.Id == newRental.CustomerId); //made this SingleOrDefault() instead of Single() so it can handle null values

            var movies = _context.Movies.Where(
                m => newRental.MoviesIds.Contains(m.Id)).ToList(); //We added ToList() here because previously our movies object was an IQueryable, and we're looking for a (generic) list.

			//Edge case 2: CustomerId is invalid - defensive programming
			if (customer == null)
			{
				return BadRequest("CustomerId is not valid.");
			}

			//Edge case 3: 1 or more movieIds are invalid - defensive programming
			if (newRental.MoviesIds.Count != movies.Count)
			{
				return BadRequest("One or more movieIds are invalid.");
			}

			foreach (var movie in movies)
			{
				//Edge case 4: a movie is not available - defensive programming
				if(movie.NumberAvailable == 0)
				{
					return BadRequest("Movie is not available.");
				}

				var rental = new Rental()
				{
					Customer = customer,
					Movie = movie,
					DateRented = DateTime.Now
				};

				movie.UpdateAvailability(rental);

				_context.Rental.Add(rental);

			}

			_context.SaveChanges();

            return Ok(); //the reason why we're not calling the "created" method here is because we're not creating a single new object. When using the created method, we need to supply the URL/URI to the newly created resource. But in this case we have multiple resources (multiple rental objects?)
        }
    }
}

//The drawback to defensive programming is that here, our code is very noisy/polluted. If another programmer comes in, they'll be like "what's happening here???"