// LICENSE PLACEHOLDER

using System;
using OpenCBS.Enums;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Accounting
{
    [Serializable]
    public class BookingToView
    {
        private OCurrency _amount;
        private double? _exchangeRate;

        public OAccountingLabels AccountingLabel { get; set; }
        public string ContractCode { get; set; }
        public OBookingDirections Direction { get; set; }
        public DateTime Date { get; set; }
        public string EventCode { get; set; }
        public bool IsExported { get; set; }

        public BookingToView(){}

        public BookingToView(OBookingDirections pDirection, OCurrency pAmount, DateTime pDate, double? pExchangeRate, string pContractCode, string pEventCode)
        {
        	Direction = pDirection;
        	_amount = pAmount;
        	Date = pDate;
        	_exchangeRate = pExchangeRate;
        	ContractCode = pContractCode;
        	EventCode = pEventCode;
        }
        	
        public double? ExchangeRate
        {
            get { return _exchangeRate; }
            set { _exchangeRate = value; }
        }

        public OCurrency   AmountInternal
		{
			get{return _amount;}
			set{_amount = value;}
        }

        public OCurrency  ExternalAmount
        {
            get
            {
                if (!_exchangeRate.HasValue)
                    return null;
                return _amount * 1 / Convert.ToDecimal(_exchangeRate.Value);
            }
        }

    }
}
