using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
	public class MembershipType
	{//IMPORTANT EF NOTES - In EF, every entity must have a key, which will be mapped to PK of the corresponding table in the database. By convention this prop should be called either Id, or the name of the type+Id. Lesson 26!!!
		public byte Id { get; set; }
		public short SignUpFee { get; set; }
		public byte DurationInMonths { get; set; }
		public byte DiscountRate { get; set; }
		public string Name { get; set; }
	}
}