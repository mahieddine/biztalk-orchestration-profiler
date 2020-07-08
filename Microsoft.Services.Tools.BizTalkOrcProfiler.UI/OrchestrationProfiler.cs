
namespace Microsoft.Sdc.Orchestration.Profiler
{
	using System;
	using System.Xml;
	using System.IO;
	using System.Collections;
	using System.Diagnostics;
	using System.Drawing;
	using System.ComponentModel;
	using System.Reflection;
	using System.Text;
	using Microsoft.Win32;
	using System.Windows.Forms;
	using Microsoft.Services.Tools.BizTalkOM;
	using Microsoft.Sdc.OrchestrationProfiler.Core;

	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private static ReportGenerator generator = null;
		private static bool stop;
		private static bool showUsage = false;
		private static ReportingConfiguration reportingConfiguration = new ReportingConfiguration();

		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TreeView tvOrchs;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.LinkLabel linkLabel6;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.LinkLabel lnkListOrchestrations;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.PictureBox pictureBox3;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.LinkLabel linkLabel13;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.TextBox txtMgmtDatabase;
		private System.Windows.Forms.TextBox txtMgmtServer;
		private System.Windows.Forms.TextBox txtDtaDatabase;
		private System.Windows.Forms.TextBox txtDtaServer;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.CheckBox cbSelectAll;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.DateTimePicker dtpFrom;
		private System.Windows.Forms.DateTimePicker dtpTo;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label9;
		private LinkLabel lnkListApplications;
		private ListBox lstBtsApps;
		private TextBox tbInstanceID;
		private CheckBox cbFilterByInstanceID;
		private System.Windows.Forms.Label label8;

		/// <summary>
		/// Constructor
		/// </summary>
		public Form1()
		{
			InitializeComponent();
			this.SetDefaults();
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			generator = new ReportGenerator();
			ProcessArgs(args);

			if (showUsage)
			{
				ShowUsage();
				return;
			}
			else
			{
				if (stop)
				{
					reportingConfiguration.ExecutionMode = ExecutionMode.Interactive;
				}

				if (reportingConfiguration.ExecutionMode == ExecutionMode.Interactive)
				{
					Application.Run(new Form1());
				}
				else
				{
					Form1 f1 = new Form1();
					f1.GenerateDocumentation();
				}
			}

			return;
		}

		/// <summary>
		/// Clean up any resources being used.
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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="percentage"></param>
		private void PercentageDocumentationComplete(int percentage)
		{
			progressBar1.Value = percentage;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TvOrchsAfterCheck(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			foreach (TreeNode tn in e.Node.Nodes)
			{
				tn.Checked = e.Node.Checked;
			}
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.cbFilterByInstanceID = new System.Windows.Forms.CheckBox();
            this.tbInstanceID = new System.Windows.Forms.TextBox();
            this.linkLabel6 = new System.Windows.Forms.LinkLabel();
            this.linkLabel13 = new System.Windows.Forms.LinkLabel();
            this.label9 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lnkListApplications = new System.Windows.Forms.LinkLabel();
            this.cbSelectAll = new System.Windows.Forms.CheckBox();
            this.lstBtsApps = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDtaDatabase = new System.Windows.Forms.TextBox();
            this.txtDtaServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMgmtDatabase = new System.Windows.Forms.TextBox();
            this.txtMgmtServer = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lnkListOrchestrations = new System.Windows.Forms.LinkLabel();
            this.tvOrchs = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(543, 568);
            this.panel2.TabIndex = 23;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(-8, -24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(548, 600);
            this.tabControl1.TabIndex = 34;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.White;
            this.tabPage3.Controls.Add(this.cbFilterByInstanceID);
            this.tabPage3.Controls.Add(this.tbInstanceID);
            this.tabPage3.Controls.Add(this.linkLabel6);
            this.tabPage3.Controls.Add(this.linkLabel13);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.progressBar1);
            this.tabPage3.Controls.Add(this.lnkListApplications);
            this.tabPage3.Controls.Add(this.cbSelectAll);
            this.tabPage3.Controls.Add(this.lstBtsApps);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.dtpTo);
            this.tabPage3.Controls.Add(this.dtpFrom);
            this.tabPage3.Controls.Add(this.panel3);
            this.tabPage3.Controls.Add(this.panel1);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.txtDtaDatabase);
            this.tabPage3.Controls.Add(this.txtDtaServer);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.txtMgmtDatabase);
            this.tabPage3.Controls.Add(this.txtMgmtServer);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.panel5);
            this.tabPage3.Controls.Add(this.lnkListOrchestrations);
            this.tabPage3.Controls.Add(this.tvOrchs);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(540, 574);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Orchestration Info";
            // 
            // cbFilterByInstanceID
            // 
            this.cbFilterByInstanceID.AutoSize = true;
            this.cbFilterByInstanceID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(153)))));
            this.cbFilterByInstanceID.Location = new System.Drawing.Point(31, 195);
            this.cbFilterByInstanceID.Name = "cbFilterByInstanceID";
            this.cbFilterByInstanceID.Size = new System.Drawing.Size(231, 17);
            this.cbFilterByInstanceID.TabIndex = 89;
            this.cbFilterByInstanceID.Text = "Profile Only These Instances (Inst1, Inst2...)";
            this.cbFilterByInstanceID.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cbFilterByInstanceID.UseVisualStyleBackColor = true;
            this.cbFilterByInstanceID.CheckStateChanged += new System.EventHandler(this.cbFilterByInstanceID_CheckStateChanged);
            // 
            // tbInstanceID
            // 
            this.tbInstanceID.Enabled = false;
            this.tbInstanceID.Location = new System.Drawing.Point(275, 194);
            this.tbInstanceID.Name = "tbInstanceID";
            this.tbInstanceID.Size = new System.Drawing.Size(245, 20);
            this.tbInstanceID.TabIndex = 87;
            // 
            // linkLabel6
            // 
            this.linkLabel6.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(153)))));
            this.linkLabel6.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(153)))));
            this.linkLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(153)))));
            this.linkLabel6.Image = ((System.Drawing.Image)(resources.GetObject("linkLabel6.Image")));
            this.linkLabel6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel6.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabel6.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(153)))));
            this.linkLabel6.Location = new System.Drawing.Point(16, 479);
            this.linkLabel6.Name = "linkLabel6";
            this.linkLabel6.Size = new System.Drawing.Size(124, 23);
            this.linkLabel6.TabIndex = 32;
            this.linkLabel6.TabStop = true;
            this.linkLabel6.Text = "Generate Report";
            this.linkLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.linkLabel6.Visible = false;
            this.linkLabel6.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(153)))));
            this.linkLabel6.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GenerateButtonClicked);
            // 
            // linkLabel13
            // 
            this.linkLabel13.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(153)))));
            this.linkLabel13.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(153)))));
            this.linkLabel13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(153)))));
            this.linkLabel13.Image = ((System.Drawing.Image)(resources.GetObject("linkLabel13.Image")));
            this.linkLabel13.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel13.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabel13.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(153)))));
            this.linkLabel13.Location = new System.Drawing.Point(464, 484);
            this.linkLabel13.Name = "linkLabel13";
            this.linkLabel13.Size = new System.Drawing.Size(56, 23);
            this.linkLabel13.TabIndex = 58;
            this.linkLabel13.TabStop = true;
            this.linkLabel13.Text = "Quit";
            this.linkLabel13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.linkLabel13.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(153)))));
            this.linkLabel13.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel13_LinkClicked);
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(153)))));
            this.label9.Location = new System.Drawing.Point(19, 449);
            this.label9.Name = "label9";
            this.label9.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label9.Size = new System.Drawing.Size(488, 32);
            this.label9.TabIndex = 84;
            this.label9.Text = "Double click an orchestration node to get the current coverage state, or \'Generat" +
    "e Report\' to capture coverage state in a CHM for all selected orchestrations";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(170, 484);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(288, 16);
            this.progressBar1.TabIndex = 55;
            this.progressBar1.Visible = false;
            // 
            // lnkListApplications
            // 
            this.lnkListApplications.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(153)))));
            this.lnkListApplications.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(153)))));
            this.lnkListApplications.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkListApplications.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(153)))));
            this.lnkListApplications.Image = ((System.Drawing.Image)(resources.GetObject("lnkListApplications.Image")));
            this.lnkListApplications.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lnkListApplications.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lnkListApplications.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(153)))));
            this.lnkListApplications.Location = new System.Drawing.Point(11, 226);
            this.lnkListApplications.Name = "lnkListApplications";
            this.lnkListApplications.Size = new System.Drawing.Size(172, 23);
            this.lnkListApplications.TabIndex = 86;
            this.lnkListApplications.TabStop = true;
            this.lnkListApplications.Text = "List Applications deployed";
            this.lnkListApplications.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lnkListApplications.VisitedLinkColor = System.Drawing.Color.RoyalBlue;
            this.lnkListApplications.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ListDeployedApplications);
            // 
            // cbSelectAll
            // 
            this.cbSelectAll.Location = new System.Drawing.Point(450, 229);
            this.cbSelectAll.Name = "cbSelectAll";
            this.cbSelectAll.Size = new System.Drawing.Size(81, 24);
            this.cbSelectAll.TabIndex = 59;
            this.cbSelectAll.Text = "Select All";
            this.cbSelectAll.Visible = false;
            this.cbSelectAll.CheckedChanged += new System.EventHandler(this.SelectAllCheckChanged);
            // 
            // lstBtsApps
            // 
            this.lstBtsApps.FormattingEnabled = true;
            this.lstBtsApps.Location = new System.Drawing.Point(16, 256);
            this.lstBtsApps.Name = "lstBtsApps";
            this.lstBtsApps.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstBtsApps.Size = new System.Drawing.Size(192, 186);
            this.lstBtsApps.TabIndex = 85;
            this.lstBtsApps.SelectedValueChanged += new System.EventHandler(this.lstBtsApps_SelectedValueChanged);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(153)))));
            this.label6.Location = new System.Drawing.Point(264, 163);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label6.Size = new System.Drawing.Size(88, 16);
            this.label6.TabIndex = 83;
            this.label6.Text = "Profile To";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(153)))));
            this.label5.Location = new System.Drawing.Point(5, 163);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label5.Size = new System.Drawing.Size(88, 16);
            this.label5.TabIndex = 82;
            this.label5.Text = "Profile From";
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "dd/MMM/yyyy HH:mm:ss";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(368, 160);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(152, 20);
            this.dtpTo.TabIndex = 81;
            this.dtpTo.Value = new System.DateTime(2005, 4, 11, 21, 48, 0, 0);
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "dd/MMM/yyyy HH:mm:ss";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(112, 160);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(152, 20);
            this.dtpFrom.TabIndex = 80;
            this.dtpFrom.Value = new System.DateTime(2005, 4, 11, 21, 48, 0, 0);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel3.Location = new System.Drawing.Point(16, 220);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(504, 2);
            this.panel3.TabIndex = 77;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel1.Location = new System.Drawing.Point(16, 144);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(504, 2);
            this.panel1.TabIndex = 76;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(153)))));
            this.label4.Location = new System.Drawing.Point(272, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 16);
            this.label4.TabIndex = 75;
            this.label4.Text = "Tracking Database Info";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(153)))));
            this.label3.Location = new System.Drawing.Point(16, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 16);
            this.label3.TabIndex = 74;
            this.label3.Text = "Management Database Info";
            // 
            // txtDtaDatabase
            // 
            this.txtDtaDatabase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDtaDatabase.Location = new System.Drawing.Point(368, 104);
            this.txtDtaDatabase.Name = "txtDtaDatabase";
            this.txtDtaDatabase.Size = new System.Drawing.Size(152, 20);
            this.txtDtaDatabase.TabIndex = 73;
            // 
            // txtDtaServer
            // 
            this.txtDtaServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDtaServer.Location = new System.Drawing.Point(368, 80);
            this.txtDtaServer.Name = "txtDtaServer";
            this.txtDtaServer.Size = new System.Drawing.Size(152, 20);
            this.txtDtaServer.TabIndex = 70;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(153)))));
            this.label1.Location = new System.Drawing.Point(264, 104);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 72;
            this.label1.Text = "Database Name";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(153)))));
            this.label2.Location = new System.Drawing.Point(280, 80);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 71;
            this.label2.Text = "Server Name";
            // 
            // txtMgmtDatabase
            // 
            this.txtMgmtDatabase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMgmtDatabase.Location = new System.Drawing.Point(112, 104);
            this.txtMgmtDatabase.Name = "txtMgmtDatabase";
            this.txtMgmtDatabase.Size = new System.Drawing.Size(152, 20);
            this.txtMgmtDatabase.TabIndex = 69;
            // 
            // txtMgmtServer
            // 
            this.txtMgmtServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMgmtServer.Location = new System.Drawing.Point(112, 80);
            this.txtMgmtServer.Name = "txtMgmtServer";
            this.txtMgmtServer.Size = new System.Drawing.Size(152, 20);
            this.txtMgmtServer.TabIndex = 66;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(153)))));
            this.label7.Location = new System.Drawing.Point(8, 104);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label7.Size = new System.Drawing.Size(88, 16);
            this.label7.TabIndex = 68;
            this.label7.Text = "Database Name";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(153)))));
            this.label8.Location = new System.Drawing.Point(24, 80);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label8.Size = new System.Drawing.Size(72, 16);
            this.label8.TabIndex = 67;
            this.label8.Text = "Server Name";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(48)))), ((int)(((byte)(21)))));
            this.panel5.Controls.Add(this.pictureBox3);
            this.panel5.Controls.Add(this.lblVersion);
            this.panel5.Controls.Add(this.label21);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(540, 40);
            this.panel5.TabIndex = 65;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(8, 1);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(40, 38);
            this.pictureBox3.TabIndex = 5;
            this.pictureBox3.TabStop = false;
            // 
            // lblVersion
            // 
            this.lblVersion.ForeColor = System.Drawing.Color.White;
            this.lblVersion.Location = new System.Drawing.Point(48, 24);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(272, 23);
            this.lblVersion.TabIndex = 4;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.White;
            this.label21.Location = new System.Drawing.Point(48, 6);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(392, 23);
            this.label21.TabIndex = 1;
            this.label21.Text = "Orchestration Profiler";
            // 
            // lnkListOrchestrations
            // 
            this.lnkListOrchestrations.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(153)))));
            this.lnkListOrchestrations.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(153)))));
            this.lnkListOrchestrations.Enabled = false;
            this.lnkListOrchestrations.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkListOrchestrations.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(153)))));
            this.lnkListOrchestrations.Image = ((System.Drawing.Image)(resources.GetObject("lnkListOrchestrations.Image")));
            this.lnkListOrchestrations.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lnkListOrchestrations.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lnkListOrchestrations.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(153)))));
            this.lnkListOrchestrations.Location = new System.Drawing.Point(211, 228);
            this.lnkListOrchestrations.Name = "lnkListOrchestrations";
            this.lnkListOrchestrations.Size = new System.Drawing.Size(219, 23);
            this.lnkListOrchestrations.TabIndex = 64;
            this.lnkListOrchestrations.TabStop = true;
            this.lnkListOrchestrations.Text = "Get Selected Apps\' Orchestrations";
            this.lnkListOrchestrations.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lnkListOrchestrations.VisitedLinkColor = System.Drawing.Color.RoyalBlue;
            this.lnkListOrchestrations.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ListDeployedOrchestrations);
            // 
            // tvOrchs
            // 
            this.tvOrchs.CheckBoxes = true;
            this.tvOrchs.Enabled = false;
            this.tvOrchs.ImageIndex = 0;
            this.tvOrchs.ImageList = this.imageList1;
            this.tvOrchs.Location = new System.Drawing.Point(214, 256);
            this.tvOrchs.Name = "tvOrchs";
            this.tvOrchs.SelectedImageIndex = 0;
            this.tvOrchs.Size = new System.Drawing.Size(306, 190);
            this.tvOrchs.Sorted = true;
            this.tvOrchs.TabIndex = 0;
            this.tvOrchs.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TvOrchsAfterCheck);
            this.tvOrchs.DoubleClick += new System.EventHandler(this.TvOrchsDoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "XML Files|*.xml";
            this.saveFileDialog1.Title = "Save configuration file";
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.linkLabel13;
            this.ClientSize = new System.Drawing.Size(543, 517);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Orchestration Profiler";
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		#region GenerateButtonClicked

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GenerateButtonClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if (this.tvOrchs.Nodes.Count > 0)
			{
				Cursor.Current = Cursors.WaitCursor;

				bool proceed = false;

				foreach (TreeNode tn in this.tvOrchs.Nodes)
				{
					foreach (TreeNode n in tn.Nodes)
					{
						if (n.Checked)
						{
							proceed = true;
							break;
						}
					}
				}

				if (!proceed)
				{
					MessageBox.Show("Please select at least one orchestration for profiling", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				try
				{
					this.linkLabel13.Visible = false;
					progressBar1.Visible = true;
					this.GenerateDocumentation();
				}
				catch (Exception ex)
				{
#if(DEBUG)
					MessageBox.Show(ex.ToString());
#endif
					MessageBox.Show(ex.Message, "Error Generating Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				finally
				{
					progressBar1.Visible = false;
					this.linkLabel13.Visible = true;
					Cursor.Current = Cursors.Default;
				}
			}

			return;
		}

		#endregion

		#region SetDefaults

		/// <summary>
		/// 
		/// </summary>
		private void SetDefaults()
		{
			this.txtDtaDatabase.Text = reportingConfiguration.DtaDatabaseName;
			this.txtDtaServer.Text = reportingConfiguration.DtaServerName;

			this.txtMgmtDatabase.Text = reportingConfiguration.MgmtDatabaseName;
			this.txtMgmtServer.Text = reportingConfiguration.MgmtServerName;

			this.dtpFrom.Value = reportingConfiguration.DateFrom;
			this.dtpTo.Value = reportingConfiguration.DateTo;

			this.lblVersion.Text = "v" + Assembly.GetEntryAssembly().GetName().Version.ToString();
		}

		#endregion

		#region ListDeployedApplications
		private void ListDeployedApplications(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			try
			{               
				this.lstBtsApps.Items.Clear();
				BizTalkInstallation bizTalkInstallation = new BizTalkInstallation();
				bizTalkInstallation.Server = this.txtMgmtServer.Text;
				bizTalkInstallation.MgmtDatabaseName = this.txtMgmtDatabase.Text;
				foreach (Microsoft.BizTalk.ExplorerOM.Application btsApp in bizTalkInstallation.GetApplicationNames())
				{
					this.lstBtsApps.Items.Add(btsApp.Name);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error Listing Deployed Applications", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				updateOrchPannel();
				Cursor.Current = Cursors.Default;
			}
		}
		#endregion

		#region ListDeployedOrchestrations

		/// <summary>
		/// List Orchestrations
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ListDeployedOrchestrations(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				this.tvOrchs.Nodes.Clear();
				BizTalkInstallation bizTalkInstallation = new BizTalkInstallation();
				bizTalkInstallation.Server = this.txtMgmtServer.Text;
				bizTalkInstallation.MgmtDatabaseName = this.txtMgmtDatabase.Text;
				ArrayList ai;
				// get the selected apps if any 
				//this.lstBtsApps.SelectedItems
				if (this.lstBtsApps.SelectedIndices.Count > 0)
				{
					ai = new ArrayList();
					foreach (int selectedItemIdx in this.lstBtsApps.SelectedIndices)
					{
						string selectedBtsAppName = this.lstBtsApps.Items[selectedItemIdx].ToString();
						ai.AddRange(bizTalkInstallation.GetOrchestrationNames(selectedBtsAppName));
					}
				}
				else
				{
                    //Is there a specified orcs in Command args?
                    if (reportingConfiguration.ExecutionMode==ExecutionMode.CommandLine && !string.IsNullOrEmpty(reportingConfiguration.OrcsList))
                        ai = bizTalkInstallation.GetSpecificOrchestrationNames(reportingConfiguration.OrcsList);
                    else
                        ai = bizTalkInstallation.GetOrchestrationNames();
                }

				foreach (string assemblyOrchestration in ai)
				{
					string[] splitArray = assemblyOrchestration.Split('|');
					string assemblyDisplayName = splitArray[0];
					string orchestrationName = splitArray[1];

					Orchestration o = bizTalkInstallation.GetOrchestration(assemblyDisplayName, orchestrationName);

					TreeNode asmNode = this.tvOrchs.Nodes[assemblyDisplayName];

					if (asmNode == null)
					{
						asmNode = new TreeNode(assemblyDisplayName, 1, 1);
						asmNode.Name = assemblyDisplayName;
						this.tvOrchs.Nodes.Add(asmNode);
					}

					TreeNode orchNode = new TreeNode(o.Name, 2, 2);

					orchNode.Tag = o;
					asmNode.Nodes.Add(orchNode);
				}

				cbSelectAll.Visible = true;
				this.linkLabel6.Visible = true; // Generate
				//this.lnkListOrchestrations.Visible = false; // List Orchs
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error Listing Deployed Orchestrations", MessageBoxButtons.OK, MessageBoxIcon.Error);
				cbSelectAll.Visible = false;
				//this.linkLabel6.Visible = false; // Generate
				//this.lnkListOrchestrations.Visible = true; // List Orchs
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private AssemblyOrchestrationPairCollection DetermineOrchestrationsToProfile()
		{
			if (reportingConfiguration.ExecutionMode == ExecutionMode.CommandLine)
			{
                ListDeployedOrchestrations(this, new LinkLabelLinkClickedEventArgs(null));
			}

			AssemblyOrchestrationPairCollection coll = new AssemblyOrchestrationPairCollection();

			foreach (TreeNode asmNode in this.tvOrchs.Nodes)
			{
				foreach (TreeNode orchNode in asmNode.Nodes)
				{
					if ((reportingConfiguration.ExecutionMode == ExecutionMode.Interactive && orchNode.Checked) ||
						reportingConfiguration.ExecutionMode == ExecutionMode.CommandLine)
					{
						AssemblyOrchestrationPair pair = new AssemblyOrchestrationPair();
						pair.AssemblyName = asmNode.Text;
						pair.OrchestrationName = orchNode.Text;
						coll.Add(pair);
					}
				}
			}

			return coll;
		}

		/// <summary>
		/// 
		/// </summary>
		private void UpdateReportingConfigurationFromUI()
		{
			reportingConfiguration.DtaDatabaseName = this.txtDtaDatabase.Text;
			reportingConfiguration.DtaServerName = this.txtDtaServer.Text;
			reportingConfiguration.MgmtDatabaseName = this.txtMgmtDatabase.Text;
			reportingConfiguration.MgmtServerName = this.txtMgmtServer.Text;
			reportingConfiguration.OutputDir = folderBrowserDialog1.SelectedPath;
			reportingConfiguration.DateFrom = this.dtpFrom.Value.ToUniversalTime();
			reportingConfiguration.DateTo = this.dtpTo.Value.ToUniversalTime();
            reportingConfiguration.InstancesIds = this.tbInstanceID.Text.Trim().Length > 0 ? this.tbInstanceID.Text.Trim().Split(',') : new string[0];
		}

		/// <summary>
		/// 
		/// </summary>
		private void GenerateDocumentation()
		{
			bool generate = true;

			if (reportingConfiguration.ExecutionMode == ExecutionMode.Interactive)
			{
				folderBrowserDialog1.Description = "Select a destination folder for the generated profile report";

				DialogResult res = folderBrowserDialog1.ShowDialog();

				if (res == DialogResult.OK)
				{
					this.UpdateReportingConfigurationFromUI();
				}
				else
				{
					generate = false;
				}
			}

			if (generate)
			{
				AssemblyOrchestrationPairCollection coll = this.DetermineOrchestrationsToProfile();
				generator.PercentageDocumentationComplete += new UpdatePercentageComplete(PercentageDocumentationComplete);
				generator.GenerateDocumentation(reportingConfiguration, coll);
			}

			return;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void linkLabel13_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			Application.Exit();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SelectAllCheckChanged(object sender, System.EventArgs e)
		{
			foreach (TreeNode asmNode in this.tvOrchs.Nodes)
			{
				asmNode.Checked = ((CheckBox)sender).Checked;
			}
			return;
		}

		private void TvOrchsDoubleClick(object sender, System.EventArgs e)
		{
			DisplayOrchestration();
		}

		#region DisplayOrchestration

		/// <summary>
		/// 
		/// </summary>
		private void DisplayOrchestration()
		{
			try
			{
				if (this.tvOrchs.SelectedNode != null &&
					this.tvOrchs.SelectedNode.Tag != null &&
					this.tvOrchs.SelectedNode.Tag is Orchestration)
				{
					Orchestration o = this.tvOrchs.SelectedNode.Tag as Orchestration;

					o.ParentAssemblyFormattedName = this.tvOrchs.SelectedNode.Parent.Text;

					o.ClearCoverageInfo();
					OrchestrationCoverageInfo info = o.GetCoverageInfo(
						this.txtDtaServer.Text,
						this.txtDtaDatabase.Text,
						this.dtpFrom.Value.ToUniversalTime(),
						this.dtpTo.Value.ToUniversalTime(),
                         this.tbInstanceID.Text.Trim().Length > 0 ? this.tbInstanceID.Text.Trim().Split(',') : new string[0]);

					OrchestrationViewer ov = new OrchestrationViewer(o);
					ov.ShowDialog(this);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error displaying orchestration", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Debug.WriteLine(ex.Message);
                File.WriteAllText("CrashReport.dat", ex.ToString());
			}
		}

		#endregion

		#region ShowUsage
		private static void ShowUsage()
		{
			Assembly a = Assembly.GetExecutingAssembly();
			StreamReader sr = new StreamReader(a.GetManifestResourceStream("Microsoft.Services.Tools.BizTalkOrchestrationProfiler.Res.usage.txt"));
			string help = sr.ReadToEnd();
			sr.Close();

			MessageBox.Show(help, "Microsoft.Sdc.Orchestration.Profiler Usage");
		}
		#endregion

		#region ProcessArgs

		private static void ProcessArgs(string[] args)
		{
			if (args.Length == 0)
				stop = true;

			for (int i = 0; i < args.Length; i++)
			{
				string[] argSplit = args[i].Split(new Char[] { ':' }, 2);
				string argName = argSplit[0].ToLower();
				string argValue;

				if (argSplit.Length == 1)
					argValue = null;
				else
					argValue = argSplit[1];

				switch (argName.ToLower())
				{
					case "/outputdir":
					case "/o":
						reportingConfiguration.OutputDir = argValue;
						break;

					case "/managementserver":
					case "/ms":
						reportingConfiguration.MgmtServerName = argValue;
						break;

					case "/managementdatabase":
					case "/md":
						reportingConfiguration.MgmtDatabaseName = argValue;
						break;

					case "/trackingserver":
					case "/ts":
						reportingConfiguration.DtaServerName = argValue;
						break;

					case "/trackingdatabase":
					case "/td":
						reportingConfiguration.DtaDatabaseName = argValue;
						break;

					case "/dateto":
						reportingConfiguration.DateTo = Convert.ToDateTime(argValue);
						break;

					case "/datefrom":
						reportingConfiguration.DateFrom = Convert.ToDateTime(argValue);
						break;

					case "/title":
					case "/t":
						reportingConfiguration.ReportTitle = argValue;
						break;

                    case "/orcslist":
                    case "/orc":
                        reportingConfiguration.OrcsList = argValue;
                        break;

                    case "/show":
					case "/s":
						reportingConfiguration.ShowOutputOnCompletion = Convert.ToBoolean(argValue);
						break;

					case "/help":
					case "/h":
					case "/?":
						showUsage = true;
						break;

					case "/def":
					case "/defaults":
						return;

					default:
						stop = true;
						break;
				}
			}
		}

		#endregion

		private void cbFilterByInstanceID_CheckStateChanged(object sender, EventArgs e)
		{
			if (cbFilterByInstanceID.Checked)
			{
				tbInstanceID.Enabled = true;
				dtpFrom.Enabled = false;
				dtpTo.Enabled = false;
			}
			else
			{
				tbInstanceID.Enabled = false;
				dtpFrom.Enabled = true;
				dtpTo.Enabled = true;
				tbInstanceID.Text = string.Empty;
			}
		}

		private void updateOrchPannel()
		{
			if (lstBtsApps.SelectedIndices.Count > 0)
			{
				tvOrchs.Enabled = true;
				lnkListOrchestrations.Enabled = true;
				cbSelectAll.Enabled = true;
			}
			else
			{
				tvOrchs.Enabled = false;
				lnkListOrchestrations.Enabled = false;
				cbSelectAll.Enabled = false;
				tvOrchs.Nodes.Clear();              
			}
		}

		private void lstBtsApps_SelectedValueChanged(object sender, EventArgs e)
		{
			updateOrchPannel();
		}
	}
}
