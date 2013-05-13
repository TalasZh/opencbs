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

using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NUnit.Framework;
using NUnit.Mocks;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Enums;
using OpenCBS.Manager;
using OpenCBS.Manager.Products;
using OpenCBS.Services;
using OpenCBS.ExceptionsHandler;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Test.Services
{
	/// <summary>
	/// Description r�sum�e de TestProductServices.
	/// </summary>
	/// 
	[TestFixture]
	public class TestRegExServices
	{
		[Test]
		public void CheckRegEx()
		{
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ID_PATTERN, @"[0-9]{2}[A-Z]{2}");
            Assert.IsFalse(ServicesProvider.GetInstance().GetRegExCheckerServices().CheckID("11111"));
            Assert.IsTrue(ServicesProvider.GetInstance().GetRegExCheckerServices().CheckID("22DD"));

        }

		
	}
}
