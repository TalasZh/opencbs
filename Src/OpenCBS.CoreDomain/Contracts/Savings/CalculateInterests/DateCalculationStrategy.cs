// LICENSE PLACEHOLDER

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
