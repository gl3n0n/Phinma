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
using EBid.Exceptions;

namespace EBid.lib.auction.data
{
	/// <summary>
	/// By GA S. 07132006
	/// </summary>
	public class AuctionDetails
	{
		private int _ID;
		private string _Description;
		private string _Duration;
		private DateTime _StartDateTime;
		private DateTime _EndDateTime;
		private DateTime _ConfirmationDeadline;
		private string _Type;
		private string _Creator;
		private string _CreatorEmail;
        private string _Sender;
        private string _Company;

		public AuctionDetails()
		{
			_ID = 0;
			_Description = _Duration = string.Empty;
			//_StartDateTime = _EndDateTime = _ConfirmationDeadline = null;
		}

		/// <summary>
		/// Converts a source row's data to auction details
		/// </summary>
		/// <param name="dt">Source row</param>
		public AuctionDetails(DataRow dr)
		{
			AuctionDetails ad = new AuctionDetails();
			ad = ConvertRow(dr);
			ID = ad.ID;
			Description = ad.Description;
			Duration = ad.Duration;
			this.Type = ad.Type;
			StartDateTime = ad.StartDateTime;
			EndDateTime = ad.EndDateTime;
			ConfirmationDeadline = ad.ConfirmationDeadline;
			Creator = ad.Creator;
			CreatorEmail = ad.CreatorEmail;
            Sender = ad.Sender;
            Company = ad.Company;
		}

		/// <summary>
		/// Gets or sets auction reference no. of this auction
		/// </summary>
		public int ID
		{
			get
			{
				return _ID;
			}
			set
			{
				_ID = value;
			}
		}

		/// <summary>
		/// Gets or sets description of this auction
		/// </summary>
		public string Description
		{
			get
			{
				return _Description;
			}
			set
			{
				_Description = value;
			}
		}

		/// <summary>
		/// Gets or sets description of this auction
		/// </summary>
		public string Duration
		{
			get
			{
				return _Duration;
			}
			set
			{
				_Duration = value;
			}
		}

		/// <summary>
		/// Gets or sets start date and time of this auction
		/// </summary>
		public DateTime StartDateTime
		{
			get
			{
				return _StartDateTime;
			}
			set
			{
				_StartDateTime = value;
			}
		}

		/// <summary>
		/// Gets or sets end date and time of this auction
		/// </summary>
		public DateTime EndDateTime
		{
			get
			{
				return _EndDateTime;
			}
			set
			{
				_EndDateTime = value;
			}
		}

		/// <summary>
		/// Gets or sets confirmation deadline of this auction
		/// </summary>
		public DateTime ConfirmationDeadline
		{
			get
			{
				return _ConfirmationDeadline;
			}
			set
			{
				_ConfirmationDeadline = value;
			}
		}

		/// <summary>
		/// Gets or sets auction type
		/// </summary>
		public string Type
		{
			get
			{
				return _Type;
			}
			set
			{
				_Type = value;
			}
		}

		/// <summary>
		/// Gets or sets auction creator
		/// </summary>
		public string Creator
		{
			get
			{
				return _Creator;
			}
			set
			{
				_Creator = value;
			}
		}

		/// <summary>
		/// Gets or sets auction creator's email address
		/// </summary>
		public string CreatorEmail
		{
			get
			{
				return _CreatorEmail;
			}
			set
			{
				_CreatorEmail = value;
			}
		}

        public string Sender
        {
            get
            {
                return _Sender;
            }
            set
            {
                _Sender = value;
            }
        }

        public string Company
        {
            get
            {
                return _Company;
            }
            set
            {
                _Company = value;
            }
        }

		protected AuctionDetails ConvertRow(DataRow dr)
		{
			AuctionDetails ad = new AuctionDetails();

			if (dr != null)
			{
				if (dr.Table.Columns.Count != 0)
				{
					if (dr.Table.Columns.Contains("ID"))
						ad.ID = int.Parse(dr.Table.Rows[0]["ID"].ToString());
					if (dr.Table.Columns.Contains("Description"))
						ad.Description = dr.Table.Rows[0]["Description"].ToString();
					if (dr.Table.Columns.Contains("Type"))
						ad.Creator = dr.Table.Rows[0]["Type"].ToString();
					if (dr.Table.Columns.Contains("ConfirmationDeadline"))
						ad.ConfirmationDeadline = DateTime.Parse(dr.Table.Rows[0]["ConfirmationDeadline"].ToString());
					if (dr.Table.Columns.Contains("StartDateTime"))
						ad.StartDateTime = DateTime.Parse(dr.Table.Rows[0]["StartDateTime"].ToString());
					if (dr.Table.Columns.Contains("EndDateTime"))
						ad.EndDateTime = DateTime.Parse(dr.Table.Rows[0]["EndDateTime"].ToString());					
					if (dr.Table.Columns.Contains("Duration"))
						ad.Duration = dr.Table.Rows[0]["Duration"].ToString();
					if (dr.Table.Columns.Contains("Creator"))
						ad.Creator = dr.Table.Rows[0]["Creator"].ToString();
					if (dr.Table.Columns.Contains("CreatorEmail"))
						ad.CreatorEmail = dr.Table.Rows[0]["CreatorEmail"].ToString();
                    if (dr.Table.Columns.Contains("Sender"))
                        ad.Sender = dr.Table.Rows[0]["Sender"].ToString();
                    if (dr.Table.Columns.Contains("Company"))
                        ad.Company = dr.Table.Rows[0]["Company"].ToString();
				}
				else
					throw new EmptyInputException("Data row contains no columns");				
			}
			else
				throw new EmptyInputException("Data row is null.");
			return ad;
		}
	}

    public class BidDetails
    {
        private int _BuyerID;
        private int _ID;
		private string _Description;				
		private DateTime _SubmissionDeadline;		
		private string _Creator;
		private string _CreatorEmail;
        private string _Company;
        private string _Sender;

		public BidDetails()
		{
			_ID = 0;
            _BuyerID = 0;
			_Description = string.Empty;			
		}

		/// <summary>
		/// Converts a source row's data to auction details
		/// </summary>
		/// <param name="dt">Source row</param>
        public BidDetails(DataRow dr)
		{
            BidDetails ad = new BidDetails();
			ad = ConvertRow(dr);
            BuyerID = ad.BuyerID;
            ID = ad.ID;
			Description = ad.Description;
            SubmissionDeadline = ad.SubmissionDeadline;
			Creator = ad.Creator;
			CreatorEmail = ad.CreatorEmail;
            Company = ad.Company;
            Sender = ad.Sender;
		}

		/// <summary>
		/// Gets or sets auction reference no. of this auction
		/// </summary>
        /// 
        public int BuyerID
        {
            get
            {
                return _BuyerID;
            }
            set
            {
                _BuyerID = value;
            }
        }

		public int ID
		{
			get
			{
				return _ID;
			}
			set
			{
				_ID = value;
			}
		}

		/// <summary>
		/// Gets or sets description of this auction
		/// </summary>
		public string Description
		{
			get
			{
				return _Description;
			}
			set
			{
				_Description = value;
			}
		}
        
		/// <summary>
		/// Gets or sets confirmation deadline of this auction
		/// </summary>
		public DateTime SubmissionDeadline
		{
			get
			{
				return _SubmissionDeadline;
			}
			set
			{
				_SubmissionDeadline = value;
			}
		}
        
		/// <summary>
		/// Gets or sets auction creator
		/// </summary>
		public string Creator
		{
			get
			{
				return _Creator;
			}
			set
			{
				_Creator = value;
			}
		}

		/// <summary>
		/// Gets or sets auction creator's email address
		/// </summary>
		public string CreatorEmail
		{
			get
			{
				return _CreatorEmail;
			}
			set
			{
				_CreatorEmail = value;
			}
		}

        public string Company
        {
            get
            {
                return _Company;
            }
            set
            {
                _Company = value;
            }
        }

        public string Sender
        {
            get
            {
                return _Sender;
            }
            set
            {
                _Sender = value;
            }
        }

		protected BidDetails ConvertRow(DataRow dr)
		{
			BidDetails ad = new BidDetails();

			if (dr != null)
			{
				if (dr.Table.Columns.Count != 0)
				{
                    if (dr.Table.Columns.Contains("BuyerID"))
                        ad.BuyerID = int.Parse(dr.Table.Rows[0]["BuyerID"].ToString());
					if (dr.Table.Columns.Contains("ID"))
						ad.ID = int.Parse(dr.Table.Rows[0]["ID"].ToString());
					if (dr.Table.Columns.Contains("Description"))
						ad.Description = dr.Table.Rows[0]["Description"].ToString();
                    if (dr.Table.Columns.Contains("SubmissionDeadline"))
                        ad.SubmissionDeadline = DateTime.Parse(dr.Table.Rows[0]["SubmissionDeadline"].ToString());
					if (dr.Table.Columns.Contains("Creator"))
						ad.Creator = dr.Table.Rows[0]["Creator"].ToString();
					if (dr.Table.Columns.Contains("CreatorEmail"))
						ad.CreatorEmail = dr.Table.Rows[0]["CreatorEmail"].ToString();
                    if (dr.Table.Columns.Contains("Company"))
                        ad.Company = dr.Table.Rows[0]["Company"].ToString();
                    if (dr.Table.Columns.Contains("Sender"))
                        ad.Sender= dr.Table.Rows[0]["Sender"].ToString();			
				}
				else
					throw new EmptyInputException("Data row contains no columns");				
			}
			else
				throw new EmptyInputException("Data row is null.");
			return ad;
		}
    }
}
