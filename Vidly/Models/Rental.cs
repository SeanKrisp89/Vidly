using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;


namespace Vidly.Models
{
	public class Rental
	{
		public byte Id { get; set; }
		public DateTime DateRented { get; set; }
		public DateTime DateReturned { get; set; }
		public int CustomerId { get; set; }
		public Customer Customer { get; set; }
		public int MovieId { get; set; }
		public Movie Movie { get; set; }

		//Need to get this question answered... when do we need the ClassId prop vs just the Class prop vs. both? I.e. CustomerId and/or Customer?
	}
}