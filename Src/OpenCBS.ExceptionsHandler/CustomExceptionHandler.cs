// LICENSE PLACEHOLDER

using System;
using OpenCBS.MultiLanguageRessources;

namespace OpenCBS.ExceptionsHandler
{
	public struct ExceptionStatus
	{
		public Exception Ex;
		public string Message;
	}
	/// <summary>
	/// Summary description for CustomExceptionHandler.
	/// </summary>
	public class CustomExceptionHandler
	{
		public static ExceptionStatus ShowExceptionText(Exception ex)
		{
		    string resourceName = ex is OctopusException ? ex.ToString() : "sqlError.Text";
		    return ShowExceptionText(ex, resourceName);
		}

        public static ExceptionStatus ShowExceptionText(Exception ex, string resourceName)
        {
            var message = MultiLanguageStrings.GetString(Ressource.ApplicationExceptionHandler, resourceName);
            var exceptionStatus = new ExceptionStatus
            {
                Ex = ex,
                Message = string.IsNullOrEmpty(message) ? resourceName : message
            };

            return exceptionStatus;            
        }
	}
}
