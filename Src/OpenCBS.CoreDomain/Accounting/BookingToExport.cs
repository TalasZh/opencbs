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
