using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace Ava.Exceptions
{
	[Serializable()]
	public class EmptyInputException : Exception
	{
		public EmptyInputException() { }
		public EmptyInputException(string message):base(message){}		 
	}
}
