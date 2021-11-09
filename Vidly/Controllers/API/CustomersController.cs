﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;

namespace Vidly.Controllers.API
{//Now remember, these methods will be returning data only! That's the whole point of this section - working with ASP.NET Web API. Building data services to pass to the client and have the client build its own HTML markup
    public class CustomersController : ApiController //Remember that your API controllers have to derive from the ApiController class
    {
		private ApplicationDbContext _context;

		public CustomersController()
		{
			_context = new ApplicationDbContext();
		}

		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		// GET /api/customers
		public IEnumerable<Customer> GetCustomers()
		{
			return _context.Customers.ToList();
		}

		// GET /api/customers/1
		public Customer GetCustomer(int id)
		{
			var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

			if(customer == null)
			{
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}

			return customer; 
		}

		// POST /customers
		[HttpPost]
		public Customer CreateCustomer(Customer customer) //Even thought we're posting, by convention you typically return the newly created resource to the client. Hence the return type (as opposed to void)
		{
			//Again, first validate (just like our New(), non-API method)
			if (!ModelState.IsValid)
			{
				throw new HttpResponseException(HttpStatusCode.BadRequest);
			}

			_context.Customers.Add(customer);
			_context.SaveChanges(); //At this point the Id property for customer will be set based on the Id generated by the Database

			return customer;
		}

		// PUT /api/customers/1
		[HttpPut]
		public void UpdateCustomer(int id, Customer customer)
		{
			if (!ModelState.IsValid)
			{
				throw new HttpResponseException(HttpStatusCode.BadRequest);
			}

			//Need to get the customer in the database that we're updating
			var customerInDb = _context.Customers.Single(c => c.Id == id);

			//If not found, throw not found exception
			if(customerInDb == null)
			{
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}

			//Otherwise, update the customer object in the Db. See lesson 65 - here he mentions Automapper that will map our properties for us so that we don't have to map manually. We'll be replacing this code later.
			customerInDb.Name = customer.Name;
			customerInDb.Birthdate = customer.Birthdate;
			customerInDb.MembershipTypeId = customer.MembershipTypeId;
			customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;

			_context.SaveChanges();
		}

		// DELETE /api/customers/1
		[HttpDelete]
		public void DeleteCustomer(int id)
		{
			var customerInDb = _context.Customers.Single(c => c.Id == id);

			if(customerInDb == null)
			{
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}

			_context.Customers.Remove(customerInDb);

			_context.SaveChanges();
		}
	}
}
