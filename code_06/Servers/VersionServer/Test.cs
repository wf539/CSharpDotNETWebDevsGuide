using System;
using System.Windows.Forms;
using System.Reflection;

namespace VersionServer
{

	public class Test:MarshalByRefObject
	{
		public Test()
		{
		}

		public String getVersion() {
			return Assembly.GetAssembly(this.GetType()).GetName().Version.ToString();
		}
	}
}
