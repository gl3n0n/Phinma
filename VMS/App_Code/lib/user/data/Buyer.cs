using System;

namespace Ava.lib.user.data
{
	public class Buyer : User
	{
		private int buyerId;
		private string buyerFirstName;
		private string buyerLastName;
		private string buyerMidName;
		private string userName;
		private string password;
		private int userType;
		private char status;
		private int companyId;
		
		public Buyer(
			int buyerId,
			string buyerFirstName,
			string buyerLastName,
			string buyerMidName,
			string userName,
			string password,
			int userType,
			char status,
			int companyId)
		{
			this.buyerId = buyerId;
			this.buyerFirstName = buyerFirstName;
			this.buyerLastName = buyerLastName;
			this.buyerMidName = buyerMidName;
			this.userName = userName;
			this.password = password;
			this.userType = userType;
			this.status = status;
			this.companyId = companyId;
		}
		
		public int BuyerId 
		{
			get 
			{
				return buyerId;
			}
			set 
			{
				buyerId = value;
			}
		}

		public string BuyerFirstName 
		{
			get 
			{
				return buyerFirstName;
			}
			set 
			{
				buyerFirstName = value;
			}
		}
				
		public string BuyerLastName 
		{
			get 
			{
				return buyerLastName;
			}
			set 
			{
				buyerLastName = value;
			}
		}

		public string BuyerMidName
		{
			get 
			{
				return buyerMidName;
			}
			set 
			{
				buyerMidName = value;
			}
		}
		/*				
		public string UserName 
		{
			get 
			{
				return userName;
			}
			set 
			{
				userName = value;
			}
		}
		*/					
		public string Password
		{
			get 
			{
				return password;
			}
			set 
			{
				password = value;
			}
		}
/*
		public int UserType 
		{
			get 
			{
				return userType;
			}
			set 
			{
				userType = value;
			}
		}
*/
		public char Status
		{
			get 
			{
				return status;
			}
			set 
			{
				status = value;
			}
		}

		public int CompanyId 
		{
			get 
			{
				return companyId;
			}
			set 
			{
				companyId = value;
			}
		}
	}
}