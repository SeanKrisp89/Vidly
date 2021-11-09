using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;
using System.ComponentModel.DataAnnotations;

namespace Vidly.ViewModels
{
	public class MovieFormViewModel
	{
		public IEnumerable<Genre> Genres { get; set; }

		//public Movie Movie { get; set; }

		//Still need to find out why Mosh put all this in his MovieFormViewModel when mine works fine with just the movie property above. As long as you initiate Movie.Id to 0 where you need to, or pass an existing movie therefore with an existing Id, then you're fine.
		//ANSWER TO ABOVE QUESTION: So the problem was that you either needed to pass a Movie property (of the MovieFormViewModel) into the viewModel in the New() method, or else you would have null for Model.Movie.Id and it would error out.
		//So to fix that, you could either initiate the Movie.Id by instantiating a new Movie object and assigning that to the MovieFormViewModel property of Movie, and it will default to 0, OR you could do it in the View itself.
		//And Mosh wants to make this a "Pure VIewModel" - SEE LESSON 60 FOR ALL THIS

		public int? Id { get; set; }

		[Required]
		public string Name { get; set; }

		//public Genre Genre { get; set; } Don't need Genre because we're only capturing Genre.Id in the form

		[Required]
		[Display(Name = "Genre")]
		public byte GenreId { get; set; }

		[Required]
		[Display(Name = "Release Date")]
		public DateTime? ReleaseDate { get; set; }

		//public DateTime DateAdded { get; set; } Not capturing DateAdded in the form

		[Display(Name = "Number in Stock")]
		[Range(1, 10)]
		[Required]
		public int? NumberInStock { get; set; }

		public MovieFormViewModel()
		{
			Id = 0;
		}

		public MovieFormViewModel(Movie movie)
		{
			Id = movie.Id;
			Name = movie.Name;
			GenreId = movie.GenreId;
			ReleaseDate = movie.ReleaseDate;
			NumberInStock = movie.NumberInStock;
		}
	}
}