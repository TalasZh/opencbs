// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OpenCbsPackageDeleteException.
	/// </summary>
	[Serializable]
	public class OpenCbsPackageDeleteException : OpenCbsPackageException
	{
		private string code;
        public OpenCbsPackageDeleteException(OpenCbsPackageDeleteExceptionEnum exceptionCode)
		{
			code = FindException(exceptionCode);
		}

        protected OpenCbsPackageDeleteException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", code);
            base.GetObjectData(info, context);
        }

		private string FindException(OpenCbsPackageDeleteExceptionEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
				case OpenCbsPackageDeleteExceptionEnum.PackageIsNull:
					returned = "PackageExceptionPackageIsNull.Text";
					break;

				case OpenCbsPackageDeleteExceptionEnum.AlreadyDeleted:
					returned = "PackageExceptionAlreadyDeleted.Text";
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
	public enum OpenCbsPackageDeleteExceptionEnum
	{
		PackageIsNull,
		AlreadyDeleted,
	}
}
