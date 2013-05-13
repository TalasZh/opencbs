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

using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.Enums;
using OpenCBS.Shared;
using OpenCBS.CoreDomain.Accounting;

namespace OpenCBS.CoreDomain.Products
{
    public interface ISavingProduct
    {
        int Id { get; set; }
        bool Delete { get; set; }
        string Name { get; set; }
        string Code { get; set; }
        OClientTypes ClientType { get; set; }
        OCurrency InitialAmountMin { get; set; }
        OCurrency InitialAmountMax { get; set; }
        OCurrency BalanceMin { get; set; }
        OCurrency BalanceMax { get; set; }
        OCurrency WithdrawingMin { get; set; }
        OCurrency WithdrawingMax { get; set; }
        OCurrency DepositMin { get; set; }
        OCurrency DepositMax { get; set; }
        OCurrency ChequeDepositMin { get; set; }
        OCurrency ChequeDepositMax { get; set; }
        OCurrency TransferMin { get; set; }
        OCurrency TransferMax { get; set; }
        double? InterestRate { get; set; }
        double? InterestRateMin { get; set; }
        double? InterestRateMax { get; set; }
        OCurrency EntryFeesMin { get; set; }
        OCurrency EntryFeesMax { get; set; }
        OCurrency EntryFees { get; set; }
        Currency Currency { get; set; }
        InstallmentType Periodicity { get; set; }
    }
}
