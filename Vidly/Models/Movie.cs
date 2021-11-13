using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
	public class Movie
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public Genre Genre { get; set; }

		[Required]
		[Display(Name="Genre")]
		public byte GenreId { get; set; }

		[Required]
		[Display(Name="Release Date")]
		public DateTime ReleaseDate { get; set; }

		[Display(Name="Date Added")]
		public DateTime DateAdded { get; set; }

		[Display(Name="Number in Stock")]
		[Range(1, 10)]
		[Required]
		public int NumberInStock { get; set; }

		[Required]
		public int NumberAvailable { get; set; }

		public static void UpdateAvailability(Rental rental)
		{
			if(rental.DateReturned == null)
			{
				rental.Movie.NumberAvailable -= 1;
			}
			else
			{
				rental.Movie.NumberAvailable += 1;
			}
		}
	}
}