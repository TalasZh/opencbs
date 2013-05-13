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
using System.Data;
using System.Data.SqlClient;
using OpenCBS.CoreDomain;
using OpenCBS.DatabaseConnection;
using OpenCBS.Manager;

namespace OpenCBS.Services
{
    public class QuestionnaireServices : MarshalByRefObject
    {
        private readonly User _user;
        private readonly QuestionnaireManager _questionnaireManager;

        public QuestionnaireServices(User pUser)
        {
            _user = pUser;
            _questionnaireManager = new QuestionnaireManager(pUser);
        }

        //For Fitness test only
        public QuestionnaireServices(string pLogin, string pPassword, string pServer, string pDatabase, string pSetupPath, string pTimeout) {}

        public void UpdateIfSent(bool isSent)
        {
            _questionnaireManager.UpdateSentField(isSent);
        }

        public void UpdateQuestionnaire(string name, 
                                        string country, 
                                        string email, 
                                        string numberOfClients, 
                                        string grossPortfolio, 
                                        string positionInCompony, 
                                        string personName, 
                                        string phone, 
                                        string skype, 
                                        string purposeOfUsage, 
                                        string otherMessages,
                                        bool isSent)
        {
            SqlTransaction transac = ConnectionManager.GetInstance().GetSqlTransaction(_user.Md5);

            try
            {
                _questionnaireManager.Update( name,
                                                        country,
                                                        email,
                                                        numberOfClients,
                                                        grossPortfolio,
                                                        positionInCompony,
                                                        personName,
                                                        phone,
                                                        skype,
                                                        purposeOfUsage,
                                                        otherMessages,
                                                        transac,
                                                        isSent
                                                     );

                transac.Commit();
            }
            catch (Exception e)
            {
                transac.Rollback();
                throw;
            }
        }

        public bool DataExist()
        {
            return _questionnaireManager.GetQuestionnaire() != null;
        }

        public void SaveQuestionnaire(string name, 
                                    string country, 
                                    string email, 
                                    string numberOfClients, 
                                    string grossPortfolio, 
                                    string positionInCompony, 
                                    string personName, 
                                    string phone, 
                                    string skype, 
                                    string purposeOfUsage, 
                                    string otherMessages,
                                    bool isSent)
        {
            SqlTransaction transac = ConnectionManager.GetInstance().GetSqlTransaction(_user.Md5);

            try
            {
                if (!DataExist())
                {
                    _questionnaireManager.SaveQuestionnaire(name, 
                                                        country, 
                                                        email, 
                                                        numberOfClients, 
                                                        grossPortfolio, 
                                                        positionInCompony, 
                                                        personName, 
                                                        phone, 
                                                        skype, 
                                                        purposeOfUsage, 
                                                        otherMessages, 
                                                        transac,
                                                        isSent);
                }
                else
                {
                    _questionnaireManager.Update(name,
                                                country,
                                                email,
                                                numberOfClients,
                                                grossPortfolio,
                                                positionInCompony,
                                                personName,
                                                phone,
                                                skype,
                                                purposeOfUsage,
                                                otherMessages,
                                                transac,
                                                isSent);
                }

                transac.Commit();
            }
            catch (Exception e)
            {
                transac.Rollback();
                throw;
            }
        }

        public MyInformation GetQuestionnaire()
        {
            return _questionnaireManager.GetQuestionnaire();
        }
    }
}
