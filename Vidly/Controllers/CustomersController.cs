using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;

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

		public ActionResult New()
		{
			//First we need to get a list of MembershipTypes from the Db. THISREQUIRED THAT WE ADD A DBSET OF TYPE MEMBERSHIPTYPES TO THE IDENTITYMODEL/APPLICATION DBCONTEXT - LESSON 40
			var membershipTypes = _context.MembershipTypes.ToList();

			var viewModel = new CustomerFormViewModel
			{
				MembershipTypes = membershipTypes
			};

			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Create(Customer customer) //Because the model behind our view is of type NewCustomerViewModel, we can use this type here and MVC framework will automatically map request data to this object (we later updated to type of Customer). This is what we call MODEL BINDING. So MVC framework BINDS the viewModel parameter to the request data. - LESSON 41
		{
			//We first need to add the object to our DbContext. Keep in mind that when you add to context, it's not in the Db yet, just in memory.
			_context.Customers.Add(customer);
			//We then need to PERSIST these changes:
			_context.SaveChanges();
			//We then want to return the user to the list of customers (i.e. the index)
			return RedirectToAction("Index", "Customers");
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

		public ActionResult Edit(int id)
		{
			//first we need to retrieve customer from Db
			var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

			if(customer == null)
			{
				return HttpNotFound();
			}

			//Since the model behind the New.cshtml page is NewCustomerViewModel, we need to pass that in.
			var viewModel = new CustomerFormViewModel
			{
				Customer = customer,
				MembershipTypes = _context.MembershipTypes.ToList() //We also need to initialize MembershipTypes prop of NewCustomerViewModel object. I believe this because we need to popualate the dropdown with membershiptype options
			};

			return View("New", viewModel);
		}
    }
}