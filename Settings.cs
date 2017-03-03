using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace MailPopUp
{
	/// <summary>
	/// Summary description for Settings.
	/// </summary>
	public class Settings : System.Windows.Forms.Form
	{
		private MailChecker mailSystem;
		private MailPopUp mParent;

		private System.Windows.Forms.Label lblUserID;
		private System.Windows.Forms.Label lblPassword;
		private System.Windows.Forms.Label lblServer;
		private System.Windows.Forms.TextBox txtServer;
		private System.Windows.Forms.TextBox txtUserID;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label lblCheck;
		private System.Windows.Forms.TextBox txtCheck;
		private System.Windows.Forms.ComboBox lbxTime;
		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.Button btnChanges;
		private System.Windows.Forms.Button btnStart;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Settings()
		{
			mailSystem = new MailChecker();

			InitializeComponent();

			mParent = null;
		}

		public Settings(MailPopUp fParent)
		{
			mailSystem = new MailChecker();

			InitializeComponent();

			mParent = fParent;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
					//Does not work, Icon is still on taskbar after program closes
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Settings));
			this.lblServer = new System.Windows.Forms.Label();
			this.lblUserID = new System.Windows.Forms.Label();
			this.lblPassword = new System.Windows.Forms.Label();
			this.txtServer = new System.Windows.Forms.TextBox();
			this.txtUserID = new System.Windows.Forms.TextBox();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.btnChanges = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lblCheck = new System.Windows.Forms.Label();
			this.txtCheck = new System.Windows.Forms.TextBox();
			this.lbxTime = new System.Windows.Forms.ComboBox();
			this.lblTitle = new System.Windows.Forms.Label();
			this.btnStart = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblServer
			// 
			this.lblServer.Location = new System.Drawing.Point(16, 48);
			this.lblServer.Name = "lblServer";
			this.lblServer.Size = new System.Drawing.Size(88, 24);
			this.lblServer.TabIndex = 0;
			this.lblServer.Text = "POP Server:";
			// 
			// lblUserID
			// 
			this.lblUserID.Location = new System.Drawing.Point(16, 96);
			this.lblUserID.Name = "lblUserID";
			this.lblUserID.Size = new System.Drawing.Size(88, 24);
			this.lblUserID.TabIndex = 1;
			this.lblUserID.Text = "User ID:";
			// 
			// lblPassword
			// 
			this.lblPassword.Location = new System.Drawing.Point(16, 120);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(88, 24);
			this.lblPassword.TabIndex = 2;
			this.lblPassword.Text = "Password:";
			// 
			// txtServer
			// 
			this.txtServer.Location = new System.Drawing.Point(112, 48);
			this.txtServer.MaxLength = 200;
			this.txtServer.Name = "txtServer";
			this.txtServer.Size = new System.Drawing.Size(200, 20);
			this.txtServer.TabIndex = 0;
			this.txtServer.Text = "";
			// 
			// txtUserID
			// 
			this.txtUserID.Location = new System.Drawing.Point(112, 96);
			this.txtUserID.MaxLength = 30;
			this.txtUserID.Name = "txtUserID";
			this.txtUserID.Size = new System.Drawing.Size(152, 20);
			this.txtUserID.TabIndex = 3;
			this.txtUserID.Text = "";
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(112, 120);
			this.txtPassword.MaxLength = 30;
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(152, 20);
			this.txtPassword.TabIndex = 4;
			this.txtPassword.Text = "";
			// 
			// btnChanges
			// 
			this.btnChanges.Location = new System.Drawing.Point(336, 48);
			this.btnChanges.Name = "btnChanges";
			this.btnChanges.Size = new System.Drawing.Size(112, 24);
			this.btnChanges.TabIndex = 5;
			this.btnChanges.Text = "Save Changes";
			this.btnChanges.Click += new System.EventHandler(this.btnChanges_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(336, 112);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(112, 24);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// lblCheck
			// 
			this.lblCheck.Location = new System.Drawing.Point(16, 72);
			this.lblCheck.Name = "lblCheck";
			this.lblCheck.Size = new System.Drawing.Size(88, 24);
			this.lblCheck.TabIndex = 8;
			this.lblCheck.Text = "Check Every:";
			// 
			// txtCheck
			// 
			this.txtCheck.Location = new System.Drawing.Point(112, 72);
			this.txtCheck.MaxLength = 5;
			this.txtCheck.Name = "txtCheck";
			this.txtCheck.Size = new System.Drawing.Size(48, 20);
			this.txtCheck.TabIndex = 1;
			this.txtCheck.Text = "";
			// 
			// lbxTime
			// 
			this.lbxTime.BackColor = System.Drawing.SystemColors.Window;
			this.lbxTime.Location = new System.Drawing.Point(168, 72);
			this.lbxTime.Name = "lbxTime";
			this.lbxTime.Size = new System.Drawing.Size(144, 21);
			this.lbxTime.TabIndex = 2;
			// 
			// lblTitle
			// 
			this.lblTitle.Location = new System.Drawing.Point(16, 16);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(432, 24);
			this.lblTitle.TabIndex = 11;
			this.lblTitle.Text = "Change mail checking options:";
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(336, 80);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(112, 24);
			this.btnStart.TabIndex = 6;
			this.btnStart.Text = "Save and Start";
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// Settings
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(464, 150);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.btnStart,
																		  this.lblTitle,
																		  this.lbxTime,
																		  this.txtCheck,
																		  this.lblCheck,
																		  this.btnCancel,
																		  this.btnChanges,
																		  this.txtPassword,
																		  this.txtUserID,
																		  this.txtServer,
																		  this.lblPassword,
																		  this.lblUserID,
																		  this.lblServer});
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Settings";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Settings";
			this.Load += new System.EventHandler(this.Settings_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void Settings_Load(object sender, System.EventArgs e)
		{
			lbxTime.Items.Add("Seconds");
			lbxTime.Items.Add("Minutes");
			lbxTime.Items.Add("Hours");

			if(mailSystem.Units == "Hours")
				lbxTime.SelectedIndex = 2;
			else if(mailSystem.Units == "Minutes")
				lbxTime.SelectedIndex = 1;
			else
				lbxTime.SelectedIndex = 0;

			txtServer.Text = mailSystem.MailServer;
			txtUserID.Text = mailSystem.UserName;
			txtPassword.Text = mailSystem.Password;
			txtCheck.Text = mailSystem.Interval.ToString();
		}

		private void btnChanges_Click(object sender, System.EventArgs e)
		{
			if (txtServer.Text == "")
			{
				lblTitle.Text = "Please enter a valid server name";
				lblTitle.ForeColor = System.Drawing.Color.OrangeRed;
			}
			else if(txtUserID.Text == "" || txtPassword.Text == "")
			{
				lblTitle.Text = "Please enter a Username and Password";
				lblTitle.ForeColor = System.Drawing.Color.OrangeRed;
			}
			else if (txtCheck.Text == "")
			{
				lblTitle.Text = "Please enter a time interval";
				lblTitle.ForeColor = System.Drawing.Color.OrangeRed;
			}
			else
			{
				try
				{
					if(Convert.ToInt32(txtCheck.Text.ToString(), 10) > 0)
					{
						mailSystem.ChangeSettings(txtUserID.Text, Convert.ToInt32(txtCheck.Text, 10), lbxTime.SelectedItem.ToString(), txtPassword.Text, txtServer.Text);
						this.Close();
					}
					else
					{
						lblTitle.Text = "Please enter a valid time interval";
						lblTitle.ForeColor = System.Drawing.Color.OrangeRed;
					}
				}
				catch(FormatException eExcep)
				{
					lblTitle.Text = "Please enter a valid time interval";
					lblTitle.ForeColor = System.Drawing.Color.OrangeRed;
				}
			}
				
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnStart_Click(object sender, System.EventArgs e)
		{
			btnChanges_Click(sender, e);
			if(mParent != null)
				mParent.StartTimer();
		
		}
	}
}
