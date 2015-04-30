using System;

namespace EBid.lib.user.data
{
	/// <summary>
	/// Summary description for BuyerInfo.
	/// </summary>
	public class BuyerInfo
	{
		public string BuyerId;
		public string BuyerFirstName;
		public string BuyerLastName;
		public string BuyerMidName;
		public string UserName;
		public string Password;
		public string UserType;
		public string Status;

		public BuyerInfo( string BuyerId,
		 string BuyerFirstName,
		 string BuyerLastName,
		 string BuyerMidName,
		 string UserName,
		 string Password,
		 string UserType,
		 string Status)
		{
			this.BuyerId = BuyerId;
			this.BuyerFirstName =  BuyerFirstName;
			this.BuyerLastName = BuyerLastName;
			this.BuyerMidName = BuyerMidName;
			this.UserName = UserName;
			this.Password = Password;
			this.UserType = UserType;
			this.Status = Status;
		}

		public BuyerInfo(string BuyerFirstName,
			string BuyerLastName,
			string BuyerMidName,
			string UserName)
		{
			this.BuyerFirstName =  BuyerFirstName;
			this.BuyerLastName = BuyerLastName;
			this.BuyerMidName = BuyerMidName;
			this.UserName = UserName;
		}

		
	}
}
