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
using System.Drawing;
using EBid.lib.constant;
using EBid.lib.utils;
using EBid.lib.bid.trans;
using System.IO;
using System.Text;
using EBid.ConnectionString;

/// <summary>
/// Summary description for CustomBoundField
/// </summary>
public class CustomBoundField : DataControlField
{

    
    private string connstring = "";
    
    public CustomBoundField()
    {

        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        //

        // TODO: Add constructor logic here

        //

    }
    #region Global Declarations
    ColorClass clr = new ColorClass();

    #endregion
    #region Public Properties



    /// <summary>

    /// This property describe weather the column should be an editable column or non editable column.

    /// </summary>

    public bool Editable
    {
        get
        {

            object value = base.ViewState["Editable"];

            if (value != null)
            {

                return Convert.ToBoolean(value);

            }

            else
            {

                return true;

            }

        }

        set
        {

            base.ViewState["Editable"] = value;

            this.OnFieldChanged();

        }

    }

   

    /// <summary>

    /// This property is to describe weather to display a check box or not. 

    /// This property works in association with Editable.

    /// </summary>

    public bool ShowCheckBox
    {

        get
        {

            object value = base.ViewState["ShowCheckBox"];

            if (value != null)
            {

                return Convert.ToBoolean(value);

            }

            else
            {

                return false;

            }

        }

        set
        {

            base.ViewState["ShowCheckBox"] = value;

            this.OnFieldChanged();

        }

    }

    /// <summary>

    /// This property describe column name, which acts as the primary data source for the column. 

    /// The data that is displayed in the column will be retreived from the given column name.

    /// </summary>

    public string DataField
    {

        get
        {

            object value = base.ViewState["DataField"];

            if (value != null)
            {

                return value.ToString();

            }

            else
            {

                return string.Empty;

            }

        }

        set
        {

            base.ViewState["DataField"] = value;

            this.OnFieldChanged();

        }

    }

    ///<summary>...
    /// This property describes whether a column is clickable or not
    ///</summary>

    public bool Clickable
    {

        get
        {

            object value = base.ViewState["Clickable"];

            if (value != null)
            {

                return Convert.ToBoolean(value);

            }

            else
            {

                return false;

            }

        }

        set
        {

            base.ViewState["Clickable"] = value;

            this.OnFieldChanged();

        }

    }
    /// <summary>
    ///Specifies header type 
    /// </summary>

    public string HeaderType
    {
        get
        {
            string value = base.ViewState["HeaderType"].ToString().Trim();
            return value;
        }
        set
        {
            base.ViewState["HeaderType"] = value;
            this.OnFieldChanged();
        }
    }


    /// <summary>
    ///Specifies header type 
    /// </summary>

    public string HeaderID
    {
        get
        {
            string value = base.ViewState["HeaderID"].ToString().Trim();
            return value;
        }
        set
        {
            base.ViewState["HeaderID"] = value;
            this.OnFieldChanged();
        }
    }

    /// <summary>
    ///Specifies if field is of type date or not
    /// </summary>


    public bool Date
    {
        get
        {

            object value = base.ViewState["Date"];

            if (value != null)
            {

                return Convert.ToBoolean(value);

            }

            else
            {

                return true;

            }

        }

        set
        {

            base.ViewState["Date"] = value;

            this.OnFieldChanged();

        }

    }
        /// <summary>
    /// Specifies forecolor
    /// </summary>


    
    public String ForeColor
    {
       
        get 
        {
            string value = base.ViewState["ForeColor"].ToString().Trim();
            return value;
        }
        set 
        {
            base.ViewState["ForeColor"] = value;
            this.OnFieldChanged();
        }
    }

    /// <summary>
    /// Specifies CommandName
    /// </summary>



    public String LinkCommandName
    {

        get
        {
            string value = base.ViewState["LinkCommandName"].ToString().Trim();
            return value;
        }
        set
        {
            base.ViewState["LinkCommandName"] = value;
            this.OnFieldChanged();
        }
    }

    /// <summary>
    /// Specifies CommandArgument
    /// </summary>



    public String LinkCommandArgument
    {

        get
        {
            string value = base.ViewState["LinkCommandArgument"].ToString().Trim();
            return value;
        }
        set
        {
            base.ViewState["LinkCommandArgument"] = value;
            this.OnFieldChanged();
        }
    }

    
    /// <summary>
    ///Specifies Vendor Id 
    /// </summary>

    public string VendorId
    {
        get
        {
            string value = base.ViewState["VendorId"].ToString().Trim();
            return value;
        }
        set
        {
            base.ViewState["VendorId"] = value;
            this.OnFieldChanged();
        }
    }


    public bool Label2
    {

        get
        {

            object value = base.ViewState["Label2"];

            if (value != null)
            {

                return Convert.ToBoolean(value);

            }

            else
            {

                return false;

            }

        }

        set
        {

            base.ViewState["Label2"] = value;

            this.OnFieldChanged();

        }

    }

    public bool Label3
    {

        get
        {

            object value = base.ViewState["Label3"];

            if (value != null)
            {

                return Convert.ToBoolean(value);

            }

            else
            {

                return false;

            }

        }

        set
        {

            base.ViewState["Label3"] = value;

            this.OnFieldChanged();

        }

    }

    public bool Label4
    {

        get
        {

            object value = base.ViewState["Label4"];

            if (value != null)
            {

                return Convert.ToBoolean(value);

            }

            else
            {

                return false;

            }

        }

        set
        {

            base.ViewState["Label4"] = value;

            this.OnFieldChanged();

        }

    }


    public bool TotlAmnt
    {

        get
        {

            object value = base.ViewState["TotlAmnt"];

            if (value != null)
            {

                return Convert.ToBoolean(value);

            }

            else
            {

                return false;

            }

        }

        set
        {

            base.ViewState["TotlAmnt"] = value;

            this.OnFieldChanged();

        }

    }


    public bool LabelDate
    {

        get
        {

            object value = base.ViewState["LabelDate"];

            if (value != null)
            {

                return Convert.ToBoolean(value);

            }

            else
            {

                return false;

            }

        }

        set
        {

            base.ViewState["LabelDate"] = value;

            this.OnFieldChanged();

        }

    }


    #endregion



    #region Overriden Life Cycle Methods

    /// <summary>

    /// Overriding the CreateField method is mandatory if you derive from the DataControlField.

    /// </summary>

    /// <returns></returns>

    protected override DataControlField CreateField()
    {

        return new BoundField();

    }



    /// <summary>

    /// Adds text controls to a cell's controls collection. Base method of DataControlField is

    /// called to import much of the logic that deals with header and footer rendering.

    /// </summary>

    /// <param name="cell">A reference to the cell</param>

    /// <param name="cellType">The type of the cell</param>

    /// <param name="rowState">State of the row being rendered</param>

    /// <param name="rowIndex">Index of the row being rendered</param>

    public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
    {

        //Call the base method.

        base.InitializeCell(cell, cellType, rowState, rowIndex);



        switch (cellType)
        {

            case DataControlCellType.DataCell:

                this.InitializeDataCell(cell, rowState);

                break;

            case DataControlCellType.Footer:

                this.InitializeFooterCell(cell, rowState);

                break;

            case DataControlCellType.Header:

                this.InitializeHeaderCell(cell, rowState);

                break;

        }

    }

    #endregion



    #region Custom Protected Methods

    /// <summary> 

    /// Determines which control to bind to data. In this a hyperlink control is bound regardless

    /// of the row state. The hyperlink control is then attached to a DataBinding event handler

    /// to actually retrieve and display data.

    /// 

    /// Note: This control was built with the assumption that it will not be used in a gridview

    /// control that uses inline editing. If you are building a custom data control field and 

    /// using this code for reference purposes key in mind that if your control needs to support

    /// inline editing you must determine which control to bind to data based on the row state.

    /// </summary>

    /// <param name="cell">A reference to the cell</param>

    /// <param name="rowState">State of the row being rendered</param>




    

    protected void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
    {

        //Check to see if the column is a editable and does not show the checkboxes.
        
        if (Editable)
        {
            string ID = Guid.NewGuid().ToString();
            TextBox txtBox = new TextBox();
            txtBox.Columns = 15;
            txtBox.ID = ID;
            
            txtBox.DataBinding += new EventHandler(txtBox_DataBinding);
            if (ViewState["ctr"]==null) 
                ViewState["ctr"] = 0;

            if ((ViewState["Last"] != null) || (ViewState["Discount"] != null) ||
            (ViewState["Total Cost"] != null) || (ViewState["Delivery Cost"] != null) ||
            (ViewState["Total Extended Cost"] != null) || (ViewState["Incoterm"] != null) ||
            (ViewState["Payment Terms"] != null) || (ViewState["Warranty"] != null))
            {
                
                if ((Convert.ToBoolean(ViewState["Last"]) == true) || (Convert.ToBoolean(ViewState["Discount"]) == true) ||
            (Convert.ToBoolean(ViewState["Total Cost"]) == true) || (Convert.ToBoolean(ViewState["Delivery Cost"]) == true) ||
            (Convert.ToBoolean(ViewState["Total Extended Cost"]) == true) || (Convert.ToBoolean(ViewState["Incoterm"]) == true) ||
            (Convert.ToBoolean(ViewState["Payment Terms"]) == true) || (Convert.ToBoolean(ViewState["Warranty"]) == true))
                {

                    if (ViewState["Last"] != null)
                    {
                        if (Convert.ToBoolean(ViewState["Last"]) == true)
                        {
                            //txtBox.ID = "txtDiscount";
                            //cell.Controls.Add(new LiteralControl("("));
                            txtBox.Visible = false;
                            cell.Controls.Add(txtBox);
                            //cell.Controls.Add(new LiteralControl(")"));
                            ViewState["Last"] = false;
                        }
                    }


                    if (ViewState["Discount"] != null)
                    {
                        if (Convert.ToBoolean(ViewState["Discount"]) == true)
                        {
                            //txtBox.ID = "txtTotalCost";
                            txtBox.Visible = false;
                            cell.Controls.Add(txtBox);
                            ViewState["Discount"] = false;
                        }
                    }


                    if (ViewState["Total Cost"] != null)
                    {
                        if (Convert.ToBoolean(ViewState["Total Cost"]) == true)
                        {
                            //txtBox.ID = "txtDeliveryCost";
                            txtBox.Visible = false;
                            cell.Controls.Add(txtBox);
                            ViewState["Total Cost"] = false;
                        }
                    }


                    if (ViewState["Delivery Cost"] != null)
                    {
                        if (Convert.ToBoolean(ViewState["Delivery Cost"]) == true)
                        {
                            //txtBox.ID = "txtTotalExtendedCost";
                            txtBox.Visible = false;
                            cell.Controls.Add(txtBox);
                            ViewState["Delivery Cost"] = false;
                        }
                    }


                    if (ViewState["Total Extended Cost"] != null)
                    {
                        if (Convert.ToBoolean(ViewState["Total Extended Cost"]) == true)
                        {
                            //cell.Controls.Add(txtBox);
                            //txtBox.ID = "txtIncoterm";
                            txtBox.Visible = false;
                            cell.Controls.Add(txtBox);
                            ViewState["Total Extended Cost"] = false;
                        }
                    }

                    if (ViewState["Incoterm"] != null)
                    {
                        if (Convert.ToBoolean(ViewState["Incoterm"]) == true)
                        {
                            //txtBox.ID = "txtPaymentTerms";
                            txtBox.Visible = false;
                            cell.Controls.Add(txtBox);
                            ViewState["Incoterm"] = false;
                        }
                    }


                    if (ViewState["Payment Terms"] != null)
                    {
                        if (Convert.ToBoolean(ViewState["Payment Terms"]) == true)
                        {
                            //txtBox.ID = "txtWarranty";
                            txtBox.Visible = false;
                            cell.Controls.Add(txtBox);
                            ViewState["Payment Terms"] = false;
                        }
                    }

                    if (ViewState["Warranty"] != null)
                    {
                        if (Convert.ToBoolean(ViewState["Warranty"]) == true)
                        {
                            //txtBox.ID = "txtRemarks";
                            //txtBox.Width = 200;
                            txtBox.Visible = false;
                            cell.Controls.Add(txtBox);
                            ViewState["Warranty"] = false;
                        }
                    }

                    
                   ViewState["ctr"] = Int32.Parse(ViewState["ctr"].ToString().Trim())+1;
                }
                else
                { 
                    IOClass IO = new IOClass();
                    int line = IO.GetTenderCount(ViewState["VendorId"].ToString().Trim());
                    
                    if (ViewState["ctrSet"] == null)
                    {
                        ViewState["ctr"] = 7 + line;
                        ViewState["ctrSet"] = "true";
                    }
                    //checking only 

                    switch (ViewState["ctr"].ToString().Trim())
                    {
                        case "7":
                            //txtBox.ID = "txtDiscount";
                            //txtBox.CssClass = "discount";
                            txtBox.Visible = false;
                            txtBox.Attributes.Add("onBlur", "GetAllAmounts()");
                            txtBox.Attributes.Add("onkeypress", "return(currencyFormat(this,'','.',event))");
                            txtBox.Attributes.Add("onKeyUp", "GetAllAmounts()");
                          //  cell.Controls.Add(new LiteralControl("("));
                            cell.Controls.Add(txtBox);
                          //  cell.Controls.Add(new LiteralControl(")"));
                            ViewState["RedDiscount"] = true;
                            ViewState["ItemRowHeader"] = false;
                            break;
                        case "6":
                            //txtBox.ID = "txtTotalCost";
                            //txtBox.ReadOnly = true;
                            //txtBox.ForeColor = clr.stringToColor("4D5B65");
                            txtBox.Visible = false;
                            cell.Controls.Add(txtBox);
                            ViewState["RedDiscount"] = false;
                            ViewState["ItemRowHeader"] = true;
                            break;
                        case "5":
                            //txtBox.ID = "txtDeliveryCost";
                            txtBox.Visible = false;
                            //txtBox.Attributes.Add("onBlur", "GetAllAmounts()");
                            //txtBox.Attributes.Add("onkeypress", "return(currencyFormat(this,'','.',event))");
                            //txtBox.Attributes.Add("onKeyUp", "GetAllAmounts()");
                            cell.Controls.Add(txtBox);
                            ViewState["RedDiscount"] = false;
                            ViewState["ItemRowHeader"] = false;
                            break;
                        case "4":
                            //txtBox.ID = "txtTotalExtendedCost";
                            //txtBox.ReadOnly = true;
                            //txtBox.ForeColor = clr.stringToColor("4D5B65");
                            txtBox.Visible = false;
                            cell.Controls.Add(txtBox);
                            ViewState["RedDiscount"] = false;
                            ViewState["ItemRowHeader"] = true;
                            break;
                        case "3":
                            //DropDownList ddlIncoterm = new DropDownList();
                            //IncotermTransaction i = new IncotermTransaction();
                            //ddlIncoterm.DataSource = i.GetIncoterm();
                            //ddlIncoterm.DataTextField = "Incoterm";
                            //ddlIncoterm.DataValueField = "Id";
                            //ddlIncoterm.DataBind();
                            //ddlIncoterm.DataBinding += new EventHandler(ddlIncoterm_DataBinding);
                            //ddlIncoterm.ID = "ddlIncoterm";
                            //cell.Controls.Add(ddlIncoterm);
                            //txtBox.ID = "txtIncoterm";
                            txtBox.Visible = false;
                            cell.Controls.Add(txtBox);
                            ViewState["RedDiscount"] = false;
                            ViewState["ItemRowHeader"] = false;
                            break;
                        case "2":
                            //txtBox.ID = "txtPaymentTerms";
                            txtBox.Visible = false;
                            cell.Controls.Add(txtBox);
                            ViewState["RedDiscount"] = false;
                            ViewState["ItemRowHeader"] = false;
                            break;
                        case "1":
                            //txtBox.ID = "txtWarranty";
                            //txtBox.Width = 200;
                            //txtBox.Height = 75;
                            //txtBox.TextMode = TextBoxMode.MultiLine;
                            txtBox.Visible = false;
                            cell.Controls.Add(txtBox);
                            ViewState["RedDiscount"] = false;
                            ViewState["ItemRowHeader"] = false;
                            break;
                        case "0":
                            //txtBox.ID = "txtRemarks";
                            //txtBox.Width = 200;
                            txtBox.Visible = false;
                            cell.Controls.Add(txtBox);
                            ViewState["ctrSet"] = null;
                            ViewState["RedDiscount"] = false;
                            ViewState["ItemRowHeader"] = false;
                            break;
                        default:
                            txtBox.Attributes.Add("onBlur", "GetAllAmounts()");
                            txtBox.Attributes.Add("onkeypress", "return(currencyFormat(this,'','.',event))");
                            txtBox.Attributes.Add("onKeyUp", "GetAllAmounts()");
                            txtBox.ID = "txtAmount";
                            txtBox.Attributes.Add("style", "text-align:right");
                            cell.Controls.Add(txtBox);
                            RequiredFieldValidator rfv = new RequiredFieldValidator();
                            rfv.ID = "rfvAmount";
                            rfv.ErrorMessage = "Unit Price is a required field.";
                            rfv.Display = ValidatorDisplay.None;
                            rfv.ControlToValidate = txtBox.ID;
                            rfv.SetFocusOnError = true;
                            cell.Controls.Add(rfv);
                            CompareValidator cpv = new CompareValidator();
                            cpv.ID = "cpvAmount";
                            cpv.ErrorMessage = "Unit Price should be a numeric field that has a value greater than 0.";
                            cpv.ValueToCompare = "0";
                            cpv.Type = ValidationDataType.Double;
                            cpv.Operator = ValidationCompareOperator.GreaterThan;
                            cpv.Display = ValidatorDisplay.None;
                            cpv.ControlToValidate = txtBox.ID;
                            cpv.SetFocusOnError = true;
                            cell.Controls.Add(cpv);
                            ViewState["RedDiscount"] = false;
                            ViewState["ItemRowHeader"] = false;
                            break;
                    }

                    ViewState["ctr"] = Int32.Parse(ViewState["ctr"].ToString().Trim()) - 1;
                }
                
            }
            else
            {
                txtBox.Attributes.Add("onBlur", "GetAllAmounts()");
                txtBox.Attributes.Add("onkeypress", "return(currencyFormat(this,'','.',event))");
                txtBox.Attributes.Add("onKeyUp", "GetAllAmounts()");
                txtBox.ID = "txtAmount";
                txtBox.Attributes.Add("style", "text-align:right");
                cell.Controls.Add(txtBox);
                RequiredFieldValidator rfv = new RequiredFieldValidator();
                rfv.ID="rfvAmount"; 
                rfv.ErrorMessage = "Unit Price is a required field.";
                rfv.Display = ValidatorDisplay.None; 
                rfv.ControlToValidate=txtBox.ID; 
                rfv.SetFocusOnError=true;
                cell.Controls.Add(rfv);
                CompareValidator cpv = new CompareValidator();
                cpv.ID = "cpvAmount";
                cpv.ErrorMessage = "Unit Price should be a numeric field that has a value greater than 0.";
                cpv.ValueToCompare = "0";
                cpv.Type = ValidationDataType.Double;
                cpv.Operator = ValidationCompareOperator.GreaterThan;
                cpv.Display = ValidatorDisplay.None;
                cpv.ControlToValidate = txtBox.ID;
                cpv.SetFocusOnError = true;
                cell.Controls.Add(cpv);
            }
        }
        else if (ShowCheckBox)
        {
                CheckBox chkBox = new CheckBox();
                chkBox.DataBinding += new EventHandler(chkBox_DataBinding);
                if (Convert.ToBoolean(ViewState["ItemRowHeader"]) == true)
                {
                    cell.CssClass = "itemDetails_th";
                }
                chkBox.Attributes.Add("onClick", "javascript:GetTenderNo(this);");
                cell.Controls.Add(chkBox);
        }
        else if (Clickable)
        {
            LinkButton lnkText = new LinkButton();
            lnkText.DataBinding += new EventHandler(lnkText_DataBinding);
            lnkText.CommandName = LinkCommandName;
            lnkText.CommandArgument = LinkCommandArgument;
            cell.Controls.Add(lnkText);
        }
        else if (Date)
        {
            ViewState["DateBinding"] = false;

            DropDownList ddlMonth = new DropDownList();
            OtherTransaction oth = new OtherTransaction();
            ddlMonth.DataSource = oth.GetMonth();
            ddlMonth.DataTextField = "Text";
            ddlMonth.DataValueField = "Value";
            ddlMonth.DataBind();
            ddlMonth.DataBinding += new EventHandler(ddlDate_DataBinding);
            ddlMonth.Width = 50;
            ddlMonth.ID = "ddlMonth";
            cell.Controls.Add(ddlMonth);
            TextBox txtDay = new TextBox();
            txtDay.Columns=2;
            txtDay.MaxLength = 2;
            txtDay.DataBinding += new EventHandler(txtDay_DataBinding);
            txtDay.ID = "txtDay";
            txtDay.Attributes.Add("onkeypress", "return NumberOnlyValidator(this)");
            cell.Controls.Add(txtDay);
            TextBox txtYear = new TextBox();
            txtYear.DataBinding += new EventHandler(txtYear_DataBinding);
            txtYear.Columns = 4;
            txtYear.MaxLength = 4;
            txtYear.ID = "txtYear";
            txtYear.Attributes.Add("onkeypress", "return NumberOnlyValidator(this)");
            cell.Controls.Add(txtYear);
            RequiredFieldValidator rfvDateMonth = new RequiredFieldValidator();
            rfvDateMonth.ID = "rfvDateMonth";
            rfvDateMonth.ErrorMessage = "Delivery Date (Month) is a required field.";
            rfvDateMonth.Display = ValidatorDisplay.None;
            rfvDateMonth.ControlToValidate = ddlMonth.ID;
            rfvDateMonth.SetFocusOnError = true;
            cell.Controls.Add(rfvDateMonth);
            RequiredFieldValidator rfvDateDay = new RequiredFieldValidator();
            rfvDateDay.ID = "rfvDateDay";
            rfvDateDay.ErrorMessage = "Delivery Date (Day) is a required field.";
            rfvDateDay.Display = ValidatorDisplay.None;
            rfvDateDay.ControlToValidate = txtDay.ID;
            rfvDateDay.SetFocusOnError = true;
            cell.Controls.Add(rfvDateDay);
            CustomValidator cv = new CustomValidator();
            cv.ID = "cvDay";
            cv.ClientValidationFunction = "ValidateDayByMonth(this, args);";
            cv.Display = ValidatorDisplay.None;
            cv.ControlToValidate = txtDay.ID;
            cv.ErrorMessage = "";
            cv.SetFocusOnError = true;
            cell.Controls.Add(cv);
            RequiredFieldValidator rfvDateYear = new RequiredFieldValidator();
            rfvDateYear.ID = "rfvDateYear";
            rfvDateYear.ErrorMessage = "Delivery Date (Year) is a required field.";
            rfvDateYear.Display = ValidatorDisplay.None;
            rfvDateYear.ControlToValidate = txtYear.ID;
            rfvDateYear.SetFocusOnError = true;
            cell.Controls.Add(rfvDateYear);
            RangeValidator rngYear = new RangeValidator();
            rngYear.ID="rgeYear";
            rngYear.ErrorMessage="Delivery Date (Year) should be between 1900 - 2100."; 
            rngYear.ControlToValidate= txtYear.ID;
            rngYear.Display=ValidatorDisplay.None; 
            rngYear.MaximumValue="2100"; 
            rngYear.MinimumValue="1900";
            rngYear.SetFocusOnError = true;
            cell.Controls.Add(rngYear);
            
           

            if (Convert.ToBoolean(ViewState["DateBinding"]) == false)
            {
                IOClass IO = new IOClass();
                int line = IO.GetTenderCount(ViewState["VendorId"].ToString().Trim());

                if (ViewState["ctrSetDate"] == null)
                {
                    ViewState["ctrDate"] = 7 + line;
                    ViewState["ctrSetDate"] = "true";

                }
                //checking only 

                switch (ViewState["ctrDate"].ToString().Trim())
                {
                    case "7":
                        
                        ViewState["RedDiscount"] = true;
                        ViewState["ItemRowHeader"] = false;
                        break;
                    case "6":
                        ViewState["RedDiscount"] = false;
                        ViewState["ItemRowHeader"] = true;
                        break;
                    case "4":
                        ViewState["RedDiscount"] = false;
                        ViewState["ItemRowHeader"] = true;
                        break;
                    case "0":
                        ViewState["ctrSetDate"] = null;
                        break;
                    default:
                        ViewState["RedDiscount"] = false;
                        ViewState["ItemRowHeader"] = false;
                        break;
                }

                ViewState["ctrDate"] = Int32.Parse(ViewState["ctrDate"].ToString().Trim()) - 1;


            }
        }
        else if (Label2)
        {
            ViewState["Label2Binding"] = false;
            string ID = Guid.NewGuid().ToString();
            Label lblText = new Label();
            lblText.DataBinding += new EventHandler(Label2_DataBinding);

            if (Convert.ToBoolean(ViewState["Label2Binding"]) == false)
            {
                IOClass IO = new IOClass();
                int line = IO.GetTenderCount(ViewState["VendorId"].ToString().Trim());

                if (ViewState["ctrSetl2"] == null)
                {
                    ViewState["ctrl2"] = 7 + line;
                    ViewState["ctrSetl2"] = "true";
                }
                //checking only 

                switch (ViewState["ctrl2"].ToString().Trim())
                {
                    case "7":
                        cell.Controls.Add(new LiteralControl("("));
                        cell.Controls.Add(lblText);
                        cell.Controls.Add(new LiteralControl(")"));
                        lblText.CssClass = "discount";
                        ViewState["RedDiscount"] = true;
                        ViewState["ItemRowHeader"] = false;
                        break;
                    case "6":
                        cell.Controls.Add(lblText);
                        ViewState["RedDiscount"] = false;
                        ViewState["ItemRowHeader"] = true;
                        break;
                    case "4":
                        cell.Controls.Add(lblText);
                        ViewState["RedDiscount"] = false;
                        ViewState["ItemRowHeader"] = true;
                        break;
                    case "0":
                        cell.Controls.Add(lblText);
                        ViewState["ctrSetl2"] = null;
                        break;
                    default:
                        cell.Controls.Add(lblText);
                        ViewState["RedDiscount"] = false;
                        ViewState["ItemRowHeader"] = false;
                        break;
                }

                ViewState["ctrl2"] = Int32.Parse(ViewState["ctrl2"].ToString().Trim()) - 1;
            }
            else
            {
                if ((ViewState["Last"] != null) || (ViewState["Discount"] != null) ||
            (ViewState["Total Cost"] != null) || (ViewState["Delivery Cost"] != null) ||
            (ViewState["Total Extended Cost"] != null) || (ViewState["Incoterm"] != null) ||
            (ViewState["Payment Terms"] != null) || (ViewState["Warranty"] != null))
                {

                    if ((Convert.ToBoolean(ViewState["Last"]) == true) || (Convert.ToBoolean(ViewState["Discount"]) == true) ||
                (Convert.ToBoolean(ViewState["Total Cost"]) == true) || (Convert.ToBoolean(ViewState["Delivery Cost"]) == true) ||
                (Convert.ToBoolean(ViewState["Total Extended Cost"]) == true) || (Convert.ToBoolean(ViewState["Incoterm"]) == true) ||
                (Convert.ToBoolean(ViewState["Payment Terms"]) == true) || (Convert.ToBoolean(ViewState["Warranty"]) == true))
                    {

                        if (ViewState["Last"] != null)
                        {
                            if (Convert.ToBoolean(ViewState["Last"]) == true)
                            {
                                Label lbl1 = new Label();
                                lbl1.Text = "(";
                                lbl1.CssClass = "discount";
                                cell.Controls.Add(lbl1);
                                lblText.CssClass = "discount";
                                cell.Controls.Add(lblText);
                                Label lbl2 = new Label();
                                lbl2.Text = ")";
                                lbl2.CssClass = "discount";
                                cell.Controls.Add(lbl2);
                                ViewState["Last"] = false;
                            }
                        }


                        if (ViewState["Discount"] != null)
                        {
                            if (Convert.ToBoolean(ViewState["Discount"]) == true)
                            {
                                cell.Controls.Add(lblText);
                                ViewState["Discount"] = false;
                            }
                        }


                        if (ViewState["Total Cost"] != null)
                        {
                            if (Convert.ToBoolean(ViewState["Total Cost"]) == true)
                            {
                                cell.Controls.Add(lblText);
                                ViewState["Total Cost"] = false;
                            }
                        }


                        if (ViewState["Delivery Cost"] != null)
                        {
                            if (Convert.ToBoolean(ViewState["Delivery Cost"]) == true)
                            {
                                cell.Controls.Add(lblText);
                                ViewState["Delivery Cost"] = false;
                            }
                        }


                        if (ViewState["Total Extended Cost"] != null)
                        {
                            if (Convert.ToBoolean(ViewState["Total Extended Cost"]) == true)
                            {
                                cell.Controls.Add(lblText);
                                ViewState["Total Extended Cost"] = false;
                            }
                        }

                        if (ViewState["Incoterm"] != null)
                        {
                            if (Convert.ToBoolean(ViewState["Incoterm"]) == true)
                            {
                                cell.Controls.Add(lblText);
                                ViewState["Incoterm"] = false;
                            }
                        }


                        if (ViewState["Payment Terms"] != null)
                        {
                            if (Convert.ToBoolean(ViewState["Payment Terms"]) == true)
                            {
                                cell.Controls.Add(lblText);
                                ViewState["Payment Terms"] = false;
                            }
                        }

                        if (ViewState["Warranty"] != null)
                        {
                            if (Convert.ToBoolean(ViewState["Warranty"]) == true)
                            {
                                cell.Controls.Add(lblText);
                                ViewState["Warranty"] = false;
                            }
                        }


                        ViewState["ctrl2"] = Int32.Parse(ViewState["ctrl2"].ToString().Trim()) + 1;
                    }
                    else
                    {
                        //for Remarks
                        cell.Controls.Add(lblText);
                    }
                }

            }
        }
        else if (LabelDate)
        {
            ViewState["LabelDateBinding"] = false;
            string ID = Guid.NewGuid().ToString();
            Label lblText = new Label();
            lblText.DataBinding += new EventHandler(LabelDate_DataBinding);

            if (ViewState["ctrlbDate"] == null)
                ViewState["ctrlbDate"] = 0;

            if (Convert.ToBoolean(ViewState["LabelDateBinding"]) == false)
            {
                IOClass IO = new IOClass();
                int line = IO.GetTenderCount(ViewState["VendorId"].ToString().Trim());

                if (ViewState["ctrSetlbDate"] == null)
                {
                    ViewState["ctrlbDate"] = 7 + line;
                    ViewState["ctrSetlbDate"] = "true";
                }
                //checking only 

                switch (ViewState["ctrlbDate"].ToString().Trim())
                {
                    case "7":
                        lblText.Visible = false;
                        cell.Controls.Add(lblText);
                        ViewState["RedDiscount"] = true;
                        ViewState["ItemRowHeader"] = false;
                        break;
                    case "6":
                        cell.Controls.Add(lblText);
                        ViewState["RedDiscount"] = false;
                        ViewState["ItemRowHeader"] = true;
                        break;
                    case "4":
                        cell.Controls.Add(lblText);
                        ViewState["RedDiscount"] = false;
                        ViewState["ItemRowHeader"] = true;
                        break;
                    case "0":
                        cell.Controls.Add(lblText);
                        ViewState["ctrSetlbDate"] = null;
                        break;
                    default:
                        cell.Controls.Add(lblText);
                        ViewState["RedDiscount"] = false;
                        ViewState["ItemRowHeader"] = false;
                        break;
                }

                ViewState["ctrlbDate"] = Int32.Parse(ViewState["ctrlbDate"].ToString().Trim()) - 1;

            }
            else
            {
                if ((ViewState["Last"] != null) || (ViewState["Discount"] != null) ||
            (ViewState["Total Cost"] != null) || (ViewState["Delivery Cost"] != null) ||
            (ViewState["Total Extended Cost"] != null) || (ViewState["Incoterm"] != null) ||
            (ViewState["Payment Terms"] != null) || (ViewState["Warranty"] != null))
                {

                    if ((Convert.ToBoolean(ViewState["Last"]) == true) || (Convert.ToBoolean(ViewState["Discount"]) == true) ||
                (Convert.ToBoolean(ViewState["Total Cost"]) == true) || (Convert.ToBoolean(ViewState["Delivery Cost"]) == true) ||
                (Convert.ToBoolean(ViewState["Total Extended Cost"]) == true) || (Convert.ToBoolean(ViewState["Incoterm"]) == true) ||
                (Convert.ToBoolean(ViewState["Payment Terms"]) == true) || (Convert.ToBoolean(ViewState["Warranty"]) == true))
                    {

                        if (ViewState["Last"] != null)
                        {
                            if (Convert.ToBoolean(ViewState["Last"]) == true)
                            {
                                lblText.Visible = false;
                                cell.Controls.Add(lblText);
                            }
                        }

                        if (ViewState["Discount"] != null)
                        {
                            if (Convert.ToBoolean(ViewState["Discount"]) == true)
                            {
                                cell.Controls.Add(lblText);
                                ViewState["Discount"] = false;
                            }
                        }

                        if (ViewState["Total Cost"] != null)
                        {
                            if (Convert.ToBoolean(ViewState["Total Cost"]) == true)
                            {
                                cell.Controls.Add(lblText);
                                ViewState["Total Cost"] = false;
                            }
                        }

                        if (ViewState["Delivery Cost"] != null)
                        {
                            if (Convert.ToBoolean(ViewState["Delivery Cost"]) == true)
                            {
                                cell.Controls.Add(lblText);
                                ViewState["Delivery Cost"] = false;
                            }
                        }

                        if (ViewState["Total Extended Cost"] != null)
                        {
                            if (Convert.ToBoolean(ViewState["Total Extended Cost"]) == true)
                            {
                                cell.Controls.Add(lblText);
                                ViewState["Total Extended Cost"] = false;
                            }
                        }

                        if (ViewState["Incoterm"] != null)
                        {
                            if (Convert.ToBoolean(ViewState["Incoterm"]) == true)
                            {
                                cell.Controls.Add(lblText);
                                ViewState["Incoterm"] = false;
                            }
                        }


                        if (ViewState["Payment Terms"] != null)
                        {
                            if (Convert.ToBoolean(ViewState["Payment Terms"]) == true)
                            {
                                cell.Controls.Add(lblText);
                                ViewState["Payment Terms"] = false;
                            }
                        }

                        if (ViewState["Warranty"] != null)
                        {
                            if (Convert.ToBoolean(ViewState["Warranty"]) == true)
                            {
                                cell.Controls.Add(lblText);
                                ViewState["Warranty"] = false;
                            }
                        }


                        ViewState["ctrlbDate"] = Int32.Parse(ViewState["ctrlbDate"].ToString().Trim()) + 1;
                    }
                   
                }

            }
            
        }
        else if (Label3)
        {
            Label lblText = new Label();
            lblText.Width= 150;
            lblText.DataBinding += new EventHandler(Label3_DataBinding);
            cell.Controls.Add(lblText);
        }
        else if (Label4)
        {
            ViewState["Binding"] = false;
            Label lblText = new Label();
            lblText.Width = 150;
            lblText.DataBinding += new EventHandler(lblText_DataBinding);
            cell.Controls.Add(lblText);

            if (Convert.ToBoolean(ViewState["Binding"]) == false)
            {
                IOClass IO = new IOClass();
                int line = IO.GetTenderCount(ViewState["VendorId"].ToString().Trim());

                if (ViewState["ctrSet1"] == null)
                {
                    ViewState["ctr1"] = 7 + line;
                    ViewState["ctrSet1"] = "true";

                }
                //checking only 

                switch (ViewState["ctr1"].ToString().Trim())
                {
                    case "7":
                        cell.Controls.Add(lblText);
                        ViewState["RedDiscount"] = true;
                        ViewState["ItemRowHeader"] = false;
                        break;
                    case "6":
                        cell.Controls.Add(lblText);
                        ViewState["RedDiscount"] = false;
                        ViewState["ItemRowHeader"] = true;
                        break;
                    case "4":
                        cell.Controls.Add(lblText);
                        ViewState["RedDiscount"] = false;
                        ViewState["ItemRowHeader"] = true;
                        break;
                    case "0":
                        cell.Controls.Add(lblText);
                        ViewState["ctrSet1"] = null;
                        break;
                    default:
                        cell.Controls.Add(lblText);
                        ViewState["RedDiscount"] = false;
                        ViewState["ItemRowHeader"] = false;
                        break;
                }

                ViewState["ctr1"] = Int32.Parse(ViewState["ctr1"].ToString().Trim()) - 1;

            }
        }
        else if (TotlAmnt)
        {
            string ID = Guid.NewGuid().ToString();
            TextBox txtBox = new TextBox();
            txtBox.Columns = 15;
            txtBox.ID = ID;
            

            txtBox.DataBinding += new EventHandler(txtBox_DataBinding);
            if (ViewState["ctr_total"] == null)
                ViewState["ctr_total"] = 0;

            if ((ViewState["Last"] != null) || (ViewState["Discount"] != null) ||
            (ViewState["Total Cost"] != null) || (ViewState["Delivery Cost"] != null) ||
            (ViewState["Total Extended Cost"] != null) || (ViewState["Incoterm"] != null) ||
            (ViewState["Payment Terms"] != null) || (ViewState["Warranty"] != null))
            {

                if ((Convert.ToBoolean(ViewState["Last"]) == true) || (Convert.ToBoolean(ViewState["Discount"]) == true) ||
            (Convert.ToBoolean(ViewState["Total Cost"]) == true) || (Convert.ToBoolean(ViewState["Delivery Cost"]) == true) ||
            (Convert.ToBoolean(ViewState["Total Extended Cost"]) == true) || (Convert.ToBoolean(ViewState["Incoterm"]) == true) ||
            (Convert.ToBoolean(ViewState["Payment Terms"]) == true) || (Convert.ToBoolean(ViewState["Warranty"]) == true))
                {

                    if (ViewState["Last"] != null)
                    {
                        if (Convert.ToBoolean(ViewState["Last"]) == true)
                        {
                            txtBox.ID = "txtDiscount";
                            txtBox.CssClass = "Discount";
                            txtBox.Attributes.Add("style", "text-align:right");
                            txtBox.Attributes.Add("onBlur", "GetAllAmounts()");
                            txtBox.Attributes.Add("onkeypress", "return(currencyFormat(this,'','.',event))");
                            txtBox.Attributes.Add("onKeyUp", "GetAllAmounts()");
                            //  cell.Controls.Add(new LiteralControl("("));
                            cell.Controls.Add(txtBox);
                            RequiredFieldValidator rfv = new RequiredFieldValidator();
                            rfv.ID = "rfvDiscount";
                            rfv.ErrorMessage = "Discount is a required field.";
                            rfv.Display = ValidatorDisplay.None;
                            rfv.ControlToValidate = txtBox.ID;
                            rfv.SetFocusOnError = true;
                            cell.Controls.Add(rfv);
                            CompareValidator cpv = new CompareValidator();
                            cpv.ID = "cpvDiscount";
                            cpv.ErrorMessage = "Discount should be a numeric field.";
                            cpv.Type = ValidationDataType.Double;
                            cpv.Operator = ValidationCompareOperator.DataTypeCheck;
                            cpv.Display = ValidatorDisplay.None;
                            cpv.ControlToValidate = txtBox.ID;
                            cpv.SetFocusOnError = true;
                            cell.Controls.Add(cpv);
                            cell.Attributes.Add("align", "right");
                            //  cell.Controls.Add(new LiteralControl(")"));
                            ViewState["Last"] = false;
                        }
                    }


                    if (ViewState["Discount"] != null)
                    {
                        if (Convert.ToBoolean(ViewState["Discount"]) == true)
                        {
                            txtBox.ID = "txtTotalCost";
                            txtBox.Attributes.Add("style", "text-align:right");
                            txtBox.ReadOnly = true;
                            txtBox.BorderStyle = BorderStyle.None;
                            txtBox.CssClass = "totalcosts";
                            txtBox.BackColor = clr.stringToColor("10659E");
                            cell.Controls.Add(txtBox);
                            cell.Attributes.Add("align", "right");
                            ViewState["Discount"] = false;
                        }
                    }


                    if (ViewState["Total Cost"] != null)
                    {
                        if (Convert.ToBoolean(ViewState["Total Cost"]) == true)
                        {
                            txtBox.ID = "txtDeliveryCost";
                            txtBox.Attributes.Add("style", "text-align:right");
                            txtBox.Attributes.Add("onBlur", "GetAllAmounts()");
                            txtBox.Attributes.Add("onkeypress", "return(currencyFormat(this,'','.',event))");
                            txtBox.Attributes.Add("onKeyUp", "GetAllAmounts()");
                            cell.Controls.Add(txtBox);
                            RequiredFieldValidator rfv = new RequiredFieldValidator();
                            rfv.ID = "rfvDeliveryCost";
                            rfv.ErrorMessage = "Delivery Cost is a required field.";
                            rfv.Display = ValidatorDisplay.None;
                            rfv.ControlToValidate = txtBox.ID;
                            rfv.SetFocusOnError = true;
                            cell.Controls.Add(rfv);
                            CompareValidator cpv = new CompareValidator();
                            cpv.ID = "cpvDeliveryCost";
                            cpv.ErrorMessage = "Delivery Cost should be a numeric field.";
                            cpv.Type = ValidationDataType.Double;
                            cpv.Operator = ValidationCompareOperator.DataTypeCheck;
                            cpv.Display = ValidatorDisplay.None;
                            cpv.ControlToValidate = txtBox.ID;
                            cpv.SetFocusOnError = true;
                            cell.Controls.Add(cpv);
                            cell.Attributes.Add("align", "right");
                            ViewState["Total Cost"] = false;
                        }
                    }


                    if (ViewState["Delivery Cost"] != null)
                    {
                        if (Convert.ToBoolean(ViewState["Delivery Cost"]) == true)
                        {
                            txtBox.ID = "txtTotalExtendedCost";
                            txtBox.Attributes.Add("style", "text-align:right");
                            txtBox.ReadOnly = true;
                            txtBox.BorderStyle = BorderStyle.None;
                            txtBox.CssClass = "totalcosts";
                            txtBox.BackColor = clr.stringToColor("10659E");
                            cell.Controls.Add(txtBox);
                            cell.Attributes.Add("align", "right");
                            ViewState["Delivery Cost"] = false;
                        }
                    }


                    if (ViewState["Total Extended Cost"] != null)
                    {
                        if (Convert.ToBoolean(ViewState["Total Extended Cost"]) == true)
                        {
                            //cell.Controls.Add(txtBox);
                            DropDownList ddlIncoterm = new DropDownList();
                            IncotermTransaction i = new IncotermTransaction();
                            ddlIncoterm.DataSource = i.GetIncoterm(connstring);
                            ddlIncoterm.DataTextField = "Incoterm";
                            ddlIncoterm.DataValueField = "Id";
                            ddlIncoterm.DataBind();
                            ddlIncoterm.DataBinding += new EventHandler(ddlIncoterm_DataBinding);
                            ddlIncoterm.ID = "ddlIncoterm";
                            cell.Controls.Add(ddlIncoterm);
                            txtBox.ID = "txtIncoterm";
                            txtBox.Visible = false;
                            cell.Controls.Add(txtBox);
                            CompareValidator cmpi = new CompareValidator();
                            cmpi.ID = "rfvIncoterm";
                            cmpi.ErrorMessage = "Incoterm is a required field.";
                            cmpi.ValueToCompare = "-1";
                            cmpi.Operator = ValidationCompareOperator.NotEqual;
                            cmpi.Type = ValidationDataType.Integer;
                            cmpi.Display = ValidatorDisplay.None;
                            cmpi.ControlToValidate = ddlIncoterm.ID;
                            cmpi.SetFocusOnError = true;
                            cell.Controls.Add(cmpi);
                            ViewState["Total Extended Cost"] = false;
                        }
                    }

                    if (ViewState["Incoterm"] != null)
                    {
                        if (Convert.ToBoolean(ViewState["Incoterm"]) == true)
                        {
                            txtBox.ID = "txtPaymentTerms";
                            cell.Controls.Add(txtBox);
                            RequiredFieldValidator rfv = new RequiredFieldValidator();
                            rfv.ID = "rfvPaymentTerms";
                            rfv.ErrorMessage = "Payment Terms is a required field.";
                            rfv.Display = ValidatorDisplay.None;
                            rfv.ControlToValidate = txtBox.ID;
                            rfv.SetFocusOnError = true;
                            cell.Controls.Add(rfv);
                            ViewState["Incoterm"] = false;
                        }
                    }


                    if (ViewState["Payment Terms"] != null)
                    {
                        if (Convert.ToBoolean(ViewState["Payment Terms"]) == true)
                        {
                            txtBox.ID = "txtWarranty";
                            txtBox.Width = 200;
                            txtBox.Height = 75;
                            txtBox.TextMode = TextBoxMode.MultiLine;
                            txtBox.Attributes.Add("style", "font-family:Arial; font-size:8pt;");
                            cell.Controls.Add(txtBox);
                            RequiredFieldValidator rfv = new RequiredFieldValidator();
                            rfv.ID = "rfvWarranty";
                            rfv.ErrorMessage = "Warranty is a required field.";
                            rfv.Display = ValidatorDisplay.None;
                            rfv.ControlToValidate = txtBox.ID;
                            rfv.SetFocusOnError = true;
                            cell.Controls.Add(rfv);
                            ViewState["Payment Terms"] = false;
                        }
                    }

                    if (ViewState["Warranty"] != null)
                    {
                        if (Convert.ToBoolean(ViewState["Warranty"]) == true)
                        {
                            txtBox.ID = "txtRemarks";
                            txtBox.Width = 200;
                            cell.Controls.Add(txtBox);
                            //RequiredFieldValidator rfv = new RequiredFieldValidator();
                            //rfv.ID = "rfvRemarks";
                            //rfv.ErrorMessage = "Remarks is a required field.";
                            //rfv.Display = ValidatorDisplay.None;
                            //rfv.ControlToValidate = txtBox.ID;
                            //rfv.SetFocusOnError = true;
                            //cell.Controls.Add(rfv);
                            ViewState["Warranty"] = false;
                        }
                    }


                    ViewState["ctr_total"] = Int32.Parse(ViewState["ctr_total"].ToString().Trim()) + 1;
                }
                else
                {
                    IOClass IO = new IOClass();
                    int line = IO.GetTenderCount(ViewState["VendorId"].ToString().Trim());

                    if (ViewState["ctr_totalSet"] == null)
                    {
                        ViewState["ctr_total"] = 7 + line;
                        ViewState["ctr_totalSet"] = "true";
                    }
                    //checking only 

                    switch (ViewState["ctr_total"].ToString().Trim())
                    {
                        case "7":
                            txtBox.ID = "txtDiscount";
                            txtBox.CssClass = "discount";
                            txtBox.Attributes.Add("style", "text-align:right");
                            txtBox.Attributes.Add("onBlur", "GetAllAmounts()");
                            txtBox.Attributes.Add("onkeypress", "return(currencyFormat(this,'','.',event))");
                            txtBox.Attributes.Add("onKeyUp", "GetAllAmounts()");
                            //  cell.Controls.Add(new LiteralControl("("));
                            cell.Controls.Add(txtBox);
                            RequiredFieldValidator rfv7 = new RequiredFieldValidator();
                            rfv7.ID = "rfvDiscount";
                            rfv7.ErrorMessage = "Discount is a required field.";
                            rfv7.Display = ValidatorDisplay.None;
                            rfv7.ControlToValidate = txtBox.ID;
                            rfv7.SetFocusOnError = true;
                            cell.Controls.Add(rfv7);
                            CompareValidator cpv = new CompareValidator();
                            cpv.ID = "cpvDiscount";
                            cpv.ErrorMessage = "Discount should be a numeric field.";
                            cpv.Type = ValidationDataType.Double;
                            cpv.Operator = ValidationCompareOperator.DataTypeCheck;
                            cpv.Display = ValidatorDisplay.None;
                            cpv.ControlToValidate = txtBox.ID;
                            cpv.SetFocusOnError = true;
                            cell.Controls.Add(cpv);
                            cell.Attributes.Add("align", "right");
                            //  cell.Controls.Add(new LiteralControl(")"));
                            ViewState["RedDiscount"] = true;
                            ViewState["ItemRowHeader"] = false;
                            break;
                        case "6":
                            txtBox.ID = "txtTotalCost";
                            txtBox.Attributes.Add("style", "text-align:right");
                            txtBox.ReadOnly = true;
                            txtBox.BorderStyle = BorderStyle.None;
                            txtBox.CssClass = "totalcosts";
                            txtBox.BackColor = clr.stringToColor("10659E");
                            cell.Controls.Add(txtBox);
                            cell.Attributes.Add("align", "right");
                            ViewState["RedDiscount"] = false;
                            ViewState["ItemRowHeader"] = true;
                            break;
                        case "5":
                            txtBox.ID = "txtDeliveryCost";
                            txtBox.Attributes.Add("style", "text-align:right");
                            txtBox.Attributes.Add("onBlur", "GetAllAmounts()");
                            txtBox.Attributes.Add("onkeypress", "return(currencyFormat(this,'','.',event))");
                            txtBox.Attributes.Add("onKeyUp", "GetAllAmounts()");
                            cell.Controls.Add(txtBox);
                            RequiredFieldValidator rfv5 = new RequiredFieldValidator();
                            rfv5.ID = "rfvDeliveryCost";
                            rfv5.ErrorMessage = "Delivery Cost is a required field.";
                            rfv5.Display = ValidatorDisplay.None;
                            rfv5.ControlToValidate = txtBox.ID;
                            rfv5.SetFocusOnError = true;
                            cell.Controls.Add(rfv5);
                            CompareValidator cpv5 = new CompareValidator();
                            cpv5.ID = "cpvDeliveryCost";
                            cpv5.ErrorMessage = "Delivery Cost should be a numeric field.";
                            cpv5.Type = ValidationDataType.Double;
                            cpv5.Operator = ValidationCompareOperator.DataTypeCheck;
                            cpv5.Display = ValidatorDisplay.None;
                            cpv5.ControlToValidate = txtBox.ID;
                            cpv5.SetFocusOnError = true;
                            cell.Controls.Add(cpv5);
                            cell.Attributes.Add("align", "right");
                            ViewState["RedDiscount"] = false;
                            ViewState["ItemRowHeader"] = false;
                            break;
                        case "4":
                            txtBox.ID = "txtTotalExtendedCost";
                            txtBox.Attributes.Add("style", "text-align:right");
                            txtBox.ReadOnly = true;
                            txtBox.BorderStyle = BorderStyle.None;
                            txtBox.CssClass = "totalcosts";
                            txtBox.BackColor = clr.stringToColor("10659E");
                            cell.Controls.Add(txtBox);
                            cell.Attributes.Add("align", "right");
                            ViewState["RedDiscount"] = false;
                            ViewState["ItemRowHeader"] = true;
                            break;
                        case "3":
                            DropDownList ddlIncoterm = new DropDownList();
                            IncotermTransaction i = new IncotermTransaction();
                            ddlIncoterm.DataSource = i.GetIncoterm(connstring);
                            ddlIncoterm.DataTextField = "Incoterm";
                            ddlIncoterm.DataValueField = "Id";
                            ddlIncoterm.DataBind();
                            ddlIncoterm.DataBinding += new EventHandler(ddlIncoterm_DataBinding);
                            ddlIncoterm.ID = "ddlIncoterm";
                            cell.Controls.Add(ddlIncoterm);
                            txtBox.ID = "txtIncoterm";
                            txtBox.Visible = false;
                            cell.Controls.Add(txtBox);
                            CompareValidator cmpi = new CompareValidator();
                            cmpi.ID = "rfvIncoterm";
                            cmpi.ErrorMessage = "Incoterm is a required field.";
                            cmpi.ValueToCompare = "-1";
                            cmpi.Operator = ValidationCompareOperator.NotEqual;
                            cmpi.Type = ValidationDataType.Integer;
                            cmpi.Display = ValidatorDisplay.None;
                            cmpi.ControlToValidate = ddlIncoterm.ID;
                            cmpi.SetFocusOnError = true;
                            cell.Controls.Add(cmpi);
                            ViewState["RedDiscount"] = false;
                            ViewState["ItemRowHeader"] = false;
                            break;
                        case "2":
                            txtBox.ID = "txtPaymentTerms";
                            cell.Controls.Add(txtBox);
                            RequiredFieldValidator rfv2 = new RequiredFieldValidator();
                            rfv2.ID = "rfvPaymentTerms";
                            rfv2.ErrorMessage = "Payment Terms is a required field.";
                            rfv2.Display = ValidatorDisplay.None;
                            rfv2.ControlToValidate = txtBox.ID;
                            rfv2.SetFocusOnError = true;
                            cell.Controls.Add(rfv2);
                            ViewState["RedDiscount"] = false;
                            ViewState["ItemRowHeader"] = false;
                            break;
                        case "1":
                            txtBox.ID = "txtWarranty";
                            txtBox.Width = 200;
                            txtBox.Height = 75;
                            txtBox.TextMode = TextBoxMode.MultiLine;
                            txtBox.Attributes.Add("style", "font-family:Arial; font-size:8pt;");
                            cell.Controls.Add(txtBox);
                            RequiredFieldValidator rfv1 = new RequiredFieldValidator();
                            rfv1.ID = "rfvWarranty";
                            rfv1.ErrorMessage = "Warranty is a required field.";
                            rfv1.Display = ValidatorDisplay.None;
                            rfv1.ControlToValidate = txtBox.ID;
                            rfv1.SetFocusOnError = true;
                            cell.Controls.Add(rfv1);
                            ViewState["RedDiscount"] = false;
                            ViewState["ItemRowHeader"] = false;
                            break;
                        case "0":
                            txtBox.ID = "txtRemarks";
                            txtBox.Width = 200;
                            cell.Controls.Add(txtBox);
                            //RequiredFieldValidator rfv0 = new RequiredFieldValidator();
                            //rfv0.ID = "rfvRemarks";
                            //rfv0.ErrorMessage = "Remarks is a required field.";
                            //rfv0.Display = ValidatorDisplay.None;
                            //rfv0.ControlToValidate = txtBox.ID;
                            //rfv0.SetFocusOnError = true;
                            //cell.Controls.Add(rfv0);
                            ViewState["ctr_totalSet"] = null;
                            ViewState["RedDiscount"] = false;
                            ViewState["ItemRowHeader"] = false;
                            break;
                        default:
                            txtBox.ReadOnly = true;
                            txtBox.Attributes.Add("style", "text-align:right");
                            txtBox.BorderStyle = BorderStyle.None;
                            txtBox.ID = "txtTotalItemCost";
                            cell.Controls.Add(txtBox);
                            cell.Attributes.Add("align", "right");
                            ViewState["RedDiscount"] = false;
                            ViewState["ItemRowHeader"] = false;
                            break;
                    }

                    ViewState["ctr_total"] = Int32.Parse(ViewState["ctr_total"].ToString().Trim()) - 1;
                }

            }
            else
            {
                txtBox.BorderStyle = BorderStyle.None;
                txtBox.Attributes.Add("style", "text-align:right");
                txtBox.ReadOnly = true;
                txtBox.ID = "txtTotalItemCost";
                cell.Controls.Add(txtBox);
                cell.Attributes.Add("align", "right");
            }  
        }
        else
        {
            Label lblText = new Label();
            lblText.Width = 150;
            lblText.DataBinding += new EventHandler(lblText_DataBinding);
            cell.Controls.Add(lblText);
        }
        if (Convert.ToBoolean(ViewState["ItemRowHeader"]) == true)
        {
            //cell.CssClass = "itemDetails_th";//using this automatically aligns item in cell to left; formatting requires that it is aligned to right for easy reading of total.
            cell.BackColor = clr.stringToColor("10659E");
            cell.CssClass = "totalcosts";
        }
    }

 



    void lblText_DataBinding(object sender, EventArgs e)
    {
        // get a reference to the control that raised the event
        Label target = (Label)sender;
        Control container = target.NamingContainer;
        // get a reference to the row object
        object dataItem = DataBinder.GetDataItem(container);
        // get the row's value for the named data field only use Eval when it is neccessary
        // to access child object values, otherwise use GetPropertyValue. GetPropertyValue
        // is faster because it does not use reflection
        object dataFieldValue = null;
        if (this.DataField.Contains("."))
        {
            dataFieldValue = DataBinder.Eval(dataItem, this.DataField);
        }
        else
         {   
            dataFieldValue = DataBinder.GetPropertyValue(dataItem, this.DataField);
        }
        // set the table cell's text. check for null values to prevent ToString errors
        if (dataFieldValue != null)
        {
            string strTitle = dataFieldValue.ToString().Trim();
            target.Text = strTitle;
            SetItemRowHeader(strTitle);
            ViewState["Binding"] = ((strTitle == "") ? true : false);

        }
    }

    void txtText_DataBinding(object sender, EventArgs e)
    {
        // get a reference to the control that raised the event
        TextBox target = (TextBox)sender;
        Control container = target.NamingContainer;
        // get a reference to the row object
        object dataItem = DataBinder.GetDataItem(container);
        // get the row's value for the named data field only use Eval when it is neccessary
        // to access child object values, otherwise use GetPropertyValue. GetPropertyValue
        // is faster because it does not use reflection
        object dataFieldValue = null;
        if (this.DataField.Contains("."))
        {
            dataFieldValue = DataBinder.Eval(dataItem, this.DataField);
        }
        else
        {
            dataFieldValue = DataBinder.GetPropertyValue(dataItem, this.DataField);
        }
        // set the table cell's text. check for null values to prevent ToString errors
        if (dataFieldValue != null)
        {
            string strTitle = dataFieldValue.ToString().Trim();
            if (strTitle == "")
            {
                target.Visible = false;
            }
            else
            {
                target.Text = strTitle; 
                target.Visible = true;
            }

                SetItemRowHeader(strTitle);
            ViewState["Binding"] = ((strTitle == "") ? true : false);

        }
    }


    void lnkText_DataBinding(object sender, EventArgs e)
    {
        // get a reference to the control that raised the event
        LinkButton target = (LinkButton)sender;
        Control container = target.NamingContainer;
        // get a reference to the row object
        object dataItem = DataBinder.GetDataItem(container);
        // get the row's value for the named data field only use Eval when it is neccessary
        // to access child object values, otherwise use GetPropertyValue. GetPropertyValue
        // is faster because it does not use reflection
        object dataFieldValue = null;
        if (this.DataField.Contains("."))
        {
            dataFieldValue = DataBinder.Eval(dataItem, this.DataField);
        }
        else
        {
            dataFieldValue = DataBinder.GetPropertyValue(dataItem, this.DataField);
        }
        // set the table cell's text. check for null values to prevent ToString errors
        if (dataFieldValue != null)
        {
            target.Text = dataFieldValue.ToString();
        }
    }


    void Label2_DataBinding(object sender, EventArgs e)
    {
        // get a reference to the control that raised the event
        Label target = (Label)sender;
        Control container = target.NamingContainer;
        // get a reference to the row object
        object dataItem = DataBinder.GetDataItem(container);
        // get the row's value for the named data field only use Eval when it is neccessary
        // to access child object values, otherwise use GetPropertyValue. GetPropertyValue
        // is faster because it does not use reflection
        object dataFieldValue = null;
        if (this.DataField.Contains("."))
        {
            dataFieldValue = DataBinder.Eval(dataItem, this.DataField);
        }
        else
        {
            dataFieldValue = DataBinder.GetPropertyValue(dataItem, this.DataField);
        }
        // set the table cell's text. check for null values to prevent ToString errors
        if (dataFieldValue != null)
        {
            string strVal = dataFieldValue.ToString();
            string strTitle = "";
            if (strVal.IndexOf(Convert.ToChar("|")) > -1)
            {
                strTitle = strVal.Substring(0, strVal.IndexOf(Convert.ToChar("|")));
            }
            if (strVal.Substring((strVal.IndexOf(Convert.ToChar("|")) + 1)) == "NONE")
            {
                target.Text = "";
            }
            else
            {
                if (strTitle == "Incoterm")
                {
                    IncotermTransaction inc = new IncotermTransaction();
                    target.Text = inc.GetIncotermName(connstring, strVal.Substring((strVal.IndexOf(Convert.ToChar("|")) + 1)));
                }
                else if (strTitle == "DATE")
                {
                    string strDate = strVal.Substring((strVal.IndexOf(Convert.ToChar("|")) + 1));
                    string[] strDate1 = strDate.Split(Convert.ToChar("/"));
                    
                    if (strDate1.Length == 3)
                    {
                        OtherTransaction oth = new OtherTransaction();
                        target.Text = oth.Month(strDate1[0]) + " " + strDate1[1] + ", " + strDate1[2];
                    }
                    else
                    {
                        target.Text = "";
                    }
                }
                else
                {
                    target.Text = strVal.Substring((strVal.IndexOf(Convert.ToChar("|")) + 1));
                }
            }
            SetItemRowHeader(strTitle);
            ViewState["Label2Binding"] = ((strTitle == "") ? true : false);
            ViewState["Last"] = ((strTitle == "Last") ? true : false);
            ViewState["Discount"] = ((strTitle == "Discount") ? true : false);
            ViewState["Total Cost"] = ((strTitle == "Total Cost") ? true : false);
            ViewState["Delivery Cost"] = ((strTitle == "Delivery Cost") ? true : false);
            ViewState["Total Extended Cost"] = ((strTitle == "Total Extended Cost") ? true : false);
            ViewState["Incoterm"] = ((strTitle == "Incoterm") ? true : false);
            ViewState["Payment Terms"] = ((strTitle == "Payment Terms") ? true : false);
            ViewState["Warranty"] = ((strTitle == "Warranty") ? true : false);
        }
    }

    void Label3_DataBinding(object sender, EventArgs e)
    {
        // get a reference to the control that raised the event
        Label target = (Label)sender;
        Control container = target.NamingContainer;
        // get a reference to the row object
        object dataItem = DataBinder.GetDataItem(container);
        // get the row's value for the named data field only use Eval when it is neccessary
        // to access child object values, otherwise use GetPropertyValue. GetPropertyValue
        // is faster because it does not use reflection
        object dataFieldValue = null;
        if (this.DataField.Contains("."))
        {
            dataFieldValue = DataBinder.Eval(dataItem, this.DataField);
        }
        else
        {
            dataFieldValue = DataBinder.GetPropertyValue(dataItem, this.DataField);
        }
        // set the table cell's text. check for null values to prevent ToString errors
        if (dataFieldValue != null)
        {

            string strVal = dataFieldValue.ToString().Trim();
            string strTitle = "";
            string strElement = "";
            if (strVal.IndexOf(Convert.ToChar("|")) > -1)
            {
                strTitle = strVal.Substring(0, strVal.IndexOf(Convert.ToChar("|")));
                strElement = strVal.Substring(strVal.IndexOf(Convert.ToChar("|")) + 1);

                string[] strArray = strVal.Split(Convert.ToChar("|"));
                if (strArray.Length == 3)
                {
                    strElement = strArray[strArray.Length - 2];
                    if (strArray[strArray.Length - 1].ToUpper().Trim() == "HIGH")
                    {
                        //highest is green
                        target.Attributes.Add("style", "font-weight:bold");
                        target.ForeColor = clr.stringToColor("007D48");
                    }
                    else if (strArray[strArray.Length - 1].ToUpper().Trim() == "LOW")
                    {
                        //lowest is red
                        target.Attributes.Add("style", "font-weight:bold");
                        target.ForeColor = clr.stringToColor("B30000");
                    }
                }
               
            }
            target.Text = strElement;
            SetItemRowHeader(strTitle);

        }
    }

    void LabelDate_DataBinding(object sender, EventArgs e)
    {
        // get a reference to the control that raised the event
        Label target = (Label)sender;
        Control container = target.NamingContainer;
        // get a reference to the row object
        object dataItem = DataBinder.GetDataItem(container);
        // get the row's value for the named data field only use Eval when it is neccessary
        // to access child object values, otherwise use GetPropertyValue. GetPropertyValue
        // is faster because it does not use reflection
        object dataFieldValue = null;
        if (this.DataField.Contains("."))
        {
            dataFieldValue = DataBinder.Eval(dataItem, this.DataField);
        }
        else
        {
            dataFieldValue = DataBinder.GetPropertyValue(dataItem, this.DataField);
        }
        // set the table cell's text. check for null values to prevent ToString errors
        if (dataFieldValue != null)
        {
            string strVal = dataFieldValue.ToString();
            string strTitle = "";
            if (strVal.IndexOf(Convert.ToChar("|")) > -1)
            {
                strTitle = strVal.Substring(0, strVal.IndexOf(Convert.ToChar("|")));
            }
            if (strVal.Substring((strVal.IndexOf(Convert.ToChar("|")) + 1)) == "NONE")
            {
                target.Text = "";
            }
            else
            {
                if (strTitle == "Incoterm")
                {
                    IncotermTransaction inc = new IncotermTransaction();
                    target.Text = inc.GetIncotermName(connstring, strVal.Substring((strVal.IndexOf(Convert.ToChar("|")) + 1)));
                }
                else if (strTitle == "DATE")
                {
                    string strDate = strVal.Substring((strVal.IndexOf(Convert.ToChar("|")) + 1));
                    string[] strDate1 = strDate.Split(Convert.ToChar("/"));

                    if (strDate1.Length == 3)
                    {
                        OtherTransaction oth = new OtherTransaction();
                        target.Text = oth.Month(strDate1[0]) + " " + strDate1[1] + ", " + strDate1[2];
                    }
                    else
                    {
                        target.Text = "";
                    }
                }
                else
                {
                    target.Text = strVal.Substring((strVal.IndexOf(Convert.ToChar("|")) + 1));

                }
            }
            SetItemRowHeader(strTitle);
            ViewState["Last"] = ((strTitle == "Last") ? true : false);
            ViewState["Discount"] = ((strTitle == "Discount") ? true : false);
            ViewState["Total Cost"] = ((strTitle == "Total Cost") ? true : false);
            ViewState["Delivery Cost"] = ((strTitle == "Delivery Cost") ? true : false);
            ViewState["Total Extended Cost"] = ((strTitle == "Total Extended Cost") ? true : false);
            ViewState["Incoterm"] = ((strTitle == "Incoterm") ? true : false);
            ViewState["Payment Terms"] = ((strTitle == "Payment Terms") ? true : false);
            ViewState["Warranty"] = ((strTitle == "Warranty") ? true : false);
            ViewState["LabelDateBinding"] = ((strTitle == "") ? true : false);
        }
    }

    //void chkBox_DataBinding(object sender, EventArgs e)
    //{
    //    // get a reference to the control that raised the event
    //    CheckBox target = (CheckBox)sender;
    //    Control container = target.NamingContainer;
    //    // get a reference to the row object
    //    object dataItem = DataBinder.GetDataItem(container);
    //    // get the row's value for the named data field only use Eval when it is neccessary
    //    // to access child object values, otherwise use GetPropertyValue. GetPropertyValue
    //    // is faster because it does not use reflection
    //    object dataFieldValue = null;
    //    if (this.DataField.Contains("."))
    //    {
    //        dataFieldValue = DataBinder.Eval(dataItem, this.DataField);
    //    }
    //    else
    //    {
    //        dataFieldValue = DataBinder.GetPropertyValue(dataItem, this.DataField);
    //    }
    //    // set the table cell's text. check for null values to prevent ToString errors
    //    if (dataFieldValue != null)
    //    {
    //        CheckBox chk = (CheckBox)dataFieldValue;
    //        string strVal = chk.Text;
    //        string[] strVal1 = strVal.Split(Convert.ToChar("|"));
    //        if (strVal1.Length == 4)
    //        {                
    //            string strTitle = strVal1[0];
    //            string strRowName = strVal1[1];
    //            string strVisible = strVal1[2];

    //            if (strVisible == "true")
    //            {
    //                target.ID = strTitle;
    //                target.Visible = true;
    //                target.Checked = chk.Checked;
    //            }
    //            else
    //            {
    //                target.Visible = false;
    //            }

    //            SetItemRowHeader(strRowName);
    //        }
            
    //    }
    //}

    void chkBox_DataBinding(object sender, EventArgs e)
    {
        // get a reference to the control that raised the event
        CheckBox target = (CheckBox)sender;
        Control container = target.NamingContainer;
        // get a reference to the row object
        object dataItem = DataBinder.GetDataItem(container);
        // get the row's value for the named data field only use Eval when it is neccessary
        // to access child object values, otherwise use GetPropertyValue. GetPropertyValue
        // is faster because it does not use reflection
        object dataFieldValue = null;
        if (this.DataField.Contains("."))
        {
            dataFieldValue = DataBinder.Eval(dataItem, this.DataField);
        }
        else
        {
            dataFieldValue = DataBinder.GetPropertyValue(dataItem, this.DataField);
        }
        // set the table cell's text. check for null values to prevent ToString errors
        if (dataFieldValue != null)
        {
            CheckBox chk = (CheckBox)dataFieldValue;
            string strVal = chk.Text;
            string[] strVal1 = strVal.Split(Convert.ToChar("|"));
            if (strVal1.Length == 4)
            {
                string strTitle = strVal1[0];
                string strRowName = strVal1[1];
                string strVisible = strVal1[2];
                string strBidTenderNo = strVal1[3];

                if (strVisible == "true")
                {
                    target.ID = strBidTenderNo + "_" + strTitle;
                    target.Visible = true;
                    target.Checked = chk.Checked;
                }
                else
                {
                    target.Visible = false;
                }

                SetItemRowHeader(strRowName);
            }

        }
    }

    protected void InitializeFooterCell(DataControlFieldCell cell, DataControlRowState rowState)
    {
        CheckBox chkBox = new CheckBox();
        cell.Controls.Add(chkBox);
    }



    protected void InitializeHeaderCell(DataControlFieldCell cell, DataControlRowState rowState)
    {
       
        switch (HeaderType)
        {
            case "LINKBUTTON":
                LinkButton lnk = new LinkButton();
                lnk.Text = this.DataField;
                ColorClass clr = new ColorClass();
                lnk.ForeColor=clr.stringToColor(ForeColor);
                lnk.CommandName = LinkCommandName;
                lnk.CommandArgument = LinkCommandArgument;
                cell.Controls.Add(lnk);
                break;
            case "CHECKBOX":
                CheckBox chk = new CheckBox();
                chk.ID = ViewState["HeaderID"].ToString().Trim();
                chk.Attributes.Add("onClick", "javascript:SelectAllCheckboxes(this);");
                cell.Controls.Add(chk);
                break;
            default:
                Label lbl = new Label();
                lbl.Text = this.DataField;
                break;
        }
        

    }



    void txtBox_DataBinding(object sender, EventArgs e)
    {

        // get a reference to the control that raised the event

        TextBox target = (TextBox)sender;

        Control container = target.NamingContainer;



        // get a reference to the row object

        object dataItem = DataBinder.GetDataItem(container);

        

        // get the row's value for the named data field only use Eval when it is neccessary

        // to access child object values, otherwise use GetPropertyValue. GetPropertyValue

        // is faster because it does not use reflection

        object dataFieldValue = null;



        if (this.DataField.Contains("."))
        {

            dataFieldValue = DataBinder.Eval(dataItem, this.DataField);

        }

        else
        {
            
            dataFieldValue = DataBinder.GetPropertyValue(dataItem, this.DataField);

        }



        // set the table cell's text. check for null values to prevent ToString errors

        if (dataFieldValue != null)
        {
            string strVal = dataFieldValue.ToString();
            if (strVal.IndexOf(Convert.ToChar("|"))==-1)
                target.Text = dataFieldValue.ToString();
            else 
            {
                target.Text = strVal.Substring((strVal.IndexOf(Convert.ToChar("|")) + 1));
                string strTitle = strVal.Substring(0, strVal.IndexOf(Convert.ToChar("|")));
                SetItemRowHeader(strTitle);
                ViewState["Last"] = ((strTitle == "Last") ? true : false);
                ViewState["Discount"] = ((strTitle == "Discount") ? true : false);
                ViewState["Total Cost"] = ((strTitle == "Total Cost") ? true : false);
                ViewState["Delivery Cost"] = ((strTitle == "Delivery Cost") ? true : false);
                ViewState["Total Extended Cost"] = ((strTitle == "Total Extended Cost") ? true : false);
                ViewState["Incoterm"] = ((strTitle == "Incoterm") ? true : false);
                ViewState["Payment Terms"] = ((strTitle == "Payment Terms") ? true : false);
                ViewState["Warranty"] = ((strTitle == "Warranty") ? true : false);

                //if (strTitle == "Discount")
                //{
                //    target.ID = "txtDiscount";
                //    target.ReadOnly = false;
                //    target.BorderStyle = BorderStyle.NotSet;
                //    target.CssClass = "Discount";
                //    target.Attributes.Add("onBlur", "GetAllAmounts()");
                //    target.Attributes.Add("onkeypress", "return(currencyFormat(this,'','.',event))");
                //    target.Attributes.Add("onKeyUp", "GetAllAmounts()");
                //}

              
             }

        }

    }

    void SetItemRowHeader(string strTitle)
    {
        switch (strTitle)
        {
            case "Last":
                ViewState["RedDiscount"] = true;
                break;
            case "Discount":
                ViewState["ItemRowHeader"] = true;
                break;
            case "Delivery Cost":
                ViewState["ItemRowHeader"] = true;
                break;
            case "DeliveryCost":
                ViewState["ItemRowHeader"] = true;
                break;
           
            default:
                ViewState["ItemRowHeader"] = false;
                break;
        }
    }

    void txtDay_DataBinding(object sender, EventArgs e)
    {

        // get a reference to the control that raised the event
        TextBox target = (TextBox)sender;
        Control container = target.NamingContainer;
        // get a reference to the row object
        object dataItem = DataBinder.GetDataItem(container);
        // get the row's value for the named data field only use Eval when it is neccessary
        // to access child object values, otherwise use GetPropertyValue. GetPropertyValue
        // is faster because it does not use reflection
        object dataFieldValue = null;
        if (this.DataField.Contains("."))
        {
            dataFieldValue = DataBinder.Eval(dataItem, this.DataField);
        }
        else
        {
            dataFieldValue = DataBinder.GetPropertyValue(dataItem, this.DataField);
        }
        // set the table cell's text. check for null values to prevent ToString errors
        if (dataFieldValue != null)
        {

            string strValue = dataFieldValue.ToString().Trim();
            if (strValue.IndexOf("|") > -1)
            {
                target.Visible = false;
            }
            else
            {
                if (dataFieldValue.ToString().Trim() != "//")
                {
                    string strDate = dataFieldValue.ToString().Trim();
                    string[] strDate1 = strDate.Split(Convert.ToChar('/'));
                    if (strDate1.Length == 3)
                    {
                        target.Text = strDate1[1].ToString().Trim();
                    }

                }
            }
        }
    }

    void txtYear_DataBinding(object sender, EventArgs e)
    {

        // get a reference to the control that raised the event
        TextBox target = (TextBox)sender;
        Control container = target.NamingContainer;
        // get a reference to the row object
        object dataItem = DataBinder.GetDataItem(container);
        // get the row's value for the named data field only use Eval when it is neccessary
        // to access child object values, otherwise use GetPropertyValue. GetPropertyValue
        // is faster because it does not use reflection
        object dataFieldValue = null;
        if (this.DataField.Contains("."))
        {
            dataFieldValue = DataBinder.Eval(dataItem, this.DataField);
        }
        else
        {
            dataFieldValue = DataBinder.GetPropertyValue(dataItem, this.DataField);
        }
        // set the table cell's text. check for null values to prevent ToString errors
        if (dataFieldValue != null)
        {
             string strValue = dataFieldValue.ToString().Trim();
             if (strValue.IndexOf("|") > -1)
             {
                 target.Visible = false;
             }
             else 
             {
                 if (dataFieldValue.ToString().Trim() != "//")
                 {
                     string strDate = dataFieldValue.ToString().Trim();
                     string[] strDate1 = strDate.Split(Convert.ToChar('/'));
                     if (strDate1.Length == 3)
                     {
                         target.Text = strDate1[2].ToString().Trim();
                     }
                 }
             }
                
        }

    }


    void ddlDate_DataBinding(object sender, EventArgs e)
    {

        // get a reference to the control that raised the event
        DropDownList target = (DropDownList)sender;
        Control container = target.NamingContainer;
        // get a reference to the row object
        object dataItem = DataBinder.GetDataItem(container);
        // get the row's value for the named data field only use Eval when it is neccessary
        // to access child object values, otherwise use GetPropertyValue. GetPropertyValue
        // is faster because it does not use reflection
        object dataFieldValue = null;
        if (this.DataField.Contains("."))
        {
            dataFieldValue = DataBinder.Eval(dataItem, this.DataField);
        }
        else
        {
            dataFieldValue = DataBinder.GetPropertyValue(dataItem, this.DataField);
        }
        // set the table cell's text. check for null values to prevent ToString errors
        if (dataFieldValue != null)
        {
            string strValue = dataFieldValue.ToString().Trim();
            if (strValue.IndexOf("|") > -1)
            {
                string strTitle = strValue.Substring(0, strValue.IndexOf("|"));
                SetItemRowHeader(strTitle);
                ViewState["DateBinding"] = ((strTitle == "") ? true : false);
                target.Visible = false;
            }
            else
            {
                if (dataFieldValue.ToString().Trim() != "//")
                {
                    string strDate = dataFieldValue.ToString().Trim();
                    string[] strDate1 = strDate.Split(Convert.ToChar('/'));
                    target.SelectedIndex = target.Items.IndexOf(target.Items.FindByValue(Convert.ToString(strDate1[0])));
                }
            }
            
        }

    }


    void ddlIncoterm_DataBinding(object sender, EventArgs e)
    {

        // get a reference to the control that raised the event
        DropDownList target = (DropDownList)sender;
        Control container = target.NamingContainer;
        // get a reference to the row object
        object dataItem = DataBinder.GetDataItem(container);
        // get the row's value for the named data field only use Eval when it is neccessary
        // to access child object values, otherwise use GetPropertyValue. GetPropertyValue
        // is faster because it does not use reflection
        object dataFieldValue = null;
        if (this.DataField.Contains("."))
        {
            dataFieldValue = DataBinder.Eval(dataItem, this.DataField);
        }
        else
        {
            dataFieldValue = DataBinder.GetPropertyValue(dataItem, this.DataField);
        }
        // set the table cell's text. check for null values to prevent ToString errors
        if (dataFieldValue != null)
        {
            string strValue = dataFieldValue.ToString().Trim();
            if (strValue.IndexOf("|") > -1)
            {
                string[] strValue1 = strValue.Split(Convert.ToChar("|"));
                strValue = strValue1[(strValue1.Length-1)];
            }
            target.SelectedIndex = target.Items.IndexOf(target.Items.FindByValue(strValue));
        }

    }
    #endregion

}


