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
using System.Diagnostics;
using LogThis;

/// <summary>
/// Created By: GA S. 07172006
/// </summary>
namespace LogHelper
{	
	public static class TextLogHelper
	{
		private static string _delimeter = "|";

        public enum LogType
        {
            Error = 0,
            Information = 1,
            Warning = 2
        }

        public static void Log(string message, LogType type)
        {
            try
            {
                string path = ConfigurationManager.AppSettings["LogDirectory"];
                string filename = ConfigurationManager.AppSettings["LogName"];

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                LogThis.Log.UseSensibleDefaults(filename, path, eloglevel.verbose);
                LogThis.Log.LogPeriod = elogperiod.day;
                LogThis.Log.LogPrefix = elogprefix.dt_loglevel;
                LogThis.Log.LogNameFormat = elognameformat.date_name;
                LogThis.Log.SetLogPath();

                switch (type)
                {
                    case LogType.Error: LogThis.Log.LogThis(message, eloglevel.error); break;
                    case LogType.Information: LogThis.Log.LogThis(message, eloglevel.info); break;
                    case LogType.Warning: LogThis.Log.LogThis(message, eloglevel.warn); break;
                }
            }
            catch { }
        }

		public static void Log(string filename, string message)
		{
			StreamWriter SW;

			if (File.Exists(filename))
			{
				// append file
				SW = File.AppendText(filename);
			}
			else
			{
				// create file
				SW = File.CreateText(filename);
			}
			SW.WriteLine(DateTime.Now.ToString("MM-dd-yy") + _delimeter + DateTime.Now.ToString("HH:mm:ss") + DateTime.Now.Millisecond.ToString() + _delimeter + message);

			SW.Close();			
		}

		public static void Log(string filename, string message, string delimeter)
		{
			StreamWriter SW;

			if (File.Exists(filename))
			{
				// append file
				SW = File.AppendText(filename);
			}
			else
			{
				// create file
				SW = File.CreateText(filename);
			}
			SW.WriteLine(DateTime.Now.ToString("MM-dd-yy") + delimeter + DateTime.Now.ToString("HH:mm:ss") + DateTime.Now.Millisecond.ToString() + delimeter + message);

			SW.Close();
		}
	}

	public static class EventLogHelper
	{
		public static void Log(string message, EventLogEntryType logtype)
		{
			try
			{
				// Setup Log System
				string programname = ConfigurationManager.AppSettings["ProgramName"];
				string logname = ConfigurationManager.AppSettings["LogName"];
				if (!EventLog.SourceExists(programname))
				{
					EventLog.CreateEventSource(programname, logname);
				}

				// Create an EventLog instance and assign its source.			
				EventLog eventLog = new EventLog();
				eventLog.Source = programname;

				eventLog.WriteEntry(message, logtype, 0);
			}
			catch { }
		}

		public static void Log(string programName, string logname, string message)
		{
			try
			{
				// Setup Log System
				if (!EventLog.SourceExists(programName))
				{
					EventLog.CreateEventSource(programName, logname);
				}

				// Create an EventLog instance and assign its source.			
				EventLog eventLog = new EventLog();
				eventLog.Source = programName;

				eventLog.WriteEntry(message, EventLogEntryType.Information, 0);
			}
			catch { }
		}

		public static void Log(string programName, string logname, string message, EventLogEntryType logtype)
		{
			try
			{
				// Setup Log System
				if (!EventLog.SourceExists(programName))
				{
					EventLog.CreateEventSource(programName, logname);
				}

				// Create an EventLog instance and assign its source.			
				EventLog eventLog = new EventLog();
				eventLog.Source = programName;

				eventLog.WriteEntry(message, logtype, 0);
			}
			catch { }
		}		
	}
}
