using System;
using System.Web;

namespace EBid.lib.bid.data
{
	public class BidTenderGeneral
	{
		private int bidTenderGeneralNo;
		private int bidRefNo;
        private int vendorID;
		
		private string currency;
		private string deliveryCost;
        private string discount;
        private string totalCost;
		
		private string incoterm;
		private string paymentTerms;		
		private string warranty;
		private string remarks;
        private string totalExtendedCost;

        private string comments;
        private string password;

        private string mode_BidTendersGeneral;
        private string mode_BidTenderComments;        

		public BidTenderGeneral()
		{
		}

		public int BidTenderGeneralNo 
		{
			get 
			{
				return bidTenderGeneralNo;
			}
			set 
			{
				bidTenderGeneralNo = value;
			}
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

        public int VendorID
        {
            get
            {
                return vendorID;
            }
            set
            {
                vendorID = value;
            }
        }

		public string Currency
		{
			get 
			{
				return currency;
			}
			set 
			{
				currency = value;
			}
		}

        public string DeliveryCost
		{
			get 
			{
				return deliveryCost;
			}
			set 
			{
				deliveryCost = value;
			}
		}
        
		public string Discount
		{
			get 
			{
				return discount;
			}
			set 
			{
				discount = value;
			}
		}
		public string TotalCost
		{
			get 
			{
				return totalCost;
			}
			set 
			{
				totalCost = value;
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

		public string PaymentTerms
		{
			get 
			{
				return paymentTerms;
			}
			set 
			{
				paymentTerms = value;
			}
		}

		
		public string Warranty
		{
			get 
			{
				return warranty;
			}
			set 
			{
				warranty = value;
			}
		}

        public string TotalExtendedCost
        {
            get
            {
                return totalExtendedCost;
            }
            set
            {
                totalExtendedCost = value;
            }
        }

        public string Remarks
        {
            get
            {
                return remarks;
            }
            set
            {
                remarks = value;
            }
        }

        public string Comments
        {
            get
            {
                return comments;
            }
            set
            {
                comments = value;
            }
        }

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


        public string Mode_BidTendersGeneral
        {
            get
            {
                return mode_BidTendersGeneral;
            }
            set
            {
                mode_BidTendersGeneral = value;
            }
        }

        public string Mode_BidTenderComments
        {
            get
            {
                return mode_BidTenderComments;
            }
            set
            {
                mode_BidTenderComments = value;
            }
        }

       

	}
}