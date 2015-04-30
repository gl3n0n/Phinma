using System;
using System.Web;

namespace EBid.lib.user.data
{
	public class Vendor : User
	{        
        private string vendorName;
        private int accredited;
        private string vendorEmail;
        private string vendorAddress;
        private string vendorAddress1;
        private string vendorAddress3;
        private int classification;
        private string accredDate;
        private string contactPerson;
        private int vendorCategory;
        private int locationId;        
        private string vendorPassword;
        private string syskey;
        private string telephoneNo;

		public Vendor()
		{
		}

        public int VendorId
        {
            get
            {
                return this.UserID;
            }
            set
            {
                this.UserID = value;
            }
        }

        public string VendorName
        {
            get
            {
                return vendorName;
            }
            set
            {
                vendorName = value;
            }
        }

        public int Accredited
        {
            get
            {
                return accredited;
            }
            set
            {
                accredited = value;
            }
        }

        public string VendorEmail
        {
            get
            {
                return vendorEmail;
            }
            set
            {
                vendorEmail = value;
            }
        }

        public string VendorAddress
        {
            get
            {
                return vendorAddress;
            }
            set
            {
                vendorAddress = value;
            }
        }

        public string VendorAddress1
        {
            get
            {
                return vendorAddress1;
            }
            set
            {
                vendorAddress1 = value;
            }
        }

        public string VendorAddress3
        {
            get
            {
                return vendorAddress3;
            }
            set
            {
                vendorAddress3 = value;
            }
        }

        public int Classification
        {
            get
            {
                return classification;
            }
            set
            {
                classification = value;
            }
        }

        public string AccredDate
        {
            get
            {
                return accredDate;
            }
            set
            {
                accredDate = value;
            }
        }

        public string ContactPerson
        {
            get
            {
                return contactPerson;
            }
            set
            {
                contactPerson = value;
            }
        }

        public int VendorCategory
        {
            get
            {
                return vendorCategory;
            }
            set
            {
                vendorCategory = value;
            }
        }

        public int LocationId
        {
            get
            {
                return locationId;
            }
            set
            {
                locationId = value;
            }
        }

        public string VendorUserName
        {
            get
            {
                return this.UserName;
            }
            set
            {
                this.UserName = value;
            }
        }

        public string VendorPassword
        {
            get
            {
                return vendorPassword;
            }
            set
            {
                vendorPassword = value;
            }
        }

        public string Syskey
        {
            get
            {
                return syskey;
            }
            set
            {
                syskey = value;
            }
        }

        public string TelephoneNo
        {
            get
            {
                return telephoneNo;
            }
            set
            {
                telephoneNo = value;
            }
        }

	}
}
