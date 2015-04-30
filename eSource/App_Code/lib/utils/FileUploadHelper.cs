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
using System.Collections;
using System.ComponentModel;
using EBid.lib.constant;

namespace EBid.lib
{
	public static class FileUploadHelper
	{
		static string defaultPrefix = "FileAttachment_";
		static string filenamePrefix = String.IsNullOrEmpty(ConfigurationManager.AppSettings["FilePrefix"]) ? "" : ConfigurationManager.AppSettings["FilePrefix"];

		/// <summary>
		/// 
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="targetFolder"></param>
		/// <returns></returns>
		public static string GetAlternativeFileName(string filename, string targetFolder)
		{		
			defaultPrefix = string.IsNullOrEmpty(filenamePrefix) ? defaultPrefix : filenamePrefix;
			string extension = "";
			int i = 1;

			// Get the original file extension
			FileInfo fInfo = new FileInfo(filename);
			extension = fInfo.Extension;
			
			// 
			filename = defaultPrefix + i + fInfo.Extension;
			fInfo = new FileInfo(targetFolder + filename);
						
			while (fInfo.Exists)
			{
				i++;
				filename = defaultPrefix + i + fInfo.Extension;
				fInfo = new FileInfo(targetFolder + filename);
			};

			return filename;
		}

        public static string GetAlternativeFileName(string extension)
        {
            string targetFolder = Constant.FILEATTACHMENTSFOLDERDIR;
            defaultPrefix = string.IsNullOrEmpty(filenamePrefix) ? defaultPrefix : filenamePrefix;            
            int i = 1;           

            string filename = defaultPrefix + i + extension;
            FileInfo fInfo = new FileInfo(targetFolder + filename);

            while (fInfo.Exists)
            {
                i++;
                filename = defaultPrefix + i + extension;
                fInfo = new FileInfo(targetFolder + filename);
            };

            return fInfo.Name;
        }

        public static string GetNewAlternativeFileName(string userId, string extension)
        {
            defaultPrefix = string.IsNullOrEmpty(filenamePrefix) ? defaultPrefix : filenamePrefix;
            string ts = DateTime.Now.ToString("_yyyyMMddHHmmss");

            string filename = defaultPrefix + userId + ts + extension;
            return filename;
        }

        public static string GetNewAlternativeFileName(string targetFolderPath, string userId, string extension)
        {
            string targetFolder = targetFolderPath;
            defaultPrefix = string.IsNullOrEmpty(filenamePrefix) ? defaultPrefix : filenamePrefix;
            int i = 1;

            string filename = defaultPrefix + userId + i + extension;
            FileInfo fInfo = new FileInfo(targetFolder + filename);

            while (fInfo.Exists)
            {
                i++;
                filename = defaultPrefix + userId + i + extension;
                fInfo = new FileInfo(targetFolder + filename);
            };

            return fInfo.Name;
        }
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="targetFolder"></param>
		/// <returns>Full filename including destination path</returns>
		public static string GetFullAlternativeFileName(string filename, string targetFolder)
		{			
			defaultPrefix = string.IsNullOrEmpty(filenamePrefix) ? defaultPrefix : filenamePrefix;
			string extension = "";
			int i = 1;

			// Get the original file extension
			FileInfo fInfo = new FileInfo(filename);
			extension = fInfo.Extension;
			
			// 
			filename = defaultPrefix + i + fInfo.Extension;
			fInfo = new FileInfo(targetFolder + filename);
						
			while (fInfo.Exists)
			{
				i++;
				filename = defaultPrefix + i + fInfo.Extension;
				fInfo = new FileInfo(targetFolder + filename);
			};
																		
			return fInfo.FullName;
		}

        public static bool IsFileForbidden(string extension)
        {
            string[] s = ConfigurationManager.AppSettings["FileExtensionsNotAllowed"].Trim().Split(new char[] { '|' });

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == extension)
                    return true;
            }
            return false;
        }
    }

    public class UploadedFile
	{
		string _fileName, _alt;
		FileInfo _fInfo;

		public UploadedFile()
		{
			_fileName = _alt = string.Empty;			
		}

		public UploadedFile(string filename)
		{
			_fileName = filename;
		}
		
		public UploadedFile(string filename, string alternative)
		{
			FileName = filename;
			_alt = alternative;			
		}

		public FileInfo File
		{
			get { return _fInfo; }			
		}
		
		public string FileName
		{
			get { return _fileName; }
			set 
			{ 
				_fileName = value;
				_fInfo = new FileInfo(_fileName);
			}
		}

		public string Alias
		{
			get { return _alt; }
			set { _alt = value; }
		}
	}

	[TypeConverter(typeof(FileTypeConverter))]
	public enum FileTypes
	{
		Document = 0,
		Image = 1,
		Executable = 2
	}

	public class FileTypeConverter : EnumConverter
	{
		private readonly Type _enumType = null;
		private static readonly Hashtable _enumStringsMap = new Hashtable();

		public FileTypeConverter(Type enumType)
			: base(enumType)
		{
			_enumType = enumType;

			if (enumType == typeof(string) && _enumStringsMap.Count == 0)
			{
				_enumStringsMap.Add(FileTypes.Document, "Document File");
				_enumStringsMap.Add(FileTypes.Executable, "Executable File");
				_enumStringsMap.Add(FileTypes.Image, "Image File");
			}
		}

		public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && value is string)
				return _enumStringsMap[value];
			return base.ConvertTo(context, culture, value, destinationType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
			if (value is string)
			{
				object convertedValue = null;

				foreach (object key in _enumStringsMap.Keys)
				{
					if (_enumStringsMap[key] == value)
					{
						convertedValue = key;
						break;
					}
				}
				return convertedValue;
			}
			return base.ConvertFrom(context, culture, value);
		}
	}
}
