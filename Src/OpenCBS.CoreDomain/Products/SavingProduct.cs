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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Enums;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Products
{
	[Serializable]
	public class SavingProduct
	{
		public int Id { get; set; }
		public bool Delete { get; set; }
		public string Name { get; set; }
		public OClientTypes ClientType { get; set; }
		public OProductTypes ProductType { get { return OProductTypes.Saving; } }
		public OCurrency InitialAmountMin { get; set; }
		public OCurrency InitialAmountMax { get; set; }
		public OCurrency BalanceMin { get; set; }
		public OCurrency BalanceMax { get; set; }
		public OCurrency WithdrawingMin { get; set; }
		public OCurrency WithdrawingMax { get; set; }
		public OCurrency DepositMin { get; set; }
		public OCurrency DepositMax { get; set; }
		public double? InterestRate { get; set; }
		public double? InterestRateMin { get; set; }
		public double? InterestRateMax { get; set; }
        public OSavingInterestBase InterestBase { get; set; }
        public OSavingInterestFrequency InterestFrequency { get; set; }
        public OSavingCalculAmountBase? CalculAmountBase { get; set; }
        public OCurrency EntryFeesMin { get; set; }
        public OCurrency EntryFeesMax { get; set; }
        public OCurrency EntryFees { get; set; }
        public Currency Currency { get; set; }
	}
}
