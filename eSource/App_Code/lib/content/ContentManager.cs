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
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections;
using EBid.lib;
using EBid.ConnectionString;

namespace EBid
{
	public static class ContentManager
	{
		static string connectionString = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
		static SqlDataReader reader;

		public static Content GetWelcomeContent(string clientid)
		{
            connectionString = Client.ConnectionString(clientid);
			bool success = false;
			Content content = new Content();

			try
			{
				reader = SqlHelper.ExecuteReader(connectionString, CommandType.StoredProcedure, "sp_GetWelcomeContent");

				if (reader.Read())
				{
					content = ReadContent(reader);
				}
				else
					success = false;

				success = true;
			}
			catch { success = false; }
			finally { if (!reader.IsClosed) reader.Close(); }
			return (success ? content : null);
		}

		public static ArrayList GetNewsContents()
		{
			bool success = false;
			Content content = new Content();						
			ArrayList newslist = new ArrayList();

			try
			{
				reader = SqlHelper.ExecuteReader(connectionString, CommandType.StoredProcedure, "sp_GetNewsContent");
				
				if (reader.HasRows)
				{
					while (reader.Read())
					{
						content = ReadContent(reader);
						newslist.Add(content);						
						content = new Content();						
					}
				}
				else
					success = false;

				success = true;
			}
			catch { success = false; }
			finally { if (!reader.IsClosed) reader.Close(); }
			return (success ? newslist : null);
		}

		public static ArrayList GetAnnouncementContents()
		{
			bool success = false;
			Content content = new Content();
			ArrayList announcementlist = new ArrayList();

			try
			{
				reader = SqlHelper.ExecuteReader(connectionString, CommandType.StoredProcedure, "sp_GetAnnouncementContent");

				if (reader.HasRows)
				{
					while (reader.Read())
					{
						content = ReadContent(reader);
						announcementlist.Add(content);
						content = new Content();
					}
				}
				else
					success = false;

				success = true;
			}
			catch { success = false; }
			finally { if (!reader.IsClosed) reader.Close(); }
			return (success ? announcementlist : null);
		}

		private static Content ReadContent(SqlDataReader reader)
		{
			Content content = new Content();
			if (reader != null)
			{
				content.ID = int.Parse(reader["ID"].ToString());
				content.Title = reader["Title"].ToString();
				content.Body = reader["Content"].ToString();
				content.DateCreated = DateTime.Parse(reader["DateCreated"].ToString());
				content.DateModified = DateTime.Parse(reader["DateModified"].ToString());
				content.CreatedBy = int.Parse(reader["CreatedBy"].ToString());
				content.ModifiedBy = int.Parse(reader["ModifiedBy"].ToString());
				content.ImageIcon = reader["ImageIcon"].ToString();
				content.Status = ConvertStringTo.Status(reader["Status"].ToString());
				content.Type = ConvertStringTo.Type(reader["Type"].ToString());

				return content;
			}
			else 
				throw new ArgumentNullException("reader");
		}
	}

	public class Content
	{
		int _id, _createdby, _modifiedby;
		string _title, _content, _createdbyuser, _modifiedbyuser, _imageicon;
		DateTime _datecreated, _datemodified;
		ContentStatus _status;
		ContentType _type;

		public Content()
		{
			_id = _createdby = _modifiedby = 0;
			_title = _content = _createdbyuser = _modifiedbyuser = _imageicon = string.Empty;
			_datecreated = _datemodified = DateTime.Parse("1/1/2000 12:00:00 AM");
		}

		public int ID
		{
			get { return _id; }
			set { _id = value; }
		}

		public string Title
		{
			get { return _title; }
			set { _title = value; }
		}

		public string Body
		{
			get { return _content; }
			set { _content = value; }
		}

		public DateTime DateCreated
		{
			get { return _datecreated; }
			set { _datecreated = value; }
		}

		public DateTime DateModified
		{
			get { return _datemodified; }
			set { _datemodified = value; }
		}

		public int CreatedBy
		{
			get { return _createdby; }
			set { _createdby = value; }
		}

		public int ModifiedBy
		{
			get { return _modifiedby; }
			set { _modifiedby = value; }
		}

		public string CreatedByUser
		{
			get { return _createdbyuser; }
			set { _createdbyuser = value; }
		}

		public string ModifiedByUser
		{
			get { return _modifiedbyuser; }
			set { _modifiedbyuser = value; }
		}

		public string ImageIcon
		{
			get { return _imageicon; }
			set { _imageicon = value; }
		}

		public ContentType Type
		{
			get { return _type; }
			set { _type = value; }
		}

		public ContentStatus Status
		{
			get { return _status; }
			set { _status = value; }
		}
	}

	public enum ContentStatus
	{
		Inactive = 0,
		Active = 1
	}

	public enum ContentType
	{
		Welcome = 0,
		News = 1,
		Announcement = 2
	}

	public static class ConvertStringTo
	{
		public static ContentStatus Status(string status)
		{
			status = status.ToLower();
			if (status == "active")
				return ContentStatus.Active;
			else
				return ContentStatus.Inactive;
		}

		public static ContentType Type(string type)
		{
			type = type.ToLower();
			if (type == "welcome")
				return ContentType.Welcome;
			else if (type == "news")
				return ContentType.News;
			else
				return ContentType.Announcement;
		}
	}
}
