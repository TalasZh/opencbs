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
using System.Collections.Generic;

namespace OpenCBS.CoreDomain.Accounting
{
    [Serializable]
	public class ProvisionTable
	{
        private List<ProvisioningRate> _provisioningRates;
        private static readonly IDictionary<string, ProvisionTable> ProvisioningTableList = new Dictionary<string, ProvisionTable>();

        public static void SuppressRemotingInfos(string pMd5)
        {
            if (ProvisioningTableList.ContainsKey(pMd5))
            {
                ProvisioningTableList.Remove(pMd5);
            }
        }

		private ProvisionTable()
        {
			_provisioningRates = new List<ProvisioningRate>();
		}


        public static ProvisionTable GetInstance(User pUser)
        {
            if (!ProvisioningTableList.ContainsKey(pUser.Md5))
                ProvisioningTableList.Add(pUser.Md5, new ProvisionTable());
            return ProvisioningTableList[pUser.Md5];
        }

		public List<ProvisioningRate> ProvisioningRates
		{
			get{return _provisioningRates;}
			set{_provisioningRates = value;}
		}

		public void Add(ProvisioningRate pR)
		{
			_provisioningRates.Add(pR);
		}

        public void Add(List<ProvisioningRate> pList)
        {
            _provisioningRates.AddRange(pList);
        }

		public ProvisioningRate GetProvisioningRate(int rank)
		{
			return rank < _provisioningRates.Count && rank >= 0 ? _provisioningRates[rank] : null;
		}

		public ProvisioningRate GetProvisiningRateByNbOfDays(int nbOfDays)
		{
			foreach (ProvisioningRate pR in _provisioningRates)
			{
				if (nbOfDays >= pR.NbOfDaysMin && nbOfDays <= pR.NbOfDaysMax)
					return pR;
			}
			return null;
		}
	}
}
