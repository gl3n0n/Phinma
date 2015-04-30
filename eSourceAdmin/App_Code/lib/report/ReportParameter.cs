using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;

namespace EBid.lib.report
{
    public class AwardedBidItemsReportParameter
    {
        #region Variables
        private DateTime _startDate;
        private DateTime _endDate;
        private ArrayList _categories;
        private ArrayList _companies;
        private ArrayList _vendors;
        private ArrayList _items;
        #endregion

        #region Constructor
        public AwardedBidItemsReportParameter()
        {
            _startDate = DateTime.Now;
            _endDate = DateTime.Now;
            _categories = new ArrayList();
            _companies = new ArrayList();
            _vendors = new ArrayList();
            _items = new ArrayList();
        }
        #endregion

        #region Properties
        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        public ArrayList Categories
        {
            get { return _categories; }
            set { _categories = value; }
        }        

        public ArrayList Companies
        {
            get { return _companies; }
            set { _companies = value; }
        }

        public ArrayList Vendors
        {
            get { return _vendors; }
            set { _vendors = value; }
        }

        public ArrayList Items
        {
            get { return _items; }
            set { _items = value; }
        }
        #endregion

        #region Methods
        public void AddCategory(string category)
        {
            if (!_categories.Contains(category))
                _categories.Add(category);
        }

        public void AddCompany(string company)
        {
            if (!_companies.Contains(company))
                _companies.Add(company);
        }

        public void AddVendor(string vendor)
        {
            if (!_vendors.Contains(vendor))
                _vendors.Add(vendor);
        }

        public void AddItem(string item)
        {
            if (!_items.Contains(item))
                _items.Add(item);
        }

        public void RemoveCategory(string category)
        {
            _categories.Remove(category);
        }

        public void RemoveCompany(string company)
        {
            _companies.Remove(company);
        }

        public void RemoveVendor(string vendor)
        {            
            _vendors.Remove(vendor);
        }

        public void RemoveItem(string item)
        {
            _items.Remove(item);
        }
        #endregion
    }

    public class AuctionBidHistoryReportParameter
    {
        #region Variables
        private string _auctionEvent;
        private bool _isExternal;
        #endregion

        #region Constructor
        public AuctionBidHistoryReportParameter()
        {
            _isExternal = false;
            _auctionEvent = "0";
        }
        #endregion

        #region Properties
        public string AuctionEvent
        {
            get { return _auctionEvent; }
            set { _auctionEvent = value; }
        }

        public bool IsExternal
        {
            get { return _isExternal; }
            set { _isExternal = value; }
        }
        #endregion
    }

    public class BidSavingsReportParameter
    {
        #region Variables
        private ArrayList _bidEvents;
        private bool _isExternal;
        #endregion

        #region Constructor
        public BidSavingsReportParameter()
        {
            _isExternal = false;
            _bidEvents = new ArrayList();
        }
        #endregion

        #region Properties
        public ArrayList BidEvents
        {
            get { return _bidEvents; }
            set { _bidEvents = value; }
        }

        public bool IsExternal
        {
            get { return _isExternal; }
            set { _isExternal = value; }
        }
        #endregion

        #region Methods
        public void AddBidEvent(string bidEvent)
        {
            if (!_bidEvents.Contains(bidEvent))
                _bidEvents.Add(bidEvent);
        }

        public void RemoveBidEvent(string bidEvent)
        {
            _bidEvents.Remove(bidEvent);
        }
        #endregion
    }

    public class AuctionSavingsReportParameter
    {
        #region Variables
        private ArrayList _auctionEvents;
        private bool _isExternal;
        #endregion

        #region Constructor
        public AuctionSavingsReportParameter()
        {
            _isExternal = false;
            _auctionEvents = new ArrayList();
        }
        #endregion

        #region Properties
        public ArrayList AuctionEvents
        {
            get { return _auctionEvents; }
            set { _auctionEvents = value; }
        }

        public bool IsExternal
        {
            get { return _isExternal; }
            set { _isExternal = value; }
        }
        #endregion

        #region Methods
        public void AddAuctionEvent(string auctionEvent)
        {
            if (!_auctionEvents.Contains(auctionEvent))
                _auctionEvents.Add(auctionEvent);
        }

        public void RemoveAuctionEvent(string auctionEvent)
        {
            _auctionEvents.Remove(auctionEvent);
        }
        #endregion
    }

    public class TotalBidEventsReportParameter
    {
        #region Variables
        private DateTime _startDate;
        private DateTime _endDate;
        private ArrayList _buyers;
        private ArrayList _companies;
        #endregion

        #region Constructor
        public TotalBidEventsReportParameter()
        {
            _startDate = DateTime.Now;
            _endDate = DateTime.Now;
            _buyers = new ArrayList();
            _companies = new ArrayList();
        }
        #endregion

        #region Properties
        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        public ArrayList Buyers
        {
            get { return _buyers; }
            set { _buyers = value; }
        }

        public ArrayList Companies
        {
            get { return _companies; }
            set { _companies = value; }
        }
        #endregion

        #region Methods
        public void AddBuyer(string buyer)
        {
            if (!_buyers.Contains(buyer))
                _buyers.Add(buyer);
        }

        public void AddCompany(string company)
        {
            if (!_companies.Contains(company))
                _companies.Add(company);
        }

        public void RemoveBuyer(string buyer)
        {
            _buyers.Remove(buyer);
        }

        public void RemoveCompany(string company)
        {
            _companies.Remove(company);
        }
        #endregion
    }

    public class VendorListReportParameter
    {
        #region Variables
        private DateTime _startDate;
        private DateTime _endDate;
        #endregion

        #region Constructor
        public VendorListReportParameter()
        {
        }
        #endregion

        #region Properties
        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }
        #endregion
    }

    public class TotalBidsReportParameter
    {
        #region Variables
        private DateTime _startDate;
        private DateTime _endDate;
        #endregion

        #region Constructor
        public TotalBidsReportParameter()
        {
        }
        #endregion

        #region Properties
        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }
        #endregion
    }

}