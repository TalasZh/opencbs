// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler.Exceptions.FundingLineExceptions
{

    [Serializable]
    public class OpenCbsCorporateException : OpenCbsException
    {
        private string _code;
        public OpenCbsCorporateException(OpenCbsCorporateExceptionEnum exception)
        {
            _code = _FindException(exception);
        }

        public override string ToString()
        {
            return _code;
        }

        protected OpenCbsCorporateException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

        private string _FindException(OpenCbsCorporateExceptionEnum exception)
        {
            string returned = String.Empty;
            switch (exception)
            {
                case OpenCbsCorporateExceptionEnum.CodeIsEmpty:
                    returned = "OctopusCorporateExceptionCodeIsEmpty.Text";
                    break;

                case OpenCbsCorporateExceptionEnum.NameIsEmpty:
                    returned = "OctopusCorporateExceptionNameIsEmpty.Text";
                    break;
                case OpenCbsCorporateExceptionEnum.CityIsEmpty:
                    returned = "OctopusCorporateExceptionCityIsEmpty.Text";
                    break;
                case OpenCbsCorporateExceptionEnum.ProvinceIsEmpty:
                    returned = "OctopusCorporateExceptionProvinceIsEmpty.Text";
                    break;
                case OpenCbsCorporateExceptionEnum.DistrictIsEmpty:
                    returned = "OctopusCorporateExceptionDistrictIsEmpty.Text";
                    break;
                case OpenCbsCorporateExceptionEnum.EmployeeIsFalseFormat:
                    returned = "OctopusCorporateExceptionEmployeeIsFalseFormat.Text";
                    break;
                case OpenCbsCorporateExceptionEnum.VolunteerIsFalseFormat:
                    returned = "OctopusCorporateExceptionVolunteerIsFalseFormat.Text";
                    break;

                case OpenCbsCorporateExceptionEnum.BranchIsEmpty:
                    returned = "BranchIsEmpty";
                    break;
            }
            return returned;
        }


       
    }

    [Serializable]
    public enum OpenCbsCorporateExceptionEnum
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
