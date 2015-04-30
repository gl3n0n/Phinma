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
using System.Web.Configuration;

namespace EBid.lib
{
	/// <summary>
	/// By: GA S. 07172006
	/// </summary>
	public static class ConfigurationHelper
	{
		public enum ProtectionProvider
		{
			DataProtectionConfigurationProvider = 0,
			RSAProtectedConfigurationProvider
		}

		public static void Protect(Page parent, ProtectionProvider protectionProvider)
		{
			Configuration config = WebConfigurationManager.OpenWebConfiguration(parent.Request.ApplicationPath);
			ConfigurationSection section = config.Sections["connectionStrings"];
			section.SectionInformation.ProtectSection(protectionProvider.ToString());
			config.Save();
		}

		public static void Unprotect(Page parent)
		{
			Configuration config = WebConfigurationManager.OpenWebConfiguration(parent.Request.ApplicationPath);
			ConfigurationSection section = config.Sections["connectionStrings"];
			section.SectionInformation.UnprotectSection();
			config.Save();
		}		
	}
}
