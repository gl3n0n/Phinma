using System;
using System.Web;

namespace EBid.lib.user.data
{
	/// <summary>
	/// Summary description for PurchaseOfficer.
	/// </summary>
	public class PurchaseOfficer : User
	{
        private int purchasingId;
        private string purchasingFirstName;
        private string purchasingLastName;
        private string purchasingMiddleName;
		private string emailAdd;
		private int deptId;
		
		public PurchaseOfficer(
		    int purchasingId,
            string purchasingFirstName,
            string purchasingLastName,
            string purchasingMiddleName,
		    string emailAdd,
            int deptId)
		{
			this.purchasingId = purchasingId;
			this.purchasingFirstName = purchasingFirstName;
			this.purchasingLastName = purchasingLastName;
			this.purchasingMiddleName = purchasingMiddleName;
			this.emailAdd = emailAdd;
			this.deptId = deptId;
		}
		
		public int PurchasingId 
		{
			get 
			{
				return purchasingId;
			}
			set 
			{
				purchasingId = value;
			}
		}

		public string PurchasingFirstName 
		{
			get 
			{
				return purchasingFirstName;
			}
			set 
			{
				purchasingFirstName = value;
			}
		}
				
		public string PurchasingLastName 
		{
			get 
			{
				return purchasingLastName;
			}
			set 
			{
				purchasingLastName = value;
			}
		}

		public string PurchasingMiddleName
		{
			get 
			{
				return purchasingMiddleName;
			}
			set 
			{
				purchasingMiddleName = value;
			}
		}
					
		public string EmailAdd
		{
			get 
			{
				return emailAdd;
			}
			set 
			{
				emailAdd = value;
			}
		}

		public int DeptId 
		{
			get 
			{
				return deptId;
			}
			set 
			{
				deptId = value;
			}
		}
		
	}
}
