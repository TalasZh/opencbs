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
