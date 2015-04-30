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
using System.Data.OleDb;
using EBid.lib.utils;
using EBid.ConnectionString;

/// <summary>
/// Summary description for TypeOfPlanTransaction
/// </summary>
/// 

namespace EBid.lib.bid.trans
{
    public class TypeOfPlanTransaction
    {
        public DataTable QueryTypeOfPlan(string connstring)
        {
            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryTypeOfPlan").Tables[0];
        }
    }
}
