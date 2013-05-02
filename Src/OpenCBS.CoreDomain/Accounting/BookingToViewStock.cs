// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using OpenCBS.Enums;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Accounting
{
    [Serializable]
    public class BookingToViewStock : List<BookingToView>
    {
        private readonly Account _account;

        public BookingToViewStock(Account pAccount)
        {
            _account = pAccount;
        }

        public OCurrency   GetSelectedBookingBalanceInInternalCurrency
        {
            get
            {
                OCurrency balance = 0;
                foreach (BookingToView booking in this)
                {
                    if (booking.Direction == OBookingDirections.Credit)
                        balance -= booking.AmountInternal;
                    else
                        balance += booking.AmountInternal;
                }
                return balance;
            }
        }

        public OCurrency  GetSelectedBookingBalanceInExternalCurrency
        {
            get
            {
                OCurrency balance = 0;
                foreach (BookingToView booking in this)
                {
                    if (!booking.ExchangeRate.HasValue)
                        return null;

                    if (booking.Direction == OBookingDirections.Credit)
                        balance -= booking.AmountInternal * Convert.ToDecimal(booking.ExchangeRate.Value);
                    else
                        balance += booking.AmountInternal * Convert.ToDecimal(booking.ExchangeRate.Value);
                }
                return balance;
            }
        }

        public OCurrency GetSelectedExportedBalanceInInternalCurrency
        {
            get
            {
                OCurrency balance = 0;
                foreach (BookingToView booking in this.FindAll(item => item.IsExported))
                {
                    if (booking.Direction == OBookingDirections.Credit)
                        balance -= booking.AmountInternal;
                    else
                        balance += booking.AmountInternal;
                }
                return balance;
            }
        }

        public OCurrency GetAccountBalance
        {
            get { return _account.Balance; }
        }
    }
}
