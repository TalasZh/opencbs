// Octopus MFS is an integrated suite for managing a Micro Finance Institution: 
// clients, contracts, accounting, reporting and risk
// Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
// Website: http://www.opencbs.com
// Contact: contact@opencbs.com

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
		    string resourceName = ex is OpenCbsException ? ex.ToString() : "sqlError.Text";
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
