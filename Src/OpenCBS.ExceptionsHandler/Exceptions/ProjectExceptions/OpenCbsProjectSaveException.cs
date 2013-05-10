// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;


namespace OpenCBS.ExceptionsHandler
{

    [Serializable]
    public class OpenCbsProjectSaveException : OpenCbsProjectException
    {
        private string code;

        protected OpenCbsProjectSaveException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", code);
            base.GetObjectData(info, context);
        }

        public OpenCbsProjectSaveException(OpenCbsProjectSaveExceptionEnum exceptionCode)
        {
            code = FindException(exceptionCode);
        }

        public override string ToString()
        {
            return code;
        }

        private string FindException(OpenCbsProjectSaveExceptionEnum exceptionId)
        {
            string returned = String.Empty;
            switch (exceptionId)
            {
                case OpenCbsProjectSaveExceptionEnum.CodeIsEmpty:
                    returned = "ProjectExceptionCodeIsNull.Text";
                    break;
                case OpenCbsProjectSaveExceptionEnum.NameIsEmpty:
                    returned = "ProjectExceptionNameIsNull.Text";
                    break;
                case OpenCbsProjectSaveExceptionEnum.AimIsEmpty:
                    returned = "ProjectExceptionAimIsNull.Text";
                    break;
                case OpenCbsProjectSaveExceptionEnum.ClientIsEmpty:
                    returned = "ProjectExceptionClientIsNull.Text";
                    break;
                case OpenCbsProjectSaveExceptionEnum.BeginDateEmpty:
                    returned = "ProjectExceptionBeginDateEmpty.Text";
                    break;
                case OpenCbsProjectSaveExceptionEnum.CAIsBad:
                    returned = "ProjectExceptionCAIsBad.Text";
                    break;
                case OpenCbsProjectSaveExceptionEnum.FinancialPlanAmountIsBad:
                    returned = "ProjectExceptionFinancialPlanAmountIsBad.Text";
                    break;
                case OpenCbsProjectSaveExceptionEnum.FinancialTotalPlanAmountIsBad:
                    returned = "ProjectExceptionFinancialTotalPlanAmountIsBad.Text";
                    break;
                case OpenCbsProjectSaveExceptionEnum.CACannotBeNullInFollowUp:
                    returned = "ProjectExceptionCACannotBeNullInFollowUp.Text";
                    break;
            }
            return returned;
        }
    }

    [Serializable]
    public enum OpenCbsProjectSaveExceptionEnum
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

