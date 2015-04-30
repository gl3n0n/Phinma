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
using System.IO;

namespace EBid.lib
{
	/// <summary>
	/// By: GA Sacramento 07142006
	/// </summary>
	public static class FileHelper
	{
		public static void DownloadFile(Page parent, string pDownloadPath, string pActualFilename, string pFilename)
		{
			string fullpath = Path.Combine(pDownloadPath, pActualFilename);

            if (File.Exists(fullpath))
            {
                FileInfo fi = new FileInfo(fullpath);
                parent.Response.Clear();
                parent.Response.ContentType = "application/file";
                parent.Response.AppendHeader("Content-Length", fi.Length.ToString());
                parent.Response.AddHeader("content-disposition", "attachment; filename=\"" + pFilename + "\"");
                parent.Response.WriteFile(fullpath);
                parent.Response.End();
            }
            else
            {                
                parent.Response.Write("<script type='text/javascript'>alert('File is missing');</script>");             
            }
		}
	}
}
