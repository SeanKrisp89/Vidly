﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Dtos
{
	public class MovieDto
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public byte GenreId { get; set; }

		[Required]
		public DateTime ReleaseDate { get; set; }

		public DateTime DateAdded { get; set; }

		[Range(1, 10)]
		[Required]
		public int NumberInStock { get; set; }
	}
}