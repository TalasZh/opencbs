// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OctopusDOAUpdateException.
	/// </summary>
    [Serializable]
    public class OctopusDOAUpdateException : OctopusDOAException
	{
		private string code;
		public OctopusDOAUpdateException(OctopusDOAUpdateExceptionEnum exceptionCode)
		{
			code = FindException(exceptionCode);
		}

        protected OctopusDOAUpdateException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", code);
            base.GetObjectData(info, context);
        }

		private string FindException(OctopusDOAUpdateExceptionEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
				case OctopusDOAUpdateExceptionEnum.NewNameIsNull:
					returned = "DOAExceptionNewNameIsNull.Text";
					break;

				case OctopusDOAUpdateExceptionEnum.NoSelect:
					returned = "DOAExceptionNoSelect.Text";
					break;
			}
			return returned;
		}

		public override string ToString()
		{
			return this.code;
		}
	}

    [Serializable]
	public enum OctopusDOAUpdateExceptionEnum
	{
		NoSelect,
		NewNameIsNull,
	}
}
