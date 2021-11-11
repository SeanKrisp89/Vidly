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

		public MembershipTypeDto MembershipType { get; set; } //LS - 80: IMPORTANT: we needed to eager load the membershiptypes in our API GetCustomers, in order to render in the view/2nd column.
															  //Problem is, we don't want to couple our Dtos to our domain models, so we created a MembershipTypeDto, and added a MembershipType property to our CustomerDto of type MembershipTypeDto. All we have in it is Id (byte) and Name.
															  //DON'T FORGET TO ADD THE NEW MAPPING TO OUR MAPPING PROFILE - we need to know how to map MembershipType to MembershipTypeDto and vise versa.

		//[Min18YearsIfAMember] had to comment this out since we cast to Customer in that class
		public DateTime? Birthdate { get; set; }
	}
}