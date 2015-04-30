<%@ WebHandler Language="C#" Class="Upload" %>

using System;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;

public class Upload : IHttpHandler 
{
   public void ProcessRequest (HttpContext context) 
   {
      context.Response.ContentType = "text/plain";
      context.Response.Expires = -1;
      try
      {
          HttpPostedFile postedFile = context.Request.Files["Filedata"];
  
          string savepath = "";
          string tempPath = "";

          tempPath = context.Request["folder"];

          //If you prefer to use web.config for folder path, uncomment below line:
          //tempPath = System.Configuration.ConfigurationManager.AppSettings["FolderPath"];

          savepath = context.Server.MapPath(tempPath);
          DateTime thisDay = DateTime.Now;
          //Response.Write(thisDay.ToString("yyyyMMddHHmmss"));
          string filename = thisDay.ToString("yyyyMMddHHmmss") + "_" + postedFile.FileName.Replace("'", "");
          filename = Regex.Replace(filename, "[^a-zA-Z0-9/_.]", "");
          //string filename = postedFile.FileName.Replace("'", "");
          if (!Directory.Exists(savepath))
          {
              Directory.CreateDirectory(savepath);
          }
          postedFile.SaveAs(savepath + @"\" + filename);
          context.Response.Write(tempPath + "/" + filename);
          context.Response.StatusCode = 200;
      }
      catch (Exception ex)
      {
          context.Response.Write("Error: " + ex.Message);
      }
   }

   public bool IsReusable 
   {
      get 
      {
          return false;
      }
   }
}