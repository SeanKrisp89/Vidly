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
		[Required]
		public Genre Genre { get; set; }
		public DateTime ReleaseDate { get; set; }
		[Required]
		[Display(Name="Date Added")]
		public DateTime DateAdded { get; set; }
		[Display(Name="Number in Stock")]
		[Required]
		public int NumberInStock { get; set; }
	}
}