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

		[Authorize(Roles = RoleName.CanManageMovies)]
		public ActionResult New()
		{
			//First we need to get a list of MembershipTypes from the Db. THIS REQUIRED THAT WE ADD A DBSET OF TYPE MEMBERSHIPTYPES TO THE IDENTITYMODEL/APPLICATION DBCONTEXT - LESSON 40
			var membershipTypes = _context.MembershipTypes.ToList();

			var viewModel = new CustomerFormViewModel
			{
				Customer = new Customer(),
				MembershipTypes = membershipTypes
			};

			return View("CustomerForm", viewModel);
		}

		[ValidateAntiForgeryToken] //Try going to your edit/new Customer page, inspect the Save button, change the value for the _RequestVerificationToken and see what happens... (LS 56)
		[HttpPost] //The SAVE method used to be "Create", but we wanted to use the same action for new customers (Create) and editing existing customers (Update)
		public ActionResult Save(Customer customer) //Because the model behind our view is of type NewCustomerViewModel, we can use this type here and MVC framework will automatically map request data to this object (we later updated to type of Customer). This is what we call MODEL BINDING. So MVC framework BINDS the viewModel parameter to the request data. - LESSON 41
		{
			//THE THREE STEPS OF VALIDATION: 1st - Add data annotations on your entities. 2nd - use ModelState.IsValid to check validity. 3rd - add validation messages to our front-end forms! LS 50
			if (!ModelState.IsValid)
			{
				var viewModel = new CustomerFormViewModel
				{
					Customer = customer,
					MembershipTypes = _context.MembershipTypes.ToList()
				};

				return View("CustomerForm", viewModel);
			}
			if(customer.Id == 0)
			{
				//We first need to add the object to our DbContext. Keep in mind that when you add to context, it's not in the Db yet, just in memory.
				_context.Customers.Add(customer);
			}
			else//To update an entity, we need to get it from the Db first.. - UPDATING DATA LESSON 44
			{
				var customerInDb = _context.Customers.Single(c => c.Id == customer.Id); //We're using "Single()" method here as opposed to "SingleOrDefault()" so that if the given customer is not found it DOES throw an exception

				//This is a very manual way of updating our Db objects. We could use Automapper... (L44)
				customerInDb.Name = customer.Name;
				customerInDb.Birthdate = customer.Birthdate;
				customerInDb.MembershipTypeId = customer.MembershipTypeId;
				customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
			}
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
			//var customers = _context.Customers.Include(c => c.MembershipType).ToList(); COMMENTED OUT LINES 90 THROUGH95 DUE TO END OF LS 79. Since our Datatables on the index view is going to send an ajax request to our customers API, we don't need the list of customers from the Db here.

			//if(customers == null)
			//{
			//	return HttpNotFound();
			//}

			//Not passing in/creating a customer viewModel so no need here

			if (User.IsInRole(RoleName.CanManageMovies))
			{
				return View(/*customers*/); //commented out customer due to end of LS 79
			}
			else
			{
				return new HttpUnauthorizedResult();
			}
			
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

			return View("CustomerForm", viewModel);
		}
    }
}