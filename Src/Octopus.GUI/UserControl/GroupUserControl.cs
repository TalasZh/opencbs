//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright ?2006,2007 OCTO Technology & OXUS Development Network
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU Lesser General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
//
// Licence : http://www.octopusnetwork.org/OverviewLicence.aspx
//
// Website : http://www.octopusnetwork.org
// Business contact: business(at)octopusnetwork.org
// Technical contact email : tech(at)octopusnetwork.org 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Octopus.CoreDomain;
using Octopus.CoreDomain.Clients;
using Octopus.CoreDomain.Contracts.Loans;
using Octopus.Extensions;
using Octopus.ExceptionsHandler;
using Octopus.GUI.Clients;
using Octopus.GUI.Tools;
using Octopus.MultiLanguageRessources;
using Octopus.Services;
using Octopus.Services.Events;
using Octopus.Enums;
using Octopus.CoreDomain.Contracts.Savings;
using Octopus.Shared.Settings;
using Octopus.Shared;

namespace Octopus.GUI.UserControl
{
    public delegate void MembersChangedEventHandler(object sender, EventArgs args);

    public class GroupUserControl : ClientControl, IDisposable
    {
        private List<Member> historyPersons;
        private IContainer components;
        private Group group;
        private GroupBox groupBoxCivilStatus;
        private Label labelHelpRequiredFields;
        private TabControl tabControlGroupInfo;
        private SweetButton buttonAddMembres;
        private ListView listViewOtherMembres;
        private ColumnHeader columnHeaderName;
        private ColumnHeader columnHeaderDateOfBirth;
        private ColumnHeader columnHeaderPassport;
        private ColumnHeader columnHeaderSex;
        private ColumnHeader columnHeaderHeadOfHousehold;
        private ColumnHeader columnHeaderDependents;
        public event EventHandler ButtonCancelClick;
        public event EventHandler ButtonSaveClick;
        public event EventHandler ButtonUpdateShareAmountClick;
        public event EventHandler ButtonBadClientClick;
        public event EventHandler ListViewContractDoubleClick;
        public event EventHandler ListViewHistoryMemberDoubleClick;
        private TabPage tabPageMembers;
        private TabPage tabPageBusinessAddress;
        private GroupBox groupBoxButtons;
        private GroupBox groupBox3;
        private SweetButton buttonNext;
        private SweetButton buttonPreview;
        private SweetButton buttonSelectAMember;
        private SweetButton buttonViewMember;
        private SweetButton buttonSaveAsLeader;
        private ImageList imageListTab;
        private GroupBox groupBoxButtonBottom;
        private GroupBox groupBoxFirstAddress;
        private GroupBox groupBoxSecondaryAddress;
        private bool groupSaved;
        private AddressUserControl addressUserControlFirst;
        private TabPage tabPageHistory;
        private ListView listViewHistoryMembers;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private AddressUserControl addressUserControlSecondaryAddress;        
        private bool _cancelClicked;
        private OCurrency loanAmount;
        private SweetButton buttonDeleteMembers;
        private ColumnHeader columnHeader8;
        private TableLayoutPanel tableLayoutPanel1;
        private SplitContainer splitContainer3;
        private TableLayoutPanel tableLayoutPanel4;
        private ApplicationSettings _generalParameters;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader9;
        private ColumnHeader columnHeader10;
        private TabPage tabPageProjects;
        private SplitContainer splitContainer1;
        private ListView listViewProjects;
        private ColumnHeader columnHeader11;
        private ColumnHeader columnHeader12;
        private ColumnHeader columnHeaderCode;
        private ColumnHeader columnHeaderNbOfContracts;
        private GroupBox groupBoxProjects;
        private SweetButton buttonViewProject;
        private SweetButton buttonAddProject;
        private TabPage tabPageSaving;
        private ColumnHeader columnHeaderLoanCycle;
        private Form _mdiParent;
        private SavingsListUserControl savingsListUserControl1;
        private CustomizableFieldsControl _customizableFieldsControl;
        public MembersChangedEventHandler MembersChanged;
        private LinkLabel linkLabelChangePhoto;
        public PictureBox pictureBox1;
        private LinkLabel linkLabelChangePhoto2;
        public PictureBox pictureBox2;
        private TableLayoutPanel tlpGroupControls;
        private Label labelName;
        private TextBox textBoxName;
        private ComboBox cmbWeekDay;
        private CheckBox cbMeetingDay;
        private Label labelDateOfEstablishment;
        private DateTimePicker dateTimePickerDateOfEstablishment;
        private Label lblWeekDay;
        private TextBox textBoxGroupLoanCycle;
        private Label labelGroupCycle;
        private ComboBox cbBranch;
        private Label label2;
        private TabPage tabPageCustomizableFields;
        private SweetButton buttonSave;
        private SweetButton buttonCancel;
        private PrintButton btnPrint;
        public event EventHandler AddSelectedSaving;
        public event EventHandler ViewSelectedSaving;
        
        
        private readonly List<ISolidarityGroup> _extensionGroups = new List<ISolidarityGroup>();

        #region Code génér?par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupUserControl));
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.listViewOtherMembres = new System.Windows.Forms.ListView();
            this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderPassport = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderDateOfBirth = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderSex = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderHeadOfHousehold = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderLoanCycle = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderDependents = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonDeleteMembers = new Octopus.GUI.UserControl.SweetButton();
            this.buttonSaveAsLeader = new Octopus.GUI.UserControl.SweetButton();
            this.buttonViewMember = new Octopus.GUI.UserControl.SweetButton();
            this.buttonAddMembres = new Octopus.GUI.UserControl.SweetButton();
            this.buttonSelectAMember = new Octopus.GUI.UserControl.SweetButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listViewProjects = new System.Windows.Forms.ListView();
            this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderCode = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderNbOfContracts = new System.Windows.Forms.ColumnHeader();
            this.groupBoxProjects = new System.Windows.Forms.GroupBox();
            this.buttonViewProject = new Octopus.GUI.UserControl.SweetButton();
            this.buttonAddProject = new Octopus.GUI.UserControl.SweetButton();
            this.tabControlGroupInfo = new System.Windows.Forms.TabControl();
            this.tabPageBusinessAddress = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxFirstAddress = new System.Windows.Forms.GroupBox();
            this.groupBoxSecondaryAddress = new System.Windows.Forms.GroupBox();
            this.tabPageMembers = new System.Windows.Forms.TabPage();
            this.tabPageHistory = new System.Windows.Forms.TabPage();
            this.listViewHistoryMembers = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.tabPageProjects = new System.Windows.Forms.TabPage();
            this.tabPageSaving = new System.Windows.Forms.TabPage();
            this.savingsListUserControl1 = new Octopus.GUI.UserControl.SavingsListUserControl();
            this.tabPageCustomizableFields = new System.Windows.Forms.TabPage();
            this.imageListTab = new System.Windows.Forms.ImageList(this.components);
            this.cbBranch = new System.Windows.Forms.ComboBox();
            this.groupBoxButtonBottom = new System.Windows.Forms.GroupBox();
            this.buttonNext = new Octopus.GUI.UserControl.SweetButton();
            this.buttonPreview = new Octopus.GUI.UserControl.SweetButton();
            this.labelHelpRequiredFields = new System.Windows.Forms.Label();
            this.groupBoxButtons = new System.Windows.Forms.GroupBox();
            this.btnPrint = new Octopus.GUI.UserControl.PrintButton();
            this.buttonCancel = new Octopus.GUI.UserControl.SweetButton();
            this.buttonSave = new Octopus.GUI.UserControl.SweetButton();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxCivilStatus = new System.Windows.Forms.GroupBox();
            this.tlpGroupControls = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxGroupLoanCycle = new System.Windows.Forms.TextBox();
            this.labelGroupCycle = new System.Windows.Forms.Label();
            this.lblWeekDay = new System.Windows.Forms.Label();
            this.cmbWeekDay = new System.Windows.Forms.ComboBox();
            this.dateTimePickerDateOfEstablishment = new System.Windows.Forms.DateTimePicker();
            this.labelDateOfEstablishment = new System.Windows.Forms.Label();
            this.cbMeetingDay = new System.Windows.Forms.CheckBox();
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.linkLabelChangePhoto2 = new System.Windows.Forms.LinkLabel();
            this.linkLabelChangePhoto = new System.Windows.Forms.LinkLabel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBoxProjects.SuspendLayout();
            this.tabControlGroupInfo.SuspendLayout();
            this.tabPageBusinessAddress.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabPageMembers.SuspendLayout();
            this.tabPageHistory.SuspendLayout();
            this.tabPageProjects.SuspendLayout();
            this.tabPageSaving.SuspendLayout();
            this.groupBoxButtonBottom.SuspendLayout();
            this.groupBoxButtons.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.groupBoxCivilStatus.SuspendLayout();
            this.tlpGroupControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer3
            // 
            resources.ApplyResources(this.splitContainer3, "splitContainer3");
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.listViewOtherMembres);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.groupBox3);
            // 
            // listViewOtherMembres
            // 
            this.listViewOtherMembres.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderPassport,
            this.columnHeaderDateOfBirth,
            this.columnHeaderSex,
            this.columnHeaderHeadOfHousehold,
            this.columnHeaderLoanCycle,
            this.columnHeaderDependents,
            this.columnHeader7,
            this.columnHeader8});
            resources.ApplyResources(this.listViewOtherMembres, "listViewOtherMembres");
            this.listViewOtherMembres.FullRowSelect = true;
            this.listViewOtherMembres.GridLines = true;
            this.listViewOtherMembres.MultiSelect = false;
            this.listViewOtherMembres.Name = "listViewOtherMembres";
            this.listViewOtherMembres.UseCompatibleStateImageBehavior = false;
            this.listViewOtherMembres.View = System.Windows.Forms.View.Details;
            this.listViewOtherMembres.DoubleClick += new System.EventHandler(this.listViewOtherMembres_DoubleClick);
            // 
            // columnHeaderName
            // 
            resources.ApplyResources(this.columnHeaderName, "columnHeaderName");
            // 
            // columnHeaderPassport
            // 
            resources.ApplyResources(this.columnHeaderPassport, "columnHeaderPassport");
            // 
            // columnHeaderDateOfBirth
            // 
            resources.ApplyResources(this.columnHeaderDateOfBirth, "columnHeaderDateOfBirth");
            // 
            // columnHeaderSex
            // 
            resources.ApplyResources(this.columnHeaderSex, "columnHeaderSex");
            // 
            // columnHeaderHeadOfHousehold
            // 
            resources.ApplyResources(this.columnHeaderHeadOfHousehold, "columnHeaderHeadOfHousehold");
            // 
            // columnHeaderLoanCycle
            // 
            resources.ApplyResources(this.columnHeaderLoanCycle, "columnHeaderLoanCycle");
            // 
            // columnHeaderDependents
            // 
            resources.ApplyResources(this.columnHeaderDependents, "columnHeaderDependents");
            // 
            // columnHeader7
            // 
            resources.ApplyResources(this.columnHeader7, "columnHeader7");
            // 
            // columnHeader8
            // 
            resources.ApplyResources(this.columnHeader8, "columnHeader8");
            // 
            // groupBox3
            // 
            this.groupBox3.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_gris;
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.buttonDeleteMembers);
            this.groupBox3.Controls.Add(this.buttonSaveAsLeader);
            this.groupBox3.Controls.Add(this.buttonViewMember);
            this.groupBox3.Controls.Add(this.buttonAddMembres);
            this.groupBox3.Controls.Add(this.buttonSelectAMember);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // buttonDeleteMembers
            // 
            resources.ApplyResources(this.buttonDeleteMembers, "buttonDeleteMembers");
            this.buttonDeleteMembers.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonDeleteMembers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonDeleteMembers.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Delete;
            this.buttonDeleteMembers.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_delete;
            this.buttonDeleteMembers.Menu = null;
            this.buttonDeleteMembers.Name = "buttonDeleteMembers";
            this.buttonDeleteMembers.UseVisualStyleBackColor = false;
            this.buttonDeleteMembers.Click += new System.EventHandler(this.buttonDeleteMembers_Click);
            // 
            // buttonSaveAsLeader
            // 
            resources.ApplyResources(this.buttonSaveAsLeader, "buttonSaveAsLeader");
            this.buttonSaveAsLeader.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonSaveAsLeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonSaveAsLeader.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Validity;
            this.buttonSaveAsLeader.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_validity;
            this.buttonSaveAsLeader.Menu = null;
            this.buttonSaveAsLeader.Name = "buttonSaveAsLeader";
            this.buttonSaveAsLeader.UseVisualStyleBackColor = false;
            this.buttonSaveAsLeader.Click += new System.EventHandler(this.buttonSaveAsLeader_Click);
            // 
            // buttonViewMember
            // 
            resources.ApplyResources(this.buttonViewMember, "buttonViewMember");
            this.buttonViewMember.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonViewMember.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonViewMember.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.View;
            this.buttonViewMember.Image = global::Octopus.GUI.Properties.Resources.theme1_1_view;
            this.buttonViewMember.Menu = null;
            this.buttonViewMember.Name = "buttonViewMember";
            this.buttonViewMember.UseVisualStyleBackColor = false;
            this.buttonViewMember.Click += new System.EventHandler(this.buttonViewMember_Click);
            // 
            // buttonAddMembres
            // 
            resources.ApplyResources(this.buttonAddMembres, "buttonAddMembres");
            this.buttonAddMembres.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonAddMembres.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonAddMembres.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.New;
            this.buttonAddMembres.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_new;
            this.buttonAddMembres.Menu = null;
            this.buttonAddMembres.Name = "buttonAddMembres";
            this.buttonAddMembres.UseVisualStyleBackColor = false;
            this.buttonAddMembres.Click += new System.EventHandler(this.buttonAddMembres_Click);
            // 
            // buttonSelectAMember
            // 
            resources.ApplyResources(this.buttonSelectAMember, "buttonSelectAMember");
            this.buttonSelectAMember.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonSelectAMember.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonSelectAMember.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Search;
            this.buttonSelectAMember.Image = global::Octopus.GUI.Properties.Resources.theme1_1_search;
            this.buttonSelectAMember.Menu = null;
            this.buttonSelectAMember.Name = "buttonSelectAMember";
            this.buttonSelectAMember.UseVisualStyleBackColor = false;
            this.buttonSelectAMember.Click += new System.EventHandler(this.buttonSelectAMember_Click);
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listViewProjects);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBoxProjects);
            // 
            // listViewProjects
            // 
            this.listViewProjects.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeaderCode,
            this.columnHeaderNbOfContracts});
            resources.ApplyResources(this.listViewProjects, "listViewProjects");
            this.listViewProjects.FullRowSelect = true;
            this.listViewProjects.GridLines = true;
            this.listViewProjects.Name = "listViewProjects";
            this.listViewProjects.UseCompatibleStateImageBehavior = false;
            this.listViewProjects.View = System.Windows.Forms.View.Details;
            this.listViewProjects.DoubleClick += new System.EventHandler(this.listViewProjects_DoubleClick);
            // 
            // columnHeader11
            // 
            resources.ApplyResources(this.columnHeader11, "columnHeader11");
            // 
            // columnHeader12
            // 
            resources.ApplyResources(this.columnHeader12, "columnHeader12");
            // 
            // columnHeaderCode
            // 
            resources.ApplyResources(this.columnHeaderCode, "columnHeaderCode");
            // 
            // columnHeaderNbOfContracts
            // 
            resources.ApplyResources(this.columnHeaderNbOfContracts, "columnHeaderNbOfContracts");
            // 
            // groupBoxProjects
            // 
            this.groupBoxProjects.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_gris_180;
            resources.ApplyResources(this.groupBoxProjects, "groupBoxProjects");
            this.groupBoxProjects.Controls.Add(this.buttonViewProject);
            this.groupBoxProjects.Controls.Add(this.buttonAddProject);
            this.groupBoxProjects.Name = "groupBoxProjects";
            this.groupBoxProjects.TabStop = false;
            // 
            // buttonViewProject
            // 
            resources.ApplyResources(this.buttonViewProject, "buttonViewProject");
            this.buttonViewProject.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonViewProject.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonViewProject.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.View;
            this.buttonViewProject.Image = global::Octopus.GUI.Properties.Resources.theme1_1_view;
            this.buttonViewProject.Menu = null;
            this.buttonViewProject.Name = "buttonViewProject";
            this.buttonViewProject.UseVisualStyleBackColor = false;
            this.buttonViewProject.Click += new System.EventHandler(this.buttonViewProject_Click);
            // 
            // buttonAddProject
            // 
            resources.ApplyResources(this.buttonAddProject, "buttonAddProject");
            this.buttonAddProject.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonAddProject.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonAddProject.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.New;
            this.buttonAddProject.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_new;
            this.buttonAddProject.Menu = null;
            this.buttonAddProject.Name = "buttonAddProject";
            this.buttonAddProject.UseVisualStyleBackColor = false;
            this.buttonAddProject.Click += new System.EventHandler(this.buttonAddProject_Click);
            // 
            // tabControlGroupInfo
            // 
            this.tabControlGroupInfo.Controls.Add(this.tabPageBusinessAddress);
            this.tabControlGroupInfo.Controls.Add(this.tabPageMembers);
            this.tabControlGroupInfo.Controls.Add(this.tabPageHistory);
            this.tabControlGroupInfo.Controls.Add(this.tabPageProjects);
            this.tabControlGroupInfo.Controls.Add(this.tabPageSaving);
            this.tabControlGroupInfo.Controls.Add(this.tabPageCustomizableFields);
            resources.ApplyResources(this.tabControlGroupInfo, "tabControlGroupInfo");
            this.tabControlGroupInfo.ImageList = this.imageListTab;
            this.tabControlGroupInfo.Name = "tabControlGroupInfo";
            this.tabControlGroupInfo.SelectedIndex = 0;
            this.tabControlGroupInfo.SelectedIndexChanged += new System.EventHandler(this.tabControlGroupInfo_SelectedIndexChanged);
            // 
            // tabPageBusinessAddress
            // 
            this.tabPageBusinessAddress.BackColor = System.Drawing.Color.White;
            this.tabPageBusinessAddress.Controls.Add(this.tableLayoutPanel1);
            resources.ApplyResources(this.tabPageBusinessAddress, "tabPageBusinessAddress");
            this.tabPageBusinessAddress.Name = "tabPageBusinessAddress";
            this.tabPageBusinessAddress.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_gris;
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.groupBoxFirstAddress, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxSecondaryAddress, 1, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // groupBoxFirstAddress
            // 
            this.groupBoxFirstAddress.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.groupBoxFirstAddress, "groupBoxFirstAddress");
            this.groupBoxFirstAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.groupBoxFirstAddress.Name = "groupBoxFirstAddress";
            this.groupBoxFirstAddress.TabStop = false;
            // 
            // groupBoxSecondaryAddress
            // 
            this.groupBoxSecondaryAddress.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.groupBoxSecondaryAddress, "groupBoxSecondaryAddress");
            this.groupBoxSecondaryAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.groupBoxSecondaryAddress.Name = "groupBoxSecondaryAddress";
            this.groupBoxSecondaryAddress.TabStop = false;
            // 
            // tabPageMembers
            // 
            this.tabPageMembers.BackColor = System.Drawing.Color.White;
            this.tabPageMembers.Controls.Add(this.splitContainer3);
            resources.ApplyResources(this.tabPageMembers, "tabPageMembers");
            this.tabPageMembers.Name = "tabPageMembers";
            this.tabPageMembers.UseVisualStyleBackColor = true;
            // 
            // tabPageHistory
            // 
            this.tabPageHistory.BackColor = System.Drawing.Color.White;
            this.tabPageHistory.Controls.Add(this.listViewHistoryMembers);
            resources.ApplyResources(this.tabPageHistory, "tabPageHistory");
            this.tabPageHistory.Name = "tabPageHistory";
            this.tabPageHistory.UseVisualStyleBackColor = true;
            // 
            // listViewHistoryMembers
            // 
            this.listViewHistoryMembers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader9,
            this.columnHeader10});
            resources.ApplyResources(this.listViewHistoryMembers, "listViewHistoryMembers");
            this.listViewHistoryMembers.FullRowSelect = true;
            this.listViewHistoryMembers.GridLines = true;
            this.listViewHistoryMembers.MultiSelect = false;
            this.listViewHistoryMembers.Name = "listViewHistoryMembers";
            this.listViewHistoryMembers.UseCompatibleStateImageBehavior = false;
            this.listViewHistoryMembers.View = System.Windows.Forms.View.Details;
            this.listViewHistoryMembers.DoubleClick += new System.EventHandler(this.listViewHistoryMembers_DoubleClick);
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // columnHeader3
            // 
            resources.ApplyResources(this.columnHeader3, "columnHeader3");
            // 
            // columnHeader4
            // 
            resources.ApplyResources(this.columnHeader4, "columnHeader4");
            // 
            // columnHeader5
            // 
            resources.ApplyResources(this.columnHeader5, "columnHeader5");
            // 
            // columnHeader6
            // 
            resources.ApplyResources(this.columnHeader6, "columnHeader6");
            // 
            // columnHeader9
            // 
            resources.ApplyResources(this.columnHeader9, "columnHeader9");
            // 
            // columnHeader10
            // 
            resources.ApplyResources(this.columnHeader10, "columnHeader10");
            // 
            // tabPageProjects
            // 
            this.tabPageProjects.Controls.Add(this.splitContainer1);
            resources.ApplyResources(this.tabPageProjects, "tabPageProjects");
            this.tabPageProjects.Name = "tabPageProjects";
            this.tabPageProjects.UseVisualStyleBackColor = true;
            // 
            // tabPageSaving
            // 
            this.tabPageSaving.Controls.Add(this.savingsListUserControl1);
            resources.ApplyResources(this.tabPageSaving, "tabPageSaving");
            this.tabPageSaving.Name = "tabPageSaving";
            this.tabPageSaving.UseVisualStyleBackColor = true;
            // 
            // savingsListUserControl1
            // 
            this.savingsListUserControl1.ButtonAddSavingsEnabled = true;
            this.savingsListUserControl1.ClientType = Octopus.Enums.OClientTypes.Group;
            resources.ApplyResources(this.savingsListUserControl1, "savingsListUserControl1");
            this.savingsListUserControl1.Name = "savingsListUserControl1";
            this.savingsListUserControl1.Load += new System.EventHandler(this.savingsListUserControl1_Load);
            this.savingsListUserControl1.AddSelectedSaving += new System.EventHandler(this.savingsListUserControl1_AddSelectedSaving);
            this.savingsListUserControl1.ViewSelectedSaving += new System.EventHandler(this.savingsListUserControl1_ViewSelectedSaving);
            // 
            // tabPageCustomizableFields
            // 
            resources.ApplyResources(this.tabPageCustomizableFields, "tabPageCustomizableFields");
            this.tabPageCustomizableFields.Name = "tabPageCustomizableFields";
            this.tabPageCustomizableFields.UseVisualStyleBackColor = true;
            // 
            // imageListTab
            // 
            this.imageListTab.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTab.ImageStream")));
            this.imageListTab.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTab.Images.SetKeyName(0, "");
            this.imageListTab.Images.SetKeyName(1, "");
            this.imageListTab.Images.SetKeyName(2, "");
            this.imageListTab.Images.SetKeyName(3, "");
            this.imageListTab.Images.SetKeyName(4, "");
            // 
            // cbBranch
            // 
            resources.ApplyResources(this.cbBranch, "cbBranch");
            this.cbBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBranch.FormattingEnabled = true;
            this.cbBranch.Name = "cbBranch";
            // 
            // groupBoxButtonBottom
            // 
            resources.ApplyResources(this.groupBoxButtonBottom, "groupBoxButtonBottom");
            this.groupBoxButtonBottom.Controls.Add(this.buttonNext);
            this.groupBoxButtonBottom.Controls.Add(this.buttonPreview);
            this.groupBoxButtonBottom.Name = "groupBoxButtonBottom";
            this.groupBoxButtonBottom.TabStop = false;
            // 
            // buttonNext
            // 
            resources.ApplyResources(this.buttonNext, "buttonNext");
            this.buttonNext.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonNext.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.buttonNext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonNext.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Next;
            this.buttonNext.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_next;
            this.buttonNext.Menu = null;
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.UseVisualStyleBackColor = false;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonPreview
            // 
            this.buttonPreview.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonPreview, "buttonPreview");
            this.buttonPreview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonPreview.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Previous;
            this.buttonPreview.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_previous;
            this.buttonPreview.Menu = null;
            this.buttonPreview.Name = "buttonPreview";
            this.buttonPreview.UseVisualStyleBackColor = false;
            this.buttonPreview.Click += new System.EventHandler(this.buttonPreview_Click);
            // 
            // labelHelpRequiredFields
            // 
            resources.ApplyResources(this.labelHelpRequiredFields, "labelHelpRequiredFields");
            this.labelHelpRequiredFields.Name = "labelHelpRequiredFields";
            // 
            // groupBoxButtons
            // 
            this.groupBoxButtons.Controls.Add(this.btnPrint);
            this.groupBoxButtons.Controls.Add(this.buttonCancel);
            this.groupBoxButtons.Controls.Add(this.buttonSave);
            resources.ApplyResources(this.groupBoxButtons, "groupBoxButtons");
            this.groupBoxButtons.Name = "groupBoxButtons";
            this.groupBoxButtons.TabStop = false;
            // 
            // btnPrint
            // 
            resources.ApplyResources(this.btnPrint, "btnPrint");
            this.btnPrint.AttachmentPoint = Octopus.Reports.AttachmentPoint.GroupDetails;
            this.btnPrint.BackColor = System.Drawing.Color.Gainsboro;
            this.btnPrint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnPrint.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Print;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.ReportInitializer = null;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Visibility = Octopus.Reports.Visibility.Group;
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonCancel.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Close;
            this.buttonCancel.Menu = null;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            resources.ApplyResources(this.buttonSave, "buttonSave");
            this.buttonSave.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonSave.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Save;
            this.buttonSave.Menu = null;
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // tableLayoutPanel4
            // 
            resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
            this.tableLayoutPanel4.Controls.Add(this.groupBoxButtonBottom, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.tabControlGroupInfo, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.groupBoxCivilStatus, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.groupBoxButtons, 0, 1);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            // 
            // groupBoxCivilStatus
            // 
            this.groupBoxCivilStatus.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_gris;
            resources.ApplyResources(this.groupBoxCivilStatus, "groupBoxCivilStatus");
            this.groupBoxCivilStatus.Controls.Add(this.tlpGroupControls);
            this.groupBoxCivilStatus.Controls.Add(this.linkLabelChangePhoto2);
            this.groupBoxCivilStatus.Controls.Add(this.linkLabelChangePhoto);
            this.groupBoxCivilStatus.Controls.Add(this.pictureBox2);
            this.groupBoxCivilStatus.Controls.Add(this.pictureBox1);
            this.groupBoxCivilStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.groupBoxCivilStatus.Name = "groupBoxCivilStatus";
            this.groupBoxCivilStatus.TabStop = false;
            // 
            // tlpGroupControls
            // 
            this.tlpGroupControls.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.tlpGroupControls, "tlpGroupControls");
            this.tlpGroupControls.Controls.Add(this.cbBranch, 1, 4);
            this.tlpGroupControls.Controls.Add(this.label2, 0, 4);
            this.tlpGroupControls.Controls.Add(this.textBoxGroupLoanCycle, 1, 3);
            this.tlpGroupControls.Controls.Add(this.labelGroupCycle, 0, 3);
            this.tlpGroupControls.Controls.Add(this.lblWeekDay, 0, 1);
            this.tlpGroupControls.Controls.Add(this.cmbWeekDay, 1, 1);
            this.tlpGroupControls.Controls.Add(this.dateTimePickerDateOfEstablishment, 1, 2);
            this.tlpGroupControls.Controls.Add(this.labelDateOfEstablishment, 0, 2);
            this.tlpGroupControls.Controls.Add(this.cbMeetingDay, 2, 1);
            this.tlpGroupControls.Controls.Add(this.labelName, 0, 0);
            this.tlpGroupControls.Controls.Add(this.textBoxName, 1, 0);
            this.tlpGroupControls.Name = "tlpGroupControls";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // textBoxGroupLoanCycle
            // 
            resources.ApplyResources(this.textBoxGroupLoanCycle, "textBoxGroupLoanCycle");
            this.textBoxGroupLoanCycle.Name = "textBoxGroupLoanCycle";
            this.textBoxGroupLoanCycle.TextChanged += new System.EventHandler(this.textBoxGroupLoanCycle_TextChanged);
            // 
            // labelGroupCycle
            // 
            resources.ApplyResources(this.labelGroupCycle, "labelGroupCycle");
            this.labelGroupCycle.BackColor = System.Drawing.Color.Transparent;
            this.labelGroupCycle.Name = "labelGroupCycle";
            // 
            // lblWeekDay
            // 
            resources.ApplyResources(this.lblWeekDay, "lblWeekDay");
            this.lblWeekDay.Name = "lblWeekDay";
            // 
            // cmbWeekDay
            // 
            resources.ApplyResources(this.cmbWeekDay, "cmbWeekDay");
            this.cmbWeekDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWeekDay.FormattingEnabled = true;
            this.cmbWeekDay.Items.AddRange(new object[] {
            resources.GetString("cmbWeekDay.Items"),
            resources.GetString("cmbWeekDay.Items1"),
            resources.GetString("cmbWeekDay.Items2"),
            resources.GetString("cmbWeekDay.Items3"),
            resources.GetString("cmbWeekDay.Items4"),
            resources.GetString("cmbWeekDay.Items5"),
            resources.GetString("cmbWeekDay.Items6")});
            this.cmbWeekDay.Name = "cmbWeekDay";
            // 
            // dateTimePickerDateOfEstablishment
            // 
            resources.ApplyResources(this.dateTimePickerDateOfEstablishment, "dateTimePickerDateOfEstablishment");
            this.dateTimePickerDateOfEstablishment.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerDateOfEstablishment.Name = "dateTimePickerDateOfEstablishment";
            this.dateTimePickerDateOfEstablishment.Value = new System.DateTime(2006, 5, 19, 0, 0, 0, 0);
            this.dateTimePickerDateOfEstablishment.ValueChanged += new System.EventHandler(this.dateTimePickerDateOfEstablishment_ValueChanged);
            // 
            // labelDateOfEstablishment
            // 
            resources.ApplyResources(this.labelDateOfEstablishment, "labelDateOfEstablishment");
            this.labelDateOfEstablishment.BackColor = System.Drawing.Color.Transparent;
            this.labelDateOfEstablishment.Name = "labelDateOfEstablishment";
            // 
            // cbMeetingDay
            // 
            resources.ApplyResources(this.cbMeetingDay, "cbMeetingDay");
            this.cbMeetingDay.Name = "cbMeetingDay";
            this.cbMeetingDay.UseVisualStyleBackColor = true;
            this.cbMeetingDay.CheckedChanged += new System.EventHandler(this.cbMeetingDay_CheckedChanged);
            // 
            // labelName
            // 
            resources.ApplyResources(this.labelName, "labelName");
            this.labelName.BackColor = System.Drawing.Color.Transparent;
            this.labelName.Name = "labelName";
            // 
            // textBoxName
            // 
            this.textBoxName.BackColor = System.Drawing.SystemColors.Window;
            this.tlpGroupControls.SetColumnSpan(this.textBoxName, 2);
            resources.ApplyResources(this.textBoxName, "textBoxName");
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // linkLabelChangePhoto2
            // 
            resources.ApplyResources(this.linkLabelChangePhoto2, "linkLabelChangePhoto2");
            this.linkLabelChangePhoto2.Name = "linkLabelChangePhoto2";
            this.linkLabelChangePhoto2.TabStop = true;
            this.linkLabelChangePhoto2.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // linkLabelChangePhoto
            // 
            resources.ApplyResources(this.linkLabelChangePhoto, "linkLabelChangePhoto");
            this.linkLabelChangePhoto.Name = "linkLabelChangePhoto";
            this.linkLabelChangePhoto.TabStop = true;
            this.linkLabelChangePhoto.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox2
            // 
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // GroupUserControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel4);
            this.Name = "GroupUserControl";
            this.Load += new System.EventHandler(this.GroupUserControl_Load);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBoxProjects.ResumeLayout(false);
            this.tabControlGroupInfo.ResumeLayout(false);
            this.tabPageBusinessAddress.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabPageMembers.ResumeLayout(false);
            this.tabPageHistory.ResumeLayout(false);
            this.tabPageProjects.ResumeLayout(false);
            this.tabPageSaving.ResumeLayout(false);
            this.groupBoxButtonBottom.ResumeLayout(false);
            this.groupBoxButtons.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.groupBoxCivilStatus.ResumeLayout(false);
            this.groupBoxCivilStatus.PerformLayout();
            this.tlpGroupControls.ResumeLayout(false);
            this.tlpGroupControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.UserControl PanelSavings
        {
            get { return savingsListUserControl1; }
        }

        public bool ButtonAddSavingsEnabled
        {
            get { return savingsListUserControl1.ButtonAddSavingsEnabled; }
            set { savingsListUserControl1.ButtonAddSavingsEnabled = value; }
        }

        public void RemoveSavings()
        {
            tabControlGroupInfo.TabPages.Remove(tabPageSaving);
        }

        public GroupUserControl(Form pMdiParent)
        {
            _mdiParent = pMdiParent;
            InitializeComponent();
            group = new Group();
            group.Leader = null;
            Initialization();
            dateTimePickerDateOfEstablishment.Value = TimeProvider.Today;
            DisplayProjects(group.Projects);
        }

        public GroupUserControl(Group group, Form pMdiParent)
        {
            _mdiParent = pMdiParent;
            InitializeComponent();
            this.group = group;
            loanAmount = group.TotalContractAmount;
            Initialization();
            InitializeGroup();
            DisplayProjects(group.Projects);
            PicturesServices ps = ServicesProvider.GetInstance().GetPicturesServices();
            if (ps.IsEnabled())
            {
                pictureBox1.Image = ps.GetPicture("GROUP", group.Id, true, 0);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox2.Image = ps.GetPicture("GROUP", group.Id, true, 1);
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = false;
                linkLabelChangePhoto.Visible = false;
                linkLabelChangePhoto2.Visible = false;
            }
            InitializeCustomizableFields(group.Id);
        }

        private void Initialization()
        {
            _generalParameters = ServicesProvider.GetInstance().GetGeneralSettings();
            loanAmount = 0;
            textBoxGroupLoanCycle.Text = "0";
            InitializeUserControlsAddress();
            groupSaved = false;
            InitializeHistoryMembers();

            foreach (Branch branch in User.CurrentUser.Branches)
            {
                cbBranch.Items.Add(branch);
            }
        }

        private void InitializeUserControlsAddress()
        {
            addressUserControlFirst = new AddressUserControl
                                          {
                                              TextBoxHomePhoneText = MultiLanguageStrings.GetString(Ressource.GroupUserControl, "Homephone.Text"),
                                              TextBoxPersonalPhoneText = MultiLanguageStrings.GetString(Ressource.GroupUserControl, "Personalphone.Text")
                                          };
            addressUserControlSecondaryAddress = new AddressUserControl
                                                     {
                                                         TextBoxHomePhoneText = MultiLanguageStrings.GetString(Ressource.GroupUserControl, "Businessphone.Text"),
                                                         TextBoxPersonalPhoneText = MultiLanguageStrings.GetString(Ressource.GroupUserControl, "Businesscellphone.Text")
                                                     };
            addressUserControlFirst.Dock = DockStyle.Fill;
            groupBoxFirstAddress.Controls.Add(addressUserControlFirst);
            addressUserControlSecondaryAddress.Dock = DockStyle.Fill;
            groupBoxSecondaryAddress.Controls.Add(addressUserControlSecondaryAddress);
        }

        private void InitializeHistoryMembers()
        {
            historyPersons = ServicesProvider.GetInstance().GetClientServices().FindHistoryPersons(group.Id);
            DisplayHistoryPersons(historyPersons);
        }

        private void InitializeGroup()
        {
            if (group.Id != 0)
            {
                textBoxName.Text = group.Name;
                textBoxGroupLoanCycle.Text = group.LoanCycle.ToString();              

                dateTimePickerDateOfEstablishment.Value = group.EstablishmentDate.HasValue
                                                              ? group.EstablishmentDate.Value
                                                              : TimeProvider.Today;
                if (group.MeetingDay.HasValue)
                {
                    cmbWeekDay.SelectedIndex = (int) group.MeetingDay;
                    cbMeetingDay.Checked = true;
                }

                DisplayMembers();                

                buttonSave.Text = MultiLanguageStrings.GetString(Ressource.Common, "updateButton.Text");

                addressUserControlFirst.District = group.District;
                addressUserControlFirst.City = group.City;
                addressUserControlFirst.Comments = group.Address;
                addressUserControlFirst.HomePhone = group.HomePhone ?? String.Empty;
                addressUserControlFirst.PersonalPhone = group.PersonalPhone ?? String.Empty;
                addressUserControlFirst.ZipCode = group.ZipCode;
                addressUserControlFirst.Email = group.Email;

                textBoxGroupLoanCycle.Text = group.LoanCycle.ToString();

                if (!group.SecondaryAddressIsEmpty)
                {
                    addressUserControlSecondaryAddress.District = group.SecondaryDistrict;
                    addressUserControlSecondaryAddress.City = group.SecondaryCity;
                    addressUserControlSecondaryAddress.Comments = group.SecondaryAddress;
                    addressUserControlSecondaryAddress.HomePhone = group.HomePhone ?? String.Empty;
                    addressUserControlSecondaryAddress.PersonalPhone = group.PersonalPhone ?? String.Empty;
                    addressUserControlSecondaryAddress.Email = group.SecondaryEmail;
                    addressUserControlSecondaryAddress.ZipCode = group.SecondaryZipCode;
                }
                DisplayProjects(group.Projects);
                DisplaySavings(group.Savings);
                cbBranch.SelectedItem = group.Branch;
            }
            else
            {
                group.LoanCycle = 0;
                dateTimePickerDateOfEstablishment.Value = TimeProvider.Today;
                group.EstablishmentDate = TimeProvider.Today;

                if (cbBranch.Items.Count > 0) cbBranch.SelectedIndex = 0;
            }
            InitPrintButton();
        }

        private void InitializeCustomizableFields(int linkId)
        {
            _customizableFieldsControl = new CustomizableFieldsControl(OCustomizableFieldEntities.SG, linkId, false)
            {
                Dock = DockStyle.Fill,
                Enabled = true,
                Name = "customizableFieldsControl",
                Visible = true
            };

            tabPageCustomizableFields.Controls.Add(_customizableFieldsControl);
        }

        public void ResetAllComponents()
        {
            group = new Group();
            textBoxName.Text = string.Empty;
            dateTimePickerDateOfEstablishment.Value = TimeProvider.Today;
            listViewOtherMembres.Items.Clear();
            listViewHistoryMembers.Items.Clear();
            addressUserControlFirst.ResetAllComponents();
            addressUserControlSecondaryAddress.ResetAllComponents();
        }

        public Person HistoryMember
        {
            get
            {
                if (listViewHistoryMembers.SelectedItems.Count != 0)
                    return (Person) listViewHistoryMembers.SelectedItems[0].Tag;
                return null;
            }
        }

        public Group Group
        {
            get { return group; }
            set
            {
                group = value;
                if (group == null)
                    ResetAllComponents();
                else
                {
                    InitializeGroup();
                }
            }
        }

        public bool GroupSaved
        {
            get { return groupSaved; }
        }

        public OCurrency LoanAmount
        {
            set
            {
                loanAmount = value;
                UpdateMembersShareAmount();
                DisplayMembers();
                SaveGroup();
            }
        }

        #region Select Members

        private void DeleteAMember()
        {
            if (listViewOtherMembres.SelectedItems.Count != 0)
            {
                if (GroupHasActiveContracts())
                {
                    Member temPerson = (Member)listViewOtherMembres.SelectedItems[0].Tag;
                    if (group.Leader != null && temPerson.Tiers.Id == group.Leader.Tiers.Id)
                    {
                        MessageBox.Show(
                            MultiLanguageStrings.GetString(Ressource.GroupUserControl, "CannotDeleteLeader.Text"), "",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        try
                        {
                            if (group.Id != 0)
                                ServicesProvider.GetInstance().GetClientServices().CheckMinNumberOfMembers(group);

                            group.DeleteMember(temPerson);
                            historyPersons.Add(temPerson);
                            DisplayHistoryPersons(historyPersons);
                            DisplayMembers();
                            if (MembersChanged != null)
                                MembersChanged(this, null);
                            _generalParameters = ApplicationSettings.GetInstance("");
                            ServicesProvider.GetInstance().GetContractServices().DeleteLoanShareAmountWhereNotDisbursed(group.Id);
                            if (group.Id != 0)
                                buttonSave_Click(this, null);
                    
                        }
                        catch (Exception ex)
                        {
                            new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog(); 
                        }

                    }
                }
            }
            else
            {
                string message = MultiLanguageStrings.GetString(Ressource.GroupUserControl, "SelectMember");
                MessageBox.Show(message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private bool GroupHasActiveContracts()
        {
            bool IsModified = true;

            foreach (var project in group.Projects)
            {
                foreach (Loan contract in project.Credits)
                {
                    if (contract.Disbursed && !contract.Closed)
                    {
                        string message = MultiLanguageStrings.GetString(Ressource.GroupUserControl, "CantModifyGroup");
                        MessageBox.Show(message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        IsModified = false;
                    }
                }
            }

            return IsModified;
        }

        private void SelectAMember()
        {
            if (GroupHasActiveContracts())
            {
                using (SearchClientForm searchClientForm = SearchClientForm.GetInstance(OClientTypes.Person, true))
                {
                    searchClientForm.ShowDialog();
                    try
                    {
                        if (group.Id != 0)
                            ServicesProvider.GetInstance().GetClientServices().CheckMaxNumberOfMembers(group);
                        if (ServicesProvider.GetInstance().GetClientServices().ClientIsAPerson(searchClientForm.Client))
                        {
                            Member pers = new Member
                                              {
                                                  Tiers = searchClientForm.Client,
                                                  LoanShareAmount = 0,
                                                  CurrentlyIn = true,
                                                  IsLeader = false,
                                                  JoinedDate = TimeProvider.Today
                                              };

                            if (ServicesProvider.GetInstance().GetClientServices().ClientCanBeAddToAGroup(searchClientForm.Client, group))
                            {
                                group.AddMember(pers);
                                DisplayMembers();
                                if (group.Id!=0)
                                    ServicesProvider.GetInstance().GetContractServices().DeleteLoanShareAmountWhereNotDisbursed(group.Id);
                                if (MembersChanged != null) MembersChanged(this, null);
                                if (group.Id != 0)
                                    buttonSave_Click(this, null);

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
                    }
                }
            }
        }

        #endregion

        #region Add members

        private void AddMembers()
        {
            if (!group.Active)
            {
                if (GroupHasActiveContracts())
                {
                    ClientForm personForm = new ClientForm(OClientTypes.Person, _mdiParent, true);
                    if (DialogResult.OK == personForm.ShowDialog())
                    {
                        try
                        {
                            if (group.Id != 0)
                                ServicesProvider.GetInstance().GetClientServices().CheckMaxNumberOfMembers(group);
                            Person pers = personForm.Person;
                            if (ServicesProvider.GetInstance().GetClientServices().ClientIsAPerson(pers))
                            {
                                group.AddMember(new Member
                                                    {
                                                        Tiers = pers,
                                                        LoanShareAmount = 0,
                                                        CurrentlyIn = true,
                                                        IsLeader = false,
                                                        JoinedDate = TimeProvider.Today
                                                    });
                                DisplayMembers();
                                ServicesProvider.GetInstance().GetContractServices().DeleteLoanShareAmountWhereNotDisbursed(group.Id);
                                if (MembersChanged != null) MembersChanged(this, null);
                                if (group.Id != 0)
                                    buttonSave_Click(this, null);
                            }
                        }
                        catch (Exception ex)
                        {
                            new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show(MultiLanguageStrings.GetString(Ressource.GroupUserControl, "CannotAddRemoveMember.Text"), "",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Save / Update Group

        private void GetDatasFromUserControlsAddress()
        {
            group.District = addressUserControlFirst.District;
            group.City = addressUserControlFirst.City;
            group.Address = addressUserControlFirst.Comments;
            group.HomePhone = addressUserControlFirst.HomePhone;
            group.HomeType = addressUserControlFirst.HomeType;
            group.PersonalPhone = addressUserControlFirst.PersonalPhone;
            group.Email = addressUserControlFirst.Email;
            group.ZipCode = addressUserControlFirst.ZipCode;

            group.SecondaryDistrict = addressUserControlSecondaryAddress.District;
            group.SecondaryCity = addressUserControlSecondaryAddress.City;
            group.SecondaryAddress = addressUserControlSecondaryAddress.Comments;
            group.SecondaryHomePhone = addressUserControlSecondaryAddress.HomePhone;
            group.SecondaryHomeType = addressUserControlSecondaryAddress.HomeType;
            group.SecondaryPersonalPhone = addressUserControlSecondaryAddress.PersonalPhone;
            group.SecondaryEmail = addressUserControlSecondaryAddress.Email;
            group.SecondaryZipCode = addressUserControlSecondaryAddress.ZipCode;
            group.MeetingDay = cbMeetingDay.Checked ? (DayOfWeek?)GetDayOfWeek() : null;
            group.Branch = (Branch) cbBranch.SelectedItem;
        }

        private DayOfWeek GetDayOfWeek()
        {
            DayOfWeek dayOfWeek = DayOfWeek.Monday;
            switch (cmbWeekDay.SelectedIndex)
            {
                case 0: dayOfWeek = DayOfWeek.Sunday;
                    break;
                case 1: dayOfWeek = DayOfWeek.Monday;
                    break;
                case 2: dayOfWeek = DayOfWeek.Tuesday;
                    break;
                case 3: dayOfWeek = DayOfWeek.Wednesday;
                    break;
                case 4: dayOfWeek = DayOfWeek.Thursday;
                    break;
                case 5: dayOfWeek = DayOfWeek.Friday;
                    break;
                case 6: dayOfWeek = DayOfWeek.Saturday;
                    break;
            }

            return dayOfWeek;
        }

        private void SaveGroup()
        {
            groupSaved = false;
            GetDatasFromUserControlsAddress();

            try
            {
                bool save = 0 == group.Id;
                if (group.Name != null)
                    group.Name = group.Name.Trim();
                _customizableFieldsControl.Check();
                
                string result = ServicesProvider
                    .GetInstance()
                    .GetClientServices()
                    .SaveSolidarityGroup(ref group, (tx, id) => _extensionGroups.ForEach(g => g.Save(group, tx)));

                if(group.Id > 0)
                    _customizableFieldsControl.Save(group.Id);

                EventProcessorServices es = ServicesProvider.GetInstance().GetEventProcessorServices();

                es.LogClientSaveUpdateEvent(group, save);
                
                ResetImagesStatuses();
                groupSaved = true;
                if(result != string.Empty) MessageBox.Show(result, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                buttonSave.Text = MultiLanguageStrings.GetString(Ressource.Common, "updateButton.Text");

                InitializeCustomizableFields(group.Id);
                //EnableDocuments();
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private void ResetImagesStatuses()
        {
            group.IsImageDeleted = false;
            group.IsImageAdded = false;
            group.IsImageUpdated = false;

            group.IsImage2Deleted = false;
            group.IsImage2Added = false;
            group.IsImage2Updated = false;
        }

        #endregion

        #region Display Members,Groups,Contracts,ChangeLeader,ViewPerson
        
        private void DisplayHistoryPersons(IEnumerable<Member> personList)
        {
            listViewHistoryMembers.Items.Clear();
            foreach (Member member in personList)
            {
                Person person = (Person) member.Tiers;
                ListViewItem listViewItem = new ListViewItem(person.Name) {Tag = member};
                listViewItem.SubItems.Add(person.IdentificationData);

                if (!person.DateOfBirth.HasValue)
                    listViewItem.SubItems.Add("-");
                else
                    listViewItem.SubItems.Add(person.DateOfBirth.Value.ToShortDateString());

                listViewItem.SubItems.Add(person.Sex.ToString());
                if (person.HouseHoldHead)
                    listViewItem.SubItems.Add(MultiLanguageStrings.GetString(Ressource.GroupUserControl, "textYes.Text"));
                else
                    listViewItem.SubItems.Add(MultiLanguageStrings.GetString(Ressource.GroupUserControl, "textNo.Text"));

                listViewItem.SubItems.Add(person.NbOfDependents.ToString());
                listViewItem.SubItems.Add(member.JoinedDate.ToShortDateString());
                listViewItem.SubItems.Add(member.LeftDate.Value.ToShortDateString());
                listViewHistoryMembers.Items.Add(listViewItem);
            }
        }

        private void UpdateMembersShareAmount()
        {
            if (group.GetNumberOfMembers != 0)
            {
                OCurrency amount = ServicesProvider.GetInstance().GetClientServices().CalculateLoanShareAmount(group.GetNumberOfMembers, loanAmount);
                
                foreach (Member entry in group.Members)
                {
                    entry.LoanShareAmount = amount;
                }
            }
        }

        public void DisplayMembers()
        {
            listViewOtherMembres.Items.Clear();

            foreach (Member entry in Group.Members)
            {
                Person person = (Person) entry.Tiers;
                ListViewItem listViewItem = new ListViewItem(person.Name) {Tag = entry};
                listViewItem.SubItems.Add(person.IdentificationData);

                if (!person.DateOfBirth.HasValue)
                    listViewItem.SubItems.Add("-");
                else
                    listViewItem.SubItems.Add(person.DateOfBirth.Value.ToShortDateString());

                listViewItem.SubItems.Add(person.Sex.ToString());
                if (person.HouseHoldHead)
                    listViewItem.SubItems.Add(MultiLanguageStrings.GetString(Ressource.GroupUserControl, "textYes.Text"));
                else
                    listViewItem.SubItems.Add(MultiLanguageStrings.GetString(Ressource.GroupUserControl, "textNo.Text"));
                listViewItem.SubItems.Add(person.LoanCycle.ToString());

                listViewItem.SubItems.Add(person.NbOfDependents.ToString());
                listViewItem.SubItems.Add(entry.JoinedDate.ToShortDateString());
                listViewItem.SubItems.Add(person.BadClient.ToString());

                if (group.Leader != null)
                {
                    if (person.Id == group.Leader.Tiers.Id)
                    {
                        listViewItem.BackColor = Color.FromArgb(0,88,56);
                        listViewItem.ForeColor = Color.White;
                    }
                    else
                        listViewItem.BackColor = Color.White;
                }
                listViewOtherMembres.Items.Add(listViewItem);
            }
        }

        private void ViewPerson(Member pMember, bool leader)
        {
            var personForm = new ClientForm((Person) pMember.Tiers, _mdiParent);
            personForm.ShowDialog();

            if(leader && personForm.DialogResult == DialogResult.OK)
                group.Leader.Tiers = personForm.Person;

            group.IsBadClient();
            InitializeGroup();
            DisplayMembers();
        }

        private void _ChangeLeader(Member newLeader)
        {
            group.Leader = newLeader;
            DisplayMembers();
        }

        #endregion

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Events Buttons Click

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (ButtonCancelClick != null)
                ButtonCancelClick(this, e);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveGroup();
            if (ButtonSaveClick != null)
                ButtonSaveClick(this, e);
        }

        private void buttonAddMembres_Click(object sender, EventArgs e)
        {
            AddMembers();
            if (ButtonBadClientClick != null)
                ButtonBadClientClick(this, e);
        }

        private void buttonSelectAMember_Click(object sender, EventArgs e)
        {
            SelectAMember();
            if (ButtonBadClientClick != null)
                ButtonBadClientClick(this, e);
        }

        private void buttonViewMember_Click(object sender, EventArgs e)
        {
            if (listViewOtherMembres.SelectedItems.Count != 0)
            {
                Member pers = (Member)listViewOtherMembres.SelectedItems[0].Tag;
                ViewPerson(pers, group.IsLeader(pers));

                if (ButtonBadClientClick != null)
                    ButtonBadClientClick(this, e);
            }
        }

        private void listViewOtherMembres_DoubleClick(object sender, EventArgs e)
        {
            var pers = (Member) listViewOtherMembres.SelectedItems[0].Tag;
            ViewPerson(pers, group.IsLeader(pers));         
            
            if (ButtonBadClientClick != null)
                ButtonBadClientClick(this, e);
        }

        private void buttonSaveAsLeader_Click(object sender, EventArgs e)
        {
            if (listViewOtherMembres.SelectedItems.Count != 0)
                _ChangeLeader(((Member)listViewOtherMembres.SelectedItems[0].Tag));
        }

        #endregion

        #region Events TextChanged

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            group.Name = ServicesHelper.CheckTextBoxText(textBoxName.Text);
        }

        private void dateTimePickerDateOfEstablishment_ValueChanged(object sender, EventArgs e)
        {
            group.EstablishmentDate = dateTimePickerDateOfEstablishment.Value;
        }
        #endregion

        private void GroupUserControl_Load(object sender, EventArgs e)
        {
            Tabs = tabControlGroupInfo;
            Client = group;
            InitDocuments();

            groupBoxFirstAddress.Size = new Size(tabPageBusinessAddress.Width/2, tabPageBusinessAddress.Height);
            
            if (!ServicesProvider.GetInstance().GetGeneralSettings().UseProjects)
                ButtonAddProjectClick(buttonViewProject, null);

            LoadExtensions();
        }

        private void LoadExtensions()
        {
            foreach (ISolidarityGroup g in Extension.Instance.Extensions.Select(e => e.QueryInterface(typeof(ISolidarityGroup))).OfType<ISolidarityGroup>())
            {
                _extensionGroups.Add(g);
                TabPage[] pages = g.GetTabPages(group);
                if (null == pages) continue;
                tabControlGroupInfo.TabPages.AddRange(pages);
            }
        }

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            if (components != null)
            {
                components.Dispose();
            }
            Dispose();
        }

        #endregion

        private void buttonDeleteMembers_Click(object sender, EventArgs e)
        {
            DeleteAMember();
            if (ButtonBadClientClick != null)
                ButtonBadClientClick(this, e);
        }

        private void textBoxGroupLoanCycle_TextChanged(object sender, EventArgs e)
        {
            int? loanCycle = ServicesHelper.ConvertStringToNullableInt32(textBoxGroupLoanCycle.Text);
            if (!loanCycle.HasValue)
            {
                textBoxGroupLoanCycle.BackColor = Color.Red;
                group.LoanCycle = 0;
            }
            else
            {
                textBoxGroupLoanCycle.BackColor = Color.White;
                group.LoanCycle = loanCycle.Value;
            }
        }

        private void listViewHistoryMembers_DoubleClick(object sender, EventArgs e)
        {
            if (ListViewHistoryMemberDoubleClick != null)
                ListViewHistoryMemberDoubleClick(this, e);
        }

        private void buttonPreview_Click(object sender, EventArgs e)
        {
            tabControlGroupInfo.SelectedIndex--;
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            tabControlGroupInfo.SelectedIndex++;
        }

        private void tabControlGroupInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonPreview.Visible = true;
            buttonNext.Visible = true;
            if (tabControlGroupInfo.SelectedIndex == 0) buttonPreview.Visible = false;

            if (tabControlGroupInfo.SelectedIndex == tabControlGroupInfo.TabPages.Count - 1)
                buttonNext.Visible = false;
        }

        public void DisplayProjects(IEnumerable<Project> pProject)
        {
            if (ServicesProvider.GetInstance().GetGeneralSettings().UseProjects)
            {
                listViewProjects.Items.Clear();
                foreach (Project project in pProject)
                {
                    ListViewItem item = new ListViewItem(project.Id.ToString()) {Tag = project};
                    item.SubItems.Add(project.Name);
                    item.SubItems.Add(project.Code);
                    item.SubItems.Add(project.Credits.Count.ToString());
                    listViewProjects.Items.Add(item);
                }
            }
            else
            {
                tabControlGroupInfo.TabPages.Remove(tabPageProjects);
            }
        }

        public void DisplaySavings(IEnumerable<ISavingsContract> pSavings)
        {
            savingsListUserControl1.DisplaySavings(pSavings);
        }

        private void listViewProjects_DoubleClick(object sender, EventArgs e)
        {
            _ViewProject(e);
        }

        public event EventHandler ButtonAddProjectClick;
        private void buttonAddProject_Click(object sender, EventArgs e)
        {
            if (ButtonAddProjectClick != null)
                ButtonAddProjectClick(this, e);
            DisplayProjects(group.Projects);
        }

        private void buttonViewProject_Click(object sender, EventArgs e)
        {
            _ViewProject(e);
        }

        public event EventHandler ViewProject;
        private void _ViewProject(EventArgs e)
        {
            if (listViewProjects.SelectedItems.Count != 0)
            {
                Project project = (Project)listViewProjects.SelectedItems[0].Tag;
                if (ViewProject != null)
                    ViewProject(project, e);
            }
        }

        private void savingsListUserControl1_AddSelectedSaving(object sender, EventArgs e)
        {
            if (AddSelectedSaving != null)
                AddSelectedSaving(sender, e);
        }

        private void savingsListUserControl1_ViewSelectedSaving(object sender, EventArgs e)
        {
            if (ViewSelectedSaving != null)
                ViewSelectedSaving(sender, e);
        }

        public void SyncLoanCycle()
        {
            textBoxGroupLoanCycle.Text = group.LoanCycle.ToString();
            DisplayMembers();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            int photoSubId = 0;
            if (sender is PictureBox)
            {
                if (((PictureBox)sender).Name=="pictureBox2")
                    photoSubId = 1;
            }
            else if (sender is LinkLabel)
            {
                if (((LinkLabel)sender).Name == "linkLabelChangePhoto2")
                    photoSubId = 1;
            }

            ShowPictureForm showPictureForm = new ShowPictureForm(group, this, photoSubId);
            showPictureForm.ShowDialog();
        }

        private void cbMeetingDay_CheckedChanged(object sender, EventArgs e)
        {
            cmbWeekDay.Enabled = cbMeetingDay.Checked;
        }

        private void InitPrintButton()
        {
            btnPrint.ReportInitializer = report => report.SetParamValue("group_id", @group.Id);
            btnPrint.LoadReports();
        }

        private void savingsListUserControl1_Load(object sender, EventArgs e)
        {

        }
    }
}