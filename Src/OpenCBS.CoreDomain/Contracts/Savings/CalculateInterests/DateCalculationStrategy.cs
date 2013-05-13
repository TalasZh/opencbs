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
using OpenCBS.CoreDomain.Contracts.Loans.Installments;

namespace OpenCBS.CoreDomain.Contracts.Savings.CalculateInterests
{
    public class DateCalculationStrategy
    {
        public static bool DateCalculationDiary(DateTime lastDate, DateTime date)
        {
            return lastDate.Year < date.Year
                        || (lastDate.Year == date.Year && lastDate.Month < date.Month)
                        || (lastDate.Year == date.Year && lastDate.Month == date.Month && lastDate.Day < date.Day)
                        || (lastDate.Year == date.Year && lastDate.Month == date.Month && lastDate.Day < date.Day);
        }

        public static bool DateCalculationWeekly(DateTime lastDate, DateTime date, int pWeekEndDay2)
        {
            int weekEndDay2 = pWeekEndDay2 == 6 ? 1 : pWeekEndDay2 + 1;

            int numbersOfDayToNextPostingDate = (weekEndDay2 <= (int)lastDate.DayOfWeek) ? 7 - ((int)lastDate.DayOfWeek - (int)weekEndDay2)
                : (int)weekEndDay2 - (int)lastDate.DayOfWeek;

            return date.Subtract(lastDate).Days >= numbersOfDayToNextPostingDate;
        }

        public static bool DateCalculationMonthly(DateTime lastDate, DateTime pDate)
        {
            return lastDate.Year < pDate.Year || (lastDate.Year == pDate.Year && lastDate.Month < pDate.Month);
        }

        public static bool DateCalculationYearly(DateTime lastDate, DateTime date)
        {
            return lastDate.Year < date.Year;
        }

        public static DateTime GetNextWeekly(DateTime date, int pWeekEndDay2)
        {
            int weekEndDay2 = pWeekEndDay2 == 6 ? 1 : pWeekEndDay2 + 1;

            return date.AddDays((weekEndDay2 <= (int)date.DayOfWeek)
                   ? 7 - ((int)date.DayOfWeek - weekEndDay2)
                   : weekEndDay2 - (int)date.DayOfWeek);
        }

        public static DateTime GetNextMaturity(DateTime date, InstallmentType periodicity, int numberPeriods)
        {
            for (int i = 0; i < numberPeriods; i++)
            {
                date = date.AddMonths(periodicity.NbOfMonths).AddDays(periodicity.NbOfDays);
            }

            return date;
        }

        public static DateTime GetLastMaturity(DateTime date, InstallmentType periodicity, int numberPeriods)
        {
           for (int i = 0; i < numberPeriods; i++)
            {
                date = date.AddMonths(-periodicity.NbOfMonths).AddDays(-periodicity.NbOfDays);
            }

           return date;
        }
    }
}
