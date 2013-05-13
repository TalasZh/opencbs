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

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using OpenCBS.CoreDomain;
using OpenCBS.Enums;
using OpenCBS.Shared;

namespace OpenCBS.Manager
{
    public class PicturesManager : Manager
    {
        public PicturesManager(User pUser) : base(pUser) { }

        public PicturesManager(string pDbConnectionString) : base(pDbConnectionString) { }

        /// <summary>
        /// Class storing picture informations.
        /// </summary>
        public sealed class PictureInfo
        {
            /// <summary>
            /// Picture group
            /// </summary>
            public string Group;
            /// <summary>
            /// Picture name
            /// </summary>
            public string Name;
            /// <summary>
            /// Picture binary data (PNG format)
            /// </summary>
            public byte[] Binary;
            /// <summary>
            /// Picture Id
            /// </summary>
            public int Id;
            /// <summary>
            /// Picture sub Id
            /// </summary>
            public int SubId;
        }


        /// <summary>
        /// Returns requested picture.
        /// </summary>
        /// <param name="pGroup">Picture group</param>
        /// <param name="pId">Picture Id</param>
        /// <param name="pSubID">Picture sub Id</param>
        /// <param name="pThumbnail">Do you want the thumbnail or the actual picture?</param>
        /// <returns>Found picture informations</returns>
        public PictureInfo GetPicture(string pGroup, int pId, int pSubID, bool pThumbnail)
        {
            string sql = pThumbnail
                ? "SELECT thumbnail,name FROM Pictures WHERE [group]=@group AND id=@id AND subid=@subid"
                : "SELECT picture,name FROM Pictures WHERE [group]=@group AND id=@id AND subid=@subid";

            using (OpenCbsCommand c = new OpenCbsCommand(sql, AttachmentsConnection))
            {
                c.AddParam("@group", pGroup);
                c.AddParam("@id", pId);
                c.AddParam("@subid", pSubID);

                using (OpenCbsReader r = c.ExecuteReader())
                {
                    if (r == null || r.Empty) return null;

                    r.Read();
                    PictureInfo pi = new PictureInfo
                    {
                        Binary = r.GetBytes(0),
                        Name = r.GetString(1),
                        Id = pId,
                        SubId = pSubID,
                        Group = pGroup
                    };
                    return pi;
                }
            }
        }

        /// <summary>
        /// Returns all associated pictures
        /// </summary>
        /// <param name="pGroup">Picture group</param>
        /// <param name="pId">Picture Id</param>
        /// <param name="pThumbnail">Do you want the thumbnail or the actual picture?</param>
        /// <returns>List of found pictures informations</returns>
        public List<PictureInfo> GetPictures(string pGroup, int pId, bool pThumbnail)
        {
            List<PictureInfo> pictures = new List<PictureInfo>();
            List<int> subids = GetPictureSubIds(pGroup, pId);
            foreach (int subid in subids)
            {
                pictures.Add(GetPicture(pGroup, pId, subid, pThumbnail));
            }
            return pictures;
        }

        /// <summary>
        /// Returns the first (smallest subid) associated picture.
        /// </summary>
        /// <param name="pGroup">Picture group</param>
        /// <param name="pId">Picture Id</param>
        /// <param name="pThumbnail">Do you want the thumbnail or the actual picture?</param>
        /// <returns></returns>
        public PictureInfo GetFirstPicture(string pGroup, int pId, bool pThumbnail)
        {
            List<int> subids = GetPictureSubIds(pGroup, pId);
            if (subids.Count > 0)
            {
                return GetPicture(pGroup, pId, subids[0], pThumbnail);
            }
            return null;
        }

        /// <summary>
        /// DeleteAccount a picture from database.
        /// </summary>
        /// <param name="pGroup">Picture group</param>
        /// <param name="pId">Picture Id</param>
        /// <param name="pSubID">Picture sub Id</param>
        /// <returns>Number of rows affected by deletion</returns>
        public int DeletePicture(string pGroup, int pId, int pSubID)
        {
            string q = @"DELETE FROM Pictures 
            WHERE [Pictures].[group]=@group AND [Pictures].[id]=@id AND [Pictures].[subid]=@subid";
            using (OpenCbsCommand c = new OpenCbsCommand(q, AttachmentsConnection))
            {
                c.AddParam("group", pGroup);
                c.AddParam("id", pId);
                c.AddParam("subid", pSubID);
                return c.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Add a new picture in the database.<br/>
        /// If pName is null or empty, an automatic name will be generated.
        /// </summary>
        /// <param name="pGroup">Picture group</param>
        /// <param name="pId">Picture Id</param>
        /// <param name="pictureSubId">Picture subId</param>
        /// <param name="pPicture">PNG picture binary data</param>
        /// <param name="pThumbnail">PNG thumbnail picture binary data</param>
        /// <param name="pName">Picture name</param>
        /// <returns></returns>
        public int AddPicture(string pGroup, int pId, int pictureSubId, byte[] pPicture, byte[] pThumbnail, string pName)
        {
            // Find the first available subid
            List<int> subIds = GetPictureSubIds(pGroup, pId);
            int foundPlace = subIds.Count;
            for (int i = 0; i < subIds.Count; i++)
            {
                if (subIds[i] != i)
                {
                    foundPlace = i;
                    break;
                }
            }
            // Add row
            string q =
                @"INSERT INTO Pictures ([group], [id] ,[subid] ,[picture] ,[thumbnail] ,[name]) 
                VALUES (@group ,@id ,@subid ,@picture ,@thumbnail ,@name)";
            using (OpenCbsCommand c = new OpenCbsCommand(q, AttachmentsConnection))
            {
                c.AddParam("group", pGroup);
                c.AddParam("id", pId);
                c.AddParam("subid", pictureSubId);
                c.AddParam("picture", pPicture);
                c.AddParam("thumbnail", pThumbnail);
                if (pName.Length < 50)
                    c.AddParam("name", pName);
                else
                    c.AddParam("name", pName.Substring(0, 49));
                c.ExecuteNonQuery();
            }
            return foundPlace;
        }

        private List<int> GetPictureSubIds(string pGroup, int pId)
        {
            string q = @"SELECT subid 
                           FROM Pictures 
                           WHERE [group]=@group AND id=@id 
                           ORDER BY subid ASC";
            using (OpenCbsCommand c = new OpenCbsCommand(q, AttachmentsConnection))
            {
                c.AddParam("group", pGroup);
                c.AddParam("id", pId);
                List<int> subIds = new List<int>();
                using (OpenCbsReader r = c.ExecuteReader())
                {
                    while (r.Read())
                    {
                        subIds.Add(r.GetInt(0));
                    }
                }
                return subIds;
            }
        }

        public void SavePicture(byte[] picture, byte[] thumbnail, int person_id, string filename, int subId)
        {
            int pictureId;
            string q = string.Format(@"INSERT INTO [dbo].[Pictures] 
                ([group], [id] ,[subid] ,[picture] ,[thumbnail] ,[name]) 
                VALUES (@group ,@person_id ,@subid ,@picture ,@thumbnail ,@name)
                SELECT CONVERT(int, SCOPE_IDENTITY())");
            using (OpenCbsCommand c = new OpenCbsCommand(q, AttachmentsConnection))
            {
                c.AddParam("group", "SECOND_PICTURE");
                c.AddParam("picture", picture);
                c.AddParam("subid", subId);
                c.AddParam("name", filename);
                c.AddParam("thumbnail", thumbnail);
                c.AddParam("person_id", person_id);
                pictureId = int.Parse(c.ExecuteScalar().ToString());
            }
            q =
                string.Format(
                    @"INSERT INTO PersonsPhotos ([person_id], [picture_id]) 
                     VALUES (@person_id, @picture_id)");
            using (OpenCbsCommand c = new OpenCbsCommand(q, AttachmentsConnection))
            {
                c.AddParam("person_id", person_id);
                c.AddParam("picture_id", pictureId);
                c.ExecuteNonQuery();
            }
        }

        public void UpdatePicture(string group, int personId, int pictureSubId, string fileName, byte[] picture,
            byte[] thumbnail)
        {
            string q = string.Format(@"
                    UPDATE Pictures 
                    SET picture=@picture, thumbnail=@thumbnail, name=@file_name
                    WHERE [Pictures].[group]=@group 
                    AND id=@person_id 
                    AND [Pictures].[subid]=@photo_sub_id");
            using (OpenCbsCommand c = new OpenCbsCommand(q, AttachmentsConnection))
            {
                c.AddParam("group", group);
                c.AddParam("person_id", personId);
                c.AddParam("picture", picture);
                c.AddParam("thumbnail", thumbnail);
                c.AddParam("file_name", fileName);
                c.AddParam("photo_sub_id", pictureSubId);
                c.ExecuteNonQuery();
            }
        }

        public byte[] GetPicture(int personId, int photoSubId, bool thumbnail)
        {
            string q;
            if (thumbnail)
                q = string.Format(@"SELECT TOP(1) [Pictures].[thumbnail]
                        FROM [Pictures]
                        WHERE [Pictures].[group]='PERSON' 
                        AND [Pictures].[id]=@person_id 
                        AND [Pictures].[subid]=@sub_id");
            else
            {
                q =
                string.Format(
                    @"SELECT TOP(1) Pictures.picture
                        FROM Pictures
                        WHERE [Pictures].[group]='PERSON' 
                        AND [Pictures].[id]=@person_id 
                        AND [Pictures].[subid]=@sub_id");
            }

            using (OpenCbsCommand c = new OpenCbsCommand(q, AttachmentsConnection))
            {
                c.AddParam("person_id", personId);
                c.AddParam("@sub_id", photoSubId);
                using (OpenCbsReader r = c.ExecuteReader())
                {
                    while (r.Read())
                    {
                        return r.GetBytes(0);
                    }
                }
                return null;
            }
        }

        public byte[] GetPicture(string clientType, int clientId, bool thumbnail, int photoSubId)
        {
            string q;
            if (thumbnail)
                q = string.Format(
                      @"SELECT TOP(1) [Pictures].[thumbnail]
                        FROM [Pictures]
                        WHERE [Pictures].[group]=@client_type 
                              AND [Pictures].[id]=@client_id 
                              AND [Pictures].[subid]=@photo_sub_id
                              ");
            else
            {
                q =
                string.Format(
                      @"SELECT TOP(1) Pictures.picture
                        FROM Pictures
                        WHERE [Pictures].[group]=@client_type 
                              AND [Pictures].[id]=@client_id
                              AND [Pictures].[subid]=@photo_sub_id 
                        ");
            }

            using (OpenCbsCommand c = new OpenCbsCommand(q, AttachmentsConnection))
            {
                c.AddParam("client_type", clientType);
                c.AddParam("client_id", clientId);
                c.AddParam("photo_sub_id", photoSubId);
                using (OpenCbsReader r = c.ExecuteReader())
                {
                    if (r.Read())
                        return r.GetBytes(0);
                    else
                        return null;
                }
            }
        }

        public bool IsEnabled()
        {
            if (null == AttachmentsConnection) return false;
            return ConnectionState.Open == AttachmentsConnection.State;
        }

        

        
       
    }
}
