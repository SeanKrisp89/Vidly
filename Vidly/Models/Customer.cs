using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
	public class Customer
	{
		public int Id { get; set; }
		//You can also override the default error message - see below
		[Required(ErrorMessage = "Please enter the customer's name.")] //Overriding Conventions - lesson 28
		[StringLength(255)]
		public string Name { get; set; }

		public bool IsSubscribedToNewsLetter { get; set; }

		//Now we need to associate our customer class with MembershipType
		public MembershipType MembershipType { get; set; } //This is what we call a navigation property, because it allows us to navigate from one type to another.
														   //These nav props are useful when you want to load an object and its related objects together from the Db (I believe this is eager loading? - Yep.)
		[Display(Name = "Membership Type")]
		public byte MembershipTypeId { get; set; }

		//So these data annotation will alter the label on our forms, but the problem with this approach is every time we do this, or if we want to change the label, we have to recompile our code. Lesson 39.
		
		[Min18YearsIfAMember]
		[Display(Name = "Date of Birth")]
		public DateTime? Birthdate { get; set; }
	}
}