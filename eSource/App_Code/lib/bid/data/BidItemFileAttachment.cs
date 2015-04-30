using System;
using System.Web;

namespace EBid.lib.bid.data
{
    public class BidItemFileAttachment
    {
        private int _fileUploadId;
        private int _bidRefNo;
        private int _buyerId;
        private DateTime _dateUploaded;
        private string _originalFileName;
        private string _actualFileName;

        public BidItemFileAttachment()
        {
            Initialize();
        }

        private void Initialize()
        {
            _fileUploadId = _bidRefNo = _buyerId = 0;
            //_dateUploaded = null;
            _originalFileName = _actualFileName = "";
        }

        public BidItemFileAttachment(string originalFileName, string actualFileName)
        {
            Initialize();
            _originalFileName = originalFileName;
            _actualFileName = actualFileName;
        }

        public BidItemFileAttachment(string originalFileName, string actualFileName, int bidrefno, int buyerid)
        {
            Initialize();
            _originalFileName = OriginalFileName;
            _actualFileName = ActualFileName;
            _bidRefNo = bidrefno;
            _buyerId = buyerid;
        }

        public int FileUploadId
        {
            get
            {
                return _fileUploadId;
            }
            set
            {
                _fileUploadId = value;
            }
        }

        public int BidRefNo
        {
            get
            {
                return _bidRefNo;
            }
            set
            {
                _bidRefNo = value;
            }
        }

        public int BuyerId
        {
            get
            {
                return _buyerId;
            }
            set
            {
                _buyerId = value;
            }
        }

        public DateTime DateUploaded
        {
            get
            {
                return _dateUploaded;
            }
            set
            {
                _dateUploaded = value;
            }
        }

        public string OriginalFileName
        {
            get
            {
                return _originalFileName;
            }
            set
            {
                _originalFileName = value;
            }
        }

        public string ActualFileName
        {
            get
            {
                return _actualFileName;
            }
            set
            {
                _actualFileName = value;
            }
        }
    }
}