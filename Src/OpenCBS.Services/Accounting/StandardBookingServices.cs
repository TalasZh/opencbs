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
using OpenCBS.Manager.Accounting;
using OpenCBS.CoreDomain;
using OpenCBS.ExceptionsHandler;
using OpenCBS.CoreDomain.Accounting;

namespace OpenCBS.Services.Accounting
{
    public class StandardBookingServices : MarshalByRefObject
    {
        private StandardBookingManager _StandardBookingManager;
		private User _user;

        #region constructors
        public StandardBookingServices(User pUser)
        {
            _user = pUser;
            _StandardBookingManager = new StandardBookingManager(pUser);
        }

 		public StandardBookingServices(string testDB)
		{
            
			
		}

        public StandardBookingServices(AccountManager accountManagement)
		{
		}
		#endregion

        private void _checkStandardBooking(Booking pBooking)
        {
            if (string.IsNullOrEmpty(pBooking.Name))
                throw new OpenCbsBookingException(OpenCbsBookingExceptionsEnum.NameIsEmpty);
            if (pBooking.CreditAccount == null)
                throw new OpenCbsBookingException(OpenCbsBookingExceptionsEnum.CreditAccountIsEmpty);
            if (pBooking.DebitAccount == null)
                throw new OpenCbsBookingException(OpenCbsBookingExceptionsEnum.DebitAccountIsEmpty);
            if (pBooking.CreditAccount.Number == pBooking.DebitAccount.Number)
                throw new OpenCbsBookingException(OpenCbsBookingExceptionsEnum.DebitAndCreditAccountAreIdentical);
        }

        public void CreateStandardBooking(Booking booking)
        {
            _checkStandardBooking(booking);
            _StandardBookingManager.CreateStandardBooking(booking);
        }

        public void DeleteStandardBooking(int Id)
        {
            _StandardBookingManager.DeleteStandardBooking(Id);
        }

        public List<Booking> SelectAllStandardBookings()
        {
           return _StandardBookingManager.SelectAllStandardBookings();
        }

        public void UpdateStandardBookings(Booking booking)
        {
            _checkStandardBooking(booking);
            _StandardBookingManager.UpdateStandardBooking(booking);
        }

        public Booking SelectStandardBookingById(int Id)
        {
            return _StandardBookingManager.SelectStandardBookingById(Id);
        }
    }
}
