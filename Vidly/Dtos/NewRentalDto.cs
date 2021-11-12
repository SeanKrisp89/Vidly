﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;
using AutoMapper;

namespace Vidly.Dtos
{
	public class NewRentalDto
	{
		public int CustomerId { get; set; }
		public List<int> MoviesIds { get; set; }
	}
}