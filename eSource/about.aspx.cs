using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EBid.lib;
using EBid.lib.constant;

public partial class about : System.Web.UI.Page
{
	protected void Page_PreInit(object sender, EventArgs e)
	{
		if (!IsPostBack)
            Title = String.Format(Constant.TITLEFORMAT, "About");
	}

	protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
			Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }

}
