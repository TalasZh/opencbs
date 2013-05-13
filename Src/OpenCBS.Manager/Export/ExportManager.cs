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
using System.Linq;
using System.Text;
using OpenCBS.CoreDomain;
using System.Data.SqlClient;
using OpenCBS.CoreDomain.Export;
using OpenCBS.CoreDomain.Export.Files;

namespace OpenCBS.Manager.Export
{
    public class ExportManager : Manager
    {
        public ExportManager(User pUser) : base(pUser) { }
        public ExportManager(string pTestDb) : base(pTestDb) { }

        public List<Installment> SelectInstallment(DateTime pStartDate, DateTime pEndDate)
        {
            List<Installment> rows = new List<Installment>();

            const string sqlText = @"SELECT [credit].[id] AS contract_id 
                                           ,[expected_date] AS installment_date
	                                       ,[contract_code]
	                                       ,[number] AS installment_number
                                           ,[interest_repayment] + [capital_repayment] - [paid_capital] - [paid_interest] AS installment_amount
	                                       ,PersonalBank.[name] AS personal_bank_name
	                                       ,PersonalBank.[BIC] AS personal_bank_bic
                                           ,PersonalBank.[IBAN1] AS personal_bank_iban_1
                                           ,PersonalBank.[IBAN2] AS personal_bank_iban_2
                                           ,BusinessBank.[name] AS business_bank_name
                                           ,BusinessBank.[BIC] AS business_bank_bic
                                           ,BusinessBank.[IBAN1] AS business_bank_iban_1
                                           ,BusinessBank.[IBAN2] AS business_bank_iban_2
                                           ,Packages.code AS product_code
                                           ,Packages.Name AS product_name
                                           ,[Persons].[id] AS client_id
                                           ,[Persons].[first_name] + ' ' + [Persons].[last_name] AS client_name
                                     FROM [Installments] i
                                     INNER JOIN Contracts ON Contracts.id = contract_id
                                     INNER JOIN Credit ON Credit.id = contract_id
                                     INNER JOIN Projects ON Projects.id = project_id
                                     INNER JOIN Persons ON Persons.id = Projects.tiers_id
                                     INNER JOIN Banks AS PersonalBank ON PersonalBank.id = Persons.personalBank_id
                                     INNER JOIN Banks AS BusinessBank ON BusinessBank.id = Persons.businessBank_id
                                     INNER JOIN Packages ON Packages.id = Credit.package_id
                                     WHERE paid_date IS NULL
                                     AND ([interest_repayment] + [capital_repayment] - [paid_capital] - [paid_interest]) != 0
                                     AND Credit.disbursed = 1
                                     AND expected_date BETWEEN @startDate AND @endDate
                                     AND (SELECT COUNT(i2.number) 
                                          FROM Installments i2
	                                      WHERE i2.contract_id = i.contract_id
	                                      AND i2.number < i.number
	                                      AND ((i2.paid_date IS NULL AND (i2.interest_repayment + i2.capital_repayment - i2.paid_capital - paid_capital) > 0)
                                           OR (i2.paid_date IS NOT NULL AND i2.pending = 1))) = 0";

            using (SqlConnection connection = GetConnection())
            using (OpenCbsCommand cmd = new OpenCbsCommand(sqlText, connection))
            {
                cmd.AddParam("@startDate", pStartDate);
                cmd.AddParam("@endDate",  pEndDate);

                using (OpenCbsReader reader = cmd.ExecuteReader())
                {
                    if (reader == null || reader.Empty) return rows;

                    while (reader.Read())
                    {
                        Installment row = new Installment
                        {
                            InstallmentDate = reader.GetDateTime("installment_date"),
                            ContractId = reader.GetInt("contract_id"),
                            ClientId = reader.GetInt("client_id"),
                            ContractCode = reader.GetString("contract_code"),
                            InstallmentNumber = reader.GetInt("installment_number"),
                            InstallmentAmount = reader.GetMoney("installment_amount"),
                            PersonalBankName = reader.GetString("personal_bank_name"),
                            PersonalBankBic = reader.GetString("personal_bank_bic"),
                            PersonalBankIban1 = reader.GetString("personal_bank_iban_1"),
                            PersonalBankIban2 = reader.GetString("personal_bank_iban_2"),
                            BusinessBankName = reader.GetString("business_bank_name"),
                            BusinessBankBic = reader.GetString("business_bank_bic"),
                            BusinessBankIban1 = reader.GetString("business_bank_iban_1"),
                            BusinessBankIban2 = reader.GetString("business_bank_iban_2"),
                            ProductCode = reader.GetString("product_code"),
                            ProductName = reader.GetString("product_name"),
                            ClientName = reader.GetString("client_name")
                        };

                        rows.Add(row);
                    }
                }
            }

            return rows;
        }
    }
}
