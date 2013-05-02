// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OctopusPackageDeleteException.
	/// </summary>
	[Serializable]
	public class OctopusPackageDeleteException : OctopusPackageException
	{
		private string code;
        public OctopusPackageDeleteException(OctopusPackageDeleteExceptionEnum exceptionCode)
		{
			code = FindException(exceptionCode);
		}

        protected OctopusPackageDeleteException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", code);
            base.GetObjectData(info, context);
        }

		private string FindException(OctopusPackageDeleteExceptionEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
				case OctopusPackageDeleteExceptionEnum.PackageIsNull:
					returned = "PackageExceptionPackageIsNull.Text";
					break;

				case OctopusPackageDeleteExceptionEnum.AlreadyDeleted:
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
	public enum OctopusPackageDeleteExceptionEnum
	{
		PackageIsNull,
		AlreadyDeleted,
	}
}
