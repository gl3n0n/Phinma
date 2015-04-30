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
using System.Data.Sql;
using System.Data.SqlClient;
using EBid.ConnectionString;

/// <summary>
/// Created By: GA S. 10162006
/// </summary>
namespace EBid.lib
{
    public static class BidTransaction
    {
        #region Bid Events
        #endregion

        #region Bid Tenders
        public static bool UpdateBidTenderStatus(string connstring, Int32 bidtenderid, int bidstatus)
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);
            SqlTransaction sqlTransact = null;
            bool isSuccessful = false;

            try
            {
                sqlConnect.Open();
                sqlTransact = sqlConnect.BeginTransaction();

                SqlParameter[] sqlparams = new SqlParameter[4];
                sqlparams[0] = new SqlParameter("@BidTenderNo", SqlDbType.Int);
                sqlparams[0].Value = bidtenderid;
                sqlparams[1] = new SqlParameter("@Status", SqlDbType.Int);
                sqlparams[1].Value = bidstatus;

                SqlHelper.ExecuteNonQuery(sqlTransact, CommandType.StoredProcedure, "sp_UpdateBidTenderStatus", sqlparams);
                sqlTransact.Commit();
                isSuccessful = true;
            }
            catch
            {
                sqlTransact.Rollback();
                isSuccessful = false;
            }
            finally
            {
                sqlConnect.Close();
            }
            return isSuccessful;
        }

        public static bool UpdateAsClarifiedStatus(string connstring, Int32 bidtenderid, int status)
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);
            SqlTransaction sqlTransact = null;
            bool isSuccessful = false;

            try
            {
                sqlConnect.Open();
                sqlTransact = sqlConnect.BeginTransaction();

                SqlParameter[] sqlparams = new SqlParameter[2];
                sqlparams[0] = new SqlParameter("@BidTenderNo", SqlDbType.Int);
                sqlparams[0].Value = bidtenderid;
                sqlparams[1] = new SqlParameter("@Status", SqlDbType.Int);
                sqlparams[1].Value = status;

                SqlHelper.ExecuteNonQuery(sqlTransact, CommandType.StoredProcedure, "sp_UpdateAsClarifiedStatus", sqlparams);
                sqlTransact.Commit();
                isSuccessful = true;
            }
            catch
            {
                sqlTransact.Rollback();
                isSuccessful = false;
            }
            finally
            {
                sqlConnect.Close();
            }
            return isSuccessful;
        }

        public static bool UpdateBidTenderRenegotiationStatus(string connstring, Int32 bidtenderid, int bidrenegotiationstatus)
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);
            SqlTransaction sqlTransact = null;
            bool isSuccessful = false;

            try
            {
                sqlConnect.Open();
                sqlTransact = sqlConnect.BeginTransaction();

                SqlParameter[] sqlparams = new SqlParameter[2];
                sqlparams[0] = new SqlParameter("@BidTenderNo", SqlDbType.Int);
                sqlparams[0].Value = bidtenderid;
                sqlparams[1] = new SqlParameter("@Status", SqlDbType.Int);
                sqlparams[1].Value = bidrenegotiationstatus;

                SqlHelper.ExecuteNonQuery(sqlTransact, CommandType.StoredProcedure, "sp_UpdateBidTenderRenegotiationStatus", sqlparams);
                sqlTransact.Commit();
                isSuccessful = true;
            }
            catch
            {
                sqlTransact.Rollback();
                isSuccessful = false;
            }
            finally
            {
                sqlConnect.Close();
            }
            return isSuccessful;
        }

        /// <summary>
        /// Save bid tender comment
        /// </summary>
        /// <param name="bidTenderNo"></param>
        /// <param name="userId"></param>
        /// <param name="comment"></param>
        /// <param name="commentType">VB: Vendor to Buyer; BV:Buyer to Vendor; BP: Buyer to Purchasing; PB: Purchasing to Buyer;</param>
        /// <returns>If successful, returns true; otherwise, false.</returns>
        public static bool SaveBidTenderComment(string connstring, int bidTenderNo, int userId, string comment, string commentType)
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);
            SqlTransaction sqlTransact = null;
            bool isSuccessful = false;

            try
            {
                sqlConnect.Open();
                sqlTransact = sqlConnect.BeginTransaction();

                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@BidTenderNo", SqlDbType.Int);
                sqlParams[0].Value = bidTenderNo;
                sqlParams[1] = new SqlParameter("@UserId", SqlDbType.Int);
                sqlParams[1].Value = userId;
                sqlParams[2] = new SqlParameter("@Comment", SqlDbType.VarChar);
                sqlParams[2].Value = comment;
                sqlParams[3] = new SqlParameter("@CommentType", SqlDbType.Char, 2);
                sqlParams[3].Value = commentType.Substring(0, 2);

                SqlHelper.ExecuteNonQuery(sqlTransact, CommandType.StoredProcedure , "sp_AddBidTenderComment", sqlParams);

                sqlTransact.Commit();
                isSuccessful = true;
            }
            catch
            {
                sqlTransact.Rollback();
                isSuccessful = false;
            }
            finally
            {
                sqlConnect.Close();
            }
            return isSuccessful;
        }
        #endregion
    }

    #region Classes
    public class BidTender
    {
        string _bidtenderno = string.Empty;
        string _biddetailno = string.Empty;
        string _vendorid = string.Empty;
        string _status = string.Empty;
        string _amount = string.Empty;
        string _deliverycost = string.Empty;
        string _discount = string.Empty;
        string _warranty = string.Empty;
        string _remarks = string.Empty;
        string _comment = string.Empty;
        string _incoterm = string.Empty;
        string _paymentTerms = string.Empty;
        string _currency = string.Empty;
        string _leadtime = string.Empty;


        public string BidTenderNo
        {
            get { return _bidtenderno; }
            set { _bidtenderno = value; }
        }

        public string BidDetailNo
        {
            get { return _biddetailno; }
            set { _biddetailno = value; }
        }

        public string VendorId
        {
            get { return _vendorid; }
            set { _vendorid = value; }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public string Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public string DeliveryCost
        {
            get { return _deliverycost; }
            set { _deliverycost = value; }
        }

        public string Discount
        {
            get { return _discount; }
            set { _discount = value; }
        }

        public string Warranty
        {
            get { return _warranty; }
            set { _warranty = value; }
        }

        public string Remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        }

        public string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }

        public string Incoterm
        {
            get { return _incoterm; }
            set { _incoterm = value; }
        }

        public string PaymentTerms
        {
            get { return _paymentTerms; }
            set { _paymentTerms = value; }
        }

        public string Currency
        {
            get { return _currency; }
            set { _currency = value; }
        }

        public string LeadTime
        {
            get { return _leadtime; }
            set { _leadtime = value; }
        }
    }

    public class BidTenderAttachment
    {
        string _bidrefno = string.Empty;
        string _vendorid = string.Empty;
        string _originalfilename = string.Empty;
        string _actualfilename = string.Empty;

        public string BidRefNo
        {
            get { return _bidrefno; }
            set { _bidrefno = value; }
        }

        public string VendorId
        {
            get { return _vendorid; }
            set { _vendorid = value; }
        }

        public string OriginalFileName
        {
            get { return _originalfilename; }
            set { _originalfilename = value; }
        }

        public string ActualFileName
        {
            get { return _actualfilename; }
            set { _actualfilename = value; }
        }
    }
    #endregion
}
