using System.Web;
using System.Web.Mvc;

namespace Vidly
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
			filters.Add(new AuthorizeAttribute()); //Adding this filter required authorization for the whole website, because when I comment it out I hit the home page first, when I enable it I have to login no matter what
			filters.Add(new RequireHttpsAttribute()); //LS 96 - requires secure connection
			
		}
	}
}
