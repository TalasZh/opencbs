// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;


namespace OpenCBS.ExceptionsHandler
{

    [Serializable]
    public class OctopusProjectSaveException : OctopusProjectException
    {
        private string code;

        protected OctopusProjectSaveException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", code);
            base.GetObjectData(info, context);
        }

        public OctopusProjectSaveException(OctopusProjectSaveExceptionEnum exceptionCode)
        {
            code = FindException(exceptionCode);
        }

        public override string ToString()
        {
            return code;
        }

        private string FindException(OctopusProjectSaveExceptionEnum exceptionId)
        {
            string returned = String.Empty;
            switch (exceptionId)
            {
                case OctopusProjectSaveExceptionEnum.CodeIsEmpty:
                    returned = "ProjectExceptionCodeIsNull.Text";
                    break;
                case OctopusProjectSaveExceptionEnum.NameIsEmpty:
                    returned = "ProjectExceptionNameIsNull.Text";
                    break;
                case OctopusProjectSaveExceptionEnum.AimIsEmpty:
                    returned = "ProjectExceptionAimIsNull.Text";
                    break;
                case OctopusProjectSaveExceptionEnum.ClientIsEmpty:
                    returned = "ProjectExceptionClientIsNull.Text";
                    break;
                case OctopusProjectSaveExceptionEnum.BeginDateEmpty:
                    returned = "ProjectExceptionBeginDateEmpty.Text";
                    break;
                case OctopusProjectSaveExceptionEnum.CAIsBad:
                    returned = "ProjectExceptionCAIsBad.Text";
                    break;
                case OctopusProjectSaveExceptionEnum.FinancialPlanAmountIsBad:
                    returned = "ProjectExceptionFinancialPlanAmountIsBad.Text";
                    break;
                case OctopusProjectSaveExceptionEnum.FinancialTotalPlanAmountIsBad:
                    returned = "ProjectExceptionFinancialTotalPlanAmountIsBad.Text";
                    break;
                case OctopusProjectSaveExceptionEnum.CACannotBeNullInFollowUp:
                    returned = "ProjectExceptionCACannotBeNullInFollowUp.Text";
                    break;
            }
            return returned;
        }
    }

    [Serializable]
    public enum OctopusProjectSaveExceptionEnum
    {
        CodeIsEmpty,
        NameIsEmpty,
        AimIsEmpty,
        ClientIsEmpty,
        BeginDateEmpty,
        CAIsBad,
        FinancialPlanAmountIsBad,
        FinancialTotalPlanAmountIsBad,
        CACannotBeNullInFollowUp
    }
}

