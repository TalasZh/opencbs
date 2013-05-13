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
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Enums;
using System.Data.SqlClient;

namespace OpenCBS.Manager.Products
{
    /// <summary>
    /// This class provides all the methods required to manages Package datas in database
    /// </summary>
    public class SavingProductManager : Manager
    {

        public SavingProductManager(User pUser) : base(pUser)
        {
        }

        public SavingProductManager(string testDB) : base(testDB)
        {
        }

        /// <summary>
        /// Method to add a package into database. We use the NullableTypes to make the correspondance between
        /// nullable int, decimal and double types in database and our own objects
        /// </summary>
        /// <param name="product">Package Object</param>
        /// <returns>The id of the package which has been added</returns>
        public int Add(ISavingProduct product)
        {
            const string q = @"INSERT INTO [SavingProducts] 
                (
                     [deleted]
                    ,[name]
                    ,[code]
                    ,[client_type]
                    ,[initial_amount_min]
                    ,[initial_amount_max]
                    ,[balance_min]
                    ,[balance_max]
                    ,[deposit_min]
                    ,[deposit_max]
                    ,[withdraw_min]
                    ,[withdraw_max]
                    ,[transfer_min]
                    ,[transfer_max]
                    ,[interest_rate]
                    ,[interest_rate_min]
                    ,[interest_rate_max]
                    ,[entry_fees_max]
                    ,[entry_fees_min]
                    ,[entry_fees]
                    ,[currency_id]
                    ,[product_type]
                )
                VALUES 
                (
                     @deleted
                    ,@name
                    ,@code
                    ,@clientType
                    ,@initialAmountMin
                    ,@initialAmountMax
                    ,@balanceMin
                    ,@balanceMax
                    ,@depositMin
                    ,@depositMax
                    ,@withdrawMin
                    ,@withdrawMax
                    ,@transferMin
                    ,@transferMax
                    ,@interestRate
                    ,@interestRateMin
                    ,@interestRateMax
                    ,@entryFeesMax
                    ,@entryFeesMin
                    ,@entryFees
                    ,@currency_id
                    ,@product_type                 
                ) 
                SELECT CONVERT(int, SCOPE_IDENTITY())";

            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                if (product is SavingsBookProduct)
                    c.AddParam("@product_type", 'B');
               
                SetProduct(c, product);
                product.Id = Convert.ToInt32(c.ExecuteScalar());
            }

            if (product is SavingsBookProduct)
            {
                AddBookProduct((SavingsBookProduct)product);
                AssignClientTypes(((SavingsBookProduct)product).ProductClientTypes, product.Id);
            }
            
            return product.Id;
        }

        private void AssignClientTypes(List<ProductClientType> productClientTypes, int productId)
        {
            foreach (ProductClientType clientType in productClientTypes)
            {
                if (clientType.IsChecked)
                {
                    string q = string.Format(@"INSERT INTO [dbo].[SavingProductsClientTypes] 
                                                        ([saving_product_id], [client_type_id])
                                                        VALUES({0},{1})", productId, clientType.TypeId);
                    using (SqlConnection conn = GetConnection())
                    using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
                    {
                        c.ExecuteNonQuery();
                    }
                }
            }
        }

        private void DeleteAssignedClientTypes(int savingProductId)
        {
            string q =
                string.Format(
                    @"DELETE FROM [dbo].[SavingProductsClientTypes]
                      WHERE [saving_product_id]={0}",
                    savingProductId);
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.ExecuteNonQuery();
            }
        }

        private void AddBookProduct(SavingsBookProduct product)
        {
            const string q = @"INSERT INTO [SavingBookProducts] 
                                    (
                                         [id]
                                        ,[interest_base]
                                        ,[interest_frequency]
                                        ,[calcul_amount_base]
                                        ,[withdraw_fees_type]
                                        ,[flat_withdraw_fees_min]
                                        ,[flat_withdraw_fees_max]
                                        ,[flat_withdraw_fees]
                                        ,[rate_withdraw_fees_min]
                                        ,[rate_withdraw_fees_max]
                                        ,[rate_withdraw_fees]
                                        ,[transfer_fees_type]
                                        ,[flat_transfer_fees_min]
                                        ,[flat_transfer_fees_max]
                                        ,[flat_transfer_fees]
                                        ,[rate_transfer_fees_min]
                                        ,[rate_transfer_fees_max]
                                        ,[rate_transfer_fees]
                                        , is_ibt_fee_flat, ibt_fee_min, ibt_fee_max, ibt_fee
                                        ,[deposit_fees]
                                        ,[deposit_fees_max]
                                        ,[deposit_fees_min]
                                        ,[close_fees]
                                        ,[close_fees_max]
                                        ,[close_fees_min]
                                        ,[management_fees]
                                        ,[management_fees_max]
                                        ,[management_fees_min]
                                        ,[management_fees_freq]
                                        ,[overdraft_fees]
                                        ,[overdraft_fees_max]
                                        ,[overdraft_fees_min]
                                        ,[agio_fees]
                                        ,[agio_fees_max]
                                        ,[agio_fees_min]
                                        ,[agio_fees_freq]
                                        ,[cheque_deposit_min]
                                        ,[cheque_deposit_max]
                                        ,[cheque_deposit_fees]
                                        ,[cheque_deposit_fees_min]
                                        ,[cheque_deposit_fees_max]
                                        ,[reopen_fees]
                                        ,[reopen_fees_min]
                                        ,[reopen_fees_max]
                                        ,[use_term_deposit]
                                        ,[term_deposit_period_min]
                                        ,[term_deposit_period_max]
                                        ,[posting_frequency]
                                  )
                                  VALUES 
                                  (
                                         @id
                                        ,@interestBase
                                        ,@interestFrequency
                                        ,@calculAmountBase
                                        ,@withdrawFeesType
                                        ,@flatWithdrawFeesMin
                                        ,@flatWithdrawFeesMax
                                        ,@flatWithdrawFees
                                        ,@rateWithdrawFeesMin
                                        ,@rateWithdrawFeesMax
                                        ,@rateWithdrawFees
                                        ,@transferFeesType
                                        ,@flatTransferFeesMin
                                        ,@flatTransferFeesMax
                                        ,@flatTransferFees
                                        ,@rateTransferFeesMin
                                        ,@rateTransferFeesMax
                                        ,@rateTransferFees
                                        ,@is_ibt_fee_flat
                                        ,@ibt_fee_min
                                        ,@ibt_fee_max 
                                        ,@ibt_fee
                                        ,@depositFees
                                        ,@depositFeesMax 
                                        ,@depositFeesMin 
                                        ,@closeFees 
                                        ,@closeFeesMax 
                                        ,@closeFeesMin 
                                        ,@managementFees 
                                        ,@managementFeesMax 
                                        ,@managementFeesMin 
                                        ,@management_fees_freq
                                        ,@overdraftFees 
                                        ,@overdraftFeesMax 
                                        ,@overdraftFeesMin 
                                        ,@agioFees 
                                        ,@agioFeesMax 
                                        ,@agioFeesMin 
                                        ,@agioFeesFreq
                                        ,@chequeDepositMin
                                        ,@chequeDepositMax
                                        ,@chequeDepositFees
                                        ,@chequeDepositFeesMin
                                        ,@chequeDepositFeesMax
                                        ,@reopenFees
                                        ,@reopenFeesMin
                                        ,@reopenFeesMax
                                        ,@use_term_deposit
                                        ,@term_deposit_period_min
                                        ,@term_deposit_period_max
                                        ,@posting_frequency
                
                                )";

            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@id", product.Id);

                SetProduct(c, product);
                c.ExecuteScalar();
            }
        }

       
        private void UpdateBookProduct(SavingsBookProduct product, bool inUse)
        {
            string q = @"UPDATE [SavingBookProducts] SET 
                                     [flat_withdraw_fees_min] = @flatWithdrawFeesMin 
                                    ,[flat_withdraw_fees_max] = @flatWithdrawFeesMax 
                                    ,[flat_withdraw_fees] = @flatWithdrawFees 
                                    ,[rate_withdraw_fees_min] = @rateWithdrawFeesMin 
                                    ,[rate_withdraw_fees_max] = @rateWithdrawFeesMax 
                                    ,[rate_withdraw_fees] = @rateWithdrawFees
                                    ,[flat_transfer_fees_min] = @flatTransferFeesMin 
                                    ,[flat_transfer_fees_max] = @flatTransferFeesMax 
                                    ,[flat_transfer_fees] = @flatTransferFees 
                                    ,[rate_transfer_fees_min] = @rateTransferFeesMin
                                    ,[rate_transfer_fees_max] = @rateTransferFeesMax
                                    ,[rate_transfer_fees] = @rateTransferFees
                                    ,[ibt_fee_min] = @ibt_fee_min
                                    ,[ibt_fee_max] = @ibt_fee_max
                                    ,[ibt_fee] = @ibt_fee
                                    ,[deposit_fees_max] = @depositFeesMax
                                    ,[deposit_fees_min] = @depositFeesMin
                                    ,[deposit_fees] = @depositFees
                                    ,[close_fees_max] = @closeFeesMax
                                    ,[close_fees_min] = @closeFeesMin
                                    ,[close_fees] = @closeFees
                                    ,[management_fees_max] = @managementFeesMax
                                    ,[management_fees_min] = @managementFeesMin
                                    ,[management_fees] = @managementFees
                                    ,[management_fees_freq] = @management_fees_freq
                                    ,[overdraft_fees_max] = @overdraftFeesMax
                                    ,[overdraft_fees_min] = @overdraftFeesMin
                                    ,[overdraft_fees] = @overdraftFees
                                    ,[agio_fees_max] = @agioFeesMax
                                    ,[agio_fees_min] = @agioFeesMin
                                    ,[agio_fees] = @agioFees
                                    ,[agio_fees_freq] = @agioFeesFreq
                                    ,[cheque_deposit_min]=@chequeDepositMin
                                    ,[cheque_deposit_max]=@chequeDepositMax
                                    ,[cheque_deposit_fees]=@chequeDepositFees
                                    ,[cheque_deposit_fees_min]=@chequeDepositFeesMin
                                    ,[cheque_deposit_fees_max]=@chequeDepositFeesMax
                                    ,[reopen_fees] = @reopenFees
                                    ,[reopen_fees_min] = @reopenFeesMin
                                    ,[reopen_fees_max] = @reopenFeesMax
                                    ,[use_term_deposit] = @use_term_deposit
                                    ,[term_deposit_period_min] = @term_deposit_period_min
                                    ,[term_deposit_period_max] = @term_deposit_period_max
                                    ,[posting_frequency] = @posting_frequency
                                    {0}
            WHERE id = @productId";

            const string queryNew = @"
            ,[interest_base] = @interestBase
            ,[interest_frequency] = @interestFrequency
            ,[calcul_amount_base] = @calculAmountBase 
            ,[withdraw_fees_type] = @withdrawFeesType
            ,[transfer_fees_type] = @transferFeesType
            ,[is_ibt_fee_flat] = @is_ibt_fee_flat";

            q = string.Format(q, inUse ? "" : queryNew);
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                SetProduct(c, product);
                c.ExecuteNonQuery();
            }
        }

       public void UpdateExistingSavingBooksContracts(ISavingProduct product)
        {
            bool isItTheFirstStatement=true;
            string updateInterestRate="";
            List<int> contractsId=new List<int>();
            SavingsBookProduct savingsBookProduct = (SavingsBookProduct) product;
            int savingProductId = savingsBookProduct.Id;
            string selectContractsId =
                string.Format(
                    @"SELECT id
                      FROM [dbo].[SavingContracts]
                      WHERE product_id=@product_id");
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(selectContractsId, conn))
            {
                c.AddParam("@product_id", savingProductId);
                using (OpenCbsReader r = c.ExecuteReader())
                {
                    while (r.Read())
                    {
                        contractsId.Add(r.GetInt("id"));
                    }
                }
            }

            string updateExistingContractsSql = @"UPDATE [dbo].[SavingBookContracts]
                                                  SET id=@id ";
            
            if (savingsBookProduct.ManagementFees.HasValue)
                updateExistingContractsSql += ",[flat_management_fees]=@managment_fees";

            if (savingsBookProduct.CloseFees.HasValue)
                updateExistingContractsSql += ",[flat_close_fees]=@close_fees";
                
            if (savingsBookProduct.DepositFees.HasValue)
                updateExistingContractsSql += ",[flat_deposit_fees]=@deposit_fees";
                
            if (savingsBookProduct.FlatWithdrawFees.HasValue)
                updateExistingContractsSql += ",[flat_withdraw_fees]=@flat_withdraw_fees";
                
            if (savingsBookProduct.FlatTransferFees.HasValue)
                updateExistingContractsSql += ",[flat_transfer_fees]=@flat_transfer_fees";

            if (savingsBookProduct.AgioFees.HasValue)
                updateExistingContractsSql += ",[rate_agio_fees]=@rate_agio_fees";

            if (savingsBookProduct.ChequeDepositFees.HasValue)
                updateExistingContractsSql += ",[cheque_deposit_fees]=@cheque_deposit_fees";

            if (savingsBookProduct.OverdraftFees.HasValue)
                updateExistingContractsSql += ",[flat_overdraft_fees]=@flat_overdraft_fees";
                
            updateExistingContractsSql += " WHERE id=@id";

            foreach (int contractId in contractsId)
            {
                using (SqlConnection conn = GetConnection())
                using (OpenCbsCommand c = new OpenCbsCommand(updateExistingContractsSql, conn))
                {
                    c.AddParam("@id", contractId);
                    if (savingsBookProduct.ManagementFees.HasValue)
                        c.AddParam("@managment_fees", savingsBookProduct.ManagementFees.Value);
                    if (savingsBookProduct.CloseFees.HasValue)
                        c.AddParam("@close_fees", savingsBookProduct.CloseFees.Value);
                    if (savingsBookProduct.DepositFees.HasValue)
                        c.AddParam("@deposit_fees", savingsBookProduct.DepositFees.Value);
                    if (savingsBookProduct.FlatWithdrawFees.HasValue)
                        c.AddParam("@flat_withdraw_fees", savingsBookProduct.FlatWithdrawFees.Value);
                    if (savingsBookProduct.FlatTransferFees.HasValue)
                        c.AddParam("@flat_transfer_fees", savingsBookProduct.FlatTransferFees.Value);
                    if (savingsBookProduct.AgioFees.HasValue)
                        c.AddParam("@rate_agio_fees", savingsBookProduct.AgioFees.Value);
                    if (savingsBookProduct.OverdraftFees.HasValue)
                        c.AddParam("@flat_overdraft_fees", savingsBookProduct.OverdraftFees.Value);
                    if (savingsBookProduct.ChequeDepositFees.HasValue)
                        c.AddParam("@cheque_deposit_fees", savingsBookProduct.ChequeDepositFees);
                    c.ExecuteNonQuery();
                }
                
                if (savingsBookProduct.InterestRate.HasValue)
                {
                    updateInterestRate = @"UPDATE [dbo].[SavingContracts] SET [interest_rate]=@interest_rate
                                           WHERE id=@id";
                    using (SqlConnection conn = GetConnection())
                    using (OpenCbsCommand c = new OpenCbsCommand(updateInterestRate, conn))
                    {
                        c.AddParam("@id", contractId);
                        c.AddParam("@interest_rate", savingsBookProduct.InterestRate);
                        c.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Update(ISavingProduct product)
        {
            const string q = @"UPDATE [SavingProducts] SET  
                                      [initial_amount_min] = @initialAmountMin
                                    , [initial_amount_max] = @initialAmountMax
                                    , [balance_min] = @balanceMin
                                    , [balance_max] = @balanceMax
                                    , [deposit_min] = @depositMin
                                    , [deposit_max] = @depositMax
                                    , [withdraw_min] = @withdrawMin
                                    , [withdraw_max] = @withdrawMax
                                    , [transfer_min] = @transferMin
                                    , [transfer_max] = @transferMax
                                    , [interest_rate] = @interestRate
                                    , [interest_rate_min] = @interestRateMin
                                    , [interest_rate_max] = @interestRateMax
                                    , [entry_fees] = @entryFees
                                    , [entry_fees_max] = @entryFeesMax
                                    , [entry_fees_min] = @entryFeesMin
                                    {0}
                                    WHERE id = @productId";

            const string sqlTextProductNotUsed = @",[client_type] = @clientType, [name] = @name, [code] = @code, [currency_id] = @currency_id";

            bool productAlreadyUsed = IsThisProductAlreadyUsed(product.Id);

            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(string.Format(q, productAlreadyUsed ? "" : sqlTextProductNotUsed), conn))
            {
                SetProduct(c, product);
                c.ExecuteNonQuery();
            }

            if (product is SavingsBookProduct)
            {
                UpdateBookProduct((SavingsBookProduct)product, productAlreadyUsed);
                DeleteAssignedClientTypes(product.Id);
                AssignClientTypes(((SavingsBookProduct)product).ProductClientTypes, product.Id);
            }
            
        }

        public bool IsThisProductAlreadyUsed(int productId)
        {
            const string q = "SELECT id FROM SavingContracts WHERE product_id = @id";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@id", productId);
                using (OpenCbsReader r = c.ExecuteReader())
                {
                    return r != null && !r.Empty;
                }
            }
        }

        /// <summary>
        /// Look if selected product name already exist in database
        /// </summary>
        /// <param name="savingProductName"></param>
        /// <returns>true or false</returns>
        public bool IsThisProductNameAlreadyExist(string savingProductName)
        {
            const string q = "SELECT name FROM SavingProducts WHERE name = @name";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@name", savingProductName);
                using (OpenCbsReader r = c.ExecuteReader())
                {
                    return r != null && !r.Empty;
                }
            }
        }

        public bool IsThisProductCodeAlreadyExist(string savingProductCode)
        {
            const string q = "SELECT code FROM SavingProducts WHERE code = @code";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@code", savingProductCode);
                using (OpenCbsReader r = c.ExecuteReader())
                {
                    return r != null && !r.Empty;
                }
            } 
        }

        private static ISavingProduct GetProduct(OpenCbsReader r)
        {
            ISavingProduct product;
            
            switch (r.GetChar("product_type"))
            {
                case 'B' : product = new SavingsBookProduct(); break;
               default : product = null; break;
            }

            product.Id = r.GetInt("id");
            product.Delete = r.GetBool("deleted");
            product.Name = r.GetString("name");
            product.Code = r.GetString("code");

            product.ClientType = r.GetChar("client_type") == 'C' ? OClientTypes.Corporate
                                 : r.GetChar("client_type") == 'G' ? OClientTypes.Group
                                 : r.GetChar("client_type") == 'I' ? OClientTypes.Person 
                                 : OClientTypes.All;

            product.InitialAmountMin = r.GetMoney("initial_amount_min");
            product.InitialAmountMax = r.GetMoney("initial_amount_max");
            product.BalanceMin = r.GetMoney("balance_min");
            product.BalanceMax = r.GetMoney("balance_max");
            product.DepositMin = r.GetMoney("deposit_min");
            product.DepositMax = r.GetMoney("deposit_max");
            product.WithdrawingMin = r.GetMoney("withdraw_min");
            product.WithdrawingMax = r.GetMoney("withdraw_max");
            product.TransferMin = r.GetMoney("transfer_min");
            product.TransferMax = r.GetMoney("transfer_max");
            product.InterestRate = r.GetNullDouble("interest_rate");
            product.InterestRateMin = r.GetNullDouble("interest_rate_min");
            product.InterestRateMax = r.GetNullDouble("interest_rate_max");
            product.EntryFees = r.GetMoney("entry_fees");
            product.EntryFeesMax = r.GetMoney("entry_fees_max");
            product.EntryFeesMin = r.GetMoney("entry_fees_min");
           
            if (product is SavingsBookProduct)
            {
                var savingBookProduct = (SavingsBookProduct) product;

                savingBookProduct.InterestBase = (OSavingInterestBase)r.GetSmallInt("interest_base");
                savingBookProduct.InterestFrequency = (OSavingInterestFrequency)r.GetSmallInt("interest_frequency");

                if (savingBookProduct.InterestBase == OSavingInterestBase.Monthly || 
                    savingBookProduct.InterestBase == OSavingInterestBase.Weekly)
                    savingBookProduct.CalculAmountBase = (OSavingCalculAmountBase)r.GetSmallInt("calcul_amount_base");

                savingBookProduct.WithdrawFeesType = (OSavingsFeesType)r.GetSmallInt("withdraw_fees_type");
                if (savingBookProduct.WithdrawFeesType == OSavingsFeesType.Flat)
                {
                    savingBookProduct.FlatWithdrawFeesMin = r.GetMoney("flat_withdraw_fees_min");
                    savingBookProduct.FlatWithdrawFeesMax = r.GetMoney("flat_withdraw_fees_max");
                    savingBookProduct.FlatWithdrawFees = r.GetMoney("flat_withdraw_fees");
                }
                else
                {
                    savingBookProduct.RateWithdrawFeesMin = r.GetNullDouble("rate_withdraw_fees_min");
                    savingBookProduct.RateWithdrawFeesMax = r.GetNullDouble("rate_withdraw_fees_max");
                    savingBookProduct.RateWithdrawFees = r.GetNullDouble("rate_withdraw_fees");
                }

                savingBookProduct.TransferFeesType = (OSavingsFeesType)r.GetSmallInt("transfer_fees_type");
                if (savingBookProduct.TransferFeesType == OSavingsFeesType.Flat)
                {
                    savingBookProduct.FlatTransferFeesMin = r.GetMoney("flat_transfer_fees_min");
                    savingBookProduct.FlatTransferFeesMax = r.GetMoney("flat_transfer_fees_max");
                    savingBookProduct.FlatTransferFees = r.GetMoney("flat_transfer_fees");
                }
                else
                {
                    savingBookProduct.RateTransferFeesMin = r.GetNullDouble("rate_transfer_fees_min");
                    savingBookProduct.RateTransferFeesMax = r.GetNullDouble("rate_transfer_fees_max");
                    savingBookProduct.RateTransferFees = r.GetNullDouble("rate_transfer_fees");
                }

                Fee fee = savingBookProduct.InterBranchTransferFee;
                fee.IsFlat = r.GetBool("is_ibt_fee_flat");
                fee.Min = r.GetNullDecimal("ibt_fee_min");
                fee.Max = r.GetNullDecimal("ibt_fee_max");
                fee.Value = r.GetNullDecimal("ibt_fee");

                savingBookProduct.DepositFees = r.GetMoney("deposit_fees");
                savingBookProduct.DepositFeesMax = r.GetMoney("deposit_fees_max");
                ((SavingsBookProduct)product).DepositFeesMin = r.GetMoney("deposit_fees_min");

                savingBookProduct.ChequeDepositMin = r.GetMoney("cheque_deposit_min");
                savingBookProduct.ChequeDepositMax = r.GetMoney("cheque_deposit_max");
                savingBookProduct.ChequeDepositFees = r.GetMoney("cheque_deposit_fees");
                savingBookProduct.ChequeDepositFeesMin = r.GetMoney("cheque_deposit_fees_min");
                savingBookProduct.ChequeDepositFeesMax = r.GetMoney("cheque_deposit_fees_max");

                savingBookProduct.CloseFees = r.GetMoney("close_fees");
                savingBookProduct.CloseFeesMax = r.GetMoney("close_fees_max");
                savingBookProduct.CloseFeesMin = r.GetMoney("close_fees_min");

                savingBookProduct.ManagementFees = r.GetMoney("management_fees");
                savingBookProduct.ManagementFeesMax = r.GetMoney("management_fees_max");
                savingBookProduct.ManagementFeesMin = r.GetMoney("management_fees_min");

                savingBookProduct.ManagementFeeFreq = new InstallmentType
                {
                    Id = r.GetInt("mgmt_fee_freq_id"),
                    Name = r.GetString("mgmt_fee_freq_name"),
                    NbOfDays = r.GetInt("mgmt_fee_freq_days"),
                    NbOfMonths = r.GetInt("mgmt_fee_freq_months")   
                };

                savingBookProduct.OverdraftFees = r.GetMoney("overdraft_fees");
                savingBookProduct.OverdraftFeesMax = r.GetMoney("overdraft_fees_max");
                savingBookProduct.OverdraftFeesMin = r.GetMoney("overdraft_fees_min");

                savingBookProduct.AgioFees = r.GetNullDouble("agio_fees");
                savingBookProduct.AgioFeesMax = r.GetNullDouble("agio_fees_max");
                savingBookProduct.AgioFeesMin = r.GetNullDouble("agio_fees_min");

                ((SavingsBookProduct)product).AgioFeesFreq = new InstallmentType
                {
                    Id = r.GetInt("agio_fees_freq_id"),
                    Name =  r.GetString("agio_fees_freq_name"),
                    NbOfDays =  r.GetInt("agio_fees_freq_days"),
                    NbOfMonths = r.GetInt("agio_fees_freq_months")
                };

                savingBookProduct.ReopenFees = r.GetMoney("reopen_fees");
                savingBookProduct.ReopenFeesMin = r.GetMoney("reopen_fees_min");
                savingBookProduct.ReopenFeesMax = r.GetMoney("reopen_fees_max");
                savingBookProduct.UseTermDeposit = r.GetBool("use_term_deposit");
                savingBookProduct.TermDepositPeriodMin = r.GetNullInt("term_deposit_period_min");
                savingBookProduct.TermDepositPeriodMax = r.GetNullInt("term_deposit_period_max");

                if (savingBookProduct.UseTermDeposit)
                {
                    savingBookProduct.InstallmentTypeId = r.GetNullInt("posting_frequency");
                    savingBookProduct.Periodicity = new InstallmentType
                                                        {
                                                            Id = r.GetInt("posting_frequency"),
                                                            Name = r.GetString("periodicity_name") ,
                                                            NbOfDays = r.GetInt("periodicity_days"),
                                                            NbOfMonths = r.GetInt("periodicity_month")
                                                        };
                }
            }
           
            
            if (r.GetNullInt("currency_id") != null)
            {
                product.Currency = new Currency
                {
                    Id = r.GetInt("currency_id"),
                    Code = r.GetString("currency_code"),
                    Name = r.GetString("currency_name"),
                    IsPivot = r.GetBool("currency_is_pivot"),
                    IsSwapped = r.GetBool("currency_is_swapped"),
                    UseCents = r.GetBool("currency_use_cents")
                };
            }

            return product;
        }

        private static void SetProduct(OpenCbsCommand c, ISavingProduct product)
        {
            c.AddParam("@deleted", product.Delete);
            c.AddParam("@name", product.Name);
            c.AddParam("@code", product.Code);

            if (product.ClientType == OClientTypes.Corporate)
                c.AddParam("@clientType", 'C');
            else if (product.ClientType == OClientTypes.Group)
                c.AddParam("@clientType", 'G');
            else if (product.ClientType == OClientTypes.Person)
                c.AddParam("@clientType", 'I');
            else
                c.AddParam("@clientType", '-');

            c.AddParam("@initialAmountMin", product.InitialAmountMin);
            c.AddParam("@initialAmountMax", product.InitialAmountMax);
            
            c.AddParam("@balanceMin", product.BalanceMin);
            c.AddParam("@balanceMax", product.BalanceMax);

            c.AddParam("@depositMin", product.DepositMin);
            c.AddParam("@depositMax", product.DepositMax);

            c.AddParam("@withdrawMin", product.WithdrawingMin);
            c.AddParam("@withdrawMax", product.WithdrawingMax);

            c.AddParam("@transferMin", product.TransferMin);
            c.AddParam("@transferMax", product.TransferMax);

            c.AddParam("@interestRate", product.InterestRate);
            c.AddParam("@interestRateMin", product.InterestRateMin);
            c.AddParam("@interestRateMax", product.InterestRateMax);
          
            c.AddParam("@entryFees", product.EntryFees);
            c.AddParam("@entryFeesMax", product.EntryFeesMax);
            c.AddParam("@entryFeesMin", product.EntryFeesMin);

            c.AddParam("@productId", product.Id);
            c.AddParam("@currency_id", product.Currency.Id);
            
            if (product is  SavingsBookProduct)
            {
                c.AddParam("@use_term_deposit", ((SavingsBookProduct)product).UseTermDeposit);
                c.AddParam("@term_deposit_period_min", ((SavingsBookProduct)product).TermDepositPeriodMin);
                c.AddParam("@term_deposit_period_max", ((SavingsBookProduct)product).TermDepositPeriodMax);
                if (((SavingsBookProduct)product).UseTermDeposit)
                {
                    c.AddParam("@posting_frequency", ((SavingsBookProduct)product).Periodicity.Id);
                }
            }
            else
            {
                c.AddParam("@use_term_deposit", null);
                c.AddParam("@term_deposit_period_min", null);
                c.AddParam("@term_deposit_period_max", null);
                c.AddParam("@posting_frequency", null);
            }

        }

        private static void SetProduct(OpenCbsCommand c, SavingsBookProduct product)
        {
            c.AddParam("@interestBase", (int)product.InterestBase);
            c.AddParam("@interestFrequency", (int)product.InterestFrequency);
            c.AddParam("@calculAmountBase", product.CalculAmountBase.HasValue ? (int)product.CalculAmountBase.Value : 0);
            c.AddParam("@productId", product.Id);

            c.AddParam("@withdrawFeesType", (int)product.WithdrawFeesType);
            c.AddParam("@flatWithdrawFeesMin", product.WithdrawFeesType == OSavingsFeesType.Flat ? product.FlatWithdrawFeesMin : null);
            c.AddParam("@flatWithdrawFeesMax", product.WithdrawFeesType == OSavingsFeesType.Flat ? product.FlatWithdrawFeesMax : null);
            c.AddParam("@flatWithdrawFees", product.WithdrawFeesType == OSavingsFeesType.Flat ? product.FlatWithdrawFees : null);
            c.AddParam("@rateWithdrawFeesMin", product.WithdrawFeesType == OSavingsFeesType.Rate ? product.RateWithdrawFeesMin : null);
            c.AddParam("@rateWithdrawFeesMax", product.WithdrawFeesType == OSavingsFeesType.Rate ? product.RateWithdrawFeesMax : null);
            c.AddParam("@rateWithdrawFees", product.WithdrawFeesType == OSavingsFeesType.Rate ? product.RateWithdrawFees : null);

            c.AddParam("@transferFeesType", (int)product.TransferFeesType);
            c.AddParam("@flatTransferFeesMin", product.TransferFeesType == OSavingsFeesType.Flat ? product.FlatTransferFeesMin : null);
            c.AddParam("@flatTransferFeesMax", product.TransferFeesType == OSavingsFeesType.Flat ? product.FlatTransferFeesMax : null);
            c.AddParam("@flatTransferFees", product.TransferFeesType == OSavingsFeesType.Flat ? product.FlatTransferFees : null);
            c.AddParam("@rateTransferFeesMin", product.TransferFeesType == OSavingsFeesType.Rate ? product.RateTransferFeesMin : null);
            c.AddParam("@rateTransferFeesMax", product.TransferFeesType == OSavingsFeesType.Rate ? product.RateTransferFeesMax : null);
            c.AddParam("@rateTransferFees", product.TransferFeesType == OSavingsFeesType.Rate ? product.RateTransferFees : null);

            Fee fee = product.InterBranchTransferFee;
            c.AddParam("@is_ibt_fee_flat", fee.IsFlat);
            c.AddParam("@ibt_fee_min", fee.Min);
            c.AddParam("@ibt_fee_max", fee.Max);
            c.AddParam("@ibt_fee", fee.Value);

            c.AddParam("@depositFeesMin", product.DepositFeesMin);
            c.AddParam("@depositFeesMax", product.DepositFeesMax);
            c.AddParam("@depositFees", product.DepositFees);
            
            c.AddParam("@chequeDepositMin", product.ChequeDepositMin);
            c.AddParam("@chequeDepositMax", product.ChequeDepositMax);
            c.AddParam("@chequeDepositFees",product.ChequeDepositFees);
            c.AddParam("@chequeDepositFeesMin",product.ChequeDepositFeesMin);
            c.AddParam("@chequeDepositFeesMax",product.ChequeDepositFeesMax);

            c.AddParam("@closeFeesMin", product.CloseFeesMin);
            c.AddParam("@closeFeesMax", product.CloseFeesMax);
            c.AddParam("@closeFees", product.CloseFees);

            c.AddParam("@managementFeesMin", product.ManagementFeesMin);
            c.AddParam("@managementFeesMax", product.ManagementFeesMax);
            c.AddParam("@managementFees", product.ManagementFees);
            c.AddParam("@management_fees_freq", product.ManagementFeeFreq.Id);

            c.AddParam("@overdraftFeesMin", product.OverdraftFeesMin);
            c.AddParam("@overdraftFeesMax", product.OverdraftFeesMax);
            c.AddParam("@overdraftFees", product.OverdraftFees);

            c.AddParam("@agioFeesMin", product.AgioFeesMin);
            c.AddParam("@agioFeesMax", product.AgioFeesMax);
            c.AddParam("@agioFees", product.AgioFees);
            c.AddParam("@agioFeesFreq", product.AgioFeesFreq.Id);

            c.AddParam("@reopenFeesMin", product.ReopenFeesMin);
            c.AddParam("@reopenFeesMax", product.ReopenFeesMax);
            c.AddParam("@reopenFees", product.ReopenFees);
            c.AddParam("@use_term_deposit", product.UseTermDeposit);
            c.AddParam("@term_deposit_period_min", product.TermDepositPeriodMin);
            c.AddParam("@term_deposit_period_max", product.TermDepositPeriodMax);
            if (product.UseTermDeposit)
            {
                c.AddParam("@posting_frequency", product.Periodicity.Id);
            }
            else
            {
                c.AddParam("@posting_frequency", null);
            }
        }

        /// <summary>
        /// This method allows us to select a package from database.  We use the NullableTypes to make the correspondance between
        /// nullable int, decimal and double types in database and our own objects
        /// </summary>
        /// <param name="productId">id's of package searched</param>
        /// <returns>A package Object if id matches with datas in database, null if not</returns>
        public SavingsBookProduct SelectSavingsBookProduct(int productId)
        {
            const string q = @"     SELECT sbp.*,
                                        prod.*, 
                                        cur.name AS currency_name, 
                                        cur.code AS currency_code,
                                        cur.is_pivot AS currency_is_pivot, 
                                        cur.is_swapped AS currency_is_swapped,
                                        cur.use_cents AS currency_use_cents,
                                        freq.id AS mgmt_fee_freq_id,
                                        freq.name AS mgmt_fee_freq_name,
                                        freq.nb_of_days AS mgmt_fee_freq_days,
                                        freq.nb_of_months AS mgmt_fee_freq_months,
                                        agio_freq.id AS agio_fees_freq_id,
                                        agio_freq.name AS agio_fees_freq_name,
                                        agio_freq.nb_of_days AS agio_fees_freq_days,
                                        agio_freq.nb_of_months AS agio_fees_freq_months,
                                        deposit_periodicity.name AS periodicity_name,
                                        deposit_periodicity.nb_of_days AS periodicity_days,
										deposit_periodicity.nb_of_months AS periodicity_month
                                        
                                FROM SavingProducts AS prod
                                        INNER JOIN SavingBookProducts AS sbp ON prod.id = sbp.id
                                        INNER JOIN Currencies AS cur ON prod.currency_id = cur.id
                                        LEFT JOIN InstallmentTypes AS freq ON sbp.management_fees_freq = freq.id
                                        LEFT JOIN InstallmentTypes AS agio_freq ON sbp.agio_fees_freq = agio_freq.id
                                        LEFT JOIN InstallmentTypes AS deposit_periodicity ON deposit_periodicity.id=sbp.posting_frequency
                                 
                                WHERE prod.id = @id
                                ";

            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@id", productId);

                using (OpenCbsReader r = c.ExecuteReader())
                {
                    if (r == null || r.Empty) return null;

                    r.Read();
                    return (SavingsBookProduct)GetProduct(r);
                }
            }
        }

       public List<ProductClientType> GetAllClientTypes()
        {
            List<ProductClientType> clientTypes = new List<ProductClientType>();
            string q = @"SELECT [id], [type_name] 
                             FROM  [dbo].[ClientTypes]";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            using (OpenCbsReader r = c.ExecuteReader())
            {
                while (r.Read())
                {
                    clientTypes.Add(new ProductClientType(r.GetInt("id"), r.GetString("type_name")));
                }
            }
            return clientTypes;
        }

        public void GetProductAssignedClientTypes(List<ProductClientType> savingClientTypes, int productId)
        {
            foreach (ProductClientType clientType in savingClientTypes)
            {
                string q = string.Format(
                    @"SELECT client_type_id, [saving_product_id]
                      FROM [dbo].[SavingProductsClientTypes]
                      WHERE client_type_id={0} AND [saving_product_id]={1}",
                      clientType.TypeId, productId);

                using (SqlConnection conn = GetConnection())
                using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
                using (OpenCbsReader r = c.ExecuteReader())
                {
                    if (!r.Empty)
                    {
                        clientType.IsChecked = true;
                    }
                }
            }
        }

        public SavingsBookProduct SelectSavingProduct(int productId)
        {
            const string q = "SELECT product_type FROM SavingProducts WHERE id = @productId";

            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@productId", productId);

                switch ((string)c.ExecuteScalar())
                {
                    case "B": return SelectSavingsBookProduct(productId);
                    //case "C": return SelectCompulsoryProduct(productId);
                    default: return null;
                }
            }
        }

        /// <summary>
        /// Select all packages in database
        /// </summary>
        /// <param name="showAlsoDeleted"></param>
        /// <returns>a list contains all packages</returns>
        /// <param name="clientType"></param>
        public List<ISavingProduct> SelectProducts(bool showAlsoDeleted, OClientTypes clientType)
        {
            List<ISavingProduct> productsList = new List<ISavingProduct>();

            string q = @"SELECT DISTINCT 
                                       [dbo].[SavingProducts].[id], 
                                       [dbo].[SavingProducts].[product_type]
                                       FROM [dbo].[SavingProducts]
                                       INNER JOIN [dbo].[SavingProductsClientTypes] ON
                                       [dbo].[SavingProductsClientTypes].[saving_product_id]=[dbo].[SavingProducts].[id] 
                                        INNER JOIN [dbo].[ClientTypes] ON 
                                        [dbo].[ClientTypes].[id]=[dbo].[SavingProductsClientTypes].[client_type_id]
                               WHERE 1 = 1";

            if (!showAlsoDeleted)
                q += " AND deleted = 0";

            switch (clientType)
            {
                case OClientTypes.Person:
                    q += " AND [dbo].[ClientTypes].[type_name] = 'Individual' ";
                    break;
                case OClientTypes.Group:
                    q += "  AND [dbo].[ClientTypes].[type_name] = 'Group' ";
                    break;
                case OClientTypes.Corporate:
                    q += "  AND [dbo].[ClientTypes].[type_name] = 'Corporate'";
                    break;
                case OClientTypes.Village:
                    q += " AND [dbo].[ClientTypes].[type_name]='Village'";
                    break;
            }

            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                using (OpenCbsReader r = c.ExecuteReader())
                {
                    if(r == null || r.Empty) return new List<ISavingProduct>();
                    while (r.Read())
                    {
                        ISavingProduct pack;

                        switch (r.GetChar("product_type"))
                        {
                            case 'B' :
                                pack = new SavingsBookProduct();
                                break;
                           default:
                                pack = null; break;
                        }

                        pack.Id = r.GetInt("id");
                        productsList.Add(pack);
                    }
                }
            }
            
            for (int i = 0; i < productsList.Count; i++)
            {
                if (productsList[i] is SavingsBookProduct)
                    productsList[i] = SelectSavingsBookProduct(productsList[i].Id);
            }
            return productsList;
        }

        public void DeleteSavingProduct(int pProductId)
        {
            const string q = "Update SavingProducts Set Deleted = 1 Where [id] = @productId";

            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@productId", pProductId);
                c.ExecuteNonQuery();
            }
        }
    }
}
