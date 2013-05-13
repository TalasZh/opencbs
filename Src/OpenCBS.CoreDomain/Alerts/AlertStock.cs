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
using System.Collections;
using System.Collections.Generic;

namespace OpenCBS.CoreDomain
{
	/// <summary>
	/// Summary description for AlertStock.
    /// </summary>
    [Serializable]
	public class AlertStock : IEnumerable
	{
		private readonly Hashtable _list;

		public AlertStock()
		{
			_list = new Hashtable();
		}

		public void Add(Alert pAlert)
		{
			AddAlert(pAlert);
		}

		public Alert GetAlert(int pContractId)
		{
            return _list.ContainsKey(pContractId) ? _list[pContractId] as Alert : null;
		}

		private void AddAlert(Alert pAlert)
		{
			if(!_list.ContainsKey(pAlert.LoanId))
				_list.Add(pAlert.LoanId,pAlert);
			
			else
			{
				Alert alert = _list[pAlert.LoanId] as Alert;
                decimal amount = alert.Amount.Value + pAlert.Amount.Value;
	
				if(alert.EffectDate > pAlert.EffectDate)
				{
					_list.Remove(pAlert.LoanId);
					pAlert.Amount = amount;
					_list.Add(pAlert.LoanId,pAlert);
				}
				else
					(_list[pAlert.LoanId] as Alert).Amount = amount;
			}
		}

		public AlertStock DisbursmentAlerts
		{
			get
			{
				AlertStock list = new AlertStock();
				foreach(Alert _alert in _list.Values)
				{
					if (_alert.Type == 'D')
						list.Add(_alert);
				}
				return list;
			}
		}

		public AlertStock RepaymentAlerts
		{
			get
			{
				AlertStock list = new AlertStock();
				foreach(Alert _alert in _list.Values)
				{
					if (_alert.Type == 'R')
						list.Add(_alert);
				}
				return list;
			}
		}


		public void Add(AlertStock alertStock)
		{
			foreach(DictionaryEntry entry in alertStock)
			{
				AddAlert((Alert)entry.Value);
			}
		}

		public int GetNumberOfAlerts
		{
			get{return _list.Count;}
		}

		public IEnumerator GetEnumerator()
		{
			return _list.GetEnumerator();
		}

        public List<Alert> SortAlertsByLoanStatus()
        {
            List<Alert> listDisbursement = new List<Alert>();
            List<Alert> listRepayment = new List<Alert>();

            foreach (Alert alert in _list.Values)
            {
                if (alert.Type == 'D')
                    listDisbursement.Add(alert);
                else
                    listRepayment.Add(alert);
            }
            listDisbursement.Sort((x, y) => x.LoanStatus.CompareTo(y.LoanStatus));
            listRepayment.Sort((x, y) => x.LoanStatus.CompareTo(y.LoanStatus));
            listDisbursement.AddRange(listRepayment);
            return listDisbursement;
        }

		public List<Alert> SortAlertsByDate()
		{
            List<Alert> listDisbursement = new List<Alert>();
            List<Alert> listRepayment = new List<Alert>();

			foreach(Alert alert in _list.Values)
			{
				if(alert.Type == 'D')
					listDisbursement.Add(alert);
				else
					listRepayment.Add(alert);
			}
			listDisbursement.Sort((x,y) => x.EffectDate.CompareTo(y.EffectDate));
            listRepayment.Sort((x, y) => x.EffectDate.CompareTo(y.EffectDate));
			listDisbursement.AddRange(listRepayment);
			return listDisbursement;
		}
	}
}
