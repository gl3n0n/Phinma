using System;
using System.Web;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace EBid.lib.auction.data
{
	public class AuctionItemDetail
	{
		private int auctionDetailNo;
		private int auctionRefNo;
		private string categoryId;
		private string dateCreated;
		private string description;
		private string item;
		private int quantity;
		private int status;
		private int subCategoryId;
		private string unitOfMeasure;

		public AuctionItemDetail()
		{
		}

		public int AuctionDetailNo
		{
			get
			{
				return auctionDetailNo;
			}
			set
			{
				auctionDetailNo = value;
			}
		}
		public int AuctionRefNo
		{
			get
			{
				return auctionRefNo;
			}
			set
			{
				auctionRefNo = value;
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
		public string Description
		{
			get
			{
				return description;
			}
			set
			{
				description = value;
			}
		}
		public int Quantity
		{
			get
			{
				return quantity;
			}
			set
			{
				 quantity = value;
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
		public int SubCategoryId
		{
			get
			{
				return subCategoryId;
			}
			set
			{
				subCategoryId = value;
			}
		}
	}
}