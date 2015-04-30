using System;

namespace EBid.lib.bid.data
{
	public class BidItem
	{
		private int bidRefNo;
		private int prRefNo;
		private string requestor;
		private string itemDescription;
		private double estItemValue;
        private string formattedEstItemValue;
		private string deadline;
		private string dateCreated;
		private int buyerId;
		private int approvedBy;
		private int groupDeptSec;
		private string category;
        private int subcategory;
		private int forAuction;
		private int bidStatus;
		private string deliveryDate;
		private int companyid;
		private string suppliers;
        private string deliverto;
        private string incoterm;
        private string fileattachments;
        private string actualfilenames;
        private string prdate;
        private string prdatemonth;
        private string prdateday;
        private string prdateyear;
        private string deadlinemonth;
        private string deadlineday;
        private string deadlineyear;
        private string deliverydatemonth;
        private string deliverydateday;
        private string deliverydateyear;
        private int bidCurrency;

		public BidItem() 
		{
		}

		
		public int BidRefNo 
		{
			get 
			{
				return bidRefNo;
			}
			set 
			{
				bidRefNo = value;
			}
		}

		public int PRRefNo 
		{
			get 
			{
				return prRefNo;
			}
			set 
			{
				prRefNo = value;
			}
		}

		public string Requestor 
		{
			get 
			{
				return requestor;
			}
			set 
			{
				requestor = value;
			}
		}

		public string ItemDescription 
		{
			get 
			{
				return itemDescription;
			}
			set 
			{
				itemDescription = value;
			}
		}

		public double EstItemValue 
		{
			get 
			{
				return estItemValue;
			}
			set 
			{
				estItemValue = value;
			}
		}

        public string FormattedEstItemValue
        {
            get
            {
                return formattedEstItemValue;
            }
            set
            {
                formattedEstItemValue = value;
            }
        }

		public string Deadline 
		{
			get 
			{
				return deadline;
			}
			set 
			{
				deadline = value;
			}
		}

		public string DateCreated 
		{
			get 
			{
				return dateCreated;
			}
			set 
			{
				dateCreated = value;
			}
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

		public int ApprovedBy
		{
			get 
			{
				return approvedBy;
			}
			set 
			{
				approvedBy = value;
			}
		}

		public int GroupDeptSec
		{
			get 
			{
				return groupDeptSec;
			}
			set 
			{
				groupDeptSec = value;
			}
		}

        public string Category
		{
			get 
			{
				return category;
			}
			set 
			{
				category = value;
			}
		}

        public int SubCategory
        {
            get
            {
                return subcategory;
            }
            set
            {
                subcategory = value;
            }
        }

		public int ForAuction
		{
			get 
			{
				return forAuction;
			}
			set 
			{
				forAuction = value;
			}
		}

		public int BidStatus
		{
			get 
			{
				return bidStatus;
			}
			set 
			{
				bidStatus = value;
			}
		}

		public string DeliveryDate
		{
			get 
			{
				return deliveryDate;
			}
			set 
			{
				deliveryDate = value;
			}
		}

		public int CompanyId
		{
			get 
			{
				return companyid;
			}
			set 
			{
				companyid = value;
			}
		
		}

		public string Suppliers
		{
			get
			{
				return suppliers;
			}
			set 
			{
				suppliers = value;
			}
		}

        public string DeliverTo
        {
            get
            {
                return deliverto;
            }
            set
            {
                deliverto = value;
            }
        }

        public string Incoterm
        {
            get
            {
                return incoterm;
            }
            set
            {
                incoterm = value;
            }
        }

        public string FileAttachments
        {
            get
            {
                return fileattachments;
            }
            set
            {
                fileattachments = value;
            }
        }

        public string ActualFileNames
        {
            get
            {
                return actualfilenames;
            }
            set
            {
                actualfilenames  = value;
            }
        }

        public string PRDate
        {
            get
            {
                return prdate;
            }
            set
            {
                prdate = value;
            }
        }

        public string PRDateMonth
        {
            get
            {
                return prdatemonth;
            }
            set
            {
                prdatemonth = value;
            }
        }
        public string PRDateDay
        {
            get
            {
                return prdateday;
            }
            set
            {
                prdateday = value;
            }
        }
        public string PRDateYear
        {
            get
            {
                return prdateyear;
            }
            set
            {
                prdateyear = value;
            }
        }

        public string DeadlineMonth
        {
            get
            {
                return deadlinemonth;
            }
            set
            {
                deadlinemonth = value;
            }
        }

        public string DeadlineDay
        {
            get
            {
                return deadlineday;
            }
            set
            {
                deadlineday = value;
            }
        }

        public string DeadlineYear
        {
            get
            {
                return deadlineyear;
            }
            set
            {
                deadlineyear = value;
            }
        }

        public string DeliveryDateMonth
        {
            get
            {
                return deliverydatemonth;
            }
            set
            {
                deliverydatemonth = value;
            }
        }

        public string DeliveryDateDay
        {
            get
            {
                return deliverydateday;
            }
            set
            {
                deliverydateday = value;
            }
        }

        public string DeliveryDateYear
        {
            get
            {
                return deliverydateyear;
            }
            set
            {
                deliverydateyear = value;
            }
        }

        public int BidCurrency
        {
            get
            {
                return bidCurrency;
            }
            set
            {
                bidCurrency = value;
            }
        }
	}
}
