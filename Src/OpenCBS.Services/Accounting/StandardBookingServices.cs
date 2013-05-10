// LICENSE PLACEHOLDER

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
