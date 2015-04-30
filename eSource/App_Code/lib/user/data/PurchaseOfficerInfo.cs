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
using EBid.Exceptions;

/// <summary>
/// By Eric 09262006
/// </summary>
public class PurchaseOfficerInfo
{
    private int _PurchasingID;
    private string _Supervisor;
    private string _EmailAdd;

	public PurchaseOfficerInfo()
	{
        _PurchasingID = 0;
	}

    public PurchaseOfficerInfo(DataRow dr)
	{
        PurchaseOfficerInfo ad = new PurchaseOfficerInfo();
		ad = ConvertRow(dr);
        PurchasingID = ad.PurchasingID;
        Supervisor = ad.Supervisor;
        EmailAdd = ad.EmailAdd;
	}

    public int PurchasingID
    {
        get
        {
            return _PurchasingID;
        }
        set
        {
            _PurchasingID = value;
        }
    }

    public string Supervisor
    {
        get
        {
            return _Supervisor;
        }
        set
        {
            _Supervisor = value;
        }
    }

    public string EmailAdd
    {
        get
        {
            return _EmailAdd;
        }
        set
        {
            _EmailAdd = value;
        }
    }

    protected PurchaseOfficerInfo ConvertRow(DataRow dr)
    {
        PurchaseOfficerInfo ad = new PurchaseOfficerInfo();

        if (dr != null)
        {
            if (dr.Table.Columns.Count != 0)
            {
                if (dr.Table.Columns.Contains("PurchasingID"))
                    ad.PurchasingID = int.Parse(dr.Table.Rows[0]["PurchasingID"].ToString());
                if (dr.Table.Columns.Contains("Supervisor"))
                    ad.Supervisor = dr.Table.Rows[0]["Supervisor"].ToString();
                if (dr.Table.Columns.Contains("EmailAdd"))
                    ad.EmailAdd = dr.Table.Rows[0]["EmailAdd"].ToString();
            }
            else
                throw new EmptyInputException("Data row contains no columns");
        }
        else
            throw new EmptyInputException("Data row is null.");
        return ad;
    }
}
