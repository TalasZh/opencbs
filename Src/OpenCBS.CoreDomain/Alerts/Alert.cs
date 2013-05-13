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

namespace OpenCBS.CoreDomain
{
    /// <summary>
    /// Summary description for Alert.
    /// </summary>
    [Serializable]
    public class Alert //needs to be renamed
    {
        private Dictionary<string, object> _alertFieldAndValues;
        private bool _useCents;
        public Alert()
        {
            _alertFieldAndValues = new Dictionary<string, object>();
        }

        public bool UseCents
        {
            get { return _useCents; }
            set { _useCents = value; }
        }

        public void AddParameter(string pParameter, object pValue)
        {
            _alertFieldAndValues.Add(pParameter, pValue);
        }

        public char Type { get; set; }

        public int LoanId
        {
            get
            {
                 return Convert.ToInt32(_alertFieldAndValues[OAlertSettings.LOAN_ID]);
            }
            set
            {
                _alertFieldAndValues.Remove(OAlertSettings.LOAN_ID);
                _alertFieldAndValues.Add(OAlertSettings.LOAN_ID, value);
            }
        }

        public string LoanCode
        {
            get
            {
                return (string)_alertFieldAndValues[OAlertSettings.LOAN_CODE];
            }
            set
            {
                _alertFieldAndValues.Remove(OAlertSettings.LOAN_CODE);
                _alertFieldAndValues.Add(OAlertSettings.LOAN_CODE, value);
            }
        }

        public string LoanStatus
        {
            get
            {
                return (string)(_alertFieldAndValues[OAlertSettings.LOAN_STATUS]);
            }
            set
            {
                _alertFieldAndValues.Remove(OAlertSettings.LOAN_STATUS);
                _alertFieldAndValues.Add(OAlertSettings.LOAN_STATUS, value);
            }
        }

        public string ClientName
        {
            get
            {
                return (string)_alertFieldAndValues[OAlertSettings.CLIENT_NAME];
            }
            set
            {
                _alertFieldAndValues.Remove(OAlertSettings.CLIENT_NAME);
                _alertFieldAndValues.Add(OAlertSettings.CLIENT_NAME, value);
            }
        }

        public OCurrency Amount
        {
            get
            {
                return Convert.ToDecimal(_alertFieldAndValues[OAlertSettings.AMOUNT]);
            }
            set
            {
                _alertFieldAndValues.Remove(OAlertSettings.AMOUNT);
                _alertFieldAndValues.Add(OAlertSettings.AMOUNT, value.Value);
            }
        }

        public OCurrency OLB
        {
            get
            {
                return Convert.ToDecimal(_alertFieldAndValues[OAlertSettings.OLB]);
            }
            set
            {
                _alertFieldAndValues.Remove(OAlertSettings.OLB);
                _alertFieldAndValues.Add(OAlertSettings.OLB, value.Value);
            }
        }

        public double InterestRate
        {
            get
            {
                return Convert.ToDouble(_alertFieldAndValues[OAlertSettings.InterestRate]);
            }
            set
            {
                _alertFieldAndValues.Remove(OAlertSettings.InterestRate);
                _alertFieldAndValues.Add(OAlertSettings.InterestRate, value);
            }
        }

        public DateTime CreationDate
        {
            get
            {
                return Convert.ToDateTime(_alertFieldAndValues[OAlertSettings.CREATION_DATE]);
            }
            set
            {
                _alertFieldAndValues.Remove(OAlertSettings.CREATION_DATE);
                _alertFieldAndValues.Add(OAlertSettings.CREATION_DATE, value);
            }
        }

        public DateTime StartDate
        {
            get
            {
                return Convert.ToDateTime(_alertFieldAndValues[OAlertSettings.START_DATE]);
            }
            set
            {
                _alertFieldAndValues.Remove(OAlertSettings.START_DATE);
                _alertFieldAndValues.Add(OAlertSettings.START_DATE, value);
            }
        }

        public DateTime CloseDate
        {
            get
            {
                return Convert.ToDateTime(_alertFieldAndValues[OAlertSettings.CLOSE_DATE]);
            }
            set
            {
                _alertFieldAndValues.Remove(OAlertSettings.CLOSE_DATE);
                _alertFieldAndValues.Add(OAlertSettings.CLOSE_DATE, value);
            }
        }

        public string InstallmentTypes
        {
            get
            {
                return (string)_alertFieldAndValues[OAlertSettings.INSTALLMENT_TYPES];
            }
            set
            {
                _alertFieldAndValues.Remove(OAlertSettings.INSTALLMENT_TYPES);
                _alertFieldAndValues.Add(OAlertSettings.INSTALLMENT_TYPES, value);
            }
        }

        public string DistrictName
        {
            get
            {
                return (string)_alertFieldAndValues[OAlertSettings.DISTRICT_NAME];
            }
            set
            {
                _alertFieldAndValues.Remove(OAlertSettings.DISTRICT_NAME);
                _alertFieldAndValues.Add(OAlertSettings.DISTRICT_NAME, value);
            }
        }

        public DateTime EffectDate
        {
            get { return Convert.ToDateTime(_alertFieldAndValues[OAlertSettings.EFFECT_DATE]); }
            set 
            {
                _alertFieldAndValues.Remove(OAlertSettings.EFFECT_DATE);
                _alertFieldAndValues.Add(OAlertSettings.EFFECT_DATE, value);
            } 
        }

        public OAlertColors Color
        {
            get
            {
                if (Amount < 0)
                    return OAlertColors.Color1;
                
                TimeSpan diff = TimeProvider.Today.Subtract(EffectDate);
                if (diff.Days > 365)
                    return OAlertColors.Color1;
                if (diff.Days >= 181)
                    return OAlertColors.Color2;
                if (diff.Days >= 91)
                    return OAlertColors.Color3;
                if (diff.Days >= 61)
                    return OAlertColors.Color4;
                if (diff.Days >= 31)
                    return OAlertColors.Color5;
                
                return diff.Days >= 1 ? OAlertColors.Color6 : OAlertColors.Color7;
            }
        }

        public Dictionary<string, object> AlertFieldAndValues
        {
            get { return _alertFieldAndValues; }
        }
    }
}
