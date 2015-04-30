using System;
using System.Web;


/// <summary>
/// Summary description for Class1
/// </summary>

namespace EBid.lib.auction.data
{
    public class AuctionItem
    {
        private int auctionRefNo;
        private int bidRefNo;
        private Int64 prRefNo;
        private string requestor;
        private string itemDescription;
        private string deadline;
        private string dateCreated;
        private int buyerId;
        private int approvedBy;
        private int groupDeptSec;
        private string category;
        private string subcategory;
        private int auctionStatus;
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
        private string auctionStartDate;
        private string auctionStartDateMonth;
        private string auctionStartDateDay;
        private string auctionStartDateYear;
        private string auctionStartTime;
        private string auctionStartTimeHour;
        private string auctionStartTimeMin;
        private string auctionStartTimeSec;
        private string auctionStartTimeAMPM;
        private string auctionEndDate;
        private string auctionEndDateMonth;
        private string auctionEndDateDay;
        private string auctionEndDateYear;
        private string auctionEndTime;
        private string auctionEndTimeHour;
        private string auctionEndTimeMin;
        private string auctionEndTimeSec;
        private string auctionEndTimeAMPM;
        private string auctionDeadline;
        private string auctionDeadlineMonth;
        private string auctionDeadlineDay;
        private string auctionDeadlineYear;
        private string auctionType;
        private string bidCurrency;
        private string incrementdecrement;

        public AuctionItem()
        {
            //
            // TODO: Add constructor logic here
            //
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

        public Int64 PRRefNo
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

        
        public string IncrementDecrement
        {
            get
            {
                return incrementdecrement;
            }
            set
            {
                incrementdecrement = value;
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

        public string SubCategory
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

        public int AuctionStatus
        {
            get
            {
                return auctionStatus;
            }
            set
            {
                auctionStatus = value;
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
                actualfilenames = value;
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

        public string AuctionStartDate
        {
            get
            {
                return auctionStartDate;
            }
            set
            {
                auctionStartDate = value;
            }
        }


        public string AuctionStartMonth
        {
            get
            {
                return auctionStartDateMonth;
            }
            set
            {
                auctionStartDateMonth = value;
            }
        }

        public string AuctionStartDay
        {
            get
            {
                return auctionStartDateDay;
            }
            set
            {
                auctionStartDateDay = value;
            }
        }

        public string AuctionStartYear
        {
            get
            {
                return auctionStartDateYear;
            }
            set
            {
                auctionStartDateYear = value;
            }
        }

        public string AuctionEndDate
        {
            get
            {
                return auctionEndDate;
            }
            set
            {
                auctionEndDate = value;
            }
        }


        public string AuctionEndMonth
        {
            get
            {
                return auctionEndDateMonth;
            }
            set
            {
                auctionEndDateMonth = value;
            }
        }

        public string AuctionEndYear
        {
            get
            {
                return auctionEndDateYear;
            }
            set
            {
                auctionEndDateYear = value;
            }
        }

        public string AuctionEndDay
        {
            get
            {
                return auctionEndDateDay;
            }
            set
            {
                auctionEndDateDay = value;
            }
        }


        public string AuctionDeadline
        {
            get
            {
                return auctionDeadline;
            }
            set
            {
                auctionDeadline = value;
            }
        }
        public string AuctionDeadlineMonth
        {
            get
            {
                return auctionDeadlineMonth;
            }
            set
            {
                auctionDeadlineMonth = value;
            }
        }

        public string AuctionDeadlineDay
        {
            get
            {
                return auctionDeadlineDay;
            }
            set
            {
                auctionDeadlineDay = value;
            }
        }

        public string AuctionDeadlineYear
        {
            get
            {
                return auctionDeadlineYear;
            }
            set
            {
                auctionDeadlineYear = value;
            }
        }

        public string AuctionStartTime
        {
            get
            {
                return auctionStartTime;
            }
            set
            {
                auctionStartTime = value;
            }
        }
        public string AuctionStartTimeHour
        {
            get
            {
                return auctionStartTimeHour;
            }
            set
            {
                auctionStartTimeHour = value;
            }
        }

        public string AuctionStartTimeMin
        {
            get
            {
                return auctionStartTimeMin;
            }
            set
            {
                auctionStartTimeMin = value;
            }
        }

        public string AuctionStartTimeSec
        {
            get
            {
                return auctionStartTimeSec;
            }
            set
            {
                auctionStartTimeSec = value;
            }
        }

        public string AuctionStartTimeAMPM
        {
            get
            {
                return auctionStartTimeAMPM;
            }
            set
            {
                auctionStartTimeAMPM = value;
            }
        }

        public string AuctionEndTime
        {
            get
            {
                return auctionEndTime;
            }
            set
            {
                auctionEndTime = value;
            }
        }

        public string AuctionEndTimeHour
        {
            get
            {
                return auctionEndTimeHour;
            }
            set
            {
                auctionEndTimeHour = value;
            }
        }

        public string AuctionEndTimeMin
        {
            get
            {
                return auctionEndTimeMin;
            }
            set
            {
                auctionEndTimeMin = value;
            }
        }

        public string AuctionEndTimeSec
        {
            get
            {
                return auctionEndTimeSec;
            }
            set
            {
                auctionEndTimeSec = value;
            }
        }

        public string AuctionEndTimeAMPM
        {
            get
            {
                return auctionEndTimeAMPM;
            }
            set
            {
                auctionEndTimeAMPM = value;
            }
        }


        public string AuctionType
        {
            get
            {
                return auctionType;
            }
            set
            {
                auctionType = value;
            }
        }

        public string BidCurrency
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
