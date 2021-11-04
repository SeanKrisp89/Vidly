﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
		//First off we need a Db context to access the database - LESSON 29
		private ApplicationDbContext _context { get; set; }

		//Initialize the DbContext. 
		public CustomersController()
		{
			_context = new ApplicationDbContext();
		}

		//This DbCOntext is a disposable object so we need to properly dispose of it.LESSON 29.
		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		// GET: Customers
		public ActionResult Index()
        {
			////hardcode our first set of customers for non-API/non-db controller
			//var customers = new List<Customer>
			//{
			//	new Customer {Name = "Sean Krispin", Id = 1},
			//	new Customer {Name = "Megan Grable", Id = 2}
			//};

			//Lesson 30 - we used the include method below for EAGER LOADING of the membership types. This is because our Customer class is associated with the MembershipType class via a Customer property of type Membershiptype. In order to use this method, we had to add the Data.Entity namespace
			var customers = _context.Customers.Include(c => c.MembershipType).ToList();

			if(customers == null)
			{
				return HttpNotFound();
			}

			//Not passing in/creating a customer viewModel so no need here

			return View(customers);
        }

		//See lesson 30 regarding eager loading. BY DEFAULT ENTITY FRAMEWORK ONLY LOADS THE CUSTOMER OBJECT (or the object passed into the model) AND NOT ITS RELATED OBJECTS/CLASSES
		//To solve this problem, we need to load the customers and their membership types together. This is called eager loading!!!
		public ActionResult Details(int id)
		{
			//var customers = new List<Customer //Original hardcoded without Db
			//{
			//	new Customer {Name = "Sean Krispin", Id = 1},
			//	new Customer {Name = "Megan Grable", Id = 2}
			//};

			var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

			if(customer == null)
			{
				return HttpNotFound();
			}

			return View(customer);
		}
    }
}