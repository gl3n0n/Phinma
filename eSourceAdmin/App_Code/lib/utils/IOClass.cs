using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using EBid.lib.constant;


/// <summary>
/// Summary description for IOClass
/// </summary>
public class IOClass
{
	public IOClass()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int GetTenderCount(string userid)
    {
        if (!(Directory.Exists(Constant.CtrDirectory + "//" + userid)))
            Directory.CreateDirectory(Constant.CtrDirectory + "//" + userid);
        string filename = Constant.CtrDirectory + "//" + userid + "//bidtendercount.txt";
        filename = filename.Replace("//", "\\");
        //if (!(File.Exists(filename)))
        //    File.CreateText(filename);

        StreamReader sr = new StreamReader(filename);
        //Read the first line of text
        string line = "";
        line = sr.ReadLine();
        //Continue to read until you reach end of file
        if ((line == "")|| (line==null))
            line = "0";
        sr.Close();
        return Int32.Parse(line);
    }
    
    public void WriteIndexToFile(string content, string VendorId)
    {

        if (!(Directory.Exists(Constant.CtrDirectory + "\\" + VendorId)))
            Directory.CreateDirectory(Constant.CtrDirectory + "\\" + VendorId);
        string filename = Constant.CtrDirectory + "\\" + VendorId + "\\bidtendercount.txt";
        //if (!(File.Exists(filename)))
        //    File.Create(filename);
          
        

        //Pass the filepath and filename to the StreamWriter Constructor
        StreamWriter sw = new StreamWriter(filename);
        
        if (content != "")
            sw.WriteLine(content);

        //Close the file
        sw.Close();
    }


}
