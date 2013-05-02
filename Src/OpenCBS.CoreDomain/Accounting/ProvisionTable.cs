// LICENSE PLACEHOLDER

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
