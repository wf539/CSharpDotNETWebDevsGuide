using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;

namespace CountServer
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class Count: MarshalByRefObject
	{

		private int mVal;

		public Count()
		{
			mVal = 0;
		}

		public override Object InitializeLifetimeService()
		{
			ILease lease = (ILease)base.InitializeLifetimeService();
			if (lease.CurrentState == LeaseState.Initial)  
			{
				lease.InitialLeaseTime = TimeSpan.FromSeconds(5);
				lease.RenewOnCallTime = TimeSpan.FromSeconds(1);
				lease.SponsorshipTimeout = TimeSpan.FromSeconds(5);
			}
			
			return lease;
		}

		public int inc()
		{
			mVal++;
			return mVal;
		}

		public int dec()
		{
			mVal--;
			return mVal;
		}
	}
}
