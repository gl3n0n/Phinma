using System;
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

/// <summary>
/// Summary description for TypeOfPlanTransaction
/// </summary>
/// 

namespace EBid.lib.bid.trans
{
    public class TypeOfPlanTransaction
    {
        private static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["EBidConnectionString"].ConnectionString;

       

        public DataTable QueryTypeOfPlan()
        {
            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryTypeOfPlan").Tables[0];
        }
    }
}
