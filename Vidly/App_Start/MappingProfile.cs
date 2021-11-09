﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Vidly.Models;
using Vidly.Dtos;

namespace Vidly.App_Start
{
	public class MappingProfile : Profile //Profile is a class defined in the Automapper namespace
	{
		public MappingProfile()
		{
			Mapper.CreateMap<Customer, CustomerDto>();
			Mapper.CreateMap<CustomerDto, Customer>();
		}
	}
}