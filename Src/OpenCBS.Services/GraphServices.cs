// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Linq;
using OpenCBS.CoreDomain.FundingLines;
using OpenCBS.Enums;
using OpenCBS.CoreDomain;
using System.Collections;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Manager.Contracts;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Services
{
	/// <summary>
	/// Summary description for GraphServices.
	/// </summary>
    public class GraphServices : MarshalByRefObject
    {
        private readonly LoanManager _contractManagement;
        private readonly User _user;


        public GraphServices(User pUser)
        {
            _contractManagement = new LoanManager(pUser);
            _user = pUser;
        }

        public GraphServices(LoanManager pContractManagement)
        {
            _contractManagement = pContractManagement;
            _user = new User();
        }

        public string[] CalculateDate(DateTime pFirstDate, int pForecastDays)
        {
            string[] list = new string[pForecastDays];
            for (int i = 0; i < pForecastDays; i++)
            {
                list[i] = pFirstDate.AddDays(i).ToShortDateString();
            }
            return list;
        }

        public double[] CalculateRealPrevisionCurve(DateTime pFirstDate, int pForecastDays)
        {
            double[] realAmount = new double[pForecastDays];

            double initialAmount = Convert.ToDouble(ChartOfAccounts.GetInstance(_user).GetPivotBalance(OAccounts.CASH).Value);
            double[] realDisbursment = CalculateRealDisbursmentCurve(pFirstDate, pForecastDays);
            double[] realRepay = CalculateRealRepayCurve(pFirstDate, pForecastDays);

            for (int i = 0; i < pForecastDays; i++)
            {
                realAmount[i] = initialAmount - realDisbursment[i] + realRepay[i];
            }
            return realAmount;
        }

        public double[] CalculateRealDisbursmentCurve(DateTime pFirstDate, int pForecastDays)
        {
            double[] list = new double[pForecastDays];


            List<KeyValuePair<DateTime, decimal>> result = _contractManagement.CalculateCashToDisburseByDay(pFirstDate,
                                                                                                            pFirstDate.AddDays(pForecastDays));
            for (int i = 0; i < pForecastDays; i++)
            {
                OCurrency amount = result.Where(x => x.Key <= pFirstDate.AddDays(i)).Sum(x => x.Value);
                list[i] = Convert.ToDouble(amount.Value);
            }
            return list;
        }

        public double[] CalculateRealRepayCurve(DateTime pFirstDate, int pForecastDays)
        {
            double[] list = new double[pForecastDays];

            List<KeyValuePair<DateTime, decimal>> result = _contractManagement.CalculateCashToRepayByDay(pFirstDate,
                                                                                                         pFirstDate.AddDays(pForecastDays));
            for (int i = 0; i < pForecastDays; i++)
            {
                OCurrency amount = result.Where(x => x.Key <= pFirstDate.AddDays(i)).Sum(x => x.Value);
                list[i] = Convert.ToDouble(amount.Value);
            }
            return list;
        }

        public double[] CalculateChartForFundingLine(FundingLine pFundingLine, DateTime pStartDate, int pDayNum, bool pAssumeLateLoansRepaidToday)
        {
            bool creditInterestsInFundingLine = ApplicationSettings.GetInstance(_user.Md5).InterestsCreditedInFL;
            List<KeyValuePair<DateTime, decimal>> result = _contractManagement.CalculateCashToRepayByDayByFundingLine(pFundingLine.Id,
                                                                                              pAssumeLateLoansRepaidToday,
                                                                                              creditInterestsInFundingLine);
            double[] y = pFundingLine.CalculateCashProvisionChart(pStartDate, pDayNum, pAssumeLateLoansRepaidToday);

            if (y == null) return y;
            for (int i = 0; i < pDayNum; i++)
            {
                decimal amount = result.Where(x => x.Key <= pStartDate.AddDays(i)).Sum(x => x.Value);
                y[i] += Convert.ToDouble(amount);
            }
            return y;
        }
    }
}
