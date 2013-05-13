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
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using OpenCBS.CoreDomain;
using OpenCBS.Enums;
using OpenCBS.Manager;

namespace OpenCBS.Services
{
    /// <summary>
    /// Services for storing pictures.
    /// </summary>
    public class PicturesServices : MarshalByRefObject
    {
        public static int PICTURE_MAX_BORDER = 600;
        public static int PICTURE_THUMBNAIL_SIZE = 128;

        private readonly Hashtable _ThumbnailsCache = new Hashtable();
        private readonly PicturesManager _manager;

        
        public PicturesServices(User pUser)
        {
            _manager = new PicturesManager(pUser);
        }

        public PicturesServices(string pDatabase)
        {
            _manager = new PicturesManager(pDatabase);
        }

        /// <summary>
        /// Returns pictures thumbnails for requested pictures.
        /// </summary>
        /// <param name="pGroup">Picture group</param>
        /// <param name="pId">Picture Id</param>
        /// <returns>List of picture informations.</returns>
        public List<Image> GetPicturesThumbnails(string pGroup, int pId)
        {
            List<Image> images = new List<Image>();
            List<PicturesManager.PictureInfo> pictures = _manager.GetPictures(pGroup, pId, true);
            foreach (PicturesManager.PictureInfo picture in pictures)
            {
              images.Add(CheckCache(picture));
            }
            
            return images;
        }

        /// <summary>
        /// Returns requested piture
        /// </summary>
        /// <param name="pGroup">Picture group</param>
        /// <param name="pId">Picture Id</param>
        /// <param name="pSubID">Picture sub Id</param>
        /// <param name="pThumbnail">Do you want the thumbnail?</param>
        /// <returns>Requested picture</returns>
        public Bitmap GetPicture(string pGroup, int pId, int pSubID, bool pThumbnail)
        {
            PicturesManager.PictureInfo picture =_manager.GetPicture(pGroup, pId, pSubID, pThumbnail);
            if (picture != null)
            {
                MemoryStream memoryStream = new MemoryStream(picture.Binary);
                Bitmap image = new Bitmap(memoryStream);
                memoryStream.Close();
                return image;
            }
            
            return null;
        }

        /// <summary>
        /// Returns requested piture
        /// </summary>
        /// <param name="pGroup">Picture group</param>
        /// <param name="pId">Picture Id</param>
        /// <param name="pSubID">Picture sub Id</param>
        /// <returns>Requested picture</returns>
        public Bitmap GetPicture(string pGroup, int pId, int pSubID)
        {
            return GetPicture(pGroup, pId, pSubID, false);
        }

        private Bitmap CheckCache(PicturesManager.PictureInfo pPicture)
        {
            string cachekey = pPicture.Group + "@_" + pPicture.Id + ":" + pPicture.SubId;
            if (_ThumbnailsCache.ContainsKey(cachekey))
            {
                return (Bitmap)_ThumbnailsCache[cachekey];
            }
            
            MemoryStream stream = new MemoryStream(pPicture.Binary);
            Bitmap image = new Bitmap(stream);
            _ThumbnailsCache.Add(cachekey, image);
            return image;
        }


        public int AddPicture(string pGroup, int pId, int photoSubId, string pFileName, string pName)
        {
            Bitmap bitmap = new Bitmap(pFileName);
            Bitmap thumbnail = SmartResize(bitmap, PICTURE_THUMBNAIL_SIZE);
            Bitmap bitmapResize = SmartResize(bitmap, PICTURE_MAX_BORDER);
            MemoryStream bitmapMStream = new MemoryStream();
            MemoryStream thumbnailMStream = new MemoryStream();
            bitmapResize.Save(bitmapMStream, ImageFormat.Png);
            thumbnail.Save(thumbnailMStream, ImageFormat.Png);
            int newPicrtureSubId = _manager.AddPicture(pGroup, pId, photoSubId, bitmapMStream.ToArray(), thumbnailMStream.ToArray(), pName);
            bitmapMStream.Close();
            thumbnailMStream.Close();
            return newPicrtureSubId;
        }

        public int AddPicture(string pGroup, int pId, int photoSubId, string pFileName)
        {
            return AddPicture(pGroup, pId, photoSubId, pFileName, Path.GetFileNameWithoutExtension(pFileName));
        }

        public void DeletePicture(string pGroup, int pId, int pSubId)
        {
            _manager.DeletePicture(pGroup, pId, pSubId);    
        }
        

        public void UpdatePicture(string group, int personID, int photoSubId, string fileName)
        {
            Bitmap picture = new Bitmap(fileName);
            Bitmap thumbnailImage = SmartResize(picture, PICTURE_THUMBNAIL_SIZE);
            Bitmap resizedImage = SmartResize(picture, PICTURE_MAX_BORDER);
            MemoryStream resizedImageStream = new MemoryStream();
            MemoryStream thumbnailImageStream = new MemoryStream();
            thumbnailImage.Save(thumbnailImageStream, ImageFormat.Png);
            resizedImage.Save(resizedImageStream, ImageFormat.Png);
            fileName = Path.GetFileNameWithoutExtension(fileName);
            _manager.UpdatePicture(group, personID, photoSubId, fileName, resizedImageStream.ToArray(), thumbnailImageStream.ToArray());
        }
        
        public Bitmap Resize(Bitmap pBitmap, int pSize)
        {
            return SmartResize(pBitmap, pSize);
        }

        private static Bitmap SmartResize(Bitmap pBitmap, int pMaxBorder)
        {
            float max = (pBitmap.Width > pBitmap.Height ? pBitmap.Width : pBitmap.Height);
            Bitmap resized = new Bitmap(
                Convert.ToInt32(pBitmap.Width / max * pMaxBorder),
                Convert.ToInt32(pBitmap.Height / max * pMaxBorder));
            Graphics g = Graphics.FromImage(resized);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(pBitmap,
                new Rectangle(0, 0, resized.Width, resized.Height),
                new Rectangle(0, 0, pBitmap.Width, pBitmap.Height),
                GraphicsUnit.Pixel);
            g.Dispose();
            return resized;
        }

        public void SavePicture(Bitmap picture, int person_id, string filename)
        {
            Bitmap thumbnailImage = SmartResize(picture, PICTURE_THUMBNAIL_SIZE);
            Bitmap resizedImage = SmartResize(picture, PICTURE_MAX_BORDER);
            MemoryStream resizedImageStream = new MemoryStream();
            MemoryStream thumbnailImageStream = new MemoryStream();
            thumbnailImage.Save(thumbnailImageStream, ImageFormat.Png);
            resizedImage.Save(resizedImageStream, ImageFormat.Png);
            filename = Path.GetFileNameWithoutExtension(filename);
            int subId = 0;
            _manager.SavePicture(resizedImageStream.ToArray(), thumbnailImageStream.ToArray(), person_id, filename, subId);
        }
        
        public Bitmap GetPicture(int personId, int pictureSubId, bool thumbnail)
        {
            MemoryStream pictureMemoryStream;
            Bitmap image;
            byte[] imageBytes;
            
            imageBytes = _manager.GetPicture(personId, pictureSubId, thumbnail);
            
            if (imageBytes!=null)
            {
                pictureMemoryStream = new MemoryStream(imageBytes);
                image=new Bitmap(pictureMemoryStream);
            }
            else
            {
                image = null;
            }
            return image;
        }

        public Bitmap GetPicture(string clientType, int clientId, bool thumbnail, int photoSubId)
        {
            MemoryStream pictureMemoryStream;
            Bitmap image;
            byte[] imageBytes;
           
            imageBytes = _manager.GetPicture(clientType, clientId, thumbnail, photoSubId);
             
            if (imageBytes != null)
            {
                pictureMemoryStream = new MemoryStream(imageBytes);
                image = new Bitmap(pictureMemoryStream);
            }
            else
            {
                image = null;
            }
            return image;
        }

        public bool IsEnabled()
        {
            return _manager.IsEnabled();
        }
    }
}
