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

namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment
{
    [Serializable]
    public class NonRepaymentPenalties
    {
        private double _initialAmount; 
        private double _olb;        
        private double _overDuePrincipal;   
        private double _overDueInterest;

        public NonRepaymentPenalties()
        {
        }

        public NonRepaymentPenalties(double pInitialAmount, double pOlb, double pOverduePrincipal, double pOverdueInterest)
        {
            _initialAmount = pInitialAmount;
            _olb = pOlb;
            _overDueInterest = pOverdueInterest;
            _overDuePrincipal = pOverduePrincipal;
        }

        public double InitialAmount
        {
            get { return _initialAmount; }
            set { _initialAmount = value; }
        }

        public double OLB
        {
            get { return _olb; }
            set { _olb = value; }
        }

        public double OverDuePrincipal
        {
            get { return _overDuePrincipal; }
            set { _overDuePrincipal = value; }
        }

        public double OverDueInterest
        {
            get { return _overDueInterest; }
            set { _overDueInterest = value; }
        }
    }

    [Serializable]
    public class NonRepaymentPenaltiesNullableValues
    {
        private double? _initialAmount; 
        private double? _olb;        
        private double? _overDuePrincipal;   
        private double? _overDueInterest;

        public NonRepaymentPenaltiesNullableValues()
        {
        }

        public NonRepaymentPenaltiesNullableValues(double? pInitialAmount, double? pOlb, double? pOverduePrincipal, double? pOverdueInterest)
        {
            _initialAmount = pInitialAmount;
            _olb = pOlb;
            _overDueInterest = pOverdueInterest;
            _overDuePrincipal = pOverduePrincipal;
        }

        public double? InitialAmount
        {
            get { return _initialAmount; }
            set { _initialAmount = value; }
        }

        public double? OLB 
        {
            get { return _olb; }
            set { _olb = value; }
        }

        public double? OverDuePrincipal
        {
            get { return _overDuePrincipal; }
            set { _overDuePrincipal = value; }
        }

        public double? OverDueInterest
        {
            get { return _overDueInterest; }
            set { _overDueInterest = value; }
        }
    }
}
