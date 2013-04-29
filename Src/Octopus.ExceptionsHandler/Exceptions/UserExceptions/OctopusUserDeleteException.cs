//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU Lesser General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
//
// Licence : http://www.octopusnetwork.org/OverviewLicence.aspx
//
// Website : http://www.octopusnetwork.org
// Business contact: business(at)octopusnetwork.org
// Technical contact email : tech(at)octopusnetwork.org 

using System;
using System.Runtime.Serialization;

namespace Octopus.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OctopusUserDeleteException.
	/// </summary>
	[Serializable]
	public class OctopusUserDeleteException : OctopusUserException
	{
		private string code;

        protected OctopusUserDeleteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            code = info.GetString("Code");
        }

		public OctopusUserDeleteException(OctopusUserDeleteExceptionEnum exceptionCode)
		{
			this.code = this.FindException(exceptionCode);
            Console.WriteLine("EXCEPTION USER DELETE EXCEPTION: {0}",code);
		}

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code",code);
            base.GetObjectData(info, context);
        }

		public override string ToString()
		{
			return this.code;
		}

		private string FindException(OctopusUserDeleteExceptionEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
				case OctopusUserDeleteExceptionEnum.UserIsNull:
					returned = "OUE1.Text";
					break;

				case OctopusUserDeleteExceptionEnum.AdministratorUser:
					returned = "OUE2.Text";
					break;

                case OctopusUserDeleteExceptionEnum.UserHasContract:
                    returned = "OUE9.Text";
                    break;

                case OctopusUserDeleteExceptionEnum.UserHasTeller:
			        returned = "OUE10.Text";
                    break;
			}
			return returned;
		}
	}

    [Serializable]
	public enum OctopusUserDeleteExceptionEnum
	{
		UserIsNull,
		AdministratorUser,
        UserHasContract,
        UserHasTeller
	}
}
