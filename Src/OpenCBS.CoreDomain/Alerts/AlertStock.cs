// LICENSE PLACEHOLDER

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
