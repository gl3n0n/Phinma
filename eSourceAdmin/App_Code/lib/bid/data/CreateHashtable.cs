using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace EBid.lib.bid.data
{
	/// <summary>
	/// Summary description for Hashtable.
	/// </summary>
	public class CreateHashtable
	{
		
		public Hashtable htable;

		public   CreateHashtable()
		{
			
		}

		public void NewHashtable()
		{
			htable = new Hashtable();
		}

		public void AddRowToHashtable(int key, WebControl[] cnt)
		{
			htable.Add(key, cnt);
			
		}

		
	}
}
