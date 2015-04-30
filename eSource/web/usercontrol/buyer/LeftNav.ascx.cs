using System;
using System.Web;
using EBid.ConnectionString;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Linq;

public partial class LeftNav : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Write(HyperLink1.NavigateUrl.ToString());
        string currPage = "";
        string url = HttpContext.Current.Request.Url.AbsoluteUri;
        currPage = url.Substring(url.LastIndexOf("/")).Replace("/","");


        string collapseThis = "";
        collapseThis = "\n<script type=\"text/javascript\">\n";
        collapseThis = collapseThis + "$(document).ready(function () {\n";
        
        string[] accrdn_Bid =  {
         (HyperLink1.NavigateUrl).Substring((HyperLink1.NavigateUrl).LastIndexOf("/")).Replace("/", ""), 
         (HyperLink10.NavigateUrl).Substring((HyperLink10.NavigateUrl).LastIndexOf("/")).Replace("/", ""), 
         (HyperLink5.NavigateUrl).Substring((HyperLink5.NavigateUrl).LastIndexOf("/")).Replace("/", ""), 
        };
        if (accrdn_Bid.Contains(currPage))
        {
            collapseThis = collapseThis + "     $('#accrdn-Bid').click();\n";
        }
        
        
        string[] accrdn_BidStat =  {
         (HyperLink12.NavigateUrl).Substring((HyperLink12.NavigateUrl).LastIndexOf("/")).Replace("/", ""), 
         (HyperLink13.NavigateUrl).Substring((HyperLink13.NavigateUrl).LastIndexOf("/")).Replace("/", ""), 
         (HyperLink14.NavigateUrl).Substring((HyperLink14.NavigateUrl).LastIndexOf("/")).Replace("/", ""), 
         (HyperLink15.NavigateUrl).Substring((HyperLink15.NavigateUrl).LastIndexOf("/")).Replace("/", ""), 
         (HyperLink16.NavigateUrl).Substring((HyperLink16.NavigateUrl).LastIndexOf("/")).Replace("/", ""), 
         (HyperLink17.NavigateUrl).Substring((HyperLink17.NavigateUrl).LastIndexOf("/")).Replace("/", ""),
        };
        if (accrdn_BidStat.Contains(currPage))
        {
            collapseThis = collapseThis + "     $('#accrdn-BidStat').click();\n";
        }
        

        string[] accrdn_Tenders =  {
         (HyperLink6.NavigateUrl).Substring((HyperLink6.NavigateUrl).LastIndexOf("/")).Replace("/", ""), 
         (HyperLink11.NavigateUrl).Substring((HyperLink11.NavigateUrl).LastIndexOf("/")).Replace("/", ""), 
         (HyperLink9.NavigateUrl).Substring((HyperLink9.NavigateUrl).LastIndexOf("/")).Replace("/", ""), 
        };
        if (accrdn_Tenders.Contains(currPage))
        {
            collapseThis = collapseThis + "     $('#accrdn-Tenders').click();\n";
        }


        string[] accrdn_Auction =  {
         (HyperLink28.NavigateUrl).Substring((HyperLink28.NavigateUrl).LastIndexOf("/")).Replace("/", ""), 
         (HyperLink32.NavigateUrl).Substring((HyperLink32.NavigateUrl).LastIndexOf("/")).Replace("/", ""), 
         (HyperLink33.NavigateUrl).Substring((HyperLink33.NavigateUrl).LastIndexOf("/")).Replace("/", ""),  
         (HyperLink34.NavigateUrl).Substring((HyperLink34.NavigateUrl).LastIndexOf("/")).Replace("/", ""),
        };
        if (accrdn_Auction.Contains(currPage))
        {
            collapseThis = collapseThis + "     $('#accrdn-Auction').click();\n";
        }


        string[] accrdn_AuctionStat =  {
         (HyperLink29.NavigateUrl).Substring((HyperLink29.NavigateUrl).LastIndexOf("/")).Replace("/", ""), 
         (HyperLink30.NavigateUrl).Substring((HyperLink30.NavigateUrl).LastIndexOf("/")).Replace("/", ""), 
         (HyperLink31.NavigateUrl).Substring((HyperLink31.NavigateUrl).LastIndexOf("/")).Replace("/", ""),  
         (HyperLink7.NavigateUrl).Substring((HyperLink7.NavigateUrl).LastIndexOf("/")).Replace("/", ""),  
         (HyperLink8.NavigateUrl).Substring((HyperLink8.NavigateUrl).LastIndexOf("/")).Replace("/", ""),
        };
        if (accrdn_AuctionStat.Contains(currPage))
        {
            collapseThis = collapseThis + "     $('#accrdn-AuctionStat').click();\n";
        }


        string[] accrdn_AuctionEvents =  {
         (HyperLink21.NavigateUrl).Substring((HyperLink21.NavigateUrl).LastIndexOf("/")).Replace("/", ""), 
         (HyperLink22.NavigateUrl).Substring((HyperLink22.NavigateUrl).LastIndexOf("/")).Replace("/", ""), 
         (HyperLink23.NavigateUrl).Substring((HyperLink23.NavigateUrl).LastIndexOf("/")).Replace("/", ""),  
        };
        if (accrdn_AuctionEvents.Contains(currPage))
        {
            collapseThis = collapseThis + "     $('#accrdn-AuctionEvents').click();\n";
        }


        string[] accrdn_Sup =  {
         (HyperLink24.NavigateUrl).Substring((HyperLink24.NavigateUrl).LastIndexOf("/")).Replace("/", ""), 
         (HyperLink25.NavigateUrl).Substring((HyperLink25.NavigateUrl).LastIndexOf("/")).Replace("/", ""),   
        };
        if (accrdn_Sup.Contains(currPage))
        {
            collapseThis = collapseThis + "     $('#accrdn-Sup').click();\n";
        }


        string[] accrdn_Rep =  {
         (HyperLink26.NavigateUrl).Substring((HyperLink26.NavigateUrl).LastIndexOf("/")).Replace("/", ""), 
         (HyperLink27.NavigateUrl).Substring((HyperLink27.NavigateUrl).LastIndexOf("/")).Replace("/", ""),   
        };
        if (accrdn_Rep.Contains(currPage))
        {
            collapseThis = collapseThis + "     $('#accrdn-Rep').click();\n";
        }


        collapseThis = collapseThis + "});\n";
        collapseThis = collapseThis + "</script>\n";
        
        Label1.Text = collapseThis;
    }
}
