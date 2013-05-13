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
