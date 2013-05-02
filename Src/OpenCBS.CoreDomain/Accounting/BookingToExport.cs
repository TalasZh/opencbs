// LICENSE PLACEHOLDER

using System;
using OpenCBS.Enums;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Accounting
{
	/// <summary>
	/// Summary description for ElementaryMvtToExport.
    /// </summary>
    [Serializable]
	public class BookingToExport
	{
	    public int Elem_mvment_id { get; set;}
	    private double? _exchangeRate;

	    public string ContractCode { get; set; }
	    public OAccountingLabels AccountingLabel { get; set; }
	    public string Purpose { get; set; }
	    public string FundingLine { get; set; }
	    public int Number { get; set; }
	    public string DebitLocalAccountNumber { get; set; }
	    public string CreditLocalAccountNumber { get; set; }
	    public OCurrency InternalAmount { get; set; }
	    public string UserName { get; set; }
	    public DateTime Date { get; set; }
	    public string EventCode { get; set; }
	    public int MovmentSetId { get; set; }
        public Currency BookingCurrency { get; set; }
	    public OCurrency ExternalAmount
		{
			get
			{
			    if(!_exchangeRate.HasValue)
					return null;
			    
                return InternalAmount / Convert.ToDecimal(_exchangeRate.Value);
			}
		}

	    public double? ExchangeRate
		{
			get	{return _exchangeRate;}
			set {_exchangeRate = value;}
		}
	}
}
