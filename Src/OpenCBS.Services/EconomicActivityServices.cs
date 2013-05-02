// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.EconomicActivities;
using OpenCBS.Manager;
using OpenCBS.ExceptionsHandler;

namespace OpenCBS.Services
{
	/// <summary>
    /// Summary description for EconomicActivities.
	/// </summary>
    public class EconomicActivityServices : MarshalByRefObject
	{
		private readonly EconomicActivityManager _doam;

        public EconomicActivityServices(User pUser)
        {
            _doam = new EconomicActivityManager(pUser);
        }

        public EconomicActivityServices(string pTestDb)
        {
            _doam = new EconomicActivityManager(pTestDb);
        }

		public EconomicActivityServices(EconomicActivityManager doam)
		{
			_doam = doam;
		}

        public List<EconomicActivity> FindAllEconomicActivities()
        {
            return _doam.SelectAllEconomicActivities();
        }

        public int AddEconomicActivity(EconomicActivity pEconomicActivity)
        {
            if (pEconomicActivity.Name == String.Empty)
                throw new OctopusDOASaveException(OctopusDOASaveExceptionEnum.NameIsNull);

            if (_doam.ThisActivityAlreadyExist(pEconomicActivity.Name, pEconomicActivity.Parent.Id))
                throw new OctopusDOASaveException(OctopusDOASaveExceptionEnum.AlreadyExist);
           
            if (pEconomicActivity.Parent.Id == 0) pEconomicActivity.Parent = null;
           
            return _doam.AddEconomicActivity(pEconomicActivity);
        }

	    public bool NodeEditable(Object pEconomicActivity)
        {
            if (pEconomicActivity == null)
                throw new OctopusDOAUpdateException(OctopusDOAUpdateExceptionEnum.NoSelect);

            if (!(pEconomicActivity is EconomicActivity))
                throw new OctopusDOAUpdateException(OctopusDOAUpdateExceptionEnum.NoSelect);

            return true;
        }

        public bool ChangeDomainOfApplicationName(EconomicActivity pEconomicActivity, string newName)
        {
            if (newName == String.Empty)
                throw new OctopusDOAUpdateException(OctopusDOAUpdateExceptionEnum.NewNameIsNull);

            if (_doam.ThisActivityAlreadyExist(newName, pEconomicActivity.Parent.Id))
                throw new OctopusDOASaveException(OctopusDOASaveExceptionEnum.AlreadyExist);

            EconomicActivity activity = pEconomicActivity;
            activity.Name = newName;

            _doam.UpdateEconomicActivity(activity);

            return true;
        }

	    public EconomicActivity FindEconomicActivity(int doaId)
		{
			return _doam.SelectEconomicActivity(doaId);
		}

        public EconomicActivity FindEconomicActivity(string name)
        {
            return _doam.SelectEconomicActivity(name);
        }

        public void DeleteEconomicActivity(EconomicActivity pEconomicActivity)
        {
            if (pEconomicActivity.HasChildrens)
                throw new OctopusDOADeleteException(OctopusDOADeleteExceptionEnum.HasChildrens);

            pEconomicActivity.Deleted = true;
            _doam.UpdateEconomicActivity(pEconomicActivity);
        }

        public void AddEconomicActivityLoanHistory(EconomicActivityLoanHistory activityLoanHistory, SqlTransaction sqlTransaction)
        {
            _doam.AddEconomicActivityLoanHistory(activityLoanHistory, sqlTransaction);
        }

        public bool EconomicActivityLoanHistoryExists(int contractId, int personId, SqlTransaction sqlTransaction)
        {
            return _doam.EconomicActivityLoanHistoryExists(contractId, personId, sqlTransaction);
        }

        public bool EconomicActivityLoanHistoryDeleted(int contractId, int personId, SqlTransaction sqlTransaction)
        {
            return _doam.EconomicActivityLoanHistoryDeleted(contractId, personId, sqlTransaction);
        }

        public void UpdateDeletedEconomicActivityLoanHistory(int contractId, int personId, int economicActivityId,
            SqlTransaction sqlTransaction, bool deleted)
        {
            _doam.UpdateDeletedEconomicActivityLoanHistory(contractId, personId, economicActivityId, sqlTransaction, deleted);
        }
	}
}
