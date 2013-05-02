// LICENSE PLACEHOLDER

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




        
