// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OctopusAccountException.
	/// </summary>
    [Serializable]
    public class GeneralSettingException : OpenCbsException
	{
		private string _code;
        public GeneralSettingException(GeneralSettingEnumException exceptionCode)
		{
			_code = _FindException(exceptionCode);
		}

		public override string ToString()
		{
			return _code;
		}

        protected GeneralSettingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

        private string _FindException(GeneralSettingEnumException exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
                case GeneralSettingEnumException.OnlyChar:
                    returned = "onlyChars.Text";
					break;

                case GeneralSettingEnumException.OnlyInt:
                    returned = "onlyInts.Text";
                    break;

                case GeneralSettingEnumException.OnlyIntAndUnderscore:
                    returned = "onlyIntsAndUnderscore.Text";
                    break;

				
			}
			return returned;
		}
	}

    [Serializable]
    public enum GeneralSettingEnumException
	{
        OnlyChar,
        OnlyInt,
        OnlyIntAndUnderscore
	}
}
