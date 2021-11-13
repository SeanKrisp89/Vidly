using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
	public class Rental
	{
		public int Id { get; set; }
		[Required]
		public Customer Customer { get; set; }
		[Required]
		public Movie Movie { get; set; }
		public DateTime DateRented { get; set; }
		public DateTime? DateReturned { get; set; }


		//Need to get this question answered... when do we need the ClassId prop vs just the Class prop vs. both? I.e. CustomerId and/or Customer?

		//ANSWER? So here's how I THINK it works. If you provide a field call ClassId, entity framework will use that as the primary key/column for the table/class, and it will not turn the navigation properties into table columns.
		//However, if you provide navigation properties such as Customer Customer or Movie Movie without CustomerId or MovieId, entity framework will create columns (PKs) for these properties as such: Class_Id (as opposed to ClassId if you provided a ClassId property)
		//Ultimately, I do not think it matters which route you go, just know that it would be accessed (I think) as rental.CustomerId vs rental.Customer.Id.....?
		//I'm gonna try one of each....
	}
}