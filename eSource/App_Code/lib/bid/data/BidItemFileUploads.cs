using System;
using System.Web;

namespace EBid.lib.bid.data
{
    public class BidItemFileUploads
    {
        private int _fileUploadId;
        private int _bidRefNo;
        private int _buyerId;
        private DateTime _dateUploaded;
        private string _originalFileName;
        private string _actualFileName;

        public BidItemFileUploads()
        {
            Initialize();
        }

        private void Initialize()
        {
            _fileUploadId = _bidRefNo = _buyerId = 0;
            //_dateUploaded = null;
            _originalFileName = _actualFileName = "";
        }

        public BidItemFileUploads(string originalFileName, string actualFileName)
        {
            Initialize();
            _originalFileName = originalFileName;
            _actualFileName = actualFileName;
        }

        public BidItemFileUploads(string originalFileName, string actualFileName, int bidrefno, int buyerid)
        {
            Initialize();
            _originalFileName = originalFileName;
            _actualFileName = actualFileName;
            _bidRefNo = bidRefNo;
            _buyerId = buyerId;
        }

        public int fileUploadId
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

        public int bidRefNo
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

        public int buyerId
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

        public DateTime dateUploaded
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

        public string originalFileName
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

        public string actualFileName
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