using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.Dtos
{
	public class CustomerDto
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Please enter the customer's name.")] //Overriding Conventions - lesson 28
		[StringLength(255)]
		public string Name { get; set; }

		public bool IsSubscribedToNewsLetter { get; set; }

		//public MembershipType MembershipType { get; set; } We want to remove MembershipType because it is a part of our domain, which creates a dependency for our Dto to our domain. Don't want domain dependencies!
														   
		public byte MembershipTypeId { get; set; }

		//[Min18YearsIfAMember] had to comment this out since we cast to Customer in that class
		public DateTime? Birthdate { get; set; }
	}
}