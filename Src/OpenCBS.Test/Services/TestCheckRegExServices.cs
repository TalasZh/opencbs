// LICENSE PLACEHOLDER

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
