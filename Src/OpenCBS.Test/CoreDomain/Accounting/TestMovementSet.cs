// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenCBS.CoreDomain;
using System.Collections;
using OpenCBS.CoreDomain.Accounting;

namespace OpenCBS.Test.CoreDomain
{
	/// <summary>
	/// This is the class test for the class MovementSet.
	/// </summary>
	[TestFixture]
	public class TestMovementSet
	{
		private AccountingTransaction movementSet;
		private List<Booking> Bookings;
        
		[SetUp]
		public void SetUp()
		{
			movementSet = new AccountingTransaction();
            Bookings = new List<Booking>();
		    Booking Booking = new Booking(1, new Account("100000", "first account"), 1000, new Account("200000", "second account"), movementSet.Date, new Branch {Id = 1});
			Bookings.Add(Booking);
		}

		[Test]
		public void TestMovementSetIdCorrectlySetAndRetrieved()
		{
			movementSet.Id = 1;
			Assert.AreEqual(1,movementSet.Id);
		}

		[Test]
		public void TestUserCorrectlySetAndRetrieved()
		{
			User user = new User();
			user.UserName = "lotrasmic";
			movementSet.User = user;
			Assert.AreEqual("lotrasmic",movementSet.User.UserName);
		}

		[Test]
		public void TestMovementSetDateCorrectlySetAndRetrieved()
		{
			DateTime date = new DateTime(2006,1,4);
			movementSet.Date = date;
			Assert.AreEqual(date,movementSet.Date);
		}

		[Test]
		public void TestMovementSetBookingListCorrectlySetAndRetrieved()
		{
			movementSet.Bookings = Bookings;
			Assert.AreEqual(Bookings.Count,movementSet.Bookings.Count);
		}

		[Test]
		public void TestAddBooking()
		{
            movementSet.AddBooking(new Booking(1, new Account("300000", "third account"), 500, new Account("0", "forth account"), movementSet.Date, new Branch{Id = 1}));
			Assert.AreEqual(1,movementSet.Bookings.Count);
		}

		[Test]
		public void TestGetBookingWhenRankCorrect()
		{
            movementSet.AddBooking(new Booking(1, new Account("300000", "third account"), 500, new Account("300000", "forth account"), movementSet.Date, new Branch{Id = 1}));
			Assert.AreEqual(1,movementSet.GetBooking(0).Number);
		}

		[Test]
		public void TestGetBookingWhenRankNotCorrect()
		{
            Assert.IsNull(movementSet.GetBooking(3));
		}

		[Test]
		public void TestMergeTwoMovementSet()
		{
			AccountingTransaction mvt1 = new AccountingTransaction();
			mvt1.Bookings = new List<Booking>();

            mvt1.AddBooking(new Booking(1, new Account("100000", "first1 account"), 1000, new Account("100000", "first2 account"), movementSet.Date, new Branch{Id = 1}));
            mvt1.AddBooking(new Booking(2, new Account("100000", "first3 account"), 1000, new Account("100000", "first4 account"), movementSet.Date, new Branch{Id = 1}));
			Assert.AreEqual(2,mvt1.Bookings.Count);
			
			AccountingTransaction mvt2 = new AccountingTransaction();
            mvt2.Bookings = new List<Booking>();
            mvt2.AddBooking(new Booking(1, new Account("100000", "second1 account"), 1000, new Account("100000", "second2 account"), movementSet.Date, new Branch{Id = 1}));
            mvt2.AddBooking(new Booking(2, new Account("100000", "second3 account"), 1000, new Account("100000", "second4 account"), movementSet.Date, new Branch{Id = 1}));
			Assert.AreEqual(2,mvt2.Bookings.Count);

			mvt1.Merge(mvt2);
			Assert.AreEqual(4,mvt1.Bookings.Count);
		}
	}
}
