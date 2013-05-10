// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OpenCbsDoaUpdateException.
	/// </summary>
    [Serializable]
    public class OpenCbsDoaUpdateException : OpenCbsDoaException
	{
		private string code;
		public OpenCbsDoaUpdateException(OpenCbsDOAUpdateExceptionEnum exceptionCode)
		{
			code = FindException(exceptionCode);
		}

        protected OpenCbsDoaUpdateException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", code);
            base.GetObjectData(info, context);
        }

		private string FindException(OpenCbsDOAUpdateExceptionEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
				case OpenCbsDOAUpdateExceptionEnum.NewNameIsNull:
					returned = "DOAExceptionNewNameIsNull.Text";
					break;

				case OpenCbsDOAUpdateExceptionEnum.NoSelect:
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
	public enum OpenCbsDOAUpdateExceptionEnum
	{
		NoSelect,
		NewNameIsNull,
	}
}
