using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
	public class CustomerFormViewModel
	{
		//We need a LIST of MembershipTypes since we're creating a MembershipType dropdown to select for new customer.
		//Additionally, we're using IEnumerable here instead of List<MembershipType> only because we do not need all the methods that come with List (add, remove, etc...) All we need is a way to iterate over the membership types.
		public  IEnumerable<MembershipType> MembershipTypes { get; set; }
		public Customer Customer { get; set; }
	}
}

//We had to add a viewmodel for this since we need to pass both Customer and MembershipType models into the view...