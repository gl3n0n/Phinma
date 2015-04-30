using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UploadFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        /*foreach (string f in Request.Files.AllKeys)
        {
            HttpPostedFile file = Request.Files[f];
            //file.SaveAs("C:\\Inetpub\\wwwroot\\EBID\\web\\fileattachments\\" + file.FileName);
            //file.SaveAs(file.FileName);
        }*/
        foreach (HttpPostedFile file in Request.Files)
        {
            file.SaveAs("C:\\Inetpub\\wwwroot\\EBID\\web\\fileattachments\\" + file.FileName);
        }
    }
}