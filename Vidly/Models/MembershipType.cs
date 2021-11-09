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


		//Lesson 54 - "Refactoring Magic Numbers". These magic numbers we're referring to were the original 0 and 1 in the Min18YearsIfAMember class, when we were checking membership types
		//You want to avoid magic numbers since they can break easily and are hard for other developers to pick up on quickly.
		//You could use an Enum, but you'd have to cast it to byte when doing the comparison in the Min18 class
		public static readonly byte Unknown = 0;
		public static readonly byte PayAsYouGo = 1;
	}
}