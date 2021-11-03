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
		[Required] //Overriding Conventions - lesson 28
		[StringLength(255)]
		public string Name { get; set; }
		public bool IsSubscribedToNewsLetter { get; set; }
		//Now we need to associate our customer class with MembershipType
		public MembershipType MembershipType { get; set; } //This is what we call a navigation property, because it allows us to navigate from one type to another.
		//These nav props are useful when you want to load an object and its related objects together from the Db (I believe this is eager loading?)
	}
}