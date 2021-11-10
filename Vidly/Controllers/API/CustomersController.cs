﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.Dtos;
using AutoMapper;

namespace Vidly.Controllers.API
{//Now remember, these methods will be returning data only! That's the whole point of this section - working with ASP.NET Web API. Building data services to pass to the client and have the client build its own HTML markup
   //Additionally, before ever writing anything to consume an API, use Postman client to validate it works!
   //Additionally additionally, at some point review lesson 67 on DTOs. According to Mosh, our APIs should never receive or return domain objects. So at some point, all our API methods below will be changed from type Customer to type CustomerDto
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
		public IEnumerable<CustomerDto> GetCustomers() //used to be of type Customer prior to Dto
		{
			//So since we're GETTING a Customer object and returning it, we need the source to be of type Customer, and translate it to CustomerDto as we pass back to the View/Client - LS 68
			return _context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
		}

		// GET /api/customers/1
		public /*CustomerDto*/ IHttpActionResult GetCustomer(int id) //used to be of type Customer prior to Dto
		{
			var customer = _context.Customers.SingleOrDefault(c => c.Id == id); 

			if(customer == null)
			{
				//throw new HttpResponseException(HttpStatusCode.NotFound);
				return NotFound();
			}
			//In GetCustomer() method, we're returning 1 customer, so we cannot use Select extension method of LINQ. 
			return Ok(Mapper.Map<Customer, CustomerDto>(customer)); 
		}

		// POST /customers
		[HttpPost]
		public /*CustomerDto*/IHttpActionResult CreateCustomer(CustomerDto customerDto) //Even thought we're posting, by convention you typically return the newly created resource to the client. Hence the return type (as opposed to void)
		{
			//Again, first validate (just like our New(), non-API method)
			if (!ModelState.IsValid)
			{
				//throw new HttpResponseException(HttpStatusCode.BadRequest);
				return BadRequest();
			}
			//Since we're creating a customer, the end product is a Customer object, thus the target format is Customer and we supply a customerDto object
			var customer = Mapper.Map<CustomerDto, Customer>(customerDto);

			_context.Customers.Add(customer);
			_context.SaveChanges(); //At this point the Id property for customer will be set based on the Id generated by the Database

			//This Customer object has an Id that was generated by the database, so we want to return it to the client
			customerDto.Id = customer.Id;

			//return customerDto;
			return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto); //See lesson 70 - Here, as part of the RESTful convention, we need to return the URI of the newly created resource to the client. REVISIT THIS IT WAS A LITTLE CONFUSING
			//I believe what we're returning here with regards to API is the RESPONSE.... maybe ask Eleazar?
		}

		// PUT /api/customers/1
		[HttpPut]
		public void UpdateCustomer(int id, CustomerDto customerDto)
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

			Mapper.Map<CustomerDto, Customer>(customerDto, customerInDb); //See end of lesson 65 for why we're passing in two args this time...
			//Otherwise, update the customer object in the Db. See lesson 65 - here he mentions Automapper that will map our properties for us so that we don't have to map manually. We'll be replacing this code later.
			//customerInDb.Name = customer.Name;
			//customerInDb.Birthdate = customer.Birthdate;
			//customerInDb.MembershipTypeId = customer.MembershipTypeId;
			//customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;

			_context.SaveChanges();
		}

		//MOSH DIDN'T USE AUTOMAPPER FOR DELETE API... I WONDER WHY?

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
