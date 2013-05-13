// Octopus MFS is an integrated suite for managing a Micro Finance Institution: 
// clients, contracts, accounting, reporting and risk
// Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
// Website: http://www.opencbs.com
// Contact: contact@opencbs.com

using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System.Drawing;
using System.IO;
using OpenCBS.Shared;

namespace OpenCBS.Manager
{
	/// <summary>
	/// Summary description for DatabaseHelper.
    /// </summary>
    [Serializable]
	public class DatabaseHelper
	{
		/// <summary>
		/// Method to check if a pValue is null in database, using the current open pReader and the name of the column to read
		/// </summary>
		/// <param name="pColName">the name of the column to read</param>
		/// <param name="pReader">the current open pReader</param>
		/// <returns>true if pValue read is DBNull.pValue, false if not</returns>
		private static bool IsDBNullValue(string pColName,SqlDataReader pReader)
		{
            return pReader.GetValue(pReader.GetOrdinal(pColName)) == DBNull.Value ? true : false;
		}

		public static string GetString(string pColName,SqlDataReader pReader)
		{
		    return !IsDBNullValue(pColName,pReader) ? pReader.GetString(pReader.GetOrdinal(pColName)) : null;
		}

        public static bool HasColumn(string pColName, SqlDataReader pReader)
        {
            for (var i = 0; i < pReader.FieldCount; i++)
                if (pReader.GetName(i).Equals(pColName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            return false;
        }

	    public static int GetInt32(string pColName,SqlDataReader pReader)
		{
			return pReader.GetInt32(pReader.GetOrdinal(pColName));
		}

		public static int? GetNullAuthorizedInt32(string pColName,SqlDataReader pReader)
		{
		    return IsDBNullValue(pColName, pReader) ? (int?) null : GetInt32(pColName, pReader);
		}

        public static int? GetNullAuthorizedSmallInt(string pColName, SqlDataReader pReader)
        {
            return IsDBNullValue(pColName, pReader) ? (int?)null : GetSmallInt(pColName, pReader);
        }

	    public static bool? GetNullAuthorizedBoolean(string pColName,SqlDataReader pReader)
	    {
	        return IsDBNullValue(pColName, pReader) ? (bool?) null : GetBoolean(pColName, pReader);
	    }

	    public static bool GetBoolean(string pColName,SqlDataReader pReader)
		{
			return pReader.GetBoolean(pReader.GetOrdinal(pColName));
		}

		public static OCurrency GetMoney(string pColName,SqlDataReader pReader)
		{
			return pReader.GetDecimal(pReader.GetOrdinal(pColName));
		}

		public static OCurrency GetNullAuthorizedMoney(string pColName,SqlDataReader pReader)
		{
		    return IsDBNullValue(pColName,pReader) ? null : GetMoney(pColName,pReader);
		}

        public static decimal? GetDecimal(string col, SqlDataReader reader)
        {
            if (IsDBNullValue(col, reader)) return null;
            return reader.GetDecimal(reader.GetOrdinal(col));
        }

        public static decimal GetNotNullDecimal(string col, SqlDataReader reader)
        {
            return reader.GetDecimal(reader.GetOrdinal(col));
        }

	    public static double GetDouble(string pColName,SqlDataReader pReader)
		{
			return pReader.GetDouble(pReader.GetOrdinal(pColName));
		}

		public static double? GetNullAuthorizedDouble(string pColName,SqlDataReader pReader)
		{
		    return IsDBNullValue(pColName, pReader) ? (double?) null : GetDouble(pColName, pReader);
		}

	    public static DateTime GetDateTime(string pColName,SqlDataReader pReader)
		{
			return pReader.GetDateTime(pReader.GetOrdinal(pColName));
		}

		public static DateTime? GetNullAuthorizedDateTime(string pColName,SqlDataReader pReader)
		{
		    return IsDBNullValue(pColName, pReader) ? (DateTime?) null : GetDateTime(pColName, pReader);
		}

	    public static char GetChar(string pColName,SqlDataReader pReader)
		{
			return pReader.GetString(pReader.GetOrdinal(pColName)).ToCharArray()[0];
		}

        public static Image GetPhoto(string pString, SqlDataReader pReader)
        {
            if (IsDBNullValue(pString, pReader))
                return null;

            byte[] temp = (byte[])pReader.GetValue(pReader.GetOrdinal(pString));
            return new Bitmap(new MemoryStream(temp));
        }

		public static int GetSmallInt(string pColName,SqlDataReader pReader)
		{
			return pReader.GetInt16(pReader.GetOrdinal(pColName));
		}

		public static void InsertMoneyParam(string pParamName,SqlCommand pCommand,OCurrency pVal)
		{
            if (!pVal.HasValue)
            {
                pCommand.Parameters.AddWithValue(pParamName, SqlMoney.Null);
            }
            else
            {
                pCommand.Parameters.Add(pParamName, SqlDbType.Money);
                pCommand.Parameters[pParamName].Value = pVal.Value;
            }
		}

        public static void InsertDecimalParam(string pParamName, SqlCommand pCommand, decimal? pVal)
        {
            if (pVal.HasValue)
            {
                pCommand.Parameters.Add(pParamName, SqlDbType.Decimal);
                pCommand.Parameters[pParamName].Value = pVal.Value;
            }
            else
            {
                pCommand.Parameters.AddWithValue(pParamName, SqlDecimal.Null);
            }
        }

        public static void InsertMoneyParam(string pParamName, SqlCommand pCommand, OCurrency? pVal)
		{
			if (!pVal.HasValue)
			{
                pCommand.Parameters.AddWithValue(pParamName, SqlMoney.Null);				
			}
			else
			{
                pCommand.Parameters.Add(pParamName, SqlDbType.Money);
                pCommand.Parameters[pParamName].Value = pVal.Value;
			}
		}

        public static void InsertInt32Param(string pParamName, SqlCommand pCommand, int pVal)
		{
			pCommand.Parameters.Add(pParamName,SqlDbType.Int);
			pCommand.Parameters[pParamName].Value = pVal;
		}

        public static void InsertSmalIntParam(string pParamName, SqlCommand pCommand, int pVal)
		{
			pCommand.Parameters.Add(pParamName,SqlDbType.SmallInt);
			pCommand.Parameters[pParamName].Value = pVal;		
		}

        public static void InsertNullValue(string pParamName, SqlCommand pCommand)
		{
			pCommand.Parameters.AddWithValue(pParamName,SqlInt32.Null);
		}

        public static void InsertInt32Param(string pParamName, SqlCommand pCommand, int? pVal)
		{
			if (!pVal.HasValue)
			{
				pCommand.Parameters.AddWithValue(pParamName,SqlInt32.Null);				
			}
			else
			{
				pCommand.Parameters.Add(pParamName,SqlDbType.Int);
				pCommand.Parameters[pParamName].Value = pVal.Value;
			}
		}

        public static void InsertDoubleParam(string pParamName, SqlCommand pCommand, double pVal)
		{
			pCommand.Parameters.Add(pParamName,SqlDbType.Float);
			pCommand.Parameters[pParamName].Value = pVal;
		}

        public static void InsertDoubleParam(string pParamName, SqlCommand pCommand, double? pVal)
		{
			if (!pVal.HasValue)
			{
				pCommand.Parameters.AddWithValue(pParamName,DBNull.Value);				
			}
			else
			{
				pCommand.Parameters.Add(pParamName,SqlDbType.Float);
				pCommand.Parameters[pParamName].Value = pVal.Value;
			}
		}

        public static void InsertBooleanParam(string pParamName, SqlCommand pCommand, bool? pVal)
		{
			if (!pVal.HasValue)
			{
				pCommand.Parameters.AddWithValue(pParamName,DBNull.Value);				
			}
			else
			{
				pCommand.Parameters.Add(pParamName,SqlDbType.Bit);
				pCommand.Parameters[pParamName].Value = pVal.Value;
			}
		}

        public static void InsertBooleanParam(string pParamName, SqlCommand pCommand, bool pVal)
		{
			pCommand.Parameters.Add(pParamName,SqlDbType.Bit);
			pCommand.Parameters[pParamName].Value = pVal;
		}

        public static void InsertStringNVarCharParam(string pParamName, SqlCommand pCommand, string pVal)
		{
			if (pVal != null)
			{
				pCommand.Parameters.Add(pParamName,SqlDbType.NVarChar);
				pCommand.Parameters[pParamName].Value = pVal;
			}
			else
				pCommand.Parameters.AddWithValue(pParamName,DBNull.Value);
		}

        public static void InsertStringVarCharParam(string pParamName, SqlCommand pCommand, string pVal)
		{
			if (pVal != null)
			{
				pCommand.Parameters.Add(pParamName,SqlDbType.VarChar,pVal.Length);
				pCommand.Parameters[pParamName].Value = pVal;
			}
			else
				pCommand.Parameters.AddWithValue(pParamName,DBNull.Value);
		}

        public static void InsertCharParam(string pParamName, SqlCommand pCommand, char pVal)
		{
			pCommand.Parameters.Add(pParamName,SqlDbType.Char,1);
			pCommand.Parameters[pParamName].Value = pVal;
		}

        public static void InsertDateTimeParam(string pParamName, SqlCommand pCommand, DateTime pVal)
		{
			pCommand.Parameters.Add(pParamName,SqlDbType.DateTime);
			pCommand.Parameters[pParamName].Value = pVal.Date;
		}

        public static void InsertDateTimeParamWithTime(string pParamName, SqlCommand pCommand, DateTime pVal)
        {
            pCommand.Parameters.Add(pParamName, SqlDbType.DateTime);
            pCommand.Parameters[pParamName].Value = pVal;
        }

        public static void InsertDateTimeParam(string pParamName, SqlCommand pCommand, DateTime? pVal)
		{
			if (!pVal.HasValue)
			{
				pCommand.Parameters.AddWithValue(pParamName,DBNull.Value);			
			}
			else
			{
				pCommand.Parameters.Add(pParamName,SqlDbType.DateTime);
                pCommand.Parameters[pParamName].Value = pVal.Value;
			}
		}

        public static void InsertObjectParam(string pParamName, SqlCommand pCommand, object pVal)
        {
            if (null == pVal)
                InsertNullValue(pParamName, pCommand);
            else if (pVal is int)
                InsertInt32Param(pParamName, pCommand, (int) pVal);
            else if (pVal is double)
                InsertDoubleParam(pParamName, pCommand, (double) pVal);
            else if (pVal is bool)
                InsertBooleanParam(pParamName, pCommand, (bool) pVal);
            else if (pVal is DateTime)
                InsertDateTimeParam(pParamName, pCommand, (DateTime) pVal);
            else if (pVal is OCurrency)
                InsertMoneyParam(pParamName, pCommand, (OCurrency) pVal);
            else if (pVal is char)
                InsertCharParam(pParamName, pCommand, (char) pVal);
            else
                InsertStringNVarCharParam(pParamName, pCommand, pVal as string);
        }
	}
}
