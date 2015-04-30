using System;

namespace Ava.lib.bid.data
{
	public class BidTender
	{
		private string bidItem;
		private string detailDesc;
		private int bidTenderNo;
		private int bidDetailNo;
		private double amount;
		private string tenderDate;
		private int vendorID;
		private int status;
        private string mode_BidTenders;
        private string vendorWithLowestAmt;
        private string vendorWithHighestAmt;

		public BidTender()
		{
		}
        public string VendorWithLowestAmt
        {
            get
            {
                return vendorWithLowestAmt;
            }
            set
            {
                vendorWithLowestAmt = value;
            }
        }

        public string VendorWithHighestAmt
        {
            get
            {
                return vendorWithHighestAmt;
            }
            set
            {
                vendorWithHighestAmt = value;
            }
        }


		public string BidItem
		{
			get 
			{
				return bidItem;
			}
			set 
			{
				bidItem = value;
			}
		}

		public string DetailDesc 
		{
			get 
			{
				return detailDesc;
			}
			set 
			{
				detailDesc = value;
			}
		}

		public int BidTenderNo 
		{
			get 
			{
				return bidTenderNo;
			}
			set 
			{
				bidTenderNo = value;
			}
		}

		public int BidDetailNo 
		{
			get 
			{
				return bidDetailNo;
			}
			set 
			{
				bidDetailNo = value;
			}
		}

		public double Amount 
		{
			get 
			{
				return amount;
			}
			set 
			{
				amount = value;
			}
		}

		public string TenderDate 
		{
			get 
			{
				return tenderDate;
			}
			set 
			{
				tenderDate = value;
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

		public int Status
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

        public string Mode_BidTenders
        {
            get
            {
                return mode_BidTenders;
            }
            set
            {
                mode_BidTenders = value;
            }
        }
	}
}
