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
            var customer = _context.Customers.Single(c => c.Id == newRental.CustomerId);

            var movies = _context.Movies.Where(
                m => newRental.MoviesIds.Contains(m.Id));

            if(customer == null)
			{
                return NotFound();
			}

			foreach (var movie in movies)
			{
				var rental = new Rental()
				{
					Customer = customer,
					Movie = movie,
					DateRented = DateTime.Now
				};

				Movie.UpdateAvailability(rental);

				_context.Rental.Add(rental);

			}

			_context.SaveChanges();

            return Ok();
        }
    }
}