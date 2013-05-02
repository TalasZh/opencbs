// LICENSE PLACEHOLDER

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
