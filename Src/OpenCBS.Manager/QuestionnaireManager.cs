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

using System.Data;
using OpenCBS.CoreDomain;
using System.Data.SqlClient;

namespace OpenCBS.Manager
{
    public class QuestionnaireManager : Manager
    {
        public QuestionnaireManager(User pUser): base(pUser){}
        public QuestionnaireManager(string pDbConnectionString): base(pDbConnectionString){}

        public void SaveQuestionnaire(
            string mfiName, 
            string country, 
            string email, 
            string numberOfClients, 
            string grossPortfolio, 
            string position, 
            string personName, 
            string phone, 
            string skype, 
            string purposeOfUsage,
            string OtherMessages,
            SqlTransaction sqlTransac,
            bool isSent)
        {
            const string sqlText = @"INSERT INTO 
                                     Questionnaire(
                                                      Name
                                                    , Country
                                                    , Email
                                                    , NumberOfClients
                                                    , GrossPortfolio
                                                    , PositionInCompony
                                                    , PersonName
                                                    , Phone
                                                    , Skype
                                                    , PurposeOfUsage
                                                    , OtherMessages
                                                    , is_sent) 
                                     VALUES (
                                                  @Name
                                                , @Country
                                                , @Email
                                                , @NumberOfClients
                                                , @GrossPortfolio
                                                , @PositionInCompony
                                                , @PersonName
                                                , @Phone
                                                , @Skype
                                                , @PurposeOfUsage
                                                , @OtherMessages
                                                , @isSent)";
            
            using (OpenCbsCommand cmd = new OpenCbsCommand(sqlText, sqlTransac.Connection, sqlTransac))
            {
                SetFields(cmd, 
                        mfiName, 
                        country, 
                        numberOfClients, 
                        grossPortfolio, 
                        personName, 
                        position, 
                        phone, 
                        email, 
                        skype, 
                        purposeOfUsage, 
                        OtherMessages, 
                        isSent);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateSentField(bool isSent)
        {
            string sql = @"UPDATE [dbo].[Questionnaire]
                           SET [is_sent] = @isSent";
            using (SqlConnection conn = GetConnection())
            using (var cmd = new OpenCbsCommand(sql, conn))
            {
                cmd.AddParam("@isSent", isSent);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(string mfiName,
                            string country,
                            string email,
                            string numberOfClients,
                            string grossPortfolio,
                            string position,
                            string personName,
                            string phone,
                            string skype,
                            string purposeOfUsage,
                            string OtherMessages,
                            SqlTransaction sqlTransac,
                            bool isSent)
        {
            string sql =
                @"
                    UPDATE [dbo].[Questionnaire]
                       SET [Name] = @Name
                          ,[Country] = @Country
                          ,[Email] = @Email
                          ,[PositionInCompony] = @PositionInCompony
                          ,[OtherMessages] = @OtherMessages
                          ,[GrossPortfolio] = @GrossPortfolio
                          ,[NumberOfClients] = @NumberOfClients
                          ,[PersonName] = @PersonName
                          ,[Phone] = @Phone
                          ,[Skype] = @Skype
                          ,[PurposeOfUsage] = @PurposeOfUsage
                          ,[is_sent] = @isSent
                     ";
            using (OpenCbsCommand cmd = new OpenCbsCommand(sql,sqlTransac.Connection, sqlTransac))
            {
                SetFields(  cmd,
                            mfiName,
                            country,
                            numberOfClients,
                            grossPortfolio,
                            personName,
                            position,
                            phone,
                            email,
                            skype,
                            purposeOfUsage,
                            OtherMessages,
                            isSent
                        );
                cmd.ExecuteNonQuery();
            }
        }

        private void SetFields(OpenCbsCommand cmd, 
                                        string mfiName, 
                                        string country, 
                                        string numberOfClients, 
                                        string grossPortfolio, string personName, 
                                        string position, 
                                        string phone, 
                                        string email, 
                                        string skype, 
                                        string purposeOfUsage, 
                                        string OtherMessages, 
                                        bool isSent)
        {
            cmd.AddParam("@Name",  mfiName);
            cmd.AddParam("@Country",  country);
            cmd.AddParam("@NumberOfClients",  numberOfClients);
            cmd.AddParam("@GrossPortfolio", grossPortfolio);

            cmd.AddParam("@PersonName", personName);
            cmd.AddParam("@PositionInCompony", position);
            cmd.AddParam("@Phone", phone);
            cmd.AddParam("@Email", email);
            cmd.AddParam("@Skype", skype);
            cmd.AddParam("@PurposeOfUsage", purposeOfUsage);
            cmd.AddParam("@OtherMessages", OtherMessages);
            cmd.AddParam("@isSent", isSent);
        }

        public MyInformation GetQuestionnaire()
        {
            const string sqlText = @"SELECT   Name
                                            , Country
                                            , Email
                                            , NumberOfClients
                                            , GrossPortfolio
                                            , PositionInCompony
                                            , PersonName
                                            , Phone
                                            , Skype
                                            , PurposeOfUsage
                                            , OtherMessages 
                                            , is_sent
                                            FROM Questionnaire";

            MyInformation myInformation = new MyInformation();
            myInformation.MfiName = null;
            using (SqlConnection conn = GetConnection())
            using (var cmd = new OpenCbsCommand(sqlText, conn))
            {
                using(OpenCbsReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Empty)
                    {
                        reader.Read();
                        myInformation.MfiName = reader.GetString("Name");
                        myInformation.Country = reader.GetString("Country");
                        myInformation.Email = reader.GetString("Email");
                        myInformation.NumberOfClients = reader.GetString("NumberOfClients");
                        myInformation.GrossPortfolio = reader.GetString("GrossPortfolio");
                        myInformation.PositionInCompany = reader.GetString("PositionInCompony");
                        myInformation.PersonName = reader.GetString("PersonName");
                        myInformation.Phone = reader.GetString("Phone");
                        myInformation.Skype = reader.GetString("Skype");
                        myInformation.PurposeOfUsage = reader.GetString("PurposeOfUsage");
                        myInformation.Comment = reader.GetString("OtherMessages");
                        myInformation.IsSent = reader.GetBool("is_sent");
                        return myInformation;
                    }
                    return null;
                }
            }
        }
    }
}
