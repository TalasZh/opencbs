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
using System.Diagnostics;
using System.Drawing;
using OpenCBS.Enums;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Alerts
{
    public enum AlertKind
    {
        Loan = 1
        , Saving = 2
    }

    public class Alert_v2
    {
        public int Id { get; set; }
        public OContractStatus Status { get; set; }
        public DateTime Date { get; set; }
        public int LateDays { get; set; }
        public OCurrency Amount { get; set; }
        public string ContractCode { get; set; }
        public string ClientName { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool UseCents { get; set; }
        public AlertKind Kind { get; set; }
        public User LoanOfficer { get; set; }
        public Color BackColor
        {
            get
            {
                if (LateDays >= 365) return Color.FromArgb(226, 0, 26);
                if (LateDays >= 180) return Color.FromArgb(255, 92, 92);
                if (LateDays >= 90) return Color.FromArgb(255, 187, 120);
                if (LateDays >= 60) return Color.FromArgb(147, 181, 167);
                if (LateDays >= 30) return Color.FromArgb(188, 209, 199);
                if (LateDays > 0) return Color.FromArgb(217, 229, 223);
                if (Status == OContractStatus.Pending && Kind == AlertKind.Saving) return Color.Orange;
                return Color.White;
            }
        }

        public int ImageIndex
        {
            get
            {
                if (AlertKind.Loan == Kind)
                {
                    switch (Status)
                    {
                        case OContractStatus.Active:
                            return 0;

                        case OContractStatus.Validated:
                        case OContractStatus.Pending:
                        case OContractStatus.Postponed:
                            return 1;

                        default:
                            Debug.Fail("Cannot be here");
                            return 0;
                    }
                }
                if (AlertKind.Saving == Kind)
                {
                    switch (Status)
                    {
                        case OContractStatus.Active:
                            return 4;
                        case OContractStatus.Pending:
                            return 4;
                        default:
                            Debug.Fail("Cannot be here");
                            return 4;
                    }
                }
                Debug.Fail("Unknown alert kind");
                return 0;
            }
        }

        public bool IsLoan
        {
            get
            {
                return AlertKind.Loan == Kind;
            }
        }

        public bool IsSaving
        {
            get
            {
                return AlertKind.Saving == Kind;
            }
        }

        public bool IsLate
        {
            get
            {
                return LateDays > 0;
            }
        }

        public bool IsActive
        {
            
            get
            {
                return OContractStatus.Active == Status;
            }
        }

        public bool IsPerformingLoan
        {
            get
            {
                return IsLoan && IsActive && !IsLate;
            }
        }

        public bool IsLateLoan
        {
            get
            {
                return IsLoan && IsLate;
            }
        }

        public string BranchName {get; set;}
    }
}
