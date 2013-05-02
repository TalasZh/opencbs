//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU Lesser General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
//
// Licence : http://www.octopusnetwork.org/OverviewLicence.aspx
//
// Website : http://www.octopusnetwork.org
// Business contact: business(at)octopusnetwork.org
// Technical contact email : tech(at)octopusnetwork.org 

using System;
using System.Collections.Generic;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Enums;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.FundingLines
{
    public interface IFundingLineLazyLoader
    {
        OCurrency GetAmount(FundingLine fl);
        OCurrency GetRealAmount(FundingLine fl);
        OCurrency GetAnticipatedAmount(FundingLine fl);
        List<FundingLineEvent> GetEvents(FundingLine fl);
    }

    [Serializable]
    public class FundingLine
    {
        private List<FundingLineEvent> _events;
        private OCurrency? _amount;
        private OCurrency _realAmount;
        private OCurrency _anticipatedAmount;
        private IFundingLineLazyLoader _lazyLoader;

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public bool Deleted { get; set; }
        public Currency Currency { get; set; }
        public string Purpose { get; set; }

        public FundingLine()
        {}

        public FundingLine(string name, bool deleted)
        {
            Name = name;
            Deleted = deleted;
        }

        public void SetLazyLoader(IFundingLineLazyLoader loader)
        {
            _lazyLoader = loader;
        }

        public override string ToString()
        {
            return Name;
        }

        public OCurrency Amount
        {
            get
            {
                if (_amount.HasValue) return _amount.Value;
                _amount = null == _lazyLoader ? 0m : _lazyLoader.GetAmount(this);
                return _amount.Value;
            }
            set { _amount = value; }
        }

        public OCurrency RealRemainingAmount
        {
            get
            {
                if (_realAmount.HasValue) return _realAmount.Value;
                _realAmount = null == _lazyLoader ? 0m : _lazyLoader.GetRealAmount(this);
                return _realAmount.Value;
            }
            set { _realAmount = value; }
        }

        public OCurrency AnticipatedRemainingAmount
        {
            get
            {
                if (_anticipatedAmount.HasValue) return _anticipatedAmount.Value;
                _anticipatedAmount = null == _lazyLoader ? 0m : _lazyLoader.GetAnticipatedAmount(this);
                return _anticipatedAmount.Value;
            }
            set { _anticipatedAmount = value; }
        }

        public List<FundingLineEvent> Events
        {
            get
            {
                if (_events != null) return _events;
                _events = null == _lazyLoader ? new List<FundingLineEvent>() : _lazyLoader.GetEvents(this);
                return _events;
            }
            set { _events = value; }
        }

        private void InvalidateAmounts()
        {
            _amount = null;
            _realAmount = null;
            _anticipatedAmount = null;
        }

        public void AddEvent(FundingLineEvent fundingLineEvent)
        {
            InvalidateAmounts();
            if (null == _events) return;
            FundingLineEvent fle = Events.Find(e => e.Id == fundingLineEvent.Id);
            if (null == fle)
            {
                Events.Add(fundingLineEvent);
            }
            else
            {
                fle.IsDeleted = false;
            }
            SortList();
        }

        public void RemoveEvent(FundingLineEvent fundingLineEvent)
        {
            if (null == _events) return;
            if (0 == Events.Count) return;
            FundingLineEvent fle = Events.Find(e => e.Id == fundingLineEvent.Id);
            if (null == fle) return;
            fle.IsDeleted = false;
        }

        public double[] CalculateCashProvisionChart(DateTime startDate, int numDays, bool assumeLateLoansRepaidToday)
        {
            double[] data = null;
            if (null == _events) return data;
            if (0 == Events.Count) return data;

            data = new double[numDays];
            OCurrency curAmount;

            for (int counter = 0; counter < numDays; counter++)
            {
                curAmount = 0;

                foreach (FundingLineEvent line in Events)
                {
                    if (line.CreationDate.Date <= startDate.AddDays(counter) &&
                        !line.Type.Equals(OFundingLineEventTypes.Commitment)
                        && !line.IsDeleted)
                    {
                        curAmount += (line.Movement == OBookingDirections.Credit)
                                         ? line.Amount
                                         : (-1.0)*(line.Amount);
                    }
                }
                data[counter] = Convert.ToDouble(curAmount.Value);
            }

            return data;
        }

        private void SortList()
        {
            Events.Sort((p1, p2) => p2.CreationDate.CompareTo(p1.CreationDate));
        }
    }
}