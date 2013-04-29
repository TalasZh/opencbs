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
	/// Summary description for OctopusInstallmentsExceptions.
	/// </summary>
    [Serializable]
    public class OctopusInstallmentsException : OctopusException
	{
		private string _code;
		public OctopusInstallmentsException(OctopusInstallmentsExceptionsEnum exceptionCode)
		{
			_code = _FindException(exceptionCode);
		}

        protected OctopusInstallmentsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

		public override string ToString()
		{
			return _code;
		}

		private string _FindException(OctopusInstallmentsExceptionsEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
				case OctopusInstallmentsExceptionsEnum.InstallmentIsNull:
					returned = "InstallmentExceptionInstallmentIsNull.Text";
					break;

				case OctopusInstallmentsExceptionsEnum.InstallmentAlreadyRepaid:
					returned = "InstallmentExceptionInstallmentAlreadyRepaid.Text";
					break;

				case OctopusInstallmentsExceptionsEnum.NotFirstInstallmentToRepay:
					returned = "InstallmentExceptionNotFirstInstallmentToRepay.Text";
					break;

				case OctopusInstallmentsExceptionsEnum.NotFirstInstallmentToCancelRepay:
					returned = "InstallmentExceptionNotFirstInstallmentToCancelRepay.Text";
					break;
			}
			return returned;
		}
	}

    [Serializable]
	public enum OctopusInstallmentsExceptionsEnum
	{
		InstallmentIsNull,
		InstallmentAlreadyRepaid,
		NotFirstInstallmentToRepay,
		NotFirstInstallmentToCancelRepay,
	}
}
