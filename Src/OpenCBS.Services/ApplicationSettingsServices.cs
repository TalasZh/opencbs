// LICENSE PLACEHOLDER

using System;
using System.Collections;
using OpenCBS.CoreDomain;
using OpenCBS.Enums;
using OpenCBS.Manager.Database;
using OpenCBS.Manager;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Services
{
	/// <summary>
	/// Summary description for DatabaseParametersServices.
	/// </summary>
	public class ApplicationSettingsServices : MarshalByRefObject
	{
        private readonly ApplicationSettingsManager _dataParamManager;
		private readonly NonWorkingDateManagement _nonWorkingDateManager;
	    private readonly User _user;

        public ApplicationSettingsServices(User pUser)
        {
            _user = pUser;
            _dataParamManager = new ApplicationSettingsManager(pUser);
            _nonWorkingDateManager = new NonWorkingDateManagement(pUser);
        }

        public ApplicationSettingsServices(string pTestDb)
        {
            _dataParamManager = new ApplicationSettingsManager(pTestDb);
            _nonWorkingDateManager = new NonWorkingDateManagement(pTestDb);
        }

	    public void FillGeneralDatabaseParameter()
		{
			_dataParamManager.FillGeneralSettings();
		}

		public void FillNonWorkingDate()
		{
			_nonWorkingDateManager.FillNonWorkingDateHelper();
		}

        public void UpdateNonWorkingDate(DictionaryEntry pEntry)
		{
			_nonWorkingDateManager.UpdatePublicHoliday(pEntry);
		}

		public void AddNonWorkingDate(DictionaryEntry pEntry)
		{
			_nonWorkingDateManager.AddPublicHoliday(pEntry);
		}

		public void DeleteNonWorkingDate(DictionaryEntry pEntry)
		{
			_nonWorkingDateManager.DeletePublicHoliday(pEntry);
		}

		public void AddDatabaseParameter(DictionaryEntry pParameter)
		{
			_dataParamManager.AddParameter(pParameter);
		}

		public void CheckApplicationSettings()
		{
            _dataParamManager.FillGeneralSettings();
            _nonWorkingDateManager.FillNonWorkingDateHelper();

            foreach (DictionaryEntry entry in ApplicationSettings.GetInstance(_user.Md5).DefaultParamList)
			{
                if (ApplicationSettings.GetInstance(_user.Md5).GetSpecificParameter(entry.Key.ToString()) == null &&
                    entry.Key.ToString() != OGeneralSettings.LATEDAYSAFTERACCRUALCEASES &&
                    entry.Key.ToString() != OGeneralSettings.WEEKENDDAY1 &&
                    entry.Key.ToString() != OGeneralSettings.WEEKENDDAY2 )
				{
					_dataParamManager.AddParameter(entry);
				}
			}
		}

        public int UpdateSelectedParameter(string pName, object pNewValue)
        {
            if (pNewValue == null)
                return _dataParamManager.UpdateSelectedParameter(pName);
            if (pNewValue is int)
                return _dataParamManager.UpdateSelectedParameter(pName, (int)pNewValue);
            if (pNewValue is bool)
                return _dataParamManager.UpdateSelectedParameter(pName, (bool)pNewValue);
            
            return _dataParamManager.UpdateSelectedParameter(pName, pNewValue.ToString());
        }

        public Guid? GetGuid()
        {
            return _dataParamManager.GetGuid();
        }

        public void SetGuid(Guid guid)
        {
            _dataParamManager.SetGuid(guid);
        }

        public void SetBuildNumber(string buildnum)
        {
            _dataParamManager.SetBuildNumber(buildnum);
        }
	}
}
