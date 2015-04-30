using System;
using System.Configuration;
using System.Web;

namespace EBid.lib.constant
{	
	public class Constant
	{
        private static HttpContext _context = HttpContext.Current;
		public static string BLANK = string.Empty;

        public enum USERTYPE
        {
            BUYER = 1,
            VENDOR = 2,
            PURCHASING = 3,
            ADMINISTRATOR = 4,
            BIDOPENINGCOMMITTEE = 5
        }

        public enum SUPPLIERTYPE
        {
            Accredited = 1,
            Unaccredited = 2,
            OneTimeSupplier = 3
        }

        public enum COMMITTEE
        {
            Purchasing = 1,            
            Finance = 2,
            Audit = 3
        }

        public enum TASK
        {
            VIEW = 0,
            ADD = 1,
            EDIT = 2,
            DELETE = 3
        }

        public static string BIDTENDERCOMMENT_BUYER_TO_PURCHASING   = "BP";
        public static string BIDTENDERCOMMENT_BUYER_TO_VENDOR       = "BV";
        public static string BIDTENDERCOMMENT_VENDOR_TO_BUYER       = "VB";
        public static string BIDTENDERCOMMENT_PURCHASING_TO_BUYER   = "PB";

        public static string REQUEST_EDIT                           = "edit";

        public static string SESSION_LASTPAGE                       = "LastPage";
		public static string SESSION_PASSWORD                       = "Password";
        public static string SESSION_USERNAME                       = "Username";
        public static string SESSION_USERID                         = "UserId";
		public static string SESSION_USERTYPE                       = "UserType";
        public static string SESSION_USEREMAIL                      = "UserEmail";
        public static string SESSION_USERFULLNAME                   = "UserFullName";
        public static string SESSION_ISUSERAUTHENTICATED            = "IsUserAuthenticated";

		public static string SESSION_BIDSUBMITPAGE                  = "BidSubmitPage";
		public static string SESSION_BIDREFNO                       = "BidRefNo";
        public static string SESSION_BIDDETAILNO                    = "BidDetailNo";
		public static string SESSION_BIDTENDERNOS                   = "BidTenderNos";
        public static string SESSION_BIDTENDERNO                    = "BidTenderNo";
        public static string SESSION_AUCTIONREFNO                   = "AuctionRefNo";
        public static string SESSION_AUCTIONDETAILNO                = "AuctionDetailNo";
		public static string SESSION_VIEW                           = "View";
        public static string SESSION_COMMENT_TYPE                   = "CommentType";
        public static string SESSION_ONLINE_AUCTION                 = "OnlineAuctionRefNo";
        public static string SESSION_CONTENTID                      = "ContentId";

        public static string QS_BIDREFNO                            = "brn";
        public static string QS_BIDDETAILNO                         = "bdn";
        public static string QS_BIDTENDERNO                         = "btn";
        public static string QS_AUCTIONREFNO                        = "arn";
        public static string QS_USERID                              = "uid";
        public static string QS_USERTYPE                            = "ut";
        public static string QS_CATEGORYID                          = "cid";
        public static string QS_SUBCATEGORYID                       = "scid";
        public static string QS_SEARCHTEXT                          = "st";
        public static string QS_CONTROLID                           = "ctrlid";
        public static string QS_TASKTYPE                            = "t";

		public static int BID_TENDER_STATUS_DRAFT                   = 0;    // after vendor created draft tender
		public static int BID_TENDER_STATUS_SUBMITTED               = 1;    // after vendor submitted bid tender to buyer
        public static int BID_TENDER_STATUS_ENDORSED                = 2;    // after buyer endorsed the bid tender to purchasing officer
		public static int BID_TENDER_STATUS_RE_NEGOTIATE            = 3;    // after purchasing officer sends the bid tender back to buyer for renegotiation
		public static int BID_TENDER_STATUS_AWARD                   = 4;    // after purchasing officer awards/approves bid tender 
        public static int BID_TENDER_STATUS_NOT_AWARDED             = 5;    //
        public static int BID_TENDER_STATUS_CONVERTED               = 6;    // after bid tender was converted
        public static int BID_TENDER_STATUS_WAIT_FOR_RE_NEGOTIATE   = 7;    // after buyer sends the bid tender back to vendor

        public static int BID_STATUS_DRAFT                          = 0;    // after buyer created draft bid event
        public static int BID_STATUS_SUBMITTED                      = 1;    // after buyer submitted bid event for approval from purchasing officer, if bid event is more than BIDLIMIT
        public static int BID_STATUS_REJECTED                       = 2;    // after purchasing officer
        public static int BID_STATUS_RE_EDIT                        = 3;    // 
        public static int BID_STATUS_APPROVED                       = 4;    // after purchasing officer approved bid event, if estimated total value >500K and <2M
                                                                            // after buyer submitted bid event,if estimated total value <500K
        public static int BID_STATUS_ENDORSED                       = 5;    // 
        public static int BID_STATUS_RE_NEGOTIATE                   = 6;    // switched renegotiate to 3 from 5 
        public static int BID_STATUS_DECLINE                        = 7;    // switched renegotiate to 3 from 5 
        public static int BID_STATUS_CANCELLED                      = 8;    // 


        public static class BIDEVENT_STATUS
        {
            public static class STATUS
            {
                public static int DRAFTED = 0;
                public static int SUBMITTED = 1;
                public static int REJECTED = 2;
                public static int FOR_RE_EDIT = 3;
                public static int APPROVED = 4;
            }
        }

        // Added By: GA S 10232006
        public static class BIDITEM_STATUS
        {
            public static class CONVERSION_STATUS
            {
                public static int NONE = 0;
                public static int FOR_CONVERSION = 1;
                public static int APPROVED = 2;
                public static int DISAPPROVED = 3;
                public static int CONVERTED = 4;
            }

            public static class WITHDRAWAL_STATUS
            {
                public static int NONE = 0;
                public static int FOR_WITHDRAWAL = 1;
                public static int APPROVED = 2;
                public static int DISAPPROVED = 3;
                public static int WITHDRAWNED = 4;
            }
        } 
       
        // Added by: Eric 10232006
        public static class BIDTENDER_STATUS
        {
            // Updated By: GA S 10252006
            public static class STATUS
            {
                public static int DRAFTED = 0;
                public static int SUBMITTED = 1;
                public static int ENDORSED = 2;
                public static int RENEGOTIATED = 3;
                public static int AWARDED = 4;
                public static int NOT_AWARDED = 5;
                public static int WITHDRAWNED = 6;
            }

            public static class RENEGOTIATION_STATUS
            {
                public static int NONE = 0;
                public static int PURCHASING_TO_BUYER = 1;
                public static int BUYER_TO_VENDOR = 2;
                public static int VENDOR_TO_BUYER = 3;
                public static int BUYER_TO_PURCHASING = 4; 
            }
        }        

        public static int BID_INVITATION_STATUS_CONFIRM             = 1;
        public static int BID_INVITATION_STATUS_DECLINE             = 2;

        public static string DRAFT          = "Draft";
        public static string SUBMITTED      = "Submitted";
        public static string REJECTED       = "Rejected";
        public static string RE_NEGOTIATE   = "Re-negotiate";
        public static string RE_EDIT        = "Re-edit";
        public static string APPROVED       = "Approved";

        public static int TRUE  = 1;
        public static int FALSE = 0;

        //File Attachment
        //public const string FileAttachmentsDirectory = "..//FileAttachments";
        public const string CtrDirectory = "C://Temp//Counters";
        public const string HEADER_TYPE_CHECKBOX = "CHECKBOX";
        public const string HEADER_TYPE_LINKBUTTON = "LINKBUTTON";
        public const string HEADER_TYPE_LABEL = "LABEL";

		// For Conversion from Bid to Auction variables (tblBidItems.ForAuction)
		public static int BID_STATUS_BID_ITEM = 0;
		public static int BID_STATUS_REQUEST_TO_CONVERT = 1;
		public static int BID_STATUS_APPROVED_FOR_AUCTION = 2;
		public static int BID_STATUS_DENIED_FOR_CONVERSION = 3;
        //item that was saved as auction item right away
        public static int AUCTION_ITEM = 4;

		// Auction Status flags (tblAuctionItems)
		public static int AUCTION_STATUS_DRAFT = 0;
		public static int AUCTION_STATUS_SUBMITTED = 1;
		public static int AUCTION_STATUS_REJECTED = 2;
		public static int AUCTION_STATUS_RE_EDIT = 3;
		public static int AUCTION_STATUS_APPROVED = 4;	// Also considered to be Upcoming Auctions
        public static int AUCTION_STATUS_CANCELLED = 5; // Auction Stopped/Cancelled
		public static int AUCTION_STATUS_FINISHED = 6;	// Auction Finished


		public static int AUCTION_TYPE_FORWARD      = 0;
		public static int AUCTION_TYPE_REVERSE      = 1;

        public static string AUCTION_TYPE_FORWARD_DESC = "Forward";
        public static string AUCTION_TYPE_REVERSE_DESC = "Reverse";

        //Bid Currency
        public static int USDOLLAR                  = 0;
        public static int PHILIPPINEPESO            = 1;
        
        public static string USDOLLAR_DESC          = "US Dollar";
        public static string PHILIPPINEPESO_DESC    = "Philippine Peso";

        //Item Type Flags
        public static int BIDITEM                   = 0;
        public static int AUCTIONITEM               = 1;

        public static string BIDITEMTYPE            = "Bid Item";
        public static string AUCTIONITEMTYPE        = "Auction Item";

		// tblAuctionParticipants status upon invitation
		public static int AUCTION_PARTICIPANT_STATUS_WAIT = 0;
		public static int AUCTION_PARTICIPANT_STATUS_APPROVED = 1;
		public static int AUCTION_PARTICIPANT_STATUS_DECLINED = 2;

        // tblVendorReferences types
        public const int REF_MAIN_CUSTOMERS = 1;
        public const int REF_BANKS = 2;
        public const int REF_AFFILIATE = 3;
        public const int REF_EXTERN_AUDITOR = 4;

        // Constants for tblBidItem Comments
        public const int BID_COMMENT = 0;
        public const int AUCTION_COMMENT = 2;

        public static string TITLEFORMAT = ConfigurationManager.AppSettings["TitleFormat"].Trim();

        // Modified By: GA S. 09062006
        // Constants For temporary storage of data
        // Parent Temp Folder, contains daily temp folder(s)
        public static string FILEPREFIX = ConfigurationManager.AppSettings["FilePrefix"].Trim();
        public static string FILEATTACHMENTSFOLDERNAME = ConfigurationManager.AppSettings["FileAttachmentsFolder"].Trim();
        public static string FILEATTACHMENTSFOLDERDIR = _context.Server.MapPath("~\\web\\" + ConfigurationManager.AppSettings["FileAttachmentsFolder"] + "\\");
        
        public static string TEMPDIR = _context.Server.MapPath("~\\" + ConfigurationManager.AppSettings["TempFilesFolder"]);
        // Daily Temp Folder, temp files are created in this folder
        public static string DAILYTEMPDIR = Constant.TEMPDIR + "\\" +
                            (DateTime.Now.Month < 10 ? "0" + DateTime.Now.Month : DateTime.Now.Month.ToString()) +
                            (DateTime.Now.Day < 10 ? "0" + DateTime.Now.Day : DateTime.Now.Day.ToString()) +
                            DateTime.Now.Year.ToString();
        public const string TEMPBIDNAME         = "TEMPBID";
        public const string TEMPAUCTIONNAME     = "TEMPAUCTION";
        public const string TEMP_SUPPLIER_STORE = "SUPPLIER";

        public const int TYPE_INDIVIDUAL = 1;
        public const int TYPE_PARTNERSHIP = 2;
        public const int TYPE_CORPORATION = 3;

        public const int COMPANYCLASS_MANUFACTURER = 1;
        public const int COMPANYCLASS_IMPORTER = 2;
        public const int COMPANYCLASS_DEALER = 3;
        public const int COMPANYCLASS_TRADER = 4;
        public const int COMPANYCLASS_NA = 5;

        public const string SHOWSKU = "1";
        public const string HIDESKU = "2";
        //2000000.00 --limit for bids to be submitted to purchasing; if lower sent to vendor right away
        //constant is double to match with estimated values that are declared as double
        public const double BIDLIMIT = 2000000.00;

        public const double BIDLIMIT_BEFOREAPPROVAL = 2000000.00;
        public const double BIDLIMIT_BEFOREOPENING = 2000000.00;
        public const int DEFAULTPRODUCTITEM = 999999;


        public const string PARAMETER_AWARDEDBIDITEMSBYITEM = "PARAMETER_AWARDEDBIDITEMSBYITEM";
        public const string PARAMETER_AWARDEDBIDITEMSBYCATEGORY = "PARAMETER_AWARDEDBIDITEMSBYCATEGORY";
        public const string PARAMETER_BIDHISTORYBYAUCTIONITEM = "PARAMETER_BIDHISTORYBYAUCTIONITEM";
        public const string PARAMETER_SAVINGSBYAUCTIONITEM = "PARAMETER_SAVINGSBYAUCTIONITEM";

        public const string PARAMETER_SAVINGSBYBIDITEM = "PARAMETER_SAVINGSBYBIDITEM";
        public const string PARAMETER_TOTALBIDEVENTS = "PARAMETER_TOTALBIDEVENTS";
        public const string PARAMETER_TOTALAUCTIONEVENTS = "PARAMETER_TOTALAUCTIONEVENTS";

        public const string PARAMETER_VENDORLIST = "PARAMETER_VENDORLIST";
        public const string PARAMETER_TOTALBIDS = "PARAMETER_TOTALBIDS";

    }
}