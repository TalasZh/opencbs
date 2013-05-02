// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler.Exceptions.FundingLineExceptions
{

    [Serializable]
    public class OctopusCorporateException : OctopusException
    {
        private string _code;
        public OctopusCorporateException(OctopusCorporateExceptionEnum exception)
        {
            _code = _FindException(exception);
        }

        public override string ToString()
        {
            return _code;
        }

        protected OctopusCorporateException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

        private string _FindException(OctopusCorporateExceptionEnum exception)
        {
            string returned = String.Empty;
            switch (exception)
            {
                case OctopusCorporateExceptionEnum.CodeIsEmpty:
                    returned = "OctopusCorporateExceptionCodeIsEmpty.Text";
                    break;

                case OctopusCorporateExceptionEnum.NameIsEmpty:
                    returned = "OctopusCorporateExceptionNameIsEmpty.Text";
                    break;
                case OctopusCorporateExceptionEnum.CityIsEmpty:
                    returned = "OctopusCorporateExceptionCityIsEmpty.Text";
                    break;
                case OctopusCorporateExceptionEnum.ProvinceIsEmpty:
                    returned = "OctopusCorporateExceptionProvinceIsEmpty.Text";
                    break;
                case OctopusCorporateExceptionEnum.DistrictIsEmpty:
                    returned = "OctopusCorporateExceptionDistrictIsEmpty.Text";
                    break;
                case OctopusCorporateExceptionEnum.EmployeeIsFalseFormat:
                    returned = "OctopusCorporateExceptionEmployeeIsFalseFormat.Text";
                    break;
                case OctopusCorporateExceptionEnum.VolunteerIsFalseFormat:
                    returned = "OctopusCorporateExceptionVolunteerIsFalseFormat.Text";
                    break;

                case OctopusCorporateExceptionEnum.BranchIsEmpty:
                    returned = "BranchIsEmpty";
                    break;
            }
            return returned;
        }


       
    }

    [Serializable]
    public enum OctopusCorporateExceptionEnum
        {
            CodeIsEmpty,
            NameIsEmpty,
            CityIsEmpty,
            DistrictIsEmpty,
            ProvinceIsEmpty,
            VolunteerIsFalseFormat,
            EmployeeIsFalseFormat,
            BranchIsEmpty
        }

}
