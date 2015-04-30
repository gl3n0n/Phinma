using System;
using System.Web;

namespace EBid.lib.bid.data
{
	public class BidItemDetail
	{
		private string item;
		private int bidDetailNo;
		private int bidRefNo;
		private string categoryId;
		private string detailDesc;
		private double estItemValue;
        private double unitPrice;
		//private char bidType;
		private int qty;
		private string deliveryDate;
		private string unitOfMeasure;
        private string deliveryDateMonth;
        private string deliveryDateDay;
        private string deliveryDateYear;

		public BidItemDetail() 
		{
		}

		public BidItemDetail(
			string item,
			int bidDetailNo,
			int bidRefNo,
			string categoryId,
			string detailDesc,
			double estItemValue,
			//char bidType,
			int qty,
			string deliveryDate,
            string unitOfMeasure)
		{
			this.item = item;
			this.bidDetailNo = bidDetailNo;
			this.bidRefNo = bidRefNo;
			this.categoryId = categoryId;
			this.detailDesc = detailDesc;
			this.estItemValue = estItemValue;
			//this.bidType = bidType;
			this.qty = qty;
			this.deliveryDate = deliveryDate;
			this.unitOfMeasure = unitOfMeasure;
		}

		public string Item 
		{
			get 
			{
				return item;
			}
			set 
			{
				item = value;
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
		
		public string CategoryId
		{
			get 
			{
				return categoryId;
			}
			set 
			{
				categoryId = value;
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

        public double UnitPrice
        {
            get
            {
                return unitPrice;
            }
            set
            {
                unitPrice = value;
            }
        }
/*		
		public char BidType		
		{
			get 
			{
				return bidType;
			}
			set 
			{
				bidType = value;
			}
		}
*/		
		public int Qty		
		{
			get 
			{
				return qty;
			}
			set 
			{
				qty = value;
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
		
		public string UnitOfMeasure
		{
			get 
			{
				return unitOfMeasure;
			}
			set 
			{
				unitOfMeasure = value;
			}
		}

        public string DeliveryDateMonth
        {
            get
            {
                return deliveryDateMonth;
            }
            set
            {
                deliveryDateMonth = value;
            }
        }

        public string DeliveryDateDay
        {
            get
            {
                return deliveryDateDay;
            }
            set
            {
                deliveryDateDay = value;
            }
        }

        public string DeliveryDateYear
        {
            get
            {
                return deliveryDateYear;
            }
            set
            {
                deliveryDateYear = value;
            }
        }
	}
}
