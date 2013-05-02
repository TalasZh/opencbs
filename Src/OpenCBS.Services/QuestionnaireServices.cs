// LICENSE PLACEHOLDER

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
