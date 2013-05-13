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
using System.Collections;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Serialization.Formatters;
using OpenCBS.CoreDomain;
using OpenCBS.Services.Accounting;
using OpenCBS.Services.Events;
using OpenCBS.Services.Rules;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;
using OpenCBS.Services.Currencies;
using OpenCBS.Services.Export;

namespace OpenCBS.Services
{
    public class Remoting : IServices
    {
        private static bool _exist;
        private string _md5 = string.Empty;
        private string _userName;
        private string _pass;
        private string _account;
        static private IRemoteOperation _remoteOperation;
        private static Remoting _remotingUniqueInstance;        

        #region Accessors

        private static User CurrentUser
        {
            get { return User.CurrentUser; }
        }

        public string Account
        {
            get { return _account; }
            set { _account = value; }
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public string Pass
        {
            get { return _pass; }
            set { _pass = value; }
        }

        public string DbName
        {
            get { return _account; }
            set { _account = value; }
        }
        #endregion

        private Remoting()
        {
            RemotingConnection();
        }

        public static Remoting GetInstance()
        {
            if (_remotingUniqueInstance == null)
                return _remotingUniqueInstance = new Remoting();
            else
                return _remotingUniqueInstance;
        }

        // Initialise the remoting connection with http channel with binary formatter
        public bool RemotingConnection()
        {
            if (!_exist)
            {
                
                HttpChannel channel = null;
                try
                {
                    IDictionary props = new Hashtable();
                    props["port"] = TechnicalSettings.RemotingServerPort;
                    props.Add("typeFilterLevel", TypeFilterLevel.Full);
                    //props.Add("timeout", 2000);
                    BinaryClientFormatterSinkProvider clientProvider = new BinaryClientFormatterSinkProvider();
                    channel = new HttpChannel(props, clientProvider, null);
                    ChannelServices.RegisterChannel(channel, false);

                    //TcpChannel channel = new TcpChannel();
                    //ChannelServices.RegisterChannel(channel, true);
                    var server = string.Format("http://{0}:{1}/RemoteOperation", TechnicalSettings.RemotingServer,
                                               TechnicalSettings.RemotingServerPort);

                    _remoteOperation = (IRemoteOperation) Activator.GetObject(typeof (IRemoteOperation), server);
                    _exist = _remoteOperation.TestRemoting();

                    //ChannelServices.GetChannelSinkProperties(_remoteOperation)["timeout"] = 0;
                }
                catch(Exception e)
                {
                    _remotingUniqueInstance = null;
                    ChannelServices.UnregisterChannel(channel);
                    throw;
                }
            }
            return true;
        }

        private void TimeoutHandler()
        {
            // Ask a new token
            _md5 = _remoteOperation.GetAuthentification(_userName, _pass, _account, "TIMEOUT", "TIMEOUT");
        }
        
        public UserServices GetUserServices()
        {
            if (_remoteOperation != null)
            {
                try
                {
                    return _remoteOperation.GetUserServices(CurrentUser);
                }
                catch
                {
                    TimeoutHandler();
                  return _remoteOperation.GetUserServices(CurrentUser);
                }
            }
            return null;
        }

        public AccountingServices GetAccountingServices()
        {
            return _remoteOperation.GetAccountingServices(CurrentUser);
        }

        public ExchangeRateServices GetExchangeRateServices()
        {
            return _remoteOperation.GetExchangeRateServices(CurrentUser);
        }

        public ChartOfAccountsServices GetChartOfAccountsServices()
        {
            return _remoteOperation.GetChartOfAccountsServices(CurrentUser);
        }

        public EventProcessorServices GetEventProcessorServices()
        {
            return _remoteOperation.GetEventProcessorServices(CurrentUser);
        }

        public StandardBookingServices GetStandardBookingServices()
        {
            return _remoteOperation.GetStandardBookingServices(CurrentUser);
        }

        public SavingProductServices GetSavingProductServices()
        {
            //remoteOperation permet de savoir ou se trouve le serveur
            return _remoteOperation.GetSavingProductServices(CurrentUser);
        }
        public RegExCheckerServices GetRegExCheckerServices()
        {
            return new RegExCheckerServices(CurrentUser);
        }
        public SavingServices GetSavingServices()
        {
            return _remoteOperation.GetSavingServices(CurrentUser);
        }

        //public CashReceiptServices GetCashReceiptServices()
        //{
        //    return remoteOperation.GetCashReceiptServices(User.CurrentUser);
        //}

        public ClientServices GetClientServices()
        {
            return _remoteOperation.GetClientServices(CurrentUser);
        }

        public LoanServices GetContractServices()
        {
            return _remoteOperation.GetContractServices(CurrentUser);
        }

        public DatabaseServices GetDatabaseServices()
        {
            return _remoteOperation.GetDatabaseServices(CurrentUser);
        }

        public EconomicActivityServices GetEconomicActivityServices()
        {
            return _remoteOperation.GetEconomicActivities(CurrentUser);
        }

        public ApplicationSettingsServices GetApplicationSettingsServices()
        {
            return _remoteOperation.GetApplicationSettingsServices(CurrentUser);
        }

        public GraphServices GetGraphServices()
        {
            return _remoteOperation.GetGraphServices(CurrentUser);
        }

        public LocationServices GetLocationServices()
        {
            return _remoteOperation.GetLocationServices(CurrentUser);
        }

        public PicturesServices GetPicturesServices()
        {
            return _remoteOperation.GetPicturesServices(CurrentUser);
        }

        public ProductServices GetProductServices()
        {
            return _remoteOperation.GetProductServices(CurrentUser);
        }

        public CollateralProductServices GetCollateralProductServices()
        {
            // TO DO: This could be later implemented for remoting
                       
            //return _remoteOperation.GetCollateralProductServices(User.CurrentUser);
            return null;
        }

        public SettingsImportExportServices GetSettingsImportExportServices()
        {
            return _remoteOperation.GetSettingsImportExportServices(CurrentUser);
        }

        public ProjectServices GetProjectServices()
        {
            return _remoteOperation.GetProjectServices(CurrentUser);
        }

        public MFIServices GetMFIServices()
        {
            return _remoteOperation.GetMFIServices(CurrentUser);
        }

        public ApplicationSettings GetGeneralSettings()
        {
            return _remoteOperation.GetGeneralSettings(CurrentUser);
        }

        public FundingLineServices GetFundingLinesServices()
        {
            return _remoteOperation.GetFundingLinesServices(CurrentUser);
        }

        public string GetAuthentification(string pOctoUsername, string pOctoPass, string pDbName, string pComputerName, string pLoginName)
        {
            _md5 = _remoteOperation.GetAuthentification(pOctoUsername, pOctoPass, pDbName, pComputerName, pLoginName);
            return _md5;
        }
        public string GetToken()
        {
            return _md5;
        }

        public void RunTimeout()
        {
            if (_remoteOperation != null)
                _remoteOperation.RunTimeout();
        }


        public NonWorkingDateSingleton GetNonWorkingDate()
        {
            return _remoteOperation.GetNonWorkingDate(CurrentUser);
        }

        public SQLToolServices GetSQLToolServices()
        {
            return _remoteOperation.GetSQLToolServices(CurrentUser);
        }

        public QuestionnaireServices GetQuestionnaireServices()
        {
            return _remoteOperation.GetQuestionnaireServices(CurrentUser);
        }

        public CurrencyServices GetCurrencyServices()
        {
            return _remoteOperation.GetCurrencyServices(CurrentUser);
        }

        public AccountingRuleServices GetAccountingRuleServices()
        {
            return _remoteOperation.GetAccountingRuleServices(CurrentUser);
        }

        public RoleServices GetRoleServices()
        {
            return _remoteOperation.GetRoleServices(CurrentUser);
        }

        public ExportServices GetExportServices()
        {
            return _remoteOperation.GetExportServices(CurrentUser);
        }

        public BranchService GetBranchService()
        {
            return _remoteOperation.GetBranchService(CurrentUser);
        }

        public TellerServices GetTellerServices()
        {
            return _remoteOperation.GetTellerServices(CurrentUser);
        }

        public CustomizableFieldsServices GetCustomizableFieldsServices()
        {
            return _remoteOperation.GetAdvancedCustomizableFieldsServices(CurrentUser);
        }

        public PaymentMethodServices GetPaymentMethodServices()
        {
            return _remoteOperation.GetPaymentMethodServices(CurrentUser);
        }

        public MenuItemServices GetMenuItemServices()
        {
            return _remoteOperation.GetMenuItemServices(CurrentUser);
        }

        #region IServices Members


        public void SuppressAllRemotingInfos(string pComputerName, string pLoginName)
        {
            _remoteOperation.SuppressAllRemotingInfos(CurrentUser, pComputerName, pLoginName);
        }

        #endregion

        #region IServices Members

       #endregion
    }
}
