using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult Index()
        {
			//hardcode our first set of customers for non-API/non-db controller
			var customers = new List<Customer>
			{
				new Customer {Name = "Sean Krispin", Id = 1},
				new Customer {Name = "Megan Grable", Id = 2}
			};

			//Not passing in/creating a customer viewModel so no need here

			return View(customers);
        }

		public ActionResult Details(int id)
		{
			var customers = new List<Customer>
			{
				new Customer {Name = "Sean Krispin", Id = 1},
				new Customer {Name = "Megan Grable", Id = 2}
			};

			var customer = customers.Find(c => c.Id == id);

			return View(customer);
		}
    }
}