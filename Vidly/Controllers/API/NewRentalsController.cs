using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Vidly.Dtos;

namespace Vidly.Controllers.API
{
    public class NewRentalsController : ApiController
    {
        // GET: NewRentals
        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {
            throw new NotImplementedException();
        }
    }
}