using System;

namespace Ava.lib.user.data
{
	/// <summary>
	/// Summary description for User.
	/// </summary>
	public class User
	{
		private int userID = 0;
		private int userType = 0;
		private string userName = "";

		public User()
		{
		}
		
		public int UserID 
		{
			get 
			{
				return userID;
			}
			set 
			{
				userID = value;
			}
		}

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
	}
}