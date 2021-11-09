using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
//This whole class is lesson 53 - CUSTOM VALIDATION

namespace Vidly.Models
{
	public class Min18YearsIfAMember : ValidationAttribute //For custom validation classes/models, you need to derive from ValidationAttribute class
	{
		//First step is to override the isValid method. Mosh recommends using the ValidationContext overload, as it gives us access to other properties of our model
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			//First, we need to check the selected membershiptype (since you must be 18 years or older for anything other than Pay As You Go) and that's where validationContext comes into the picture
			//ObjectInstance gives us access to the containing class - in this case, Customer. An since it's of type Object, we need to cast it to Customer
			var customer = (Customer)validationContext.ObjectInstance;

			//Now we can check the selected MembershipType. We added == 0 later because if the user does not enter a value for membership type, the default value for int data types is 0
			if(customer.MembershipTypeId == 0 || customer.MembershipTypeId == 1)
			{
				return ValidationResult.Success; //Since 1 = Pay As You Go and since we don't care about age for that membership, we assume the age is valid
			}
			
			if(customer.Birthdate == null)
			{
				return new ValidationResult("Birthdate is required"); //In order to indicate an error, you instantiate a new ValidationResult
			}

			var age = DateTime.Today.Year - customer.Birthdate.Value.Year;

			return (age >= 18) 
				? ValidationResult.Success 
				: new ValidationResult("Customer should be at least 18 years old for a membership.");
		}
	}
}