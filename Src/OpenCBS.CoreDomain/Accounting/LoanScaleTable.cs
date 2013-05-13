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

namespace OpenCBS.CoreDomain.Accounting
{
    [Serializable]
    public class LoanScaleTable
    {
        private ArrayList _loanscaleRates;
        private static readonly IDictionary<string, LoanScaleTable> LoanscaleTableList = new Dictionary<string, LoanScaleTable>();
        public static void SuppressRemotingInfos(string pMd5)
        {
            if (LoanscaleTableList.ContainsKey(pMd5))
            {
                LoanscaleTableList.Remove(pMd5);
            }
        }

        private LoanScaleTable()
        {
            _loanscaleRates = new ArrayList();
        }
        public static LoanScaleTable GetInstance(User pUser)
        {
            if (!LoanscaleTableList.ContainsKey(pUser.Md5))
                LoanscaleTableList.Add(pUser.Md5, new LoanScaleTable());
            return LoanscaleTableList[pUser.Md5];
         }

        public ArrayList LoanScaleRates
        {
            get { return _loanscaleRates; }
            set { _loanscaleRates = value; }
        }
        public void AddLoanScaleRate(LoanScaleRate lt)
        {
            _loanscaleRates.Add(lt);
        }

        public void DeleteLoanScaleRate(LoanScaleRate lt)
        {
            _loanscaleRates.Remove(lt);
            int number = 1;
            foreach (LoanScaleRate lsr in _loanscaleRates)
            {
                lsr.Number = number;
                number++;
            }
        }

        public LoanScaleRate GetLoanScaleRate(int rank)
        {
            return rank < _loanscaleRates.Count && rank >= 0 ? (LoanScaleRate)_loanscaleRates[rank] : null;

        }
        public LoanScaleRate GetLoanScaleRatebyNum(int ScaleNum)
        {
            foreach (LoanScaleRate lR in _loanscaleRates)
            {
                if (ScaleNum >= lR.ScaleMin && ScaleNum <= lR.ScaleMax)
                    return lR;
            }
            return null;

        }
    }

}




        
