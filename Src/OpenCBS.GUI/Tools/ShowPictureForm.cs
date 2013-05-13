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
using System.Drawing;
using System.Windows.Forms;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.GUI.Clients;
using OpenCBS.GUI.UserControl;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.Services;

namespace OpenCBS.GUI.Tools
{
    public partial class ShowPictureForm : Form
    {
        private Person person;
        private int photoSubId;
        private Bitmap image;
        private PersonUserControl personUserControl;
        private NonSolidaryGroupForm villageForm;
        private Village village;
        private GroupUserControl groupUserControl;
        private Group group;
        private Corporate corporate;
        private CorporateUserControl corporateUserControl;
        private string CaptionText { get; set; }
       
        public ShowPictureForm(Bitmap pPicture)
        {
            InitializeComponent();
            UserPicture.Image = pPicture;
            UserPicture.Height = pPicture.Height;
            UserPicture.Width = pPicture.Width;                        
        }

        public ShowPictureForm(Person person, int photoSubId, PersonUserControl personUserControl)
        {
            InitializeComponent();
            CaptionText = MultiLanguageStrings.GetString(Ressource.ClientForm, "Person.Text")+": "+ person.Name;
            this.person = person;
            this.photoSubId = photoSubId;
            this.personUserControl = personUserControl;
            addPhotoButton.Text = MultiLanguageStrings.GetString(Ressource.ShowPictureForm, "captionAdd.Text");
            changePhotoButton.Text = MultiLanguageStrings.GetString(Ressource.ShowPictureForm, "captionChange.Text");
            ShowPicture();
            InitializeButtons();
        }

        private void InitializeButtons()
        {
            if (UserPicture.Image != null)
            {
                addPhotoButton.Enabled = false;
                deletePhotoButton.Enabled = true;
                changePhotoButton.Enabled = true;
            }
            else
            {
                addPhotoButton.Enabled = true;
                deletePhotoButton.Enabled = false;
                changePhotoButton.Enabled = false;
            }
        }

        public ShowPictureForm(Village village, NonSolidaryGroupForm villageForm, int photoSubId)
        {
            InitializeComponent();
            CaptionText = MultiLanguageStrings.GetString(Ressource.ClientForm, "NonSolidaryGroup") + ": " + village.Name;
            this.villageForm = villageForm;
            this.village = village;
            this.photoSubId = photoSubId;
            addPhotoButton.Text = MultiLanguageStrings.GetString(Ressource.ShowPictureForm, "captionAdd.Text");
            changePhotoButton.Text = MultiLanguageStrings.GetString(Ressource.ShowPictureForm, "captionChange.Text");
            ShowPicture();
            InitializeButtons();
        }

        public ShowPictureForm(Group group, GroupUserControl groupUserControl, int photoSubId)
        {
            InitializeComponent();
            CaptionText = MultiLanguageStrings.GetString(Ressource.ClientForm, "Group.Text") + ": " + group.Name;
            this.groupUserControl = groupUserControl;
            this.group = group;
            this.photoSubId = photoSubId;
            addPhotoButton.Text = MultiLanguageStrings.GetString(Ressource.ShowPictureForm, "captionAdd.Text");
            changePhotoButton.Text = MultiLanguageStrings.GetString(Ressource.ShowPictureForm, "captionChange.Text");
            ShowPicture();
            InitializeButtons();
        }

        public ShowPictureForm(Corporate corporate, CorporateUserControl corporateUserControl, int photoSubId)
        {
            InitializeComponent();
            CaptionText = MultiLanguageStrings.GetString(Ressource.ClientForm, "Corporate.Text") + ": " + corporate.Name;
            this.corporateUserControl = corporateUserControl;
            this.corporate = corporate;
            this.photoSubId = photoSubId;
            addPhotoButton.Text = MultiLanguageStrings.GetString(Ressource.ShowPictureForm, "captionAdd.Text");
            changePhotoButton.Text = MultiLanguageStrings.GetString(Ressource.ShowPictureForm, "captionChange.Text");
            ShowPicture();
            InitializeButtons();
        }

        private void ShowPicture()
        {
           if (personUserControl!=null)
           {
               if (person.Id == 0)
               {
                   UserPicture.Image = null;
               }
               if (photoSubId==0 && !string.IsNullOrEmpty(person.ImagePath))
               {
                   Bitmap tempImage = new Bitmap(person.ImagePath);
                   image = ServicesProvider.GetInstance().GetPicturesServices().Resize(tempImage, 600);
               }
               else if (photoSubId==1 && !string.IsNullOrEmpty(person.Image2Path))
               {
                   Bitmap tempImage = new Bitmap(person.Image2Path);
                   image = ServicesProvider.GetInstance().GetPicturesServices().Resize(tempImage, 600);
               }
               else
               {
                   image = ServicesProvider.GetInstance().GetPicturesServices().GetPicture("PERSON", person.Id, false, photoSubId);
               }
           }
           else if (villageForm!=null)
           {
               if (village.Id == 0)
               {
                   UserPicture.Image = null;
               }

               if (photoSubId==0)
               {
                   if (!string.IsNullOrEmpty(village.ImagePath))
                   {
                       Bitmap tempImage = new Bitmap(village.ImagePath);
                       image = ServicesProvider.GetInstance().GetPicturesServices().Resize(tempImage, 600);
                   }
                   else
                   {
                       image = ServicesProvider.GetInstance().GetPicturesServices().GetPicture("VILLAGE", village.Id, false, photoSubId);
                   }
               }
               else if (photoSubId==1)
               {
                   if (!string.IsNullOrEmpty(village.Image2Path))
                   {
                       Bitmap tempImage = new Bitmap(village.Image2Path);
                       image = ServicesProvider.GetInstance().GetPicturesServices().Resize(tempImage, 600);
                   }
                   else
                   {
                       image = ServicesProvider.GetInstance().GetPicturesServices().GetPicture("VILLAGE", village.Id, false, photoSubId);
                   }
               }
               
           }
           else if (groupUserControl!=null)
           {
               if (photoSubId==0)
               {
                   if (!string.IsNullOrEmpty(group.ImagePath))
                   {
                       Bitmap tempImage = new Bitmap(group.ImagePath);
                       image = ServicesProvider.GetInstance().GetPicturesServices().Resize(tempImage, 600);
                   }
                   else
                   {
                       image = ServicesProvider.GetInstance().GetPicturesServices().GetPicture("GROUP", group.Id, false,
                                                                                               photoSubId);
                   }
               }
               else if (photoSubId==1)
               {
                   if (!string.IsNullOrEmpty(group.Image2Path))
                   {
                       Bitmap tempImage = new Bitmap(group.Image2Path);
                       image = ServicesProvider.GetInstance().GetPicturesServices().Resize(tempImage, 600);
                   }
                   else
                   {
                       image = ServicesProvider.GetInstance().GetPicturesServices().GetPicture("GROUP", group.Id, false,
                                                                                               photoSubId);
                   }
               }
           }

           else if (corporateUserControl != null)
           {
               if (photoSubId == 0)
               {
                   if (!string.IsNullOrEmpty(corporate.ImagePath))
                   {
                       Bitmap tempImage=new Bitmap(corporate.ImagePath);
                       image = ServicesProvider.GetInstance().GetPicturesServices().Resize(tempImage, 600);
                   }
                   else
                   {
                       image = ServicesProvider.GetInstance().GetPicturesServices().GetPicture("CORPORATE", corporate.Id,
                                                                                               false, photoSubId);
                   }
               }
               else if (photoSubId == 1)
               {
                   if (!string.IsNullOrEmpty(corporate.Image2Path))
                   {
                       Bitmap tempImage = new Bitmap(corporate.Image2Path);
                       image = ServicesProvider.GetInstance().GetPicturesServices().Resize(tempImage, 600);
                   }
                   else
                   {
                       image = ServicesProvider.GetInstance().GetPicturesServices().GetPicture("CORPORATE", corporate.Id,
                                                                                               false, photoSubId);
                   }
               }
           }
           if (image != null)
           {
               UserPicture.Image = image;
               UserPicture.Height = image.Height;
               UserPicture.Width = image.Width;
               deletePhotoButton.Enabled = true;
           }
        }
           
        private void pictureBox_SizeChanged(object sender, EventArgs e)
        {
            gpPicture.Width = UserPicture.Width + 25;
            gpPicture.Height = UserPicture.Height + 25;
        }

        private void AddPhotoButtonClick(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                DefaultExt = "jpg",
                Filter = @"JPEG (*.jpg)|*.jpg|BMP (*.bmp)|*.bmp|All files (*.*)|*.*",
                FilterIndex = 1
            };
            string fileName = dlg.ShowDialog() == DialogResult.OK ? dlg.FileName : null;
            if (string.IsNullOrEmpty(fileName)) return;
            if (personUserControl!=null)
            {
                if (photoSubId == 0)
                {
                    person.IsImageAdded = true;
                }
                else if (photoSubId == 1)
                {
                    person.IsImage2Added = true;
                }
             }

            else if (villageForm!=null)
            {
                if (photoSubId==0)
                {
                    village.IsImageAdded = true;
                }
                else if (photoSubId==1)
                {
                    village.IsImage2Added = true;
                }
                
            }
            else if (groupUserControl!=null)
            {
                if (photoSubId==0)
                {
                    group.IsImageAdded = true;
                }
                else if (photoSubId==1)
                {
                    group.IsImage2Added = true;
                }
            }
            else if (corporateUserControl!=null)
            {
                if (photoSubId==0)
                {
                   corporate.IsImageAdded = true;
                }
                else if (photoSubId==1)
                {
                    corporate.IsImage2Added = true;
                }
            }

            ProcessImage(fileName);
            changePhotoButton.Enabled = true;
            deletePhotoButton.Enabled = true;
            addPhotoButton.Enabled = false;
        }

        private void ProcessImage(string fileName)
        {
            if (personUserControl!=null)
            {
                if (photoSubId == 0)
                {
                    person.ImagePath = fileName;
                    Bitmap bitmap = new Bitmap(fileName);
                    Bitmap resized = ServicesProvider.GetInstance().GetPicturesServices().Resize(bitmap, 600);
                    UserPicture.Height = resized.Height;
                    UserPicture.Width = resized.Width;
                    UserPicture.Image = resized;
                    Bitmap thumbnail = ServicesProvider.GetInstance().GetPicturesServices().Resize(bitmap, 128);
                    personUserControl.pictureBox.Image = thumbnail;
                    personUserControl.pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    UserPicture.Image = resized;
                }
                else if (photoSubId == 1)
                {
                    person.Image2Path = fileName;
                    Bitmap bitmap = new Bitmap(fileName);
                    Bitmap resized = ServicesProvider.GetInstance().GetPicturesServices().Resize(bitmap, 600);
                    UserPicture.Height = resized.Height;
                    UserPicture.Width = resized.Width;
                    UserPicture.Image = resized;
                    Bitmap thumbnail = ServicesProvider.GetInstance().GetPicturesServices().Resize(bitmap, 128);
                    personUserControl.pictureBox2.Image = thumbnail;
                    personUserControl.pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                    UserPicture.Image = resized;
                }
            }

            else if (villageForm!=null)
            {
               if (photoSubId==0)
               {
                   village.ImagePath = fileName;
                   Bitmap bitmap = new Bitmap(fileName);
                   Bitmap resized = ServicesProvider.GetInstance().GetPicturesServices().Resize(bitmap, 600);
                   UserPicture.Height = resized.Height;
                   UserPicture.Width = resized.Width;
                   UserPicture.Image = resized;
                   Bitmap thumbnail = ServicesProvider.GetInstance().GetPicturesServices().Resize(bitmap, 128);
                   villageForm.pictureBox1.Image = thumbnail;
                   villageForm.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                   UserPicture.Image = resized;
               }
               else if (photoSubId==1)
               {
                   village.Image2Path = fileName;
                   Bitmap bitmap = new Bitmap(fileName);
                   Bitmap resized = ServicesProvider.GetInstance().GetPicturesServices().Resize(bitmap, 600);
                   UserPicture.Height = resized.Height;
                   UserPicture.Width = resized.Width;
                   UserPicture.Image = resized;
                   Bitmap thumbnail = ServicesProvider.GetInstance().GetPicturesServices().Resize(bitmap, 128);
                   villageForm.pictureBox2.Image = thumbnail;
                   villageForm.pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                   UserPicture.Image = resized;
               }
            }

            else if (groupUserControl!=null)
            {
                if (photoSubId==0)
                {
                    group.ImagePath = fileName;
                    Bitmap bitmap = new Bitmap(fileName);
                    Bitmap resized = ServicesProvider.GetInstance().GetPicturesServices().Resize(bitmap, 600);
                    UserPicture.Height = resized.Height;
                    UserPicture.Width = resized.Width;
                    UserPicture.Image = resized;
                    Bitmap thumbnail = ServicesProvider.GetInstance().GetPicturesServices().Resize(bitmap, 128);
                    groupUserControl.pictureBox1.Image = thumbnail;
                    groupUserControl.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    UserPicture.Image = resized;
                }
                else if (photoSubId==1)
                {
                    group.Image2Path = fileName;
                    Bitmap bitmap=new Bitmap(fileName);
                    Bitmap resized = ServicesProvider.GetInstance().GetPicturesServices().Resize(bitmap, 600);
                    UserPicture.Height = resized.Height;
                    UserPicture.Width = resized.Width;
                    UserPicture.Image = resized;
                    Bitmap thumbnail = ServicesProvider.GetInstance().GetPicturesServices().Resize(bitmap, 128);
                    groupUserControl.pictureBox2.Image = thumbnail;
                    groupUserControl.pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                    UserPicture.Image = resized;
                }
            }
            else if (corporateUserControl!=null)
            {
                if (photoSubId==0)
                {
                    corporate.ImagePath = fileName;
                    Bitmap bitmap = new Bitmap(fileName);
                    Bitmap resized = ServicesProvider.GetInstance().GetPicturesServices().Resize(bitmap, 600);
                    UserPicture.Height = resized.Height;
                    UserPicture.Width = resized.Width;
                    UserPicture.Image = resized;
                    Bitmap thumbnail = ServicesProvider.GetInstance().GetPicturesServices().Resize(bitmap, 128);
                    corporateUserControl.pictureBox1.Image = thumbnail;
                    corporateUserControl.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    UserPicture.Image = resized;
                }
                else if (photoSubId==1)
                {
                    corporate.Image2Path = fileName;
                    Bitmap bitmap = new Bitmap(fileName);
                    Bitmap resized = ServicesProvider.GetInstance().GetPicturesServices().Resize(bitmap, 600);
                    UserPicture.Height = resized.Height;
                    UserPicture.Width = resized.Width;
                    UserPicture.Image = resized;
                    Bitmap thumbnail = ServicesProvider.GetInstance().GetPicturesServices().Resize(bitmap, 128);
                    corporateUserControl.pictureBox2.Image = thumbnail;
                    corporateUserControl.pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                    UserPicture.Image = resized;
                }
            }
        }

        private void deletePhotoButton_Click(object sender, EventArgs e)
        {
           if (personUserControl!=null)
           {
               if (photoSubId == 0)
               {
                   person.IsImageDeleted = true;
                   person.IsImageAdded = false;
                   person.IsImageUpdated = false;
                   person.ImagePath = "";
                   personUserControl.pictureBox.Image = null;
               }
               else if (photoSubId == 1)
               {
                   person.IsImage2Deleted = true;
                   person.IsImage2Added = false;
                   person.IsImage2Updated = false;
                   person.Image2Path = "";
                   personUserControl.pictureBox2.Image = null;
               }
           }
           else if (villageForm!=null)
           {
               if (photoSubId==0)
               {
                   village.IsImageDeleted = true;
                   village.IsImageAdded = false;
                   village.ImagePath = "";
                   villageForm.pictureBox1.Image = null;
               }
               else if (photoSubId==1)
               {
                   village.IsImage2Deleted = true;
                   village.IsImage2Added = false;
                   village.Image2Path = "";
                   villageForm.pictureBox2.Image = null;
               }
           }
           else if (groupUserControl!=null)
           {
               if (photoSubId==0)
               {
                   group.IsImageDeleted = true;
                   group.IsImageAdded = false;
                   group.ImagePath = "";
                   groupUserControl.pictureBox1.Image = null;
               }
               else if (photoSubId==1)
               {
                   group.IsImage2Deleted = true;
                   group.IsImage2Added = false;
                   group.Image2Path = "";
                   groupUserControl.pictureBox2.Image = null;
               }
           }
           else if (corporateUserControl!=null)
           {
               if (photoSubId == 0)
               {
                   corporate.IsImageDeleted = true;
                   corporate.IsImageAdded = false;
                   corporate.ImagePath = "";
                   corporateUserControl.pictureBox1.Image = null;
               }
               else if (photoSubId == 1)
               {
                   corporate.IsImage2Deleted = true;
                   corporate.IsImage2Added = false;
                   corporate.Image2Path = "";
                   corporateUserControl.pictureBox2.Image = null;
               }
           }
           UserPicture.Image = null;
           changePhotoButton.Enabled = false;
           addPhotoButton.Enabled = true;
        }

        private void ShowPictureForm_Load(object sender, EventArgs e)
        {
            if (person != null)
            {
                labelPersonName.Text = CaptionText;
            }
            else if (village != null)
            {
                labelPersonName.Text = CaptionText;
            }
            else if (group != null)
            {
                labelPersonName.Text = CaptionText;
            }
            else if (corporate != null)
                labelPersonName.Text =  CaptionText;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void changePhotoButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                DefaultExt = "jpg",
                Filter = @"JPEG (*.jpg)|*.jpg|BMP (*.bmp)|*.bmp|All files (*.*)|*.*",
                FilterIndex = 1
            };
            
            string fileName = dlg.ShowDialog() == DialogResult.OK ? dlg.FileName : null;
            if (string.IsNullOrEmpty(fileName)) return;
            if (personUserControl!=null)
            {
                if (photoSubId == 0)
                {
                    person.ImagePath = fileName;
                    person.IsImageUpdated = true;
                    person.IsImageDeleted = false;
                    ProcessImage(fileName);
                }
                if (photoSubId == 1)
                {
                    person.Image2Path = fileName;
                    person.IsImage2Updated = true;
                    person.IsImageDeleted = false;
                    ProcessImage(fileName);
                }
            }
            else if (villageForm!=null)
            {
               if (photoSubId==0)
               {
                   village.ImagePath = fileName;
                   village.IsImageUpdated = true;
                   village.IsImageDeleted = false;
                   ProcessImage(fileName);
               }
               else if (photoSubId==1)
               {
                   village.Image2Path = fileName;
                   village.IsImage2Updated = true;
                   village.IsImage2Deleted = false;
                   ProcessImage(fileName);
               }
                
            }
            else if (groupUserControl!=null)
            {
                if (photoSubId==0)
                {
                    group.ImagePath = fileName;
                    group.IsImageDeleted = false;
                    group.IsImageUpdated = true;
                    ProcessImage(fileName);
                }
                else if (photoSubId==1)
                {
                    group.Image2Path = fileName;
                    group.IsImage2Deleted = false;
                    group.IsImage2Updated = true;
                    ProcessImage(fileName);
                }
            }
            else if (corporateUserControl!=null)
            {
                if (photoSubId == 0)
                {
                    corporate.ImagePath = fileName;
                    corporate.IsImageDeleted = false;
                    corporate.IsImageUpdated = true;
                    ProcessImage(fileName);
                }
                else if (photoSubId == 1)
                {
                    corporate.Image2Path = fileName;
                    corporate.IsImage2Deleted = false;
                    corporate.IsImage2Updated = true;
                    ProcessImage(fileName);
                }
            }
        }
    }
}
