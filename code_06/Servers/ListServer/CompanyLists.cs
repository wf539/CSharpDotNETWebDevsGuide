using System;

namespace ListServer
{
	/// <summary>
	/// Summary description for CompanyLists.
	/// </summary>
	public class CompanyLists: MarshalByRefObject
	{
		
		private String[] Countries = {"Spain","France","Italy"};

		public String[] getCountryList()
		{
			return Countries;
		}

	}
}
